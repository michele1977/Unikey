import { Injectable } from '@angular/core';
import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {Observable} from 'rxjs';
import {LoginService} from './login.service';

@Injectable({
  providedIn: 'root'
})
export class InterceptorService implements HttpInterceptor {

  constructor(private service: LoginService) {
  }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const jwt = localStorage.getItem('token');
    if (jwt !== null) {
      req = req.clone({
        setHeaders: {
          Authorization: 'bearer ' + jwt
        }
      });
    } else {
      this.service.router.navigateByUrl('').then();
    }
    return next.handle(req);
  }

}
