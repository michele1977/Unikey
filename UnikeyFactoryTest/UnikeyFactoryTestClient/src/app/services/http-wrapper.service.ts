import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {take} from 'rxjs/operators';
import {Router} from '@angular/router';
import {RefreshTokenService} from './refreshtoken.service';

@Injectable({
  providedIn: 'root'
})
export class HttpWrapperService {

  constructor(private httpClient: HttpClient, private router: Router, private refreshService: RefreshTokenService) {
  }

  invokePostUrl(url: string, obj): Promise<any> {
    return new Promise<any>((resolve, reject) => {
      this.httpClient.post(url, obj)
        .subscribe(
          param => {
            resolve(param);
          },
          error => {
            if (error instanceof HttpErrorResponse && error.status === 400) {
              this.refreshService.Refresh().subscribe(
                newJwt => {
                  localStorage.setItem('token', newJwt);
                  console.log(newJwt);
                  this.refreshService.router.navigateByUrl(url).then();
                });
            } else {
              reject(error);
            }
          });
    });
  }
}
