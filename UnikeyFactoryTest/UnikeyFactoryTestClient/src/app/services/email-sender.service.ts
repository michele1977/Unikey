import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {EmailModel} from '../models/emailModel';

@Injectable({
  providedIn: 'root'
})
export class EmailSenderService {
  constructor(private http: HttpClient) {
  }

  sendEmail(emailModel: EmailModel) {
    return this.http.post('http://localhost/UnikeyFactoryTest.WebAPI/api/Test/sendMail', emailModel);
  }
}
