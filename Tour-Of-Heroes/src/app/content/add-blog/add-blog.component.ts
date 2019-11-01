import { ToastrService } from 'ngx-toastr';
import { BlogService } from './../../services/blog.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-add-blog',
  templateUrl: './add-blog.component.html',
  styleUrls: ['./add-blog.component.css']
})

export class AddBlogComponent implements OnInit {
  registerForm: FormGroup;
  @ViewChild('blogImage', { static: false }) blogImage;
  blogImageFile: File;

  constructor(private formBuilder: FormBuilder, private blogService: BlogService, private toastr: ToastrService) { }

  ngOnInit() {
    this.registerForm = this.formBuilder.group({
      title: [''],
      content: [''],
      blogImage: [''],
    }, {
    });
  }

  stageBlogImageFile(): void {
    this.blogImageFile = this.blogImage.nativeElement.files[0];
  }

  add(title: string, content: string): void {
    const formData = new FormData();
    const image = this.blogImageFile;
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
