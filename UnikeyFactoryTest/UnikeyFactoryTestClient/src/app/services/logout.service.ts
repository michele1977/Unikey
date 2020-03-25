import { Injectable } from '@angular/core';
import {Router, RouterEvent, RouterOutlet, RoutesRecognized} from '@angular/router';
import {filter, first, map, take} from 'rxjs/operators';
import {HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class LogoutService {
  user: string;

  constructor(private router: Router) {
  }

  setVisible(): boolean {
    const checkLocalStorage = this.checkLocalStorage();
    const checkErrorPage = this.checkErrorPage();
    return checkLocalStorage && checkErrorPage;
  }

  private checkLocalStorage(): boolean {
    const jwt = localStorage.getItem('token');
    const name = localStorage.getItem('userInfo');
    let isVisible = false;

    if (jwt !== null) {
      isVisible = true;
      this.user = name;
    }
    return isVisible;
  }

  private checkErrorPage(): boolean {
    let isVisible = true;
    if (this.router.url.match('/error')) {
      isVisible = false;
    }
    return isVisible;
  }

  public logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('userInfo');
    this.router.navigateByUrl('').then();
  }
}
