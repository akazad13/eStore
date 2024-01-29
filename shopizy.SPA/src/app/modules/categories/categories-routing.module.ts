import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddEditCategoryComponent } from './add-edit-category/add-edit-category.component';
import { CategoryListComponent } from './category-list/category-list.component';

const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: '',
        component: CategoryListComponent,
        data: {
          breadcrumb: '',
          title: 'Catergory List'
        }
      },
      {
        path: 'add',
        component: AddEditCategoryComponent,
        data: {
          breadcrumb: 'Add',
          title: 'Add New Category'
        }
      },
      {
        path: 'edit/:id',
        component: AddEditCategoryComponent,
        data: {
          breadcrumb: 'Edit',
          title: 'Edit Category'
        }
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: []
})
export class CategoriesRoutingModule {}
