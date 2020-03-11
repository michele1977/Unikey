import { Injectable } from '@angular/core';
import {HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse} from '@angular/common/http';
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
    req = req.clone({
        setHeaders: {
          Authorization: 'bearer ' + jwt
        }
      });

    return next.handle(req);
  }

  interceptErr(err: HttpErrorResponse) {
    if (err.status === 401 ) {
      this.service.router.navigateByUrl('').then();
    }
  }

}
