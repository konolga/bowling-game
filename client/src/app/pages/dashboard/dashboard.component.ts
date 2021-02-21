import { Component, OnInit } from '@angular/core';
import { FrameData } from '@models/frameData';
import { AlertifyService } from '@services/alertify.service';
import { GameService } from '@services/game.service';
import { Score } from '@models/score';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

const availablePins: number = 11; // with 0
const availableGames: number = 10;
@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent implements OnInit {
  public frames: FrameData[];
  public message: string;
  public options: Array<number>;
  public userInputs: FormGroup;
  public isGameOver: boolean = false;

  private currentFrame: FrameData;
  private isBonusFrameInit: boolean = false;
  private nextPinNumber: string;
  private nextFrameName: any;
  private bonusBalls: number;

  constructor(
    private gameService: GameService,
    private alertify: AlertifyService,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit(): void {
    this.initGame();
  }

  initGame() {
    this.frames = [];
    this.message = 'Welcome to the game, enter your first pin here:';
    this.userInputs = this.formBuilder.group({
      pin: ['', [Validators.required]],
    });
    this.removeUsedOptions(0);
    this.isGameOver = false;
    this.bonusBalls = 0;
  }

  removeUsedOptions(usedPins: number): void {
    this.options = [...Array(availablePins - usedPins)].map((v, i) => 0 + i);
  }

  onSubmit() {
    this.setPinsToFrame();
    if (this.bonusBalls > 0) {
      this.updatePrevFrame();
    }
    this.calculateScore();
    this.updateCurrentFrame();
    if (this.isBonusGame() || this.isBonusFrameInit) {
      this.moveToBonusFrame();
    } else {
      this.moveToNextFrame();
    }
    if (this.isGameOver) {
      this.saveScore();
    }
  }

  saveScore() {
    this.gameService.saveScore(this.frames[this.frames.length - 1].Result).subscribe(response  => {
      this.alertify.success(`We saved your result history`);
    }, error => {

    });
  }

  isBonusGame(): boolean {
    return this.frames.length >= 10 && this.bonusBalls >= 0;
  }

  setPinsToFrame(): void {
    if (this.currentFrame == null) {
      this.currentFrame = new FrameData();
      this.currentFrame.Result =
        this.frames.length > 0 ? this.frames[this.frames.length - 1].Result : 0;
      this.currentFrame.FirstPin = +this.userInputs.value.pin;
      this.frames.push(this.currentFrame);
    } else if (
      (this.isBonusGame() || this.isBonusFrameInit) &&
      this.currentFrame.SecondPin >= 0
    ) {
      this.currentFrame.ThirdPin = +this.userInputs.value.pin;
    } else {
      this.currentFrame.SecondPin = +this.userInputs.value.pin;
    }
    this.removeUsedOptions(this.userInputs.value.pin);
  }

  calculateScore(): void {
    if (this.isStrike(this.currentFrame.FirstPin)) {
      this.currentFrame.Result += 10;
      this.bonusBalls = 2;
      this.currentFrame.SecondPin = 0;
    } else if (
      this.isSpare(this.currentFrame.FirstPin, this.currentFrame.SecondPin)
    ) {
      this.currentFrame.Result += 10;
      this.bonusBalls = 1;
    } else {
      this.currentFrame.Result += this.sumOfPinsInFrame([
        this.currentFrame.FirstPin,
        this.currentFrame.SecondPin,
        this.currentFrame.ThirdPin,
      ]);
      this.bonusBalls--;
    }
  }

  updateCurrentFrame(): void {
    this.frames[this.frames.length - 1].Result = this.currentFrame.Result;
  }

  updatePrevFrame(): void {
    this.frames[this.frames.length - 2].Result += this.sumOfPinsInFrame([
      this.currentFrame.FirstPin,
      this.currentFrame.SecondPin,
      this.currentFrame.ThirdPin,
    ]);
    this.frames[this.frames.length - 1].Result += this.sumOfPinsInFrame([
      this.currentFrame.FirstPin,
      this.currentFrame.SecondPin,
      this.currentFrame.ThirdPin,
    ]);
  }

  moveToNextFrame() {
    let optionsToRemove = this.currentFrame.FirstPin || 0;
    this.nextPinNumber = 'second';
    this.nextFrameName = this.frames.length;
    if (this.currentFrame.SecondPin >= 0) {
      this.currentFrame = null;
      this.nextPinNumber = 'first';
      this.nextFrameName++;
      optionsToRemove = 0;
    }
    if (this.nextFrameName > availableGames) {
      this.isGameOver = true;
      this.currentFrame = null;
    }
    this.removeUsedOptions(optionsToRemove);
    this.setUserMessage();
  }

  moveToBonusFrame(): void {
    let optionsToRemove = 0;
    this.nextFrameName = 'Bonus';
    if (!this.isBonusFrameInit) {
      this.nextPinNumber = 'first';
      this.currentFrame = null;
      this.isBonusFrameInit = true;
    } else if (this.bonusBalls > 0) {
      this.isGameOver = true;
      this.currentFrame = null;
    } else if (this.currentFrame.ThirdPin >= 0) {
      this.isGameOver = true;
      this.currentFrame = null;
      optionsToRemove = 10;
    } else if (this.currentFrame.SecondPin >= 0) {
      this.nextPinNumber = 'third';
      optionsToRemove =
        this.currentFrame.SecondPin + this.currentFrame.FirstPin;
    } else if (this.currentFrame.FirstPin >= 0) {
      this.nextPinNumber = 'second';
      optionsToRemove = this.currentFrame.FirstPin;
    }
    this.removeUsedOptions(optionsToRemove);
    this.setUserMessage();
  }

  setUserMessage(): void {
    if (!this.isGameOver) {
      this.message = `Enter your ${this.nextPinNumber} pin for ${this.nextFrameName} Frame`;
    } else {
      this.message = `Game over, your final score is ${
        this.frames[this.frames.length - 1].Result
      }`;
    }
  }
  sumOfPinsInFrame(array: number[]) {
    for (let index = array.length; index >= 0; index--) {
      if (array[index] >= 0) {
        return array[index];
      }
    }
  }

  isStrike(firstNum: number) {
    let isStrike = firstNum === 10;
    isStrike ? this.alertify.success(`Strike!`) : null;
    return isStrike;
  }

  isSpare(firstNum: number, secondNum: number) {
    let isSpare = firstNum + secondNum === 10;
    isSpare ? this.alertify.warning(`Spare!`) : null;
    return firstNum + secondNum === 10;
  }
}
