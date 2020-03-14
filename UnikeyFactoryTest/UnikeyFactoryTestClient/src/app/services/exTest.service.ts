import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {ExTest} from '../models/ex-test';
import { LOCALHOST_URL } from '../constants/api.const';

const reqUrl = LOCALHOST_URL + 'ExTest/';

@Injectable({
  providedIn: 'root'
})
export class ExTestService {
  constructor(private http: HttpClient) { }

  getExTestById(id: number) {
    return this.http.get<ExTest>(reqUrl + 'Get/' + id);
  }

  getExTestByTestId(pageNum: number, pageSize: number, textFilter: string, id: number) {
    return this.http.get<ExTest[]>(
      reqUrl + 'GetByTestId?pageNum=' + pageNum + '&pageSize=' + pageSize + '&filter=' + textFilter + '&id=' + id
    );
  }
}
