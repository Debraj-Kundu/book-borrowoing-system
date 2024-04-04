import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpResponse,
} from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import { LoginService } from '../Shared/Service/login.service';
import { Router } from '@angular/router';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  constructor(private loginService: LoginService, private router: Router) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    const jwtToken = this.loginService.getToken();
    if(jwtToken == null)
      return next.handle(request);
    if (jwtToken) {
      request = request.clone({
        setHeaders: { Authorization: `Bearer ${jwtToken}` },
      });
    }

    return next.handle(request).pipe(
      catchError((err) => {
        if (err instanceof HttpResponse) {
          if (err.status == 401) {
            console.log('Token expired');
            this.router.navigate(['/login']);
          }
        }
        return throwError(() => new Error('Some other error occured'));
      })
    );
  }
}
