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

  constructor(public service: LoginService) {
  }

  @Output() switch: EventEmitter<any> = new EventEmitter<any>();

  changeForm() {
    this.switch.emit();
  }

  login(user: User) {
    this.service.login(user).subscribe(
      jwt => {
        this.setSession(jwt);
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

  private setSession(jwt: string) {
    localStorage.setItem('token', jwt);
  }
}
