import { Component, OnDestroy } from '@angular/core';
import { CreateCategoryRequest } from '../models/create-category-request';
import { CategoryService } from '../services/category.service';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-category',
  templateUrl: './create-category.component.html',
  styleUrls: ['./create-category.component.css']
})
export class CreateCategoryComponent implements OnDestroy {

  model: CreateCategoryRequest;
  private createCategorySubscription?: Subscription;

  constructor(private categoryService: CategoryService, private router: Router){
    this.model = {
      name: '',
      urlHandle: ''
    };
  }
  


  handleOnCreate(){
    this.createCategorySubscription = this.categoryService.createCategory(this.model).subscribe({
      next: (response) => {
        this.router.navigateByUrl('/manage/categories');
      }
    });
  }

  ngOnDestroy(): void {
    this.createCategorySubscription?.unsubscribe();
  }
}
