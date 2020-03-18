import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {TestList} from '../models/test-list';
import {Test} from '../models/test';

const reqUrl = 'http://localhost/UnikeyFactoryTest.WebAPI/api/';

@Injectable({
  providedIn: 'root'
})


export class TestListService {
  constructor( private http: HttpClient) { }

  getTests(numPage: number, pageSize: number, filter: string) {
    return this.http.get<Test[]>(reqUrl + `Test?pageNum=` + numPage + '&pageSize=' + pageSize + '&filter=' + filter);
  }
}
