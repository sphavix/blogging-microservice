import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { BlogPostService } from '../services/blog-post.service';
import { Article } from '../models/create-blodpost.model';
import { CategoryService } from '../../category/services/category.service';
import { Category } from '../../category/models/category.model';
import { EditBlogPostRequest } from '../models/edit-blogpost-request.model';

@Component({
  selector: 'app-edit-blogpost',
  templateUrl: './edit-blogpost.component.html',
  styleUrls: ['./edit-blogpost.component.css']
})
export class EditBlogpostComponent implements OnInit, OnDestroy{
  id: string | null = null;
  model?: Article;
  categories$?: Observable<Category[]>;
  selectedCategories?: string[];
  routeSubscription?: Subscription;
  updateBlogPostSubscription?: Subscription;
  getBlogPostSubscription?: Subscription;

  
  constructor(private route: ActivatedRoute, private blogPostService: BlogPostService, 
    private router: Router, private categoryService: CategoryService) {

  }
  
  
  ngOnInit(): void {
    this.categories$ = this.categoryService.getCategories();
    this.routeSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');

        // Get articles from the Api
        if(this.id){
          this.getBlogPostSubscription = this.blogPostService.getBlogPostById(this.id).subscribe({
            next: (response) => {
              this.model = response;

              this.selectedCategories = response.categories.map(x => x.id);
            }
          });
        }
        
      }
    });
  }

  onEditBlogPost(): void {
    // convert model to the request object
    if(this.model && this.id){
      var editBlogPost: EditBlogPostRequest = {
        title: this.model.title,
        author: this.model.author,
        content: this.model.content,
        urlHandle: this.model.urlHandle,
        shortDescription: this.model.shortDescription,
        featureImage: this.model.featureImage,
        isVisible: this.model.isVisible,
        publishedDate: this.model.publishedDate,
        categories: this.selectedCategories ?? []
      };

      this.updateBlogPostSubscription = this.blogPostService.editBlogPost(this.id, editBlogPost).subscribe({
        next: (response) => {
          this.router.navigateByUrl('/manage/blogposts');
        }
      });
    }
  }

  onCancel() : void {
    this.router.navigateByUrl('/manage/blogposts');
  }

  ngOnDestroy(): void {
    this.routeSubscription?.unsubscribe();
    this.updateBlogPostSubscription?.unsubscribe();
    this.getBlogPostSubscription?.unsubscribe();
  }

}
