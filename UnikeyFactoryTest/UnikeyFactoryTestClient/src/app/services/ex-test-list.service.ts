import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { ExTestList } from '../models/ex-test-list';
import { LOCALHOST_URL } from '../constants/api.const';
import {HttpWrapperService} from './http-wrapper.service';

const reqUrl = LOCALHOST_URL + 'ExTest/';

@Injectable({
  providedIn: 'root'
})


export class ExTestListService {
  constructor( private httpWrapper: HttpWrapperService) { }

  getExTests(numPage: number, pageSize: number, filter: string) {
    return this.httpWrapper.invokeGetUrl(reqUrl + `GetAll?pageNum=` + numPage + '&pageSize=' + pageSize +  '&filter=' + filter);
  }
}
