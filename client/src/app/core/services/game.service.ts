import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Score } from '@models/score';
import { AuthService } from '@services/auth.service';
import { of } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class GameService {
  scoreUrl = environment.scoreAPI;
  constructor(private http: HttpClient, private authService: AuthService) {}

  saveScore(total: number) {
    if (this.authService.loggedIn() && this.authService.currentUser != null) {
      let score: Score = new Score();
      score.TotalScore = total;
      score.UserId = this.authService.currentUser.Id;

      var formData: any = new FormData();
      formData.append('Score', JSON.stringify(score));

      return this.http.post(this.scoreUrl, formData);
    }
    return of(false);
  }
}
