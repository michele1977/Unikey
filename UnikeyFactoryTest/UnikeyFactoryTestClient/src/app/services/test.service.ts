import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {Test} from '../models/test';
import {map} from 'rxjs/operators';
import {Observable} from 'rxjs';
import { LOCALHOST_URL } from '../constants/api.const';
import {Router} from '@angular/router';
import {HttpWrapperService} from './http-wrapper.service';

// const reqUrl = 'http://localhost:44329/api/Test/';
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

  updateTest(test: Test) {
    return this.httpWrapper.invokePatchUrl(reqUrl + 'Update', test);
  }
}
