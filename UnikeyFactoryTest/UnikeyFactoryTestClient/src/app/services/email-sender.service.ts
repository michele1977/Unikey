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
    this.http.post('https://localhost:44351/api/Test/sendMail', emailModel)
      .subscribe(() => console.log('Email inviata con successo'));
  }
}
