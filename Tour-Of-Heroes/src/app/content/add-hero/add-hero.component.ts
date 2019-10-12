import { Component, OnInit, ViewChild } from '@angular/core';

import { Hero } from '../../hero';
import { HeroService } from '../../hero.service';
import { FormGroup, FormBuilder, Validators, FormArray } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add-hero',
  templateUrl: './add-hero.component.html',
  styleUrls: ['./add-hero.component.css']
})
export class AddHeroComponent implements OnInit {
  registerForm: FormGroup;
  submitted = false;
  @ViewChild('heroImage', { static: false }) heroImage;
  @ViewChild('heroCoverImage', { static: false }) heroCoverImage;
  heroImageFile: File;
  heroCoverImageFile: File;

  constructor(private heroService: HeroService, private formBuilder: FormBuilder, private toastr: ToastrService) {
  }

  ngOnInit() {
    this.registerForm = this.formBuilder.group({
      heroName: ['', Validators.required],
      heroRealName: ['', Validators.required],
      heroImage: ['', Validators.required],
      heroCoverImage: ['', Validators.required],
      heroBirthday: ['', Validators.required],
      heroGender: ['', Validators.required],
      heroDescription: ['', Validators.required],
      moviesArray: this.formBuilder.array([])
    }, {
    });
  }

  stageHeroImageFile(): void {
    this.heroImageFile = this.heroImage.nativeElement.files[0];
  }

  stageHeroCoverImageFile(): void {
    this.heroCoverImageFile = this.heroCoverImage.nativeElement.files[0];
  }

  get moviesArray() {
    return this.registerForm.get('moviesArray') as FormArray;
  }

  addMovie() {
    this.moviesArray.push(this.formBuilder.control(''));
  }

  add(name: string, description: string, realName: string, birthday: Date, gender: string, movieTitle: string[]): void {
    name = name.trim();
    console.log(JSON.stringify(this.registerForm.value));
    description = description.trim();
    const image = this.heroImageFile;
    const coverImage = this.heroCoverImageFile;
    if (!name || !description || !image || !coverImage) { return; }
    this.heroService.addHero({ name, description, image, coverImage, realName, birthday, gender, movieTitle } as Hero)
      .subscribe(hero => {
        this.toastr.success(`You have create a new character: ${name}`, 'Success !');
      });
  }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.registerForm.invalid) {
      return;
    }
  }

  onReset() {
    this.submitted = false;
    this.registerForm.reset();
  }

  get f() { return this.registerForm.controls; }
}
