import { Component, OnInit } from '@angular/core';
import { CreateArticleRequest } from '../models/create-blogpost-request.model';
import { BlogPostService } from '../services/blog-post.service';
import { Router } from '@angular/router';
import { CategoryService } from '../../category/services/category.service';
import { Observable } from 'rxjs';
import { Category } from '../../category/models/category.model';

@Component({
  selector: 'app-create-blogpost',
  templateUrl: './create-blogpost.component.html',
  styleUrls: ['./create-blogpost.component.css']
})
export class CreateBlogpostComponent implements OnInit {
  model: CreateArticleRequest;
  categories$?: Observable<Category[]>;

  constructor(private articleService: BlogPostService, private categoryService: CategoryService, private router: Router) {
    this.model = {
      title: '',
      shortDescription: '',
      content: '',
      featureImageUrl: '',
      urlHandle: '',
      author: '',
      publishedDate: new Date(),
      isVisible: true,
      categories: []
    }
  }


  ngOnInit(): void {
    this.categories$ = this.categoryService.getCategories();
  }

  onCreateBlogPost() : void {
    console.log(this.model);
    this.articleService.createBlogPost(this.model).subscribe({
      next: (response) => {
        this.router.navigateByUrl('/manage/blogposts')
      }
    });
  }
}
