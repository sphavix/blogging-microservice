import { Component, OnDestroy } from '@angular/core';
import { CreateCategoryRequest } from '../models/create-category-request';
import { CategoryService } from '../services/category.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-create-category',
  templateUrl: './create-category.component.html',
  styleUrls: ['./create-category.component.css']
})
export class CreateCategoryComponent implements OnDestroy {

  model: CreateCategoryRequest;
  private createCategorySubscription?: Subscription;

  constructor(private categoryService: CategoryService){
    this.model = {
      name: '',
      urlHandle: ''
    };
  }
  


  handleOnCreate(){
    this.createCategorySubscription = this.categoryService.createCategory(this.model).subscribe({
      next: (response) => {
        console.log('The api call was successful');
      }
    });
  }

  ngOnDestroy(): void {
    this.createCategorySubscription?.unsubscribe();
  }
}
