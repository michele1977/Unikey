import {Component, DoCheck} from '@angular/core';
import {Router} from '@angular/router';
import {IconsService} from '../../services/icons.service';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.css']
})
export class LogoutComponent implements DoCheck{
  isVisible: boolean;
  user: string;

  constructor(private router: Router, public icons: IconsService) { }

  ngDoCheck(): void {
      const name = localStorage.getItem('userInfo');
      const jwt = localStorage.getItem('token');
      if (name !== null || jwt !== null) {
       this.user = name;
       this.isVisible = true;
     } else {
       this.isVisible = false;
     }
    }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('userInfo');
    this.user = null;
    this.router.navigateByUrl('').then();
  }

}
