import { ToastrService } from 'ngx-toastr';
import { UserService } from '../../services/user.service';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { Globals } from 'src/app/globals/globals';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  formModel = {
    UserName: '',
    Password: ''
  };

  constructor(private service: UserService, private router: Router, private toastr: ToastrService, public globals: Globals) { }

  ngOnInit() {
    if (localStorage.getItem('token') != null) {
      this.router.navigateByUrl('/heroes?page=1');
    }
  }

  onSubmit(form: NgForm) {
    this.service.login(form.value).subscribe(
      (res: any) => {
        localStorage.setItem('token', res.token);
        this.router.navigateByUrl('/heroes?page=1');
        this.toastr.success('You have logged in.', 'Authentication success.');
      },
      err => {
        if (err.status === 400) {
          this.toastr.error('Incorrect username or password.', 'Authentication failed.');
        } else if (err.status === 403) {
          this.toastr.error('You have entered 3 invalid attempts! Please wait 5 min and try again.', 'Ooops!');
        } else {
          console.log(err);
        }
      }
    );
  }
}
