import { Injectable } from '@angular/core';
import {User} from '../models/user';
import {HttpClient} from '@angular/common/http';
import {LOCALHOST_URL} from '../constants/api.const';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  LOGIN_URL = LOCALHOST_URL + 'User/Login';
  constructor(private httpClient: HttpClient) { }

  login(user: User) {
    return this.httpClient.post<string>(this.LOGIN_URL, user)
      .subscribe(jwt => {
        this.setSession(jwt);
      });
  }

  private setSession(jwt: string) {
    localStorage.setItem('token', jwt);
  }
}
