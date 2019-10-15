import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  private href = '';

  constructor(private router: Router) { }

  ngOnInit() {
    this.href = this.router.url;
    const charToUpper = this.href.charAt(1).toUpperCase();
    return charToUpper + this.href.substring(2, this.href.length);
  }

  clearLocalStorage() {
    localStorage.removeItem('token');
    this.router.navigateByUrl('user/login');
  }
}
