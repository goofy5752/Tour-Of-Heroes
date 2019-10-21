import { Title } from '@angular/platform-browser';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Globals } from 'src/app/globals/globals';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  constructor(private router: Router, private titleService: Title, private globals: Globals) { }

  ngOnInit() {
  }

  clearLocalStorage() {
    localStorage.removeItem('token');
    this.globals.isLogged = false;
    this.router.navigateByUrl('user/login');
  }

  getDocTitle(): string {
    return this.titleService.getTitle();
  }
}
