import { Component, OnInit, ViewChild } from '@angular/core';

import { Hero } from '../hero';
import { HeroService } from '../hero.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-add-hero',
  templateUrl: './add-hero.component.html',
  styleUrls: ['./add-hero.component.css']
})
export class AddHeroComponent implements OnInit {
  heroes: Hero[];
  registerForm: FormGroup;
  submitted = false;
  @ViewChild('heroImage', { static: false }) heroImage;
  @ViewChild('heroCoverImage', { static: false }) heroCoverImage;
  heroImageFile: File;
  heroCoverImageFile: File;

  constructor(private heroService: HeroService, private formBuilder: FormBuilder) {
  }

  ngOnInit() {
    this.registerForm = this.formBuilder.group({
      heroName: ['', Validators.required],
      heroRealName: ['', Validators.required],
      heroImage: ['', Validators.required],
      heroCoverImage: ['', Validators.required],
      heroBirthday: ['', Validators.required],
      heroGender: ['', Validators.required],
      heroDescription: ['', Validators.required]
    }, {
    });
  }

  stageHeroImageFile(): void {
    this.heroImageFile = this.heroImage.nativeElement.files[0];
    console.log(this.heroImageFile);
  }

  stageHeroCoverImageFile(): void {
    this.heroCoverImageFile = this.heroCoverImage.nativeElement.files[0];
    console.log(this.heroCoverImageFile);
  }

  add(name: string, description: string, realName: string, birthday: Date, gender: string): void {
    name = name.trim();
    description = description.trim();
    const image = this.heroImageFile;
    const coverImage = this.heroCoverImageFile;
    if (!name || !description || !image || !coverImage) { return; }
    this.heroService.addHero({ name, description, image, coverImage, realName, birthday, gender } as Hero)
      .subscribe(hero => {
        this.heroes.push(hero);
      });
  }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.registerForm.invalid) {
      return;
    }

    // display form values on success
    alert('SUCCESS!! :-)\n\n' + JSON.stringify(this.registerForm.value, null, 4));
  }

  onReset() {
    this.submitted = false;
    this.registerForm.reset();
  }

  get f() { return this.registerForm.controls; }
}
