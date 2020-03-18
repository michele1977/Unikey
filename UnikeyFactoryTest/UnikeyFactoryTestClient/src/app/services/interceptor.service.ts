import { Injectable } from '@angular/core';
import {HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse} from '@angular/common/http';
import {Observable, of} from 'rxjs';
import {LoginService} from './login.service';
import {catchError, first, map, single, tap} from 'rxjs/operators';
import {RefreshtokenService} from './refreshtoken.service';
import {LogoutService} from './logout.service';

@Injectable({
  providedIn: 'root'
})
export class InterceptorService implements HttpInterceptor {

  constructor(private service: LoginService, private logoutService: LogoutService ) {
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const jwt = localStorage.getItem('token');
    req = req.clone({
      setHeaders: {
        Authorization: 'bearer ' + jwt
      }
      });

    return next.handle(req).pipe(
      map((event: HttpResponse<any>) => {
        if (event.status === 201) {
        localStorage.setItem('token', event.body);
        const URL = this.service.router.url;
        this.service.router.navigateByUrl(URL).then();
      }
        return event;
      }),
      catchError((error: any) => {
        if (error instanceof HttpErrorResponse && error.status === 401) {
          this.service.router.navigateByUrl('').then();
        }
        return of(error);
      }),
    );
  }
}
