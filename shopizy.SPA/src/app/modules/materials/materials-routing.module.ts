import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AddEditMaterialComponent } from './add-edit-material/add-edit-material.component';
import { MaterialListComponent } from './material-list/material-list.component';

const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: '',
        component: MaterialListComponent,
        data: {
          breadcrumb: '',
          title: 'Material List'
        }
      },
      {
        path: 'add',
        component: AddEditMaterialComponent,
        data: {
          breadcrumb: 'Add',
          title: 'Add New Material'
        }
      },
      {
        path: 'edit/:id',
        component: AddEditMaterialComponent,
        data: {
          breadcrumb: 'Edit',
          title: 'Edit Material'
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
export class MaterialsRoutingModule {}
