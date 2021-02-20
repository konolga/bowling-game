import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class GameService {

  constructor() { }

  private rolls: number[] = new Array(21).fill(0);
  private currentRoll = 0;

  roll(pins: number) {
    this.rolls[this.currentRoll++] = pins;
  }


}
