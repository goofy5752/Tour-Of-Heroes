import { User } from './../entities/user';
import { Globals } from 'src/app/globals/globals';
import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HeroService } from './hero.service';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private fb: FormBuilder, private http: HttpClient, public globals: Globals, private heroService: HeroService) { }
  readonly BaseURI = 'https://localhost:44353/api';
  readonly BaseUserURI = 'https://localhost:44353/api/users';

  formModel = this.fb.group({
    UserName: ['', Validators.required],
    Email: ['', Validators.email],
    FullName: [''],
    EditorRoleCode: [''],
    Passwords: this.fb.group({
      Password: ['', [Validators.required, Validators.minLength(4)]],
      ConfirmPassword: ['', Validators.required]
    }, { validator: this.comparePasswords })
  });

  getUser(id: string): Observable<User> {
    const url = `${this.BaseUserURI}/${id}`;
    return this.http.get<User>(url).pipe(
      tap(_ => {
        if (this.globals.showActivity) {
          this.heroService.log(`fetched hero ${id}`);
        }
      }));
  }

  comparePasswords(fb: FormGroup) {
    const confirmPswrdCtrl = fb.get('ConfirmPassword');
    // passwordMismatch
    // confirmPswrdCtrl.errors={passwordMismatch:true}
    if (confirmPswrdCtrl.errors == null || 'passwordMismatch' in confirmPswrdCtrl.errors) {
      // tslint:disable-next-line: triple-equals
      if (fb.get('Password').value != confirmPswrdCtrl.value) {
        confirmPswrdCtrl.setErrors({ passwordMismatch: true });
      } else {
        confirmPswrdCtrl.setErrors(null);
      }
    }
  }

  register() {
    const body = {
      UserName: this.formModel.value.UserName,
      Email: this.formModel.value.Email,
      FullName: this.formModel.value.FullName,
      EditorRoleCode: this.formModel.value.EditorRoleCode,
      Password: this.formModel.value.Passwords.Password
    };
    return this.http.post(this.BaseURI + '/ApplicationUser/Register', body);
  }

  login(formData) {
    return this.http.post(this.BaseURI + '/ApplicationUser/Login', formData);
  }

  roleMatch(allowedRoles): boolean {
    let isMatch = false;
    const payLoad = JSON.parse(window.atob(localStorage.getItem('token').split('.')[1]));
    const userRole = payLoad.role;
    allowedRoles.forEach(element => {
      if (userRole === element) {
        isMatch = true;
        return false;
      }
    });
    return isMatch;
  }
}
