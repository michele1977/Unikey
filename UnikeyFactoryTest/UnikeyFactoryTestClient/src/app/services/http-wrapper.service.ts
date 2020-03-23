import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
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
            if (error instanceof HttpErrorResponse) {
              switch (error.status) {
                case 400:
                  this.refreshService.Refresh().subscribe(
                    newJwt => {
                      localStorage.setItem('token', newJwt);
                      resolve(this.invokePostUrl(url, obj));
                    });
                  break;
                case 401:
                  this.refreshService.router.navigateByUrl('').then();
                  break;
                default:
                  this.refreshService.router.navigateByUrl('/error').then();
              }
            } else {
              reject(error);
            }
          });
    });
  }

  invokeGetUrl(url: string): Promise<any> {
    return new Promise<any>((resolve, reject) => {
      this.httpClient.get(url)
        .subscribe(
          param => {
            resolve(param);
          },
          error => {
            if (error instanceof HttpErrorResponse) {
              switch (error.status) {
                case 400:
                  this.refreshService.Refresh().subscribe(
                    newJwt => {
                      localStorage.setItem('token', newJwt);
                      resolve(this.invokeGetUrl(url));
                    });
                  break;
                case 401:
                  this.refreshService.router.navigateByUrl('').then();
                  break;
                default:
                  this.refreshService.router.navigateByUrl('/error').then();
              }
            } else {
              reject(error);
            }
          });
    });
  }

  invokeDeleteUrl(url: string): Promise<any> {
    return new Promise<any>((resolve, reject) => {
      this.httpClient.delete(url)
        .subscribe(
          param => {
            resolve(param);
          },
          error => {
            if (error instanceof HttpErrorResponse) {
              switch (error.status) {
                case 400:
                  this.refreshService.Refresh().subscribe(
                    newJwt => {
                      localStorage.setItem('token', newJwt);
                      resolve(this.invokeDeleteUrl(url));
                    });
                  break;
                case 401:
                  this.refreshService.router.navigateByUrl('').then();
                  break;
                default:
                  this.refreshService.router.navigateByUrl('/error').then();
              }
            } else {
              reject(error);
            }
          });
    });
  }

  invokePutUrl(url: string, obj): Promise<any> {
    return new Promise<any>((resolve, reject) => {
      this.httpClient.put(url, obj)
        .subscribe(
          param => {
            resolve(param);
          },
          error => {
            if (error instanceof HttpErrorResponse) {
              switch (error.status) {
                case 400:
                  this.refreshService.Refresh().subscribe(
                    newJwt => {
                      localStorage.setItem('token', newJwt);
                      resolve(this.invokePutUrl(url, obj));
                    });
                  break;
                case 401:
                  this.refreshService.router.navigateByUrl('').then();
                  break;
                default:
                  this.refreshService.router.navigateByUrl('/error').then();
              }
            } else {
              reject(error);
            }
          });
    });
  }

  invokePatchUrl(url: string, obj): Promise<any> {
    return new Promise<any>((resolve, reject) => {
      this.httpClient.patch(url, obj)
        .subscribe (
          param => {
            resolve(param);
          },
          error => {
            if (error instanceof HttpErrorResponse) {
              switch (error.status) {
                case 400:
                  this.refreshService.Refresh().subscribe(
                    newJwt => {
                      localStorage.setItem('token', newJwt);
                      resolve(this.invokePutUrl(url, obj));
                    });
                  break;
                case 401:
                  this.refreshService.router.navigateByUrl('').then();
                  break;
                default:
                  this.refreshService.router.navigateByUrl('/error').then();
              }
            } else {
              reject(error);
            }
          });
    });
  }
}
