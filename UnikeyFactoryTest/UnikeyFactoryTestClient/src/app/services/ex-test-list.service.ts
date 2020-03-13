import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { ExTestList } from '../models/ex-test-list';

const reqUrl = 'https://localhost:44329/api/';

@Injectable({
  providedIn: 'root'
})


export class ExTestListService {
  constructor( private http: HttpClient) { }

  getExTests(numPage: number, pageSize: number, filter: string) {
    return this.http.get<ExTestList>(reqUrl + `ExTest/GetAll?pageNum=` + numPage + '&pageSize=' + pageSize +  '&filter=' + filter);
  }
}
