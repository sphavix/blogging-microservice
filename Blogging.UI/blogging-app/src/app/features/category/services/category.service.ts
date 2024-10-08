import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateCategoryRequest } from '../models/create-category-request';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  constructor(private http: HttpClient) { }

  createCategory(model: CreateCategoryRequest): Observable<void> {
    return this.http.post<void>('https://localhost:7062/api/categories', model);
  }
}
