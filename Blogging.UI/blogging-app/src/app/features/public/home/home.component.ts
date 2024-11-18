import { Component, OnInit } from '@angular/core';
import { BlogPostService } from '../../blog-post/services/blog-post.service';
import { Observable } from 'rxjs';
import { Article } from '../../blog-post/models/create-blodpost.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  articles$?: Observable<Article[]>;
  constructor (private articlesService: BlogPostService){
    
  }


  ngOnInit(): void {
    this.articles$ = this.articlesService.getBlogPosts();
  }

}
