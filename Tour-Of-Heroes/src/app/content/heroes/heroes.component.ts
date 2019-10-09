import { PageResult } from './../../pageResult';
import { Component, OnInit, Inject } from '@angular/core';

import { Hero } from '../../hero';
import { HeroService } from '../../hero.service';
import { HttpClient } from '@angular/common/http';

import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-heroes',
  templateUrl: './heroes.component.html',
  styleUrls: ['./heroes.component.css']
})
export class HeroesComponent implements OnInit {
  private http: HttpClient;
  baseUrl = 'https://localhost:44353';
  public Hero: Hero[];
  public pageNumber = 1;
  public Count: number;
  // tslint:disable-next-line: max-line-length
  constructor(private heroService: HeroService, http: HttpClient, private router: Router, private route: ActivatedRoute, private toastr: ToastrService) {
    this.http = http;

    http.get<PageResult<Hero>>(this.baseUrl + '/api/heroes/all').subscribe(result => {
      this.Hero = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
    }, error => console.error(error));
  }

  public onPageChange = (pageNumber) => {
    this.http.get<PageResult<Hero>>(this.baseUrl + '/api/heroes/all?page=' + pageNumber).subscribe(result => {
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

  delete(hero: Hero): void {
    this.Hero = this.Hero.filter(h => h !== hero);
    this.heroService.deleteHero(hero).subscribe();
    this.toastr.success(`You have deleted superhero: ${hero.name}`, 'Success !');
  }
}
