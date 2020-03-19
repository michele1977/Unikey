import { Injectable } from '@angular/core';
import {HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse} from '@angular/common/http';
import {Observable, of} from 'rxjs';
import {LoginService} from './login.service';
import {catchError, map} from 'rxjs/operators';
import {RefreshTokenService} from './refreshtoken.service';

@Injectable({
  providedIn: 'root'
})
export class InterceptorService implements HttpInterceptor {

  constructor(private loginService: LoginService, private refreshService: RefreshTokenService) {
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const jwt = localStorage.getItem('token');

    req = req.clone({
      setHeaders: {
        Authorization: 'bearer ' + jwt
      }
      });

    return next.handle(req).pipe(
      catchError((error: any) => {
        if (error instanceof HttpErrorResponse) {
          switch (error.status) {

            default:
              this.loginService.router.navigateByUrl('/error').then();
          }
        }
        return of(error);
      })
    );
  }
}
