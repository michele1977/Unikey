import { Injectable } from '@angular/core';
import {HttpClient, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {Observable} from 'rxjs';
import {LOCALHOST_URL} from '../constants/api.const';
import {User} from '../models/user';
import {Router} from '@angular/router';
import {HttpWrapperService} from './http-wrapper.service';

@Injectable({
  providedIn: 'root'
})
export class RefreshTokenService {
  REFRESH_URL = LOCALHOST_URL + 'User/Refresh';
  constructor(public httpClient: HttpClient, public router: Router) {
  }

  Refresh() {
    const jwt = localStorage.getItem('token');
    return this.httpClient.post<string>(this.REFRESH_URL, {token: jwt});
  }
}
