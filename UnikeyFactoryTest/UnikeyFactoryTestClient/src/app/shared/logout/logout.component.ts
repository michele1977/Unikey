import {Component, DoCheck, Input, OnChanges, SimpleChanges} from '@angular/core';
import {Router} from '@angular/router';
import {IconsService} from '../../services/icons.service';
import {LogoutService} from '../../services/logout.service';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.css']
})
export class LogoutComponent {

  constructor( public icons: IconsService, public logoutService: LogoutService) {
  }
  logout() {
      this.logoutService.logout();
  }

}
