import { Injectable } from '@angular/core';
import {Router, RouterEvent, RoutesRecognized} from '@angular/router';
import {filter, first, map, take} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class LogoutService {
  user: string;

  isVisible = true;

  constructor(private router: Router) {
  }

  setVisible(): boolean {
    return this.checkLocalStorage() && this.checkErrorPage();
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
    this.router.events.pipe(
      filter(event => {
        return !!(event instanceof RoutesRecognized && ((event.url.match('/error') || event.url.match(''))));
      }))
      .subscribe(() => {
        this.isVisible = false;
        console.log('test');
      });
    return this.isVisible;
  }

  public logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('userInfo');
    this.router.navigateByUrl('').then();
  }
}
