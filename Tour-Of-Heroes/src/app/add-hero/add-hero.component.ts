import { Component, OnInit, ViewChild } from '@angular/core';

import { Hero } from '../hero';
import { HeroService } from '../hero.service';

@Component({
  selector: 'app-add-hero',
  templateUrl: './add-hero.component.html',
  styleUrls: ['./add-hero.component.css']
})
export class AddHeroComponent implements OnInit {
  heroes: Hero[];
  @ViewChild('heroImage', { static: false }) heroImage;
  @ViewChild('heroCoverImage', { static: false }) heroCoverImage;
  heroImageFile: File;
  heroCoverImageFile: File;

  constructor(private heroService: HeroService) { }

  ngOnInit() {
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
}
