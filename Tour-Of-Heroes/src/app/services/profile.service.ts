import { HeroService } from './hero.service';
import { Profile } from './../entities/profile';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Globals } from '../globals/globals';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {

  readonly BaseURI = 'https://localhost:44353/api/profile'; // profile API URI

  constructor(private http: HttpClient,
              private heroService: HeroService,
              public globals: Globals) { }

  getProfile(id: string): Observable<Profile> {
    const url = `${this.BaseURI}/${id}`;
    return this.http.get<Profile>(url).pipe(
      tap(_ => { if (this.globals.showActivity) { this.heroService.log(`fetched profile ${id}`); } }));
  }
}
