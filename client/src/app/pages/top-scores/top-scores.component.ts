import { Component, OnInit } from '@angular/core';
import { Score } from '@models/score';
import { User } from '@models/user';
import { GameService } from '@services/game.service';

const TOP_USERS_NUMBER: number = 5;
@Component({
  selector: 'app-top-scores',
  templateUrl: './top-scores.component.html',
  styleUrls: ['./top-scores.component.css'],
})
export class TopScoresComponent implements OnInit {
  public scores: any;
  constructor(private gameService: GameService) {}

  ngOnInit(): void {
    this.getTopScores();
  }
  getTopScores() {
    this.gameService.GetTopUsers(TOP_USERS_NUMBER)
      .subscribe((response: any) => {
        this.scores = response;
      },
      (error) => {}
    );
  }
}
