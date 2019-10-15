import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import Movie from '../entities/movie';

const apikey = '?api_key=f9b276a8a665a41333c2def2f632a2e4';
const urlMoviedb = 'https://api.themoviedb.org/3/';

@Injectable({
  providedIn: 'root'
})
export class MovieService {

  constructor(private http: HttpClient) { }

  getMovieByTitle(title: string) {
    return this.http.get<Movie[]>(urlMoviedb + 'search/movie' + apikey + `&query=${title}` + `&page=${1}`);
  }
}
