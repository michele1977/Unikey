import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse, HttpHeaders} from '@angular/common/http';
import {User} from '../models/user';
import {Observable} from 'rxjs';
import {catchError} from 'rxjs/operators';
import { LOCALHOST_URL } from '../constants/api.const';
import {HttpWrapperService} from './http-wrapper.service';

const URL = LOCALHOST_URL + '/User/Subscribe';

@Injectable({
  providedIn: 'root'
})
export class SubscribeService {

  constructor(private httpWrapper: HttpWrapperService) { }

    /*subscribeUser(user: User) {
     const r = this.http.post(URL, user, {observe: 'response'})
       .subscribe(data => {this.responseError = data; console.log(this.responseError); });
     return this.responseError;*/

  register(user: User) {
    // const reqHeader = new HttpHeaders().set('Content-Type', 'application/json')
    //   .set('Accept', 'application/json');

    return this.httpWrapper.invokePostUrl(URL, user);
    // return this.http.post(URL, user, {headers : reqHeader});
  }
}
