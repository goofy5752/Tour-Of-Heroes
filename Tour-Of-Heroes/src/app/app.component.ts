import { Globals } from './globals/globals';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(public globals: Globals) {
    if (localStorage.getItem('token') !== null) {
      const token = JSON.stringify(localStorage.getItem('token'));
      const jwtData = token.split('.')[1];
      const decodedJwtJsonData = window.atob(jwtData);
      const decodedJwtData = JSON.parse(decodedJwtJsonData);
      const role = decodedJwtData.role;
      if (role === 'Admin') {
        this.globals.isAdmin = true;
        return;
      }
      if (role === 'Editor') {
        this.globals.isEditor = true;
        return;
      }
    }
  }
  title = 'Tour of Heroes';
}
