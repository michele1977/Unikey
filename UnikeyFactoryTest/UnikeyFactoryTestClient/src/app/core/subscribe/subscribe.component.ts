import { Component, OnInit } from '@angular/core';
import {SubscribeService} from '../../services/subscribe.service';
import {NgForm} from '@angular/forms';
import {User} from '../../models/user';

@Component({
  selector: 'app-subscribe',
  templateUrl: './subscribe.component.html',
  styleUrls: ['./subscribe.component.css']
})
export class SubscribeComponent {

  user: User;
  minLength = 1;
  maxLength = 50;

  constructor(private service: SubscribeService) { }

  subscribe(form: NgForm) {

  }
}
