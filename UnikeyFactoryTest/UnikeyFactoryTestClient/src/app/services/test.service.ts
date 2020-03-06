import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Test} from '../models/test';

@Injectable({
  providedIn: 'root'
})
export class TestService {
  constructor(private http: HttpClient) { }
  createTest(test: Test) {
      return this.http.post('https://localhost:44329/api/Test', test);
  }
}
