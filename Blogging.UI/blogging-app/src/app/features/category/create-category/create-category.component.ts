import { Component } from '@angular/core';
import { CreateCategoryRequest } from '../models/create-category-request';

@Component({
  selector: 'app-create-category',
  templateUrl: './create-category.component.html',
  styleUrls: ['./create-category.component.css']
})
export class CreateCategoryComponent {

  model: CreateCategoryRequest;

  constructor(){
    this.model = {
      name: '',
      urlHandle: ''
    };
  }


  handleOnCreate(){
    console.log(this.model)
  }
}
