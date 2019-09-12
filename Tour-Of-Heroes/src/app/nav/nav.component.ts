import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { isNumber } from 'util';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  private href = '';

  constructor(private router: Router) {}

  ngOnInit() {
      this.href = this.router.url;
      const charToUpper =  this.href.charAt(1).toUpperCase();
      // const lastChar = isNumber(this.href.charAt(this.href.length - 1));
      // if (lastChar === false) {
      //   return 'Hero details';
      // }
      // return typeof lastChar;
      return charToUpper + this.href.substring(2, this.href.length);
  }

}
