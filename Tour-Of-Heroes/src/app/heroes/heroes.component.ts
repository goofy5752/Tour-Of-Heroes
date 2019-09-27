import { Component, OnInit } from '@angular/core';

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
  // Some array of things.
  public heroData = [];
  // Pagination parameters.
  // tslint:disable-next-line: ban-types
  p: Number = 1;
  // tslint:disable-next-line: ban-types
  count: Number = 6;

  constructor(private heroService: HeroService) { }

  ngOnInit() {
    const edikvosi: any = this.getHeroes();
    this.heroData = edikvosi;
  }

  getHeroes(): void {
    this.heroService.getHeroes()
      .subscribe(heroes => this.heroes = heroes);
  }

  delete(hero: Hero): void {
    this.heroes = this.heroes.filter(h => h !== hero);
    this.heroService.deleteHero(hero).subscribe();
  }
}
