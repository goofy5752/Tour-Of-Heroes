import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Globals } from '../globals/globals';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {


  constructor(private router: Router, public globals: Globals) {
  }
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean {
    if (localStorage.getItem('token') != null) {
      this.globals.isLogged = true;
      // console.log(this.globals.isLogged);
      return true;
    } else {
      this.router.navigate(['user/login']);
      this.globals.isLogged = false;
      // console.log(this.globals.isLogged);
      return false;
    }
  }
}
