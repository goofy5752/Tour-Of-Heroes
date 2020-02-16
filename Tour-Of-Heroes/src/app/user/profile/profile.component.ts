import { LocationService } from './../../services/location.service';
import { ToastrService } from 'ngx-toastr';
import { ProfileService } from './../../services/profile.service';
import { Profile } from './../../entities/profile';
import { Title } from '@angular/platform-browser';
import { Component, OnInit, Input, ViewChild } from '@angular/core';
import * as signalR from '@aspnet/signalr';

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
  x = document.getElementById('demo');

  constructor(private titleService: Title,
              private profileService: ProfileService,
              private toastr: ToastrService,
              private locationService: LocationService) { }

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

    const connection = new signalR.HubConnectionBuilder()
      .configureLogging(signalR.LogLevel.Information)
      .withUrl('https://localhost:44353/api/profile')
      .build();

    connection.start().then(() => {
      console.log('Connected!');
    }).catch(err => {
      return console.error(err.toString());
    });

    connection.on('UpdateProfileImage', (profileImage: string) => {
      this.profile.profileImage = profileImage;
    });
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

  getPosition() {
    this.locationService.getPosition().then(pos => {
         console.log(`Positon: ${pos.lat} ${pos.lng}`);
         const latlon = pos.lat + ',' + pos.lng;
         const imgUrl = 'https://maps.googleapis.com/maps/api/staticmap?center' + latlon +
          '&zoom=14&size=400x300&sensor=false&key=AIzaSyAas9bVlq4nMTk4iB88cngmN6tEOTEFUjw';
         console.log(imgUrl);
         document.getElementById('demo').innerHTML = '<img src=\'' + imgUrl + '\'>';
      });
  }
}
