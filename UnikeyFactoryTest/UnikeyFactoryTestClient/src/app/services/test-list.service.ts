import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {TestList} from '../models/test-list';
import {HttpWrapperService} from './http-wrapper.service';

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
