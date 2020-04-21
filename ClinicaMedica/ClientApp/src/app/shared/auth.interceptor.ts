import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable, from } from 'rxjs';

import { switchMap } from 'rxjs/operators';
import { AuthService } from './auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private authService: AuthService) {  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    if (req.url.search('login') !== -1) {
      return next.handle(req);
    } else {
      if (this.authService.tokenExpired()) {
        this.authService.logout();
      }
    //   if (this.authService.tokenExpired()) {
    //     return from(this.authService.refreshToken())
    //       .pipe(
    //         switchMap(authModel => {
    //           this.authService.setSession(authModel);

    //           const cloned = req.clone({
    //             headers: req.headers.set('Authorization', 'Bearer '.concat(authModel.access_token)) });

    //           return next.handle(cloned);
    //         })
    //       );
    //   } else {
        const cloned = req.clone({
          headers: req.headers.set('Authorization', 'Bearer '.concat(localStorage.getItem('token'))) });

        return next.handle(cloned);
    //  }
    }
  }
}
