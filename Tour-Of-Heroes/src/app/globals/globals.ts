import { Injectable } from '@angular/core';

@Injectable()
export class Globals {
  showActivity = true;
  public isLogged: any;
  public isAdmin = false;
  public isEditor = false;

  isLogger() {
    if (localStorage.getItem('token') != null) {
      this.isLogged = true;
    } else {
      this.isLogged = false;
    }
  }
}
