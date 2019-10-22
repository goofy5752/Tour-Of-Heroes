import { ProfileService } from './../../services/profile.service';
import { Profile } from './../../entities/profile';
import { Title } from '@angular/platform-browser';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  @Input() profile: Profile;

  constructor(private titleService: Title, private profileService: ProfileService) { }

  ngOnInit() {
    const token = JSON.stringify(localStorage.getItem('token'));
    const jwtData = token.split('.')[1];
    const decodedJwtJsonData = window.atob(jwtData);
    const decodedJwtData = JSON.parse(decodedJwtJsonData);
    const userId = decodedJwtData.UserID;

    this.profileService.getProfile(userId).subscribe(
      data => this.profile = data
    );
    this.titleService.setTitle('Profile');
  }
}
