import { UserService } from './../services/user.service';
import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Globals } from '../globals/globals';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {


  constructor(private router: Router, public globals: Globals, private userService: UserService) {
  }
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean {
    if (localStorage.getItem('token') != null) {
      const roles = next.data[`permittedRoles`];
      this.globals.isLogged = true;
      if (roles) {
        if (this.userService.roleMatch(roles)) { return true; } else {
          this.router.navigate(['user/forbidden']);
          return false;
        }
      }
      return true;
    } else {
      this.router.navigate(['user/login']);
      this.globals.isLogged = false;
      // console.log(this.globals.isLogged);
      return false;
    }
  }
}
