import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { BlogPostService } from '../services/blog-post.service';
import { Article } from '../models/create-blodpost.model';
import { CategoryService } from '../../category/services/category.service';
import { Category } from '../../category/models/category.model';
import { EditBlogPostRequest } from '../models/edit-blogpost-request.model';
import { UploadPictureService } from 'src/app/shared/components/image-upload-selector/upload-picture.service';

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
  isImageSelectorVisible: boolean = false;


  routeSubscription?: Subscription;
  updateBlogPostSubscription?: Subscription;
  getBlogPostSubscription?: Subscription;
  deleteBlogPostSubscription?: Subscription;
  selectedPictureSubscription?: Subscription;

  
  constructor(private route: ActivatedRoute, private blogPostService: BlogPostService, 
    private router: Router, private categoryService: CategoryService, private pictureService: UploadPictureService) {

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

        // render an image url on the field by getting the value from the service
        this.selectedPictureSubscription = this.pictureService.onSelectedPicture().subscribe({
          next: (response) => {
            if(this.model){
              this.model.featureImageUrl = response.pictureUrl;
              this.isImageSelectorVisible = false;
            }
          }
        });
        
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
        featureImageUrl: this.model.featureImageUrl,
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

  onDeleteBlogPost(): void {
    if(this.id){
      this.deleteBlogPostSubscription = this.blogPostService.deleteBlogPost(this.id).subscribe({
        next: (response) => {
          this.router.navigateByUrl('/manage/blogposts');
        }
      });
    }
  }

  onCancel() : void {
    this.router.navigateByUrl('/manage/blogposts');
  }

  openImageSelector(): void {
    this.isImageSelectorVisible = true;
  }

  closeImageSelector(): void {
    this.isImageSelectorVisible = false;
  }

  ngOnDestroy(): void {
    this.routeSubscription?.unsubscribe();
    this.updateBlogPostSubscription?.unsubscribe();
    this.getBlogPostSubscription?.unsubscribe();
    this.deleteBlogPostSubscription?.unsubscribe();
    this.selectedPictureSubscription?.unsubscribe();
  }

}
