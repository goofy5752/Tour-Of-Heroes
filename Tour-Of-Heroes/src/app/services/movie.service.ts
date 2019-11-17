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
  private apikey = '?api_key=f9b276a8a665a41333c2def2f632a2e4';
  private urlMoviedb = 'https://api.themoviedb.org/3/';

  constructor(private http: HttpClient,
              private globals: Globals,
              private heroService: HeroService) { }

  getMovieByTitle(title: string) {
    return this.http.get<Movie[]>(this.urlMoviedb + 'search/movie' + this.apikey + `&query=${title.toLowerCase()}`);
  }

  likeMovie(fd: FormData): Observable<Movie> {
    return this.http.post<Movie>(`${this.moviesUrl}/like`, fd).pipe(
      tap((likedMovie: Movie) => {
        if (this.globals.showActivity) {
          this.heroService.log(`liked movie w/ title=${likedMovie.title}`);
        }
      })
    );
  }

  /** Delete movie from the server */
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
