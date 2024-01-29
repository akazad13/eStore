import { NgModule } from '@angular/core';
import { SharedModule } from 'src/app/shared/shared.module';
import { MaterialsRoutingModule } from './materials-routing.module';

import { AddEditMaterialComponent } from './add-edit-material/add-edit-material.component';
import { MaterialListComponent } from './material-list/material-list.component';

@NgModule({
  declarations: [MaterialListComponent, AddEditMaterialComponent],
  imports: [MaterialsRoutingModule, SharedModule]
})
export class MaterialsModule {}
