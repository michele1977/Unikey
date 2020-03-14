import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {Test} from '../models/test';
import {map} from 'rxjs/operators';
import {Observable} from 'rxjs';
import { LOCALHOST_URL } from '../constants/api.const';

// const reqUrl = 'http://localhost:44329/api/Test/';
const reqUrl = LOCALHOST_URL + 'Test/';

@Injectable({
  providedIn: 'root'
})
export class TestService {
  constructor(private http: HttpClient) { }

  createTest(test: Test) {
      return this.http.post(reqUrl + 'Create', test);
  }

  getTest(id: number): Observable<Test> {
    return this.http.get<Test>(reqUrl + 'Get/' + id);
  }

  updateTest(test: Test) {
    return this.http.patch<number>(reqUrl + 'Update', test);
  }
}
