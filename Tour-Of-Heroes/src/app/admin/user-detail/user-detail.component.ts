import { ToastrService } from 'ngx-toastr';
import { User } from './../../entities/user';
import { Title } from '@angular/platform-browser';
import { UserService } from './../../services/user.service';
import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserActivity } from 'src/app/entities/userActivity';

@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.css']
})
export class UserDetailComponent implements OnInit {
  userId = '';
  @Input() user: User;
  activity;

  constructor(private route: ActivatedRoute,
              private userService: UserService,
              private titleService: Title,
              private toastr: ToastrService) { }

  ngOnInit() {
    this.route.params.subscribe(() => {
      this.getUser();
    });
  }

  getUser(): void {
    const id = this.route.snapshot.paramMap.get('id');
    this.userService.getUser(id)
      .subscribe(user => {
        this.userId = id;
        this.user = user;
        this.activity = user.activity;
        this.titleService.setTitle(`${this.user.fullName} Details`);
      });
  }

  updateUser(role: string) {
    this.userService.updateUser(this.userId, role).subscribe(
      () => {
        this.toastr.success(`The user have new title: ${role}`, 'Successfully changed !');
      },
      error => {
        if (error.status === 400) {
          this.toastr.error(`Please enter a role that is different from the previous one.`, 'Edit failed.');
        } else if (error.status === 500) {
          this.toastr.error(`The user you are trying to rename is probably removed from the server.`, 'Edit failed.');
        } else {
          console.log(error);
        }
      }
    );
  }
}
