import {Component} from '@angular/core';
import {SubscribeService} from '../../services/subscribe.service';
import {User} from '../../models/user';
import {HttpErrorResponse} from '@angular/common/http';
import {map} from 'rxjs/operators';


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

  constructor(private service: SubscribeService) { }

  subscribe(user: User) {
    this.errorsList = null;
    this.service.register(user)
      .pipe(map((res: Response) => res.json()))
      .subscribe(
        data => {

          console.log('OK');
          // this.router.navigate(['/login']);
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
        });
  }


}
