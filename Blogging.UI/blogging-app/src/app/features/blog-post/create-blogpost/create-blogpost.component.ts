import { Component } from '@angular/core';
import { CreateArticleRequest } from '../models/create-blogpost-request.model';
import { BlogPostService } from '../services/blog-post.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-blogpost',
  templateUrl: './create-blogpost.component.html',
  styleUrls: ['./create-blogpost.component.css']
})
export class CreateBlogpostComponent {
  model: CreateArticleRequest;

  constructor(private articleService: BlogPostService, private router: Router) {
    this.model = {
      title: '',
      shortDescription: '',
      content: '',
      featureImage: '',
      urlHandle: '',
      author: '',
      publishedDate: new Date(),
      isVisible: true
    }
  }

  onCreateBlogPost() : void {
    this.articleService.createBlogPost(this.model).subscribe({
      next: (response) => {
        this.router.navigateByUrl('/manage/blogposts')
      }
    });
  }
}
