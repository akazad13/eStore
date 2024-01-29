import { Routes } from '@angular/router';

export const content: Routes = [
  {
    path: 'dashboard',
    loadChildren: () =>
      import('../../modules/dashboard/dashboard.module').then(
        (m) => m.DashboardModule
      ),
    data: {
      breadcrumb: 'Dashboard'
    }
  },
  {
    path: 'products',
    loadChildren: () =>
      import('../../modules/products/products.module').then(
        (m) => m.ProductsModule
      ),
    data: {
      breadcrumb: 'Products'
    }
  },
  {
    path: 'materials',
    loadChildren: () =>
      import('../../modules/materials/materials.module').then(
        (m) => m.MaterialsModule
      ),
    data: {
      breadcrumb: 'Materials'
    }
  },
  {
    path: 'categories',
    loadChildren: () =>
      import('../../modules/categories/categories.module').then(
        (m) => m.CategoriesModule
      ),
    data: {
      breadcrumb: 'Categories'
    }
  }
];
