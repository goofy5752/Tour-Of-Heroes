import { Globals } from './globals/globals';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(public globals: Globals) {}
  title = 'Tour of Heroes';
}
