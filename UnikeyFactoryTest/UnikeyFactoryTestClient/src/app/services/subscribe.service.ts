import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {User} from '../models/user';

const URL = 'https://localhost:44329/api/User/Subscribe';

@Injectable({
  providedIn: 'root'
})
export class SubscribeService {

  constructor(private http: HttpClient) { }

    /*subscribeUser(user: User) {
     const r = this.http.post(URL, user, {observe: 'response'})
       .subscribe(data => {this.responseError = data; console.log(this.responseError); });
     return this.responseError;*/

  register(user: User) {
    const reqHeader = new HttpHeaders().set('Content-Type', 'application/json')
      .set('Accept', 'application/json');

    return this.http.post(URL, user, {headers : reqHeader});
  }
}
