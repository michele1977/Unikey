import { Component, OnInit } from '@angular/core';
import {SubscribeService} from '../../services/subscribe.service';
import {NgForm} from '@angular/forms';

@Component({
  selector: 'app-subscribe',
  templateUrl: './subscribe.component.html',
  styleUrls: ['./subscribe.component.css']
})
export class SubscribeComponent {

  constructor(private service: SubscribeService) { }

  subscribe(form: NgForm) {

  }
}
