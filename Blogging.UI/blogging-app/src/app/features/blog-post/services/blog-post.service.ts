import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { CreateArticleRequest } from '../models/create-blogpost-request.model';
import { Article } from '../models/create-blodpost.model';
import { EditBlogPostRequest } from '../models/edit-blogpost-request.model';

@Injectable({
  providedIn: 'root'
})
export class BlogPostService {

  constructor(private http: HttpClient) { 

  }

  getBlogPosts(): Observable<Article[]> {
    return this.http.get<Article[]>(`${environment.apiBaseUrl}/api/articles`);
  }

  getBlogPostById(id: string): Observable<Article> {
    return this.http.get<Article>(`${environment.apiBaseUrl}/api/articles/${id}`);
  }

  createBlogPost(data: CreateArticleRequest) : Observable<Article> {
    return this.http.post<Article>(`${environment.apiBaseUrl}/api/articles`, data);
  }

  editBlogPost(id: string, updatedBlogPost: EditBlogPostRequest): Observable<Article> {
    return this.http.put<Article>(`${environment.apiBaseUrl}/api/articles/${id}`, updatedBlogPost);
  }


}
