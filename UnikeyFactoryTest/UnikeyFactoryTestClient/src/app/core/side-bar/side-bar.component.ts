import {Component, DoCheck} from '@angular/core';
import {SideBarService} from '../../services/side-bar.service';
import {Router} from '@angular/router';
import {IconsService} from '../../services/icons.service';


@Component({
  selector: 'app-side-bar',
  templateUrl: './side-bar.component.html',
  styleUrls: ['./side-bar.component.css']
})

export class SideBarComponent implements DoCheck {

  isLogoutVisible: boolean;
  user: string;
  userName: string;


  constructor(public nav: SideBarService, private router: Router, public icons: IconsService) {}
    public isButtonVisible = true;
    Toggle() {
     this.isButtonVisible = false;
    }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('userInfo');
    this.user = null;
    this.router.navigateByUrl('').then();
  }

  ngDoCheck(): void {
    let name = localStorage.getItem('userInfo');
    const jwt = localStorage.getItem('token');
    if (jwt === null) {
      this.isLogoutVisible = false;
    } else if (name === null && jwt !== null) {
      name = '';
      this.isLogoutVisible = true;
    } else {
      this.user = ', ' + name.substring(0, 1);
      this.isLogoutVisible = true;
      this.userName = name;
    }
  }
}
