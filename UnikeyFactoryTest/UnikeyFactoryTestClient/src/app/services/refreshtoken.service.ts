import { Injectable } from '@angular/core';
import {HttpClient, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {Observable} from 'rxjs';
import {LOCALHOST_URL} from '../constants/api.const';
import {User} from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class RefreshtokenService {
  LOGIN_URL = LOCALHOST_URL + 'User/Login';
  user: User;

  constructor(public httpClient: HttpClient) {
  }

  Refresh() {
    this.user.username = 'Lollolone';
    this.user.password = 'Unikey1!';
    this.httpClient.post<string>(this.LOGIN_URL, this.user).subscribe(
      jwt => localStorage.setItem('token', jwt));
    console.log('Refresh');
  }
}
