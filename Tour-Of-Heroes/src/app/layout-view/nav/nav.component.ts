import { Title } from '@angular/platform-browser';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  constructor(private router: Router, private titleService: Title) { }

  ngOnInit() {
  }

  clearLocalStorage() {
    localStorage.removeItem('token');
    this.router.navigateByUrl('user/login');
  }

  getDocTitle(): string {
    return this.titleService.getTitle();
  }
}
