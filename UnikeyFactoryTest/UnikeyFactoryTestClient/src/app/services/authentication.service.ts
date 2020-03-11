import { Injectable } from '@angular/core';
import {CanActivate, Router} from '@angular/router';
import {LoginService} from './login.service';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService implements CanActivate {

  constructor(private loginService: LoginService, private router: Router) {
  }

  canActivate(): boolean {
    const jwt = localStorage.getItem('token');
    if (jwt === null) {
      this.router.navigateByUrl('').then();
      return false;
    }
    return true;
  }
}
