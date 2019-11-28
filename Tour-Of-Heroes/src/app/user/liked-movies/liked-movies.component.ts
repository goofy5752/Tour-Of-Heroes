import { ToastrService } from 'ngx-toastr';
import { MovieService } from 'src/app/services/movie.service';
import { Component, OnInit } from '@angular/core';
import { Globals } from 'src/app/globals/globals';
import { ActivatedRoute, Router } from '@angular/router';
import { HeroService } from 'src/app/services/hero.service';
import { OrderPipe } from 'ngx-order-pipe';
import { PageResult } from 'src/app/entities/pageResult';
import { tap } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { LikedMovie } from 'src/app/entities/likedMovie';

@Component({
  selector: 'app-liked-movies',
  templateUrl: './liked-movies.component.html',
  styleUrls: ['./liked-movies.component.css']
})
export class LikedMoviesComponent implements OnInit {
  placements: string[] = ['top', 'left', 'right', 'bottom'];
  popoverTitle = 'Enter your password to confirm!';
  confirmClicked = false;
  cancelClicked = false;
  baseUrl = 'https://localhost:44353/api/likedmovies';
  likedMovies: LikedMovie[];
  pageNumber;
  Count;
  movieFilter: any = {
    title: ''
  };

  constructor(public globals: Globals,
              private http: HttpClient,
              private route: ActivatedRoute,
              private router: Router,
              private heroService: HeroService,
              orderPipe: OrderPipe,
              private movieService: MovieService,
              private toastr: ToastrService) {
    this.http.get<PageResult<LikedMovie>>(this.baseUrl + '/likes?page=1').pipe(tap(_ => {
      if (this.globals.showActivity) {
        this.heroService.log(`fetched users from page ${this.pageNumber}`);
      }
    })).subscribe(result => {
      this.likedMovies = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
    }, error => console.error(error));
  }

  public onPageChange = (pageNumber) => {
    // tslint:disable-next-line: max-line-length
    this.http.get<PageResult<LikedMovie>>(this.baseUrl + '/all?page=' + pageNumber).pipe(tap(_ => {
      if (this.globals.showActivity) {
        this.heroService.log(`fetched posts from page ${pageNumber}`);
      }
    })).subscribe(result => {
      this.likedMovies = result.items;
      this.pageNumber = result.pageIndex;
      this.router.navigate([], {
        relativeTo: this.route,
        queryParams: {
          page: this.pageNumber
        },
        queryParamsHandling: 'merge',
        // preserve the existing query params in the route
        skipLocationChange: false
        // do not trigger navigation
      });
      this.Count = result.count;
    }, error => console.error(error));
  }

  ngOnInit() {
  }

  dislikeMovie(movieId: number) {
    this.movieService.dislikeMovie(movieId).subscribe(
      () => {
        this.likedMovies = this.likedMovies.filter(h => h.id !== movieId);
        this.toastr.success(`You have disliked movie with id ${movieId}`, 'Success !!');
      },
      error => {
        if (error.status === 400 || error.status === 500) {
          this.toastr.error('Unexpected error. Please try again later !', 'Oops !');
        } else {
          console.log(error);
        }
      }
    );
  }
}
