import { Component, OnDestroy, OnInit } from '@angular/core';
import { CreateArticleRequest } from '../models/create-blogpost-request.model';
import { BlogPostService } from '../services/blog-post.service';
import { Router } from '@angular/router';
import { CategoryService } from '../../category/services/category.service';
import { Observable, Subscription } from 'rxjs';
import { Category } from '../../category/models/category.model';
import { UploadPictureService } from 'src/app/shared/components/image-upload-selector/upload-picture.service';

@Component({
  selector: 'app-create-blogpost',
  templateUrl: './create-blogpost.component.html',
  styleUrls: ['./create-blogpost.component.css']
})
export class CreateBlogpostComponent implements OnInit, OnDestroy {
  model: CreateArticleRequest;
  categories$?: Observable<Category[]>;


  isImageSelectorVisible: boolean = false;

  selectedPictureSubscription?: Subscription;

  constructor(private articleService: BlogPostService, private categoryService: CategoryService, private router: Router, private pictureService: UploadPictureService) {
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

    this.selectedPictureSubscription = this.pictureService.onSelectedPicture().subscribe({
      next: (response) => {
        this.model.featureImageUrl = response.pictureUrl;
        this.closeImageSelector();
      }
    })
  }

  onCreateBlogPost() : void {
    console.log(this.model);
    this.articleService.createBlogPost(this.model).subscribe({
      next: (response) => {
        this.router.navigateByUrl('/manage/blogposts')
      }
    });
  }

  openImageSelector(): void {
    this.isImageSelectorVisible = true;
  }

  closeImageSelector(): void {
    this.isImageSelectorVisible = false;
  }


  ngOnDestroy(): void {
    this.selectedPictureSubscription?.unsubscribe();
  }

}
