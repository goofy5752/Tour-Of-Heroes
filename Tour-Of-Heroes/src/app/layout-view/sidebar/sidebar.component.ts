import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {

  constructor(private titleService: Title) { }

  ngOnInit() {
  }

  setDocTitle(title: string) {
    this.titleService.setTitle(title);
  }

}
