import { EditHistory } from './../../editHistory';
import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location, JsonPipe } from '@angular/common';

import { Hero } from '../../hero';
import { HeroService } from '../../hero.service';
import { MovieService } from 'src/app/movie.service';
import Movie from '../../movie';

@Component({
  selector: 'app-hero-detail',
  templateUrl: './hero-detail.component.html',
  styleUrls: ['./hero-detail.component.css']
})

export class HeroDetailComponent implements OnInit {
  @Input() hero: Hero;
  @Input() movies: Movie[];
  allMovies: Movie[];
  index: number;
  searchResults: any;

  constructor(
    private route: ActivatedRoute,
    private heroService: HeroService,
    private movieService: MovieService,
    private location: Location
  ) { this.allMovies = {} as Movie[]; }

  ngOnInit(): void {
    this.route.params.subscribe(() => {
      this.getHero();
    });
  }

  getHero(): void {
    const id = +this.route.snapshot.paramMap.get('id');
    this.heroService.getHero(id)
      .subscribe(hero => { this.hero = hero; this.getMovies(); });
  }

  getMovies() {
    for (const title of this.hero.movies) {
      this.movieService.getMovieByTitle(`${title}`).subscribe(data => {
        this.searchResults = data;
        this.allMovies = this.searchResults.results;
      });
    }
  }

  goBack(): void {
    this.location.back();
  }

  save(): void {
    this.heroService.updateHero(this.hero)
      .subscribe(() => this.goBack());
  }

  delete(editHistory: EditHistory) {
    this.hero.editHistory = this.hero.editHistory.filter(h => h !== editHistory);
    this.heroService.deleteHistory(editHistory).subscribe();
  }
}
