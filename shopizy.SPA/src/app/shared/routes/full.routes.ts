import { Routes } from '@angular/router';

export const full: Routes = [
  {
    path: 'error',
    loadChildren: () =>
      import('../../modules/error-pages/error-pages.module').then(
        (m) => m.ErrorPagesModule
      )
  },
  {
    path: 'auth',
    loadChildren: () =>
      import('../../modules/authentication/authentication.module').then(
        (m) => m.AuthenticationModule
      )
  }
];