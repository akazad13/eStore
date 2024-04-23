import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { IAuthApi, ICategoryApi, IMaterialApi, IProductApi, IUserApi } from '../base';
import { AuthApi } from './auth.api';
import { ProductApi } from './product.api';
import { CategoryApi } from './category.api';
import { MaterialApi } from './material.api';
import { UserApi } from './user.api';

@NgModule({
  imports: [CommonModule],
  providers: [
    { provide: IAuthApi, useClass: AuthApi },
    { provide: IProductApi, useClass: ProductApi },
    { provide: ICategoryApi, useClass: CategoryApi },
    { provide: IMaterialApi, useClass: MaterialApi },
    { provide: IUserApi, useClass: UserApi }
  ]
})
export class ApiModule {}
