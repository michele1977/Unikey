import { Component } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Router} from '@angular/router';
import {IconsService} from '../../services/icons.service';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.css']
})
export class LogoutComponent {

  constructor(private router: Router, public icons: IconsService) { }

  logout() {
    localStorage.removeItem('token');
    this.router.navigateByUrl('').then();
  }

}
