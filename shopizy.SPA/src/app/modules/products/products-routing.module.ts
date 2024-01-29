import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProductListComponent } from './product-list/product-list.component';
import { AddProductComponent } from './add-product/add-product.component';
import { EditProductComponent } from './edit-product/edit-product.component';

const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: '',
        component: ProductListComponent,
        data: {
          breadcrumb: '',
          title: 'Product List'
        }
      },
      {
        path: 'add',
        component: AddProductComponent,
        data: {
          breadcrumb: 'add',
          title: 'Add New Product'
        }
      },
      {
        path: 'edit/:id',
        component: EditProductComponent,
        data: {
          breadcrumb: 'edit'
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
export class ProductsRoutingModule {}
