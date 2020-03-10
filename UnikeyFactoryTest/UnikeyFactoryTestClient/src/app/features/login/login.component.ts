import { Component, OnInit } from '@angular/core';
import {User} from '../../models/user';
import {NgForm} from '@angular/forms';
import {LoginService} from '../../services/login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  minLength = 1;
  maxLength = 50;
  passwordPattern = '^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[*@$!#%&()^_~{}+=|.]).{6,50}$';

  constructor(private service: LoginService) {
  }

  login(user: User) {
    this.service.login(user);
    console.log();
  }
}
