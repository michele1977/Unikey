import {Component} from '@angular/core';
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
  isLogged: boolean;
  isPostBack: boolean;

  constructor(public service: LoginService) {
  }

  login(user: User) {
    this.service.login(user).subscribe(
      jwt => {
        this.setSession(jwt);
        this.isLogged = true;
        console.log(this.isLogged);
      }, error => {}, () => {
        if (this.isLogged) {
          this.service.router.navigateByUrl('testList')
            .then();
        } else {
          this.service.router.navigateByUrl('')
            .then();
          this.isPostBack = true;
          console.log(this.isLogged);
        }
      }
    );
  }

  private setSession(jwt: string) {
    localStorage.setItem('token', jwt);
  }
}
