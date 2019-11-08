import { Globals } from '../../globals/globals';
import { Profile } from '../../entities/profile';
import { Title } from '@angular/platform-browser';
import { Component, OnInit } from '@angular/core';
import { HeroService } from 'src/app/services/hero.service';
import { PageResult } from 'src/app/entities/pageResult';
import { tap } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';
import { OrderPipe } from 'ngx-order-pipe';

@Component({
  selector: 'app-user-controller',
  templateUrl: './user-controller.component.html',
  styleUrls: ['./user-controller.component.css']
})
export class UserControllerComponent implements OnInit {
  public Profile: Profile[];
  sortedProfile: any[];
  public pageNumber = 1;
  public Count: number;
  order = 'userName';
  reverse: boolean;
  baseUrl = 'https://localhost:44353/api/users';

  constructor(private titleService: Title,
              public globals: Globals,
              private http: HttpClient,
              private route: ActivatedRoute,
              private router: Router,
              private heroService: HeroService,
              orderPipe: OrderPipe) {
    this.http.get<PageResult<Profile>>(this.baseUrl + '/all?page=1').pipe(tap(_ => {
      if (this.globals.showActivity) {
        this.heroService.log(`fetched users from page ${this.pageNumber}`);
      }
    })).subscribe(result => {
      this.Profile = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
    }, error => console.error(error));
    this.sortedProfile = orderPipe.transform(this.Profile, 'userName');
  }

  public onPageChange = (pageNumber) => {
    // tslint:disable-next-line: max-line-length
    this.http.get<PageResult<Profile>>(this.baseUrl + '/all?page=' + pageNumber).pipe(tap(_ => {
      if (this.globals.showActivity) {
        this.heroService.log(`fetched posts from page ${pageNumber}`);
      }
    })).subscribe(result => {
      this.Profile = result.items;
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
    this.titleService.setTitle('Users');
  }

  setOrder(value: string) {
    if (this.order === value) {
      this.reverse = !this.reverse;
    }

    this.order = value;
  }
}
