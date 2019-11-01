import { Title } from '@angular/platform-browser';
import { Comments } from './../../entities/comment';
import { CommentService } from './../../services/comment.service';
import { ToastrService } from 'ngx-toastr';
import { BlogService } from './../../services/blog.service';
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit, Input } from '@angular/core';
import { Blog } from 'src/app/entities/blog';
import * as signalR from '@aspnet/signalr';

@Component({
  selector: 'app-blog-detail',
  templateUrl: './blog-detail.component.html',
  styleUrls: ['./blog-detail.component.css']
})
export class BlogDetailComponent implements OnInit {

  @Input() blog: Blog;
  originalComments;
  orderedComments;

  constructor(private route: ActivatedRoute,
              private blogService: BlogService,
              private toastr: ToastrService,
              private commentService: CommentService,
              private titleService: Title) { }

  ngOnInit() {
    this.route.params.subscribe(() => {
      this.getPost();
    });

    const connection = new signalR.HubConnectionBuilder()
      .configureLogging(signalR.LogLevel.Information)
      .withUrl('https://localhost:44353/api/blog')
      .build();

    connection.start().then(() => {
      console.log('Connected!');
    }).catch(err => {
      return console.error(err.toString());
    });

    connection.on('BroadcastComment', (comment: Comments) => {
      this.orderedComments.push(comment);
      this.sortBy('publishedOn');
    });
  }

  getPost(): void {
    const id = +this.route.snapshot.paramMap.get('id');
    this.blogService.getPostDetail(id)
      .subscribe(post => {
        this.blog = post;
        this.originalComments = this.blog.comments;
        this.sortBy('publishedOn');
        this.titleService.setTitle(`Topic Details`);
      });
  }

  postComment(comment: string) {
    const token = JSON.stringify(localStorage.getItem('token'));
    const jwtData = token.split('.')[1];
    const decodedJwtJsonData = window.atob(jwtData);
    const decodedJwtData = JSON.parse(decodedJwtJsonData);
    const userId = decodedJwtData.UserID;
    if (comment === '') {
      this.toastr.error(`Write something.`, 'Spam ?');
      return;
    }
    console.log(comment);
    this.commentService.postComment(userId, this.blog.id, comment, 'Blog').subscribe(
      () => {
        this.toastr.success(`You have added a comment`, 'Success !');
      },
      error => {
        if (error.status === 400 || error.status === 500) {
          this.toastr.error(`Your comment is not posted try again later.`, 'Something wrong');
        } else {
          console.log(error);
        }
      }
    );
  }

  sortBy(field: string) {
    this.originalComments.sort((a: any, b: any) => {
      if (a[field] > b[field]) {
        return -1;
      } else if (a[field] < b[field]) {
        return 1;
      } else {
        return 0;
      }
    });
    this.orderedComments = this.originalComments;
  }
}
