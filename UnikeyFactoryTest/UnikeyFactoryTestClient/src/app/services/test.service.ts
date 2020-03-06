import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {Test} from '../models/test';
import {map} from 'rxjs/operators';

const reqUrl = 'https://localhost:44329/api/';

@Injectable({
  providedIn: 'root'
})
export class TestService {
  constructor(private http: HttpClient) { }

  createTest(test: Test) {
      return this.http.post(reqUrl + 'Test', test);
  }

  getTest(id: number) {
    return this.http.get(reqUrl + 'Test/' + id);
  }
}
