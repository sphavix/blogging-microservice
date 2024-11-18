import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BlogPostService } from '../../blog-post/services/blog-post.service';
import { Observable } from 'rxjs';
import { Article } from '../../blog-post/models/create-blodpost.model';

@Component({
  selector: 'app-article-details',
  templateUrl: './article-details.component.html',
  styleUrls: ['./article-details.component.css']
})
export class ArticleDetailsComponent implements OnInit {

  url: string | null = null;
  article$?: Observable<Article>;

  constructor(private route: ActivatedRoute, private articlesService: BlogPostService) {

  }


  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (params) => {
        this.url = params.get('url');
      }
    });

    //get article by url
    if(this.url){
      this.article$ = this.articlesService.getBlogPostByUrlHandle(this.url)
    }
  }

}
