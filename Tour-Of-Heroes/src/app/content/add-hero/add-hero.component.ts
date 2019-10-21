import { Component, OnInit, ViewChild } from '@angular/core';
import { HeroService } from '../../services/hero.service';
import { FormGroup, FormBuilder, Validators, FormArray } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Title } from '@angular/platform-browser';

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

  constructor(private heroService: HeroService,
              private formBuilder: FormBuilder,
              private toastr: ToastrService,
              private titleService: Title) { }

  ngOnInit() {
    this.registerForm = this.formBuilder.group({
      heroName: ['', Validators.required],
      heroRealName: ['', Validators.required],
      heroImage: ['', Validators.required],
      heroCoverImage: ['', Validators.required],
      heroBirthday: ['', Validators.required],
      heroGender: ['', Validators.required],
      heroDescription: ['', Validators.required],
      movieTitle: this.formBuilder.array([])
    }, {
    });

    this.setDocTitleAddHero();
  }

  stageHeroImageFile(): void {
    this.heroImageFile = this.heroImage.nativeElement.files[0];
  }

  stageHeroCoverImageFile(): void {
    this.heroCoverImageFile = this.heroCoverImage.nativeElement.files[0];
  }

  addMovie() {
    this.movieTitleFormGroup.push(this.formBuilder.control(''));
  }

  removeMovie(index) {
    this.movieTitleFormGroup.removeAt(index);
  }

  get movieTitleFormGroup() {
    return this.registerForm.get('movieTitle') as FormArray;
  }

  add(name: string, description: string, realName: string, birthday: Date, gender: string, movieTitle: string): void {
    name = name.trim();
    const formData = new FormData();
    description = description.trim();
    const datestr = (new Date(birthday)).toUTCString();
    const image = this.heroImageFile;
    const coverImage = this.heroCoverImageFile;
    formData.append('name', name);
    formData.append('description', description);
    formData.append('image', image, image.name);
    formData.append('coverImage', coverImage, coverImage.name);
    formData.append('realName', realName);
    formData.append('birthday', datestr);
    formData.append('gender', gender);
    formData.append('movieTitle', JSON.stringify(this.registerForm.get('movieTitle').value));
    if (!name || !description || !image || !coverImage) { return; }
    this.heroService.addHero(formData)
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

  setDocTitleAddHero() {
    this.titleService.setTitle('Add hero');
  }

  get f() { return this.registerForm.controls; }
}
