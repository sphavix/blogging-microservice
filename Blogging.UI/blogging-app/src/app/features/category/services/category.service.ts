import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateCategoryRequest } from '../models/create-category-request';
import { Category } from '../models/category.model';
import { environment } from 'src/environments/environment';
import { UpdateCategoryRequest } from '../models/update-category-request.model';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  constructor(private http: HttpClient) { }

  createCategory(model: CreateCategoryRequest): Observable<void> {
    return this.http.post<void>(`${environment.apiBaseUrl}/api/categories`, model);
  }

  getCategories(): Observable<Category[]>{
    return this.http.get<Category[]>(`${environment.apiBaseUrl}/api/categories`);
  }

  getCategory(id: string): Observable<Category> {
    return this.http.get<Category>(`${environment.apiBaseUrl}/api/categories/${id}`);
  }

  updateCategory(id: string, updateCategoryRequest: UpdateCategoryRequest): Observable<Category> {
    return this.http.put<Category>(`${environment.apiBaseUrl}/api/categories/${id}`, updateCategoryRequest);
  }
}
