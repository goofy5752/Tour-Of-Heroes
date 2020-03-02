import { Globals } from 'src/app/globals/globals';
import { HeroService } from './hero.service';
import { Comments } from './../entities/comment';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CommentService {

  httpOptions = {
    headers: new HttpHeaders()
      .append('Content-Disposition', 'multipart/form-data')
  };

  private commentsUrl = 'https://localhost:44353/api/comments'; // URL to comments API

  constructor(private heroService: HeroService, private globals: Globals, private http: HttpClient) { }

  postComment(userId: string, id: number, comment: string, action: string): Observable<Comments> {
    return this.http.post<Comments>(`${this.commentsUrl}/create-comment`, { userId, id, comment, action }, this.httpOptions)
    .pipe(
      tap((newComment: Comments) => {
        if (this.globals.showActivity) {
          this.heroService.log(`added comment w/ id=${newComment.id}`);
        }
      })
    );
  }

  deleteComment(comment: Comments | number): Observable<Comments> {
    const id = typeof comment === 'number' ? comment : comment.id;
    const url = `${this.commentsUrl}/${id}`;

    return this.http.delete<Comments>(url, this.httpOptions).pipe(
      tap(_ => {
        if (this.globals.showActivity) {
          this.heroService.log(`deleted comment id=${id}`);
        }
      })
    );
  }
}
