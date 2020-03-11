import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Test} from '../models/test';
import {Observable} from 'rxjs';
import {ExTest} from '../models/ex-test';

const reqUrl = 'https://localhost:44329/api/';

@Injectable({
  providedIn: 'root'
})
export class ExTestService {
  constructor(private http: HttpClient) { }


  getExTestByTestId(id: number) {
    return this.http.get<ExTest[]>(reqUrl + 'ExTest/GetByTestId/' + id);
  }
}
