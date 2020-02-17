import { Globals } from './../../globals/globals';
import { Comments } from './../../entities/comment';
import { CommentService } from './../../services/comment.service';
import { EditHistory } from '../../entities/editHistory';
import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

import { Hero } from '../../entities/hero';
import { HeroService } from '../../services/hero.service';
import { MovieService } from 'src/app/services/movie.service';
import Movie from '../../entities/movie';
import { ToastrService } from 'ngx-toastr';
import { Title } from '@angular/platform-browser';
import { Profile } from 'selenium-webdriver/firefox';
import * as signalR from '@aspnet/signalr';

@Component({
  selector: 'app-hero-detail',
  templateUrl: './hero-detail.component.html',
  styleUrls: ['./hero-detail.component.css']
})

export class HeroDetailComponent implements OnInit {
  placements: string[] = ['top', 'left', 'right', 'bottom'];
  popoverTitle = 'Enter your password to confirm!';
  confirmClicked = false;
  cancelClicked = false;
  orderedComments: Comments[];
  originalComments;
  mySubscription: any;
  userId = '';

  @Input() hero: Hero;
  @Input() profile: Profile;
  allMovies: Movie[];
  index: number;
  searchResults: any;
  constructor(
    private route: ActivatedRoute,
    private heroService: HeroService,
    private movieService: MovieService,
    private location: Location,
    private toastr: ToastrService,
    private titleService: Title,
    private commentService: CommentService,
    public globals: Globals
  ) {
    this.allMovies = [];
  }

  ngOnInit(): void {
    this.route.params.subscribe(() => {
      this.getHero();
    });

    const connection = new signalR.HubConnectionBuilder()
      .configureLogging(signalR.LogLevel.Information)
      .withUrl('https://localhost:44353/api/comments')
      .build();

    connection.start().then(() => {
      console.log('Connected!');
    }).catch(err => {
      return console.error(err.toString());
    });

    connection.on('BroadcastComment', (comment: Comments) => {
      this.orderedComments.push(comment);
      this.sortBy('publishedOn');
    });

    connection.on('DeleteComment', (commentId: number) => {
      for (const i of this.orderedComments) {
        if (i.id === commentId) {
          if (i.isDeleted === true) {
            this.toastr.warning(`Comment is already deleted`, 'Oops !');
            return;
          }
          this.toastr.success(`You have deleted your comment`, 'Success !');
          i.isDeleted = true;
          return;
        }
      }
    });
  }

  getHero(): void {
    const id = +this.route.snapshot.paramMap.get('id');
    this.heroService.getHero(id)
      .subscribe(hero => {
        const token = JSON.stringify(localStorage.getItem('token'));
        const jwtData = token.split('.')[1];
        const decodedJwtJsonData = window.atob(jwtData);
        const decodedJwtData = JSON.parse(decodedJwtJsonData);
        this.userId = decodedJwtData.UserID;
        this.hero = hero;
        this.allMovies = [];
        this.originalComments = hero.comments;
        this.getMovies();
        this.sortBy('publishedOn');
        this.titleService.setTitle(`${this.hero.name} Details`);
      });
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
    this.heroService.updateHero(this.hero).subscribe(
      () => {
        this.toastr.success(`You have entered new value: ${this.hero.name}`, 'Successfully renamed !');
      },
      error => {
        if (error.status === 400) {
          this.toastr.error(`Please enter a name that is different from the previous one.`, 'Renamed failed.');
        } else if (error.status === 500) {
          this.toastr.error(`The character you are trying to rename is probably removed from the server.`, 'Rename failed.');
        } else {
          console.log(error);
        }
      }
    );
  }

  deleteHistory(editHistory: EditHistory) {
    this.hero.editHistory = this.hero.editHistory.filter(h => h !== editHistory);
    this.heroService.deleteHistory(editHistory).subscribe();
  }

  deleteMovie(movie: Movie, password: string) {
    // tslint:disable-next-line: max-line-length
    this.movieService.deleteMovie(movie.title, password).subscribe(
      () => {
        this.allMovies = this.allMovies.filter(h => h !== movie);
        this.toastr.success(`You have deleted movie: ${movie.title}`, 'Success !');
      },
      error => {
        if (error.status === 400 || error.status === 500) {
          this.toastr.error(`You have entered wrong password.`, 'Authentication failed.');
        } else {
          console.log(error);
        }
      }
    );
  }

  deleteComment(comment: Comments) {
    this.commentService.deleteComment(comment).subscribe(
      () => { },
      error => {
        console.log(error);
      });
  }

  postComment(comment: string) {
    if (comment === '') {
      this.toastr.error(`Write something.`, 'Spam ?');
      return;
    }
    console.log(comment);
    this.commentService.postComment(this.userId, this.hero.id, comment, 'Hero').subscribe(
      () => {
        this.toastr.success(`You have added a comment`, 'Success !');
      },
      error => {
        if (error.status === 400 || error.status === 500) {
          this.toastr.error(`Your comment is not posted try again later.`, 'Something wrong');
        } else {
          console.log(error);
        }
      }
    );
  }

  likeMovie(title: string, posterPath: string, voteAverage: number, releaseDate: string, voteCount: number): void {
    const formData = new FormData();
    formData.append('title', title);
    formData.append('posterPath', posterPath);
    formData.append('voteAverage', voteAverage.toString());
    formData.append('releaseDate', releaseDate);
    formData.append('voteCount', voteCount.toString());
    formData.append('userId', this.userId);
    this.movieService.likeMovie(formData)
      .subscribe(() => {
        console.log('dasdasdasdasdsd');
        this.toastr.success(`You liked a movie with title: ${title}`, 'Success !');
      },
      err => {
        if (err.status === 400) {
        this.toastr.error(`You already liked a movie with title: ${title}`, 'Oops !');
        }
      });
  }

  sortBy(field: string) {
    this.originalComments.sort((a: any, b: any) => {
      if (a[field] > b[field]) {
        return -1;
      } else if (a[field] < b[field]) {
        return 1;
      } else {
        return 0;
      }
    });
    this.orderedComments = this.originalComments;
  }
}
