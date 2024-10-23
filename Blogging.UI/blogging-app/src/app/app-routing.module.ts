import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategoryListComponent } from './features/category/category-list/category-list.component';
import { CreateCategoryComponent } from './features/category/create-category/create-category.component';
import { EditCategoryComponent } from './features/category/edit-category/edit-category.component';
import { BlogpostListComponent } from './features/blog-post/blogpost-list/blogpost-list.component';
import { CreateBlogpostComponent } from './features/blog-post/create-blogpost/create-blogpost.component';
import { EditBlogpostComponent } from './features/blog-post/edit-blogpost/edit-blogpost.component';

const routes: Routes = [
  {
    path: 'manage/categories',
    component: CategoryListComponent
  },
  {
    path: 'manage/categories/create',
    component: CreateCategoryComponent
  },
  {
    path: 'manage/categories/:id',
    component: EditCategoryComponent
  },
  {
    path: 'manage/blogposts',
    component: BlogpostListComponent
  },
  {
    path: 'manage/blogposts/create',
    component: CreateBlogpostComponent
  },
  {
    path: 'manage/blogposts/:id',
    component: EditBlogpostComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
