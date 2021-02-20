import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Score } from '@models/score';
import { AuthService } from '@services/auth.service';

@Injectable({
  providedIn: 'root',
})
export class GameService {
  scoreUrl = environment.scoreAPI;
  constructor(private http: HttpClient, private authService: AuthService) {}

  saveScore(total: number) {
    let formData: any = new FormData();
    if (this.authService.loggedIn() && this.authService.currentUser != null) {
      let score: Score = new Score();
      score.TotalScore = total;
      score.UserId = this.authService.currentUser.Id;

      formData.append('score', JSON.stringify(score));
      return this.http.post(this.scoreUrl, formData);
    }
  }
}
