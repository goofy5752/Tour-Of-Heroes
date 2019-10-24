import { ToastrService } from 'ngx-toastr';
import { ProfileService } from './../../services/profile.service';
import { Profile } from './../../entities/profile';
import { Title } from '@angular/platform-browser';
import { Component, OnInit, Input, ViewChild } from '@angular/core';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})

export class ProfileComponent implements OnInit {
  @Input() profile: Profile;
  @ViewChild('profileImage', { static: false }) profileImage: any;
  userId = '';
  profileImageFile: File;

  constructor(private titleService: Title,
              private profileService: ProfileService,
              private toastr: ToastrService) { }

  ngOnInit() {
    const token = JSON.stringify(localStorage.getItem('token'));
    const jwtData = token.split('.')[1];
    const decodedJwtJsonData = window.atob(jwtData);
    const decodedJwtData = JSON.parse(decodedJwtJsonData);
    this.userId = decodedJwtData.UserID;

    this.profileService.getProfile(this.userId).subscribe(
      data => this.profile = data
    );
    this.titleService.setTitle('Profile');
  }

  updateEmail(email: string) {
    this.profileService.updateEmail(this.userId, email).subscribe(
      () => {
        this.toastr.success(`You have entered new value: ${email}`, 'Successfully renamed !');
      },
      error => {
        if (error.status === 400) {
          this.toastr.error(`Please enter a email that is different from the previous one.`, 'Renamed failed.');
        } else {
          console.log(error);
        }
      }
    );
  }

  stageProfileImageFile(): void {
    this.profileImageFile = this.profileImage.nativeElement.files[0];
  }

  updateProfileImage() {
    this.profileService.updateImage(this.userId, this.profileImageFile).subscribe(
      () => {
        this.toastr.success(`Your new profile picture is updated.`, 'Successfully uploaded !');
      },
      error => {
        if (error.status === 400) {
          this.toastr.error(`Choose a image to upload.`, 'Upload failed.');
        } else {
          console.log(error);
        }
      }
    );
  }
}
