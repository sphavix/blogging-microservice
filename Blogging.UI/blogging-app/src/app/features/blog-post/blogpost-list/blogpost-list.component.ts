import { Component, OnInit } from '@angular/core';
import { BlogPostService } from '../services/blog-post.service';
import { Observable } from 'rxjs';
import { Article } from '../models/create-blodpost.model';

@Component({
  selector: 'app-blogpost-list',
  templateUrl: './blogpost-list.component.html',
  styleUrls: ['./blogpost-list.component.css']
})
export class BlogpostListComponent implements OnInit {
  
  blogPosts$?: Observable<Article[]>;
  
  constructor(private blopPostAervice: BlogPostService) {

  }
  
  
  
  ngOnInit(): void {
    // retrieve data from the api
    this.blogPosts$ = this.blopPostAervice.getBlogPosts();
  }

}
