import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {EmailModel} from '../models/emailModel';
import {LOCALHOST_URL} from '../constants/api.const';
import {HttpWrapperService} from './http-wrapper.service';

@Injectable({
  providedIn: 'root'
})
export class EmailSenderService {
 URL = LOCALHOST_URL + '/Test/SendMail';
  constructor(private httpWrapper: HttpWrapperService) {
  }

  sendEmail(emailModel: EmailModel) {
    return this.httpWrapper.invokePostUrl(this.URL, emailModel);
  }
}
