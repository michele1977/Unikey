import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {TestList} from '../models/test-list';

const reqUrl = 'https://localhost:44329/api/';

@Injectable({
  providedIn: 'root'
})


export class TestListService {
  constructor( private http: HttpClient) { }

  getTests(numPage: number, pageSize: number, filter: string) {
    return this.http.get<TestList>(reqUrl + `Test?pageNum=` + numPage + '&pageSize=' + pageSize + '&filter=' + filter);
  }
}
