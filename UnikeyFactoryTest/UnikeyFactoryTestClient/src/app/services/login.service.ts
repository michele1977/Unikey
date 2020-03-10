import { Injectable } from '@angular/core';
import {User} from '../models/user';
import {HttpClient} from '@angular/common/http';
import {LOCALHOST_URL} from '../constants/api.const';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  LOGIN_URL = LOCALHOST_URL + 'User/Login';
  prova: string;
  constructor(private httpClient: HttpClient) { }

  login(user: User): string {
    console.log(user);
    this.httpClient.post<any>(this.LOGIN_URL, user)
      .subscribe(jwt => {
        this.prova = jwt;
        // this.setSession(jwt);
      });

    return this.prova;
  }

  setSession(jwt: string) {

  }
}
