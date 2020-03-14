import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { ExTestList } from '../models/ex-test-list';
import { LOCALHOST_URL } from '../constants/api.const';

const reqUrl = LOCALHOST_URL + 'ExTest/';

@Injectable({
  providedIn: 'root'
})


export class ExTestListService {
  constructor( private http: HttpClient) { }

  getExTests(numPage: number, pageSize: number, filter: string) {
    return this.http.get<ExTestList>(reqUrl + `GetAll?pageNum=` + numPage + '&pageSize=' + pageSize +  '&filter=' + filter);
  }
}
