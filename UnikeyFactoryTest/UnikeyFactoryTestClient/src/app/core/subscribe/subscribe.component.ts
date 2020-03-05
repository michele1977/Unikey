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
  minLength = 1;
  maxLength = 50;


  subscribe(form: NgForm) {

  }
}
