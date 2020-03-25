import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Test} from '../models/test';
import {Observable} from 'rxjs';
import { LOCALHOST_URL } from '../constants/api.const';
import {ExTest} from '../models/ex-test';
import {TestSubject} from '../models/testSubject';
import {Router} from '@angular/router';
import {HttpWrapperService} from './http-wrapper.service';

const reqUrl = LOCALHOST_URL + 'Test/';

@Injectable({
  providedIn: 'root'
})
export class TestService {
  constructor(private http: HttpClient, private httpWrapper: HttpWrapperService) { }

  createTest(test: Test) {
      return this.httpWrapper.invokePostUrl(reqUrl + 'Create', test);
  }

  getTest(id: number): Promise<Test> {
    return this.httpWrapper.invokeGetUrl(reqUrl + 'Get/' + id);
  }

  getTestByUrl(url: string) {
    return this.http.get<Test>(reqUrl + 'GetByUrl?url=' + url);
  }

  updateTest(test: Test) {
    return this.httpWrapper.invokePatchUrl(reqUrl + 'Update', test);
  }

  downloadPdf(testId: number): Observable<Blob> {
    return this.http.get(reqUrl + 'GetPdf?testId=' + testId, {responseType: 'blob'});
  }

  deleteTest(id: number): Promise<Test> {
    return this.httpWrapper.invokeDeleteUrl(reqUrl + 'Delete/' + id);
  }
}
