import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { CreateArticleRequest } from '../models/create-blogpost-request.model';
import { Article } from '../models/create-blodpost.model';

@Injectable({
  providedIn: 'root'
})
export class BlogPostService {

  constructor(private http: HttpClient) { 

  }

  createBlogPost(data: CreateArticleRequest) : Observable<Article> {
    return this.http.post<Article>(`${environment.apiBaseUrl}/api/articles`, data);
  }
}
