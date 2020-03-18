import {Component, DoCheck, Input, OnChanges, SimpleChanges} from '@angular/core';
import {Router} from '@angular/router';
import {IconsService} from '../../services/icons.service';
import {LogoutService} from '../../services/logout.service';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.css']
})
export class LogoutComponent /*implements DoCheck*/{
  isVisible: boolean;
  user: string;

  constructor(private router: Router, public icons: IconsService, public logoutService: LogoutService) {
  }
  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('userInfo');
    this.user = null;
    this.router.navigateByUrl('').then();
  }

  /*ngDoCheck(): void {
    let name = localStorage.getItem('userInfo');
    const jwt = localStorage.getItem('token');
    if (jwt === null) {
      this.isVisible = false;
    } else if (name === null && jwt !== null){
      name = '';
      this.isVisible = true;
    } else {
      this.user = ', ' + name;
      this.isVisible = true;
    }
  }*/
}
