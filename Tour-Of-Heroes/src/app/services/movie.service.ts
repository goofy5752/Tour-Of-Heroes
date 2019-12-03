import { LikedMovie } from './../entities/likedMovie';
import { HeroService } from './hero.service';
import { Globals } from './../globals/globals';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import Movie from '../entities/movie';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class MovieService {
  private moviesUrl = 'https://localhost:44353/api/movies'; // URL to movies api
  private likedMoviesUrl = 'https://localhost:44353/api/likedmovies'; // URL to liked movies api
  private apikey = '?api_key=f9b276a8a665a41333c2def2f632a2e4';
  private urlMoviedb = 'https://api.themoviedb.org/3/';

  constructor(private http: HttpClient,
              private globals: Globals,
              private heroService: HeroService) { }

  getMovieByTitle(title: string) {
    return this.http.get<Movie[]>(this.urlMoviedb + 'search/movie' + this.apikey + `&query=${title.toLowerCase()}`);
  }

  getLikedMovies(): Observable<LikedMovie> {
    const url = `${this.likedMoviesUrl}/likes`;
    return this.http.get<LikedMovie>(url).pipe(
      tap(_ => {
        if (this.globals.showActivity) {
          this.heroService.log(`fetched profile da`);
        }
      })
    );
  }

  likeMovie(fd: FormData): Observable<LikedMovie> {
    return this.http.post<LikedMovie>(`${this.likedMoviesUrl}/like`, fd).pipe(
      tap((likedMovie: LikedMovie) => {
        if (this.globals.showActivity) {
          this.heroService.log(`liked movie`);
        }
      })
    );
  }

  dislikeMovie(movieId: number): Observable<LikedMovie> {
    const url = `${this.likedMoviesUrl}/dislike`;
    const httpParams = new HttpParams().set('movieId', movieId.toString());
    const options = { params: httpParams };

    return this.http.delete<LikedMovie>(url, options).pipe(
      tap(_ => {
        if (this.globals.showActivity) {
          this.heroService.log(`deleted movie with id=${movieId}`);
        }
      })
    );
  }

  deleteMovie(movie: Movie | string, password: string): Observable<Movie> {
    const title = typeof movie === 'string' ? movie : movie;
    const url = `${this.moviesUrl}/${title}`;
    const httpParams = new HttpParams().set('password', password);
    const options = { params: httpParams };

    return this.http.delete<Movie>(url, options).pipe(
      tap(_ => {
        if (this.globals.showActivity) {
          this.heroService.log(`deleted movie title=${title}`);
        }
      })
    );
  }
}
