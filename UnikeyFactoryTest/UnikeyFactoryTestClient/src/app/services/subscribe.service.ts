import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {User} from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class SubscribeService {
  errors: any;
  succesfullySubscribed: boolean;
  constructor(private http: HttpClient) { this.succesfullySubscribed = false; }

  subscribeUser(user: User) {
    return this.http.post('https://localhost:99/api/Subscribe', {user})
      .subscribe(res => {
      this.succesfullySubscribed = true;
    }, error => {
      this.errors = error;
    });
  }
}
