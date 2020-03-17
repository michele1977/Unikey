import { Injectable } from '@angular/core';
import {HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse} from '@angular/common/http';
import {Observable, of} from 'rxjs';
import {LoginService} from './login.service';
import {catchError, map, tap} from 'rxjs/operators';
import {RefreshtokenService} from './refreshtoken.service';

@Injectable({
  providedIn: 'root'
})
export class InterceptorService implements HttpInterceptor {

  constructor(private service: LoginService, private refreshService: RefreshtokenService ) {
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
        if (error instanceof HttpErrorResponse && error.status === 401) {
          if ('Token has expired' === 'Token has expired') {
              console.log('interceptor');
              this.refreshService.Refresh();
              this.service.router.navigateByUrl(req.url).then();
          }
          this.service.router.navigateByUrl('').then();
        }
        return of(error);
      })
    );
  }
}
