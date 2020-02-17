import { Title } from '@angular/platform-browser';
import { UserService } from '../../services/user.service';
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  constructor(public service: UserService, private router: Router, private toastr: ToastrService, private titleService: Title) { }

  ngOnInit() {
    this.service.formModel.reset();
    if (localStorage.getItem('token') != null) {
      this.router.navigateByUrl('/heroes?page=1');
    }
    this.titleService.setTitle('Register');
  }

  onSubmit() {
    this.service.register().subscribe(
      (res: any) => {
        if (res.succeeded) {
          this.service.formModel.reset();
          this.router.navigateByUrl('user/login');
          this.toastr.success('New user created!', 'Registration successful.');
        } else {
          res.errors.forEach(element => {
            switch (element.code) {
              case 'DuplicateUserName':
                this.toastr.error('Username is already taken', 'Registration failed.');
                break;

              default:
              this.toastr.error(element.description, 'Registration failed.');
              break;
            }
          });
        }
      },
      err => {
        console.log(err);
      }
    );
  }
}
