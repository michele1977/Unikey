import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Test} from '../models/test';
import {Observable} from 'rxjs';
import { LOCALHOST_URL } from '../constants/api.const';
import {ExTest} from '../models/ex-test';
import {TestSubject} from '../models/testSubject';

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

  getTestByUrl(url: string) {
    return this.http.get<Test>(reqUrl + 'GetByUrl?url=' + url);
  }

  updateTest(test: Test) {
    return this.http.patch<number>(reqUrl + 'Update', test);
  }

  downloadPdf(testId: number): Observable<Blob> {
    return this.http.get(reqUrl + 'GetPdf?testId=' + testId, {responseType: 'blob'});
  }
}
