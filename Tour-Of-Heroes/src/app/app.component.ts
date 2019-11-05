import { Globals } from './globals/globals';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(public globals: Globals) {
    const token = JSON.stringify(localStorage.getItem('token'));
    const jwtData = token.split('.')[1];
    const decodedJwtJsonData = window.atob(jwtData);
    const decodedJwtData = JSON.parse(decodedJwtJsonData);
    const role = decodedJwtData.role;
    if (role === 'Admin') {
      this.globals.isAdmin = true;
    } else {
      this.globals.isAdmin = false;
    }
  }
  title = 'Tour of Heroes';
}
