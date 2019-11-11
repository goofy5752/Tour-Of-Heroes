import { User } from './../../entities/user';
import { Title } from '@angular/platform-browser';
import { UserService } from './../../services/user.service';
import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.css']
})
export class UserDetailComponent implements OnInit {

  @Input() profile: User;

  constructor(private route: ActivatedRoute, private userService: UserService, private titleService: Title) { }

  ngOnInit() {
    this.route.params.subscribe(() => {
      this.getUser();
    });
  }

  getUser(): void {
    const id = this.route.snapshot.paramMap.get('id');
    this.userService.getUser(id)
      .subscribe(user => {
        this.profile = user;
        this.titleService.setTitle(`${this.profile.fullName} Details`);
      });
  }
}
