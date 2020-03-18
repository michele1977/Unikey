import {Component, EventEmitter, Output} from '@angular/core';
import {User} from '../../models/user';
import {LoginService} from '../../services/login.service';
import {finalize} from 'rxjs/operators';
import {from} from 'rxjs';
import { LoaderService } from 'src/app/services/loader.service';

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

  constructor(public service: LoginService, private loader: LoaderService) {
  }

  @Output() switch: EventEmitter<any> = new EventEmitter<any>();

  changeForm() {
    this.switch.emit();
  }

  login(user: User) {
    this.loader.publish('show');
    this.service.login(user).subscribe(
      jwt => {
        this.setSession(jwt, user);
      }, () => {
        this.service.router.navigateByUrl('')
          .then(() => this.loader.publish('hide'));
        this.isPostBack = true;
      }, () => {
          this.service.router.navigateByUrl('testList')
            .then(() => this.loader.publish('hide'));
      }
    );
  }

  private setSession(jwt: string, user: User) {
    localStorage.setItem('token', jwt);
    localStorage.setItem('userInfo', user.username);
  }
}
