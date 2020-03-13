import {Injectable, Output} from '@angular/core';
import {User} from '../models/user';
import {HttpClient} from '@angular/common/http';
import {LOCALHOST_URL} from '../constants/api.const';
import {Router} from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  LOGIN_URL = LOCALHOST_URL + 'User/Login';

  constructor(public httpClient: HttpClient, public router: Router) { }

  login(user: User) {
    return this.httpClient.post<string>(this.LOGIN_URL, user);
  }
}
