import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { BlogService } from 'src/app/services/blog.service';
import { ToastrService } from 'ngx-toastr';
import { Title } from '@angular/platform-browser';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { Blog } from 'src/app/entities/blog';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-blog-edit',
  templateUrl: './blog-edit.component.html',
  styleUrls: ['./blog-edit.component.css']
})
export class BlogEditComponent implements OnInit {

  @Input() blog: Blog;
  registerForm: FormGroup;
  @ViewChild('blogImage', { static: false }) blogImage;
  blogImageFile: File;

  title = '';
  authorUserName = '';
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
      height: '13rem',
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
              private formBuilder: FormBuilder,
              private blogService: BlogService,
              private toastr: ToastrService,
              private titleService: Title) { }

  ngOnInit() {

    const id = +this.route.snapshot.paramMap.get('id');
    this.blogService.getPostDetail(id)
      .subscribe(post => {
        this.blog = post;
        this.title = post.title;
        this.content = post.content;
        this.titleService.setTitle(`Edit Post`);
      });
    this.registerForm = this.formBuilder.group({
      title: [''],
      content: [''],
      blogImage: [''],
    });

    this.titleService.setTitle(`Add new post`);
  }

  stageBlogImageFile(): void {
    this.blogImageFile = this.blogImage.nativeElement.files[0];
  }

  edit(title: string, content: string): void {
    const formData = new FormData();
    const image = this.blogImageFile;
    content = document.getElementsByClassName('angular-editor-textarea').item(0).innerHTML;
    formData.append('id', this.blog.id.toString());
    formData.append('title', title);
    formData.append('content', content);
    formData.append('blogImage', image, image.name);
    this.blogService.editPost(formData)
      .subscribe(() => {
        this.toastr.success(`The post is edited successfully`, 'Success !');
      });
    this.toastr.success(`The post is edited successfully`, 'Success !');
  }

  onReset() {
    this.registerForm.reset();
  }
}
