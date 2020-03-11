import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Test} from '../models/test';
import {Observable} from 'rxjs';
import {ExTest} from '../models/ex-test';

const reqUrl = 'https://localhost:44329/api/ExTest/';

@Injectable({
  providedIn: 'root'
})
export class ExTestService {
  constructor(private http: HttpClient) { }

  getExTestById(id: number) {
    return this.http.get<ExTest>(reqUrl + 'Get/' + id);
  }

  getExTestByTestId(id: number) {
    return this.http.get<ExTest[]>(reqUrl + 'GetByTestId/' + id);
  }
}
