import {Component, EventEmitter, Output} from '@angular/core';
import {User} from '../../models/user';
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
  isPostBack: boolean;

  constructor(public service: LoginService) {
    const userName = localStorage.getItem('userInfo');
    const token = localStorage.getItem('token');
    if (token !== null) {
      this.service.router.navigateByUrl('testList').then();
    }
  }

  @Output() switch: EventEmitter<any> = new EventEmitter<any>();

  changeForm() {
    this.switch.emit();
  }

  login(user: User) {
    this.service.login(user).subscribe(
      jwt => {
        this.setSession(jwt, user);
      }, () => {
        this.service.router.navigateByUrl('')
          .then();
        this.isPostBack = true;
      }, () => {
          this.service.router.navigateByUrl('testList')
            .then();
      }
    );
  }

  private setSession(jwt: string, user: User) {
    localStorage.setItem('token', jwt);
    localStorage.setItem('userInfo', user.username);
  }
}
