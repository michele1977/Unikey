import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {User} from '../models/user';
import {Observable} from 'rxjs';
import {catchError} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class SubscribeService {
  errors: any;

  constructor(private http: HttpClient) {}

  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      return error.message;
    } else {
      return [error.status];
    }
  }

  subscribeUser(user: User) {
    return this.http.post<User>('https://localhost:99/api/Subscribe', {user})
  }
}
