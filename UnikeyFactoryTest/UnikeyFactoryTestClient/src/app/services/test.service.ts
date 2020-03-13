import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {Test} from '../models/test';
import {map} from 'rxjs/operators';
import {Observable} from 'rxjs';

const reqUrl = 'http://localhost/UnikeyFactoryTest.WebAPI/api//Test/';

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

  updateTest(test: Test){
    return this.http.patch<number>(reqUrl + 'Update', test);
  }
}
