import { Title } from '@angular/platform-browser';
import { Globals } from 'src/app/globals/globals';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Blog } from 'src/app/entities/blog';
import { PageResult } from 'src/app/entities/pageResult';
import { tap } from 'rxjs/operators';
import { HeroService } from 'src/app/services/hero.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-blog',
  templateUrl: './blog.component.html',
  styleUrls: ['./blog.component.css']
})
export class BlogComponent implements OnInit {

  public Blog: Blog[];
  public pageNumber = 1;
  public Count: number;
  baseUrl = 'https://localhost:44353/api/blog';

  constructor(private heroService: HeroService,
              private http: HttpClient,
              private route: ActivatedRoute,
              private router: Router,
              public globals: Globals,
              private titleService: Title) {
    // tslint:disable-next-line: max-line-length
    http.get<PageResult<Blog>>(this.baseUrl + '/all').pipe(tap(_ => { if (this.globals.showActivity) { this.heroService.log(`fetched posts from page ${this.pageNumber}`); } })).subscribe(result => {
      this.Blog = result.items;
      this.pageNumber = result.pageIndex;
      this.Count = result.count;
    }, error => console.error(error));
  }

  public onPageChange = (pageNumber) => {
    // tslint:disable-next-line: max-line-length
    this.http.get<PageResult<Blog>>(this.baseUrl + '/all?page=' + pageNumber).pipe(tap(_ =>  {if (this.globals.showActivity) {this.heroService.log(`fetched posts from page ${pageNumber}`); } } )).subscribe(result => {
      this.Blog = result.items;
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
    this.setDocTitleBlog();
    // this.getHeroes(); ---- temporary disbled
  }

  setDocTitleBlog() {
    this.titleService.setTitle('Blog');
  }

}
