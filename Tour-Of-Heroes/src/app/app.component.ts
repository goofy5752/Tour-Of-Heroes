import { Component } from '@angular/core';
import { Globals } from './globals/globals';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Tour of Heroes';

  constructor(public globals: Globals) {}
}
