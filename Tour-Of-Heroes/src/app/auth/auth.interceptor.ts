import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Router } from '@angular/router';
import { Globals } from '../globals/globals';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

    constructor(private router: Router, public globals: Globals) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if (localStorage.getItem('token') != null) {

            const payLoad = JSON.parse(window.atob(localStorage.getItem('token').split('.')[1]));
            const userRole = payLoad.role;
            if (userRole === 'Admin') {
                this.globals.isAdmin = true;
            } else {
                this.globals.isAdmin = false;
            }

            if (userRole === 'Editor') {
                this.globals.isEditor = true;
            } else {
                this.globals.isEditor = false;
            }

            const clonedReq = req.clone({
                headers: req.headers.append('Authorization', 'Bearer ' + localStorage.getItem('token'))
            });
            return next.handle(clonedReq).pipe(
                tap(
                    succ => { },
                    err => {
                        // tslint:disable-next-line: triple-equals
                        if (err.status === 401) {
                            localStorage.removeItem('token');
                            this.router.navigateByUrl('user/login');
                        } else if (err.status === 403) {
                            this.router.navigateByUrl('user/forbidden');
                        }
                    }
                )
            );
        } else {
            return next.handle(req.clone());
        }
    }
}
