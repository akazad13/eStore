import { NgModule } from '@angular/core';
import { AddEditCategoryComponent } from './add-edit-category/add-edit-category.component';
import { CategoryListComponent } from './category-list/category-list.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { CategoriesRoutingModule } from './categories-routing.module';

@NgModule({
  imports: [CategoriesRoutingModule, SharedModule],
  declarations: [CategoryListComponent, AddEditCategoryComponent]
})
export class CategoriesModule {}
