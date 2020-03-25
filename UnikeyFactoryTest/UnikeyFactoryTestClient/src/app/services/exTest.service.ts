import {Injectable} from '@angular/core';
import {ExTest} from '../models/ex-test';
import { LOCALHOST_URL } from '../constants/api.const';
import {TestSubject} from '../models/testSubject';
import {HttpWrapperService} from './http-wrapper.service';
import {HttpClient} from '@angular/common/http';

const reqUrl = LOCALHOST_URL + 'ExTest/';

@Injectable({
  providedIn: 'root'
})
export class ExTestService {
  constructor(private httpWrapper: HttpWrapperService, private http: HttpClient) { }

  getExTestById(id: number) {
    // return this.http.get<ExTest>(reqUrl + 'Get/' + id);
    return this.httpWrapper.invokeGetUrl(reqUrl + 'Get/' + id);

  }

  getExTestByTestId(pageNum: number, pageSize: number, textFilter: string, id: number) {
    // return this.http.get<ExTest[]>(
    //   reqUrl + 'GetByTestId?pageNum=' + pageNum + '&pageSize=' + pageSize + '&filter=' + textFilter + '&id=' + id
    // );
    return this.httpWrapper.invokeGetUrl(reqUrl + 'GetByTestId?pageNum=' + pageNum + '&pageSize=' + pageSize + '&filter=' + textFilter
      + '&id=' + id);
  }

  getExTestByTestUrl(guid: string, subject: TestSubject) {
    return this.http.get<ExTest>(reqUrl + 'GetExTestByTestUrl?guid=' + guid + '&subject=' + subject.firstName + ' ' + subject.lastName);
  }

  saveExTest(exTest: ExTest) {
    return this.http.post<ExTest>(reqUrl + 'Create', exTest);
  }
}
