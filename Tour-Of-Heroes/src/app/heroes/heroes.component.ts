import { Component, OnInit, ViewChild } from '@angular/core';

import { Hero } from '../hero';
import { HeroService } from '../hero.service';

@Component({
  selector: 'app-heroes',
  templateUrl: './heroes.component.html',
  styleUrls: ['./heroes.component.css']
})
export class HeroesComponent implements OnInit {
  heroes: Hero[];
  http: any;
  @ViewChild('heroImage', { static: false }) heroImage;
  @ViewChild('heroCoverImage', { static: false }) heroCoverImage;
  heroImageFile: File;
  heroCoverImageFile: File;

  constructor(private heroService: HeroService) { }

  ngOnInit() {
    this.getHeroes();
  }

  getHeroes(): void {
    this.heroService.getHeroes()
      .subscribe(heroes => this.heroes = heroes);
  }

  stageHeroImageFile(): void {
    this.heroImageFile = this.heroImage.nativeElement.files[0];
    console.log(this.heroImageFile);
  }

  stageHeroCoverImageFile(): void {
    this.heroCoverImageFile = this.heroCoverImage.nativeElement.files[0];
    console.log(this.heroCoverImageFile);
  }

  add(name: string, description: string): void {
    name = name.trim();
    description = description.trim();
    const image = this.heroImageFile;
    const coverImage = this.heroCoverImageFile;
    if (!name || !description || !image || !coverImage) { return; }
    this.heroService.addHero({ name, description, image, coverImage } as Hero)
      .subscribe(hero => {
        this.heroes.push(hero);
      });
  }

  delete(hero: Hero): void {
    this.heroes = this.heroes.filter(h => h !== hero);
    this.heroService.deleteHero(hero).subscribe();
  }
}
