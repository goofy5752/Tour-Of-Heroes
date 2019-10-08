import { Globals } from './../globals';
import { Component, OnInit } from '@angular/core';
import { MessageService } from '../message.service';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {
  public buttonName: any = 'Show';
  checkedOrNo = '';

  constructor(public messageService: MessageService, public globals: Globals) {}

  ngOnInit() {
    console.log(this.globals.showActivity);
  }

  toggle() {
    this.globals.showActivity = !this.globals.showActivity;
    // this.show = this.globals.show;
    // CHANGE THE NAME OF THE BUTTON.
    if (this.globals.showActivity) {
      this.buttonName = 'Hide';
    } else {
      this.buttonName = 'Show';
    }
  }
}
