import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategoryListComponent } from './features/category/category-list/category-list.component';
import { CreateCategoryComponent } from './features/category/create-category/create-category.component';
import { EditCategoryComponent } from './features/category/edit-category/edit-category.component';

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
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
