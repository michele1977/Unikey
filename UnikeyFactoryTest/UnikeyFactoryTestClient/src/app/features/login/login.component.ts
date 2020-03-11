import {Component, EventEmitter, Output} from '@angular/core';
import {User} from '../../models/user';
import {LoginService} from '../../services/login.service';
import {finalize} from 'rxjs/operators';
import {from} from 'rxjs';

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
  isLogin = true;

  constructor(public service: LoginService) {
  }
  @Output() switch: EventEmitter<boolean> = new EventEmitter<boolean>();
  changeForm() {
    this.isLogin = !this.isLogin;
    this.switch.emit(this.isLogin);
  }
  login(user: User) {
    this.service.login(user).subscribe(
      jwt => {
        this.setSession(jwt);
      }, error => {
        this.service.router.navigateByUrl('')
          .then();
        this.isPostBack = true;
      }, () => {
          this.service.router.navigateByUrl('testList')
            .then();
      }
    );
  }

  private setSession(jwt: string) {
    localStorage.setItem('token', jwt);
  }
}
