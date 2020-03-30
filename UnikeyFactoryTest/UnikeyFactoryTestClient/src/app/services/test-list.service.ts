import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {TestList} from '../models/test-list';
import {HttpWrapperService} from './http-wrapper.service';
import {catchError, retry} from 'rxjs/operators';
import {Observable, throwError} from 'rxjs';
import {error} from '@angular/compiler/src/util';

const reqUrl = 'http://localhost/UnikeyFactoryTest.WebAPI/api/';

@Injectable({
  providedIn: 'root'
})


export class TestListService {
  constructor(private http: HttpClient, private httpWrapper: HttpWrapperService) { }
  getTests(numPage: number, pageSize: number, filter: string) {
    return this.httpWrapper.invokeGetUrl(reqUrl + `Test?pageNum=` + numPage + '&pageSize=' + pageSize + '&filter=' + filter);
  }
}
