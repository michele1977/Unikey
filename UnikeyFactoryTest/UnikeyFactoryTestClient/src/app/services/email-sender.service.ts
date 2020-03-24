import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {EmailModel} from '../models/emailModel';
import {LOCALHOST_URL} from '../constants/api.const';

@Injectable({
  providedIn: 'root'
})
export class EmailSenderService {
 URL = LOCALHOST_URL + '/Test/SendMail';
  constructor(private http: HttpClient) {
  }

  sendEmail(emailModel: EmailModel) {
    return this.http.post('http://localhost/UnikeyFactoryTest.WebAPI/api/Test/SendMail', emailModel);
  }
}
