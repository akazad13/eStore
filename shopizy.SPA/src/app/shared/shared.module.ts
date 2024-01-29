import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { BreadcrumbComponent } from './components/breadcrumb/breadcrumb.component';
import { ContentLayoutComponent } from './components/layout/content-layout/content-layout.component';
import { FooterComponent } from './components/footer/footer.component';
import { FullLayoutComponent } from './components/layout/full-layout/full-layout.component';
import { HeaderComponent } from './components/header/header.component';
import { LoaderComponent } from './components/loader/loader.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';

import { NavService } from './services/nav.service';

import { HasErrorPipe } from './pipes/has-error.pipe';
import { IsInvalidPipe } from './pipes/is-invalid.pipe';
import { HasRoleDirective } from './directives/hasRole.directive';
import { NumberOnlyDirective } from './directives/Number-only.directive';
import { PaginationModule } from 'ngx-bootstrap/pagination';
// pipes

@NgModule({
  declarations: [
    LoaderComponent,
    ContentLayoutComponent,
    FullLayoutComponent,
    HeaderComponent,
    FooterComponent,
    BreadcrumbComponent,
    SidebarComponent,
    // pipes
    HasErrorPipe,
    IsInvalidPipe,
    HasRoleDirective,
    NumberOnlyDirective
  ],
  imports: [CommonModule, RouterModule, FormsModule, ReactiveFormsModule],
  exports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    LoaderComponent,
    HasErrorPipe,
    IsInvalidPipe,
    PaginationModule
  ],
  providers: [NavService]
})
export class SharedModule {}
