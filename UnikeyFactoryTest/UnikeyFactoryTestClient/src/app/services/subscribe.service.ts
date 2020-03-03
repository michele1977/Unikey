import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {User} from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class SubscribeService {

  constructor(private http: HttpClient) { }

  subscribe(user: User) {
    this.http.post<User>('https://localhost:44348/api/FakeUserApi/Subscribe', user).subscribe();
  }
}
