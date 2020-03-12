import {Component, EventEmitter, Output} from '@angular/core';
import {SubscribeService} from '../../services/subscribe.service';
import {User} from '../../models/user';
import {HttpErrorResponse} from '@angular/common/http';
import {map} from 'rxjs/operators';
import {Router} from '@angular/router';


@Component({
  selector: 'app-subscribe',
  templateUrl: './subscribe.component.html',
  styleUrls: ['./subscribe.component.css']
})
export class SubscribeComponent {

  user: User;
  passwordPattern = '^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[*@$!#%&()^_~{}+=|.]).{6,50}$';
  minLength = 1;
  maxLength = 50;
  error: HttpErrorResponse;
  errorsList: string[];

  @Output() switch: EventEmitter<any> = new EventEmitter<any>();

  constructor(private service: SubscribeService, private router: Router) { }
  
  changeForm() {
    this.switch.emit();
  }

  goToMain() {
    this.router.navigateByUrl('create');
  }

  subscribe(user: User) {
    this.errorsList = null;
    this.service.register(user)
      .subscribe(
        () => {

          this.router.navigateByUrl('create');
          },
          (error: HttpErrorResponse) => {

            this.error = error;
            this.errorsList = [];
            const modelstate = error.error.ModelState;
            for (const key in modelstate) {
              if (modelstate.hasOwnProperty(key)) {
                const val = modelstate[key];
                this.errorsList.push(val);
              }
            }
          }
        );
  }
}
