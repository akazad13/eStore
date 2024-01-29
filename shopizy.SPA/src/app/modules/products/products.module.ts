import { NgModule } from '@angular/core';

import { SharedModule } from 'src/app/shared/shared.module';
import { ProductsRoutingModule } from './products-routing.module';

import { ProductListComponent } from './product-list/product-list.component';
import { AddProductComponent } from './add-product/add-product.component';
import { EditProductComponent } from './edit-product/edit-product.component';
import { AngularEditorModule } from '@kolkov/angular-editor';

@NgModule({
  imports: [ProductsRoutingModule, SharedModule, AngularEditorModule],
  declarations: [
    ProductListComponent,
    EditProductComponent,
    AddProductComponent
  ]
})
export class ProductsModule {}
