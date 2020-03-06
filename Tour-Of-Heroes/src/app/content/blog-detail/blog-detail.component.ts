import { Globals } from './../../globals/globals';
import { Title } from '@angular/platform-browser';
import { Comments } from './../../entities/comment';
import { CommentService } from './../../services/comment.service';
import { ToastrService } from 'ngx-toastr';
import { BlogService } from './../../services/blog.service';
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit, Input } from '@angular/core';
import { Location } from '@angular/common';
import { Blog } from 'src/app/entities/blog';
import * as signalR from '@aspnet/signalr';
import { AngularEditorConfig } from '@kolkov/angular-editor';

@Component({
  selector: 'app-blog-detail',
  templateUrl: './blog-detail.component.html',
  styleUrls: ['./blog-detail.component.css']
})
export class BlogDetailComponent implements OnInit {
  placements: string[] = ['top', 'left', 'right', 'bottom'];
  popoverTitle = 'Enter your password to confirm!';
  confirmClicked = false;
  cancelClicked = false;
  @Input() blog: Blog;
  title = '';
  authorUserName = '';
  blogImage = '';
  latestPosts;
  postId;
  likes = 0;
  dislikes = 0;
  isLiked;
  isDisliked;
  publishedOn: Date;
  originalComments;
  orderedComments;
  content;

  editorConfig: AngularEditorConfig = {
    editable: true,
      spellcheck: true,
      height: '11rem',
      minHeight: '5rem',
      maxHeight: 'auto',
      width: 'auto',
      minWidth: '0',
      translate: 'yes',
      enableToolbar: true,
      showToolbar: true,
      placeholder: 'Enter text here...',
      defaultParagraphSeparator: '',
      defaultFontName: '',
      defaultFontSize: '',
      fonts: [
        {class: 'arial', name: 'Arial'},
        {class: 'times-new-roman', name: 'Times New Roman'},
        {class: 'calibri', name: 'Calibri'},
        {class: 'comic-sans-ms', name: 'Comic Sans MS'}
      ],
      customClasses: [
      {
        name: 'quote',
        class: 'quote',
      },
      {
        name: 'redText',
        class: 'redText'
      },
      {
        name: 'titleText',
        class: 'titleText',
        tag: 'h1',
      },
    ],
    uploadUrl: 'v1/image',
    sanitize: true,
    toolbarPosition: 'top',
};

  constructor(private route: ActivatedRoute,
              private blogService: BlogService,
              private toastr: ToastrService,
              private commentService: CommentService,
              private titleService: Title,
              private location: Location,
              public globals: Globals) { }

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

    connection.on('DeleteComment', (commentId: number) => {
      for (const i of this.orderedComments) {
        if (i.id === commentId) {
          if (i.isDeleted === true) {
            this.toastr.warning(`Comment is already deleted`, 'Oops !');
            return;
          }
          this.toastr.success(`You have deleted your comment`, 'Success !');
          i.isDeleted = true;
          return;
        }
      }
    });
  }

  getPost(): void {
    const id = +this.route.snapshot.paramMap.get('id');
    this.blogService.getPostDetail(id)
      .subscribe(post => {
        this.blog = post;
        this.title = this.blog.title;
        this.authorUserName = this.blog.authorUserName;
        this.publishedOn = this.blog.publishedOn;
        this.blogImage = this.blog.blogImage;
        this.postId = id;
        this.latestPosts = this.blog.latestPosts;
        this.likes = this.blog.likes;
        this.dislikes = this.blog.dislikes;
        this.isLiked = this.blog.isLiked;
        this.isDisliked = this.blog.isDisliked;
        this.originalComments = this.blog.comments;
        this.sortBy('publishedOn');
        this.content = post.content;
        this.titleService.setTitle(`Topic Details`);
      });
  }

  postComment(comment: string) {
    const token = JSON.stringify(localStorage.getItem('token'));
    const jwtData = token.split('.')[1];
    const decodedJwtJsonData = window.atob(jwtData);
    const decodedJwtData = JSON.parse(decodedJwtJsonData);
    comment = document.getElementsByClassName('angular-editor-textarea').item(0).innerHTML;
    const userId = decodedJwtData.UserID;
    if (comment === '') {
      this.toastr.error(`Write something.`, 'Spam ?');
      return;
    }
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

  deleteComment(comment: Comments) {
    this.commentService.deleteComment(comment).subscribe(
      () => { },
      error => {
        console.log(error);
      });
  }

  deletePost(blog: Blog, password: string): void {
    this.blogService.deletePost(blog, password).subscribe(
      () => {
        this.goBack();
        this.toastr.success(`You have deleted post: ${blog.title}`, 'Success !');
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

  likePost() {
    this.blogService.likePost(this.postId)
      .subscribe(() => {
        this.toastr.success(`You liked a post with title: ${this.title}`, 'Success !');
      }, err => {
        if (err.status === 400) {
          this.toastr.error('You have already liked this post.', 'Oops!');
        }
      });
  }

  dislikePost(): void {
    const formData = new FormData();
    formData.append('id', this.postId);
    this.blogService.dislikePost(formData)
      .subscribe(() => {
        this.toastr.success(`You have disliked a new post: ${this.title}`, 'Success !');
      },
      err => {
        if (err.status === 400) {
          this.toastr.error(`You have already disliked this post.`, 'Oops !');
        }
      });
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

  goBack(): void {
    this.location.back();
  }
}
