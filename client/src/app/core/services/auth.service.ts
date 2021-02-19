import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { User } from '@models/user';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  userUrl = environment.userAPI;
  constructor(private http: HttpClient) {}

  auth(user: User) {
    var formData: any = new FormData();
    formData.append('username', user.Username);

    return this.http.post(this.userUrl, formData);
  }
}
