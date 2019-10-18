import { Title } from '@angular/platform-browser';
import { Globals } from '../../globals/globals';
import { tap } from 'rxjs/operators';
import { PageResult } from '../../entities/pageResult';
import { Component, OnInit, Inject } from '@angular/core';

import { Hero } from '../../entities/hero';
import { HeroService } from '../../services/hero.service';
import { HttpClient } from '@angular/common/http';

import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-heroes',
  templateUrl: './heroes.component.html',
  styleUrls: ['./heroes.component.css']
})
export class HeroesComponent implements OnInit {
  placements: string[] = ['top', 'left', 'right', 'bottom'];
  popoverTitle = 'Enter your password to confirm!';
  confirmClicked = false;
  cancelClicked = false;
  private http: HttpClient;
  baseUrl = 'https://localhost:44353';
  public Hero: Hero[];
  public pageNumber = 1;
  public Count: number;
  // tslint:disable-next-line: max-line-length
  constructor(private heroService: HeroService,
              http: HttpClient,
              private router: Router,
              private route: ActivatedRoute,
              private toastr: ToastrService,
              public globals: Globals,
              private titleService: Title) {
    this.http = http;

    // tslint:disable-next-line: max-line-length
    http.get<PageResult<Hero>>(this.baseUrl + '/api/heroes/all').pipe(tap(_ => {if (this.globals.showActivity) { this.heroService.log(`fetched heroes from page ${this.pageNumber}`); } } )).subscribe(result => {
      this.Hero = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
    }, error => console.error(error));
  }

  public onPageChange = (pageNumber) => {
    // tslint:disable-next-line: max-line-length
    this.http.get<PageResult<Hero>>(this.baseUrl + '/api/heroes/all?page=' + pageNumber).pipe(tap(_ =>  {if (this.globals.showActivity) {this.heroService.log(`fetched heroes from page ${pageNumber}`); } } )).subscribe(result => {
      this.Hero = result.items;
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
    // this.getHeroes(); ---- temporary disbled
  }

  //  getHeroes(): void {
  //    this.heroService.getHeroes()
  //      .subscribe(heroes => this.Hero = heroes);
  //  }

  delete(hero: Hero, password: string): void {
    this.heroService.deleteHero(hero, password).subscribe(
      () => {
        this.Hero = this.Hero.filter(h => h !== hero);
        this.toastr.success(`You have deleted superhero: ${hero.name}`, 'Success !');
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

  setDocTitle(title: string) {
    this.titleService.setTitle(title);
  }
}
