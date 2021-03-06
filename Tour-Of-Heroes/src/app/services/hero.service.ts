import { EditHistory } from '../entities/editHistory';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { Hero } from '../entities/hero';
import { MessageService } from './message.service';
import { Globals } from '../globals/globals';


@Injectable({ providedIn: 'root' })
export class HeroService {
  private heroesUrl = 'https://localhost:44353/api/heroes';  // URL to heroes api
  private historyUrl = 'https://localhost:44353/api/history'; // URL to history api

  httpOptions = {
    headers: new HttpHeaders()
      .append('Content-Disposition', 'multipart/form-data')
  };

  constructor(
    private http: HttpClient,
    private messageService: MessageService,
    public globals: Globals) { }

  /** GET heroes from the server */
  // getHeroes(): Observable<Hero[]> {
  //   return this.http.get<Hero[]>(this.heroesUrl)
  //     .pipe(
  //       tap(_ => { if (this.globals.showActivity) { this.log('fetched heroes'); } }),
  //       catchError(this.handleError<Hero[]>('getHeroes', []))
  //     );
  // }

  /** GET hero by id. Return `undefined` when id not found */
  getHeroNo404<Data>(id: number): Observable<Hero> {
    const url = `${this.heroesUrl}/?id=${id}`;
    return this.http.get<Hero[]>(url)
      .pipe(
        map(heroes => heroes[0]), // returns a {0|1} element array
        tap(h => {
          const outcome = h ? `fetched` : `did not find`;
          this.log(`${outcome} hero id=${id}`);
        }),
        catchError(this.handleError<Hero>(`getHero id=${id}`))
      );
  }

  /** GET hero by id. Will 404 if id not found */
  getHero(id: number): Observable<Hero> {
    const url = `${this.heroesUrl}/${id}`;
    return this.http.get<Hero>(url).pipe(
      tap(_ => { if (this.globals.showActivity) { this.log(`fetched hero ${id}`); } }),
      catchError(this.handleError<Hero>(`getHero id=${id}`))
    );
  }

  /* GET heroes whose name contains search term */
  searchHeroes(term: string): Observable<Hero[]> {
    if (!term.trim()) {
      // if not search term, return empty hero array.
      return of([]);
    }
    return this.http.get<Hero[]>(`${this.heroesUrl}/get-heroes?name=${term}`).pipe(
      tap(_ => { if (this.globals.showActivity) { this.log(`found heroes matching "${term}"`); } }),
      catchError(this.handleError<Hero[]>('searchHeroes', []))
    );
  }

  //////// Save methods //////////

  /** POST: add a new hero to the server */
  addHero(fd: FormData): Observable<Hero> {
    return this.http.post<Hero>(`${this.heroesUrl}/create-hero`, fd, this.httpOptions).pipe(
      tap((newHero: Hero) => { if (this.globals.showActivity) { this.log(`added hero w/ id=${newHero.id}`); } }),
      catchError(this.handleError<Hero>('addHero'))
    );
  }

  /** DELETE: delete the hero from the server */
  deleteHero(hero: Hero | number, password): Observable<Hero> {
    const id = typeof hero === 'number' ? hero : hero.id;
    const url = `${this.heroesUrl}/${id}`;
    const httpParams = new HttpParams().set('password', password);
    const options = { params: httpParams };

    return this.http.delete<Hero>(url, options).pipe(
      tap(_ => { if (this.globals.showActivity) { this.log(`deleted hero id=${id}`); } }));
  }

  /** Delete edit history from the server */
  deleteHistory(editHistory: EditHistory | number): Observable<EditHistory> {
    const id = typeof editHistory === 'number' ? editHistory : editHistory.id;
    const url = `${this.historyUrl}/${id}`;

    return this.http.delete<EditHistory>(url, this.httpOptions).pipe(
      tap(_ => { if (this.globals.showActivity) { this.log(`deleted history id=${id}`); } }),
      catchError(this.handleError<EditHistory>('deleteHistory'))
    );
  }

  //  PUT: update the hero on the server
  updateHero(hero: Hero): Observable<any> {
    const url = `${this.heroesUrl}/${hero.id}`;
    return this.http.put(url, { name: hero.name }, this.httpOptions).pipe(
      tap(_ => { if (this.globals.showActivity) { this.log(`updated hero id=${hero.id}`); } })
    );
  }

  /**
   * Handle Http operation that failed.
   * Let the app continue.
   * @param operation - name of the operation that failed
   * @param result - optional value to return as the observable result
   */
  public handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // TODO: better job of transforming error for user consumption
      this.log(`${operation} failed: ${error.message}`);

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }

  /** Log a HeroService message with the MessageService */
  log(message: string) {
    this.messageService.add(`HeroService: ${message}`);
  }
}
