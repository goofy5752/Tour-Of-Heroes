import { ToastrService } from 'ngx-toastr';
import { BlogService } from './../../services/blog.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { AngularEditorConfig } from '@kolkov/angular-editor';

@Component({
  selector: 'app-add-blog',
  templateUrl: './add-blog.component.html',
  styleUrls: ['./add-blog.component.css']
})

export class AddBlogComponent implements OnInit {
  registerForm: FormGroup;
  @ViewChild('blogImage', { static: false }) blogImage;
  blogImageFile: File;

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

  constructor(private formBuilder: FormBuilder, private blogService: BlogService, private toastr: ToastrService) { }

  ngOnInit() {
    this.registerForm = this.formBuilder.group({
      title: [''],
      content: [''],
      blogImage: [''],
    });
  }

  stageBlogImageFile(): void {
    this.blogImageFile = this.blogImage.nativeElement.files[0];
  }

  add(title: string, content: string): void {
    const formData = new FormData();
    const image = this.blogImageFile;
    content = document.getElementsByClassName('angular-editor-textarea').item(0).innerHTML;
    console.log(content);
    formData.append('title', title);
    formData.append('content', content);
    formData.append('blogImage', image, image.name);
    this.blogService.createPost(formData)
      .subscribe(() => {
        this.toastr.success(`You have create a new post: ${title}`, 'Success !');
      });
  }

  onReset() {
    this.registerForm.reset();
  }
}
