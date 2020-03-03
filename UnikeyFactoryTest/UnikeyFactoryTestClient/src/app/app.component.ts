import { Component } from '@angular/core';
import {SubscribeService} from './services/subscribe.service';
import {User} from './models/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'UnikeyFactoryTestClient';

  constructor(private service: SubscribeService) {
  }

  user: User = { UserName: 'Federicchione', Password: 'Unikey1!' };

  subscribe() {
    this.service.subscribe(this.user);
  }
}
