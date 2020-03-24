import {AfterContentInit, Component, DoCheck, Input, OnInit} from '@angular/core';
import {EmailSenderService} from '../../services/email-sender.service';

import {EmailModel} from '../../models/emailModel';
import {NgbActiveModal} from '@ng-bootstrap/ng-bootstrap';
import {IconsService} from '../../services/icons.service';
import {LoaderService} from '../../services/loader.service';
import validate = WebAssembly.validate;
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-email-modal',
  templateUrl: './email-modal.component.html',
  styleUrls: ['./email-modal.component.css']
})
export class EmailModalComponent {


  constructor(
    private emailSender: EmailSenderService,
    public activeModal: NgbActiveModal,
    public icons: IconsService,
    private loader: LoaderService) { }

  emailValidator = '^([a-zA-Z0-9-.]+)@[a-zA-Z_]+?\\.[a-zA-Z]{2,3}$';
  testId: number;
  emailSent = false;
  emailError = false;
  email: EmailModel = {
    Name: '',
    Email: '',
    Id: 0
  };

  sendEmail(form: NgForm) {
    this.loader.publish('show');
    this.email.Id = this.testId;
    this.email.Name = form.value.name;
    this.email.Email = form.value.email;
    this.emailSender.sendEmail(this.email).then( res => {
        this.emailSent = true;
        this.loader.publish('hide');
      },
      error => {
        this.emailError = true;
        this.loader.publish('hide');
      });
  }

}
