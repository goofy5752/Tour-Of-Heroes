import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class MovieService {
  private apikey = 'f9b276a8a665a41333c2def2f632a2e4';
  private urlMoviedb = 'https://api.themoviedb.org/3';

  constructor() { }
}
