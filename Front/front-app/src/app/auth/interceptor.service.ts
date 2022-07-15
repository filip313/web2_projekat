import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators'

@Injectable({
  providedIn: 'root'
})
export class InterceptorService implements HttpInterceptor{

  constructor(private router: Router) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    
    if(sessionStorage.getItem('token') != null){
      const cloneReq = req.clone({
        headers: req.headers.set('Authorization', 'Bearer ' + sessionStorage.getItem('token'))
      });
      return next.handle(cloneReq).pipe(
        tap(
          succ => { },
          err => {
            if(err.status == 401){
              sessionStorage.removeItem('token');
              this.router.navigateByUrl('/user/login');
            }
          }
        )
      )
    }
    else{
      return next.handle(req.clone());
    }
  }
}
