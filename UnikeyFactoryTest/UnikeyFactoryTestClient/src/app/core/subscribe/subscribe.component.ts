import { Component, OnInit } from '@angular/core';
import {SubscribeService} from '../../services/subscribe.service';
import {NgForm} from '@angular/forms';
import {User} from '../../models/user';
import {isNotNullOrUndefined} from 'codelyzer/util/isNotNullOrUndefined';

@Component({
  selector: 'app-subscribe',
  templateUrl: './subscribe.component.html',
  styleUrls: ['./subscribe.component.css']
})
export class SubscribeComponent {

  constructor(private service: SubscribeService) { }

  user: User;
  errorPass = false;
  emptyError = false;

  subscribe(form: NgForm) {
      if (form.value.Password === form.value.retyped) {
        this.user = form.value;
        this.service.subscribe(this.user);
        this.errorPass = false;
        this.emptyError = false;
      } else {
        this.errorPass = true;
      }
  }
}
