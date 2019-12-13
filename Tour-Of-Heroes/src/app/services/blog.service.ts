import { Blog } from './../entities/blog';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { HeroService } from './hero.service';
import { Globals } from '../globals/globals';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class BlogService {
  private blogUrl = 'https://localhost:44353/api/blog'; // URL to comments API

  httpOptions = {
    headers: new HttpHeaders()
      .append('Content-Disposition', 'multipart/form-data')
  };

  constructor(private heroService: HeroService, public globals: Globals, private http: HttpClient) {
  }

  getPostDetail(id: number): Observable<Blog> {
    const url = `${this.blogUrl}/${id}`;
    return this.http.get<Blog>(url).pipe(
      tap(_ => {
        if (this.globals.showActivity) {
          this.heroService.log(`fetched blog ${id}`);
        }
      }));
  }

  createPost(fd: FormData): Observable<Blog> {
    return this.http.post<Blog>(`${this.blogUrl}/create-post`, fd, this.httpOptions).pipe(
      tap((newPost: Blog) => {
        if (this.globals.showActivity) {
          this.heroService.log(`added post w/ id=${newPost.id}`);
        }
      }));
  }

  deletePost(blog: Blog | number, password): Observable<Blog> {
    const id = typeof blog === 'number' ? blog : blog.id;
    const url = `${this.blogUrl}/${id}`;
    const httpParams = new HttpParams().set('password', password);
    const options = { params: httpParams };

    return this.http.delete<Blog>(url, options).pipe(
      tap(_ => { if (this.globals.showActivity) { this.heroService.log(`deleted hero id=${id}`); } }));
  }

  likePost(postId: number): Observable<Blog> {
    const httpParams = new HttpParams().set('postId', postId.toString());
    const options = { params: httpParams };
    console.log(postId);
    return this.http.post<Blog>(`${this.blogUrl}/like`, options).pipe(
      tap(() => {
        if (this.globals.showActivity) {
          this.heroService.log(`liked movie`);
        }
      })
    );
  }
}
