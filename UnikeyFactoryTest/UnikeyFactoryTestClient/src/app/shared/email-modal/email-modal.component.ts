import {Component, DoCheck, Input, OnInit} from '@angular/core';
import {EmailSenderService} from '../../services/email-sender.service';
import {NgForm} from '@angular/forms';
import {EmailModel} from '../../models/emailModel';

@Component({
  selector: 'app-email-modal',
  templateUrl: './email-modal.component.html',
  styleUrls: ['./email-modal.component.css']
})
export class EmailModalComponent implements DoCheck {
  @Input() show = false;
  @Input() testId: number;

  email: EmailModel;

  constructor(private emailSender: EmailSenderService) {
    console.log('Ciao, sono il modal');
    console.log(this.show);
  }

  ngDoCheck(): void {
    console.log(this.show);
    if (this.show === true) {
      const btn = document.getElementById('btnOpenModal').click();
      console.log('sono visibile');
    }
    console.log(this.show);
  }

  sendEmail(form: NgForm) {
    this.email.Id = this.testId;
    this.email.Name = form.value.name;
    this.email.Email = form.value.email;
    this.emailSender.sendEmail(this.email);
  }

}
