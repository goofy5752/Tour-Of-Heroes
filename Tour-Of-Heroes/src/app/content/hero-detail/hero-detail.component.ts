import { EditHistory } from '../../entities/editHistory';
import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

import { Hero } from '../../entities/hero';
import { HeroService } from '../../services/hero.service';
import { MovieService } from 'src/app/services/movie.service';
import Movie from '../../entities/movie';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-hero-detail',
  templateUrl: './hero-detail.component.html',
  styleUrls: ['./hero-detail.component.css']
})

export class HeroDetailComponent implements OnInit {
  placements: string[] = ['top', 'left', 'right', 'bottom'];
  popoverTitle = 'Enter your password to confirm!';
  popoverMessage = 'Are you really <b>sure</b> you want to do this?';
  confirmText = 'Yes <i class="glyphicon glyphicon-ok"></i>';
  cancelText = 'No <i class="glyphicon glyphicon-remove"></i>';
  confirmClicked = false;
  cancelClicked = false;

  @Input() hero: Hero;
  allMovies: Movie[];
  index: number;
  searchResults: any;

  constructor(
    private route: ActivatedRoute,
    private heroService: HeroService,
    private movieService: MovieService,
    private location: Location,
    private toastr: ToastrService,
  ) { this.allMovies = []; }

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
    for (const movie of this.hero.movies) {
      this.movieService.getMovieByTitle(`${movie.title.toLowerCase()}`).subscribe(data => {
        this.searchResults = data;
        for (const currentMovie of this.searchResults.results) {
          if (currentMovie.title.toLowerCase() === movie.title.toLowerCase()) {
            currentMovie.vote_average = currentMovie.vote_average / 2;
            this.allMovies.push(currentMovie);
          }
        }
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

  deleteHistory(editHistory: EditHistory) {
    this.hero.editHistory = this.hero.editHistory.filter(h => h !== editHistory);
    this.heroService.deleteHistory(editHistory).subscribe();
  }

  deleteMovie(movie: Movie, password: string) {
    // tslint:disable-next-line: max-line-length
    this.movieService.deleteMovie(movie.title, password).subscribe(() => this.allMovies = this.allMovies.filter(h => h !== movie), error => { if (error.status != 401) { console.log(error); this.toastr.success(`You have deleted movie: ${movie.title}`, 'Success !'); } else { this.toastr.success(`You have deleted movie: ${movie.title}`, 'Success !'); } });
  }
}
