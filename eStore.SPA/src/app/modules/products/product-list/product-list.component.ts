import { Category } from '../../../shared/model/category/category.model';
import { Product } from 'src/app/shared/model/Product/Product.model';
import { Component, OnInit } from '@angular/core';
import { finalize, firstValueFrom } from 'rxjs';
import { ICategoryApi, IProductApi } from 'src/app/api';
import { handleError } from 'src/app/shared/functions/error-handler';
import {
  Pagination,
  PaginationDefaults
} from 'src/app/shared/model/pagination/pagination.model';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {
  pagination!: Pagination;
  products!: Product[];
  categories!: Category[];
  searchProductName: string = '';
  selectedOption: string = '';

  constructor(
    private productApi: IProductApi,
    private categoryApi: ICategoryApi
  ) {}

  async ngOnInit() {
    await this.getProducts();
    await this.getCategories();
  }

  async pageChanged(event: any): Promise<void> {
    if (this.pagination != null) {
      this.pagination.currentPage = event.page;
    }
    await this.getProducts();
  }

  async getProducts(): Promise<void> {
    try {
      const data = await firstValueFrom(
        this.productApi.getProducts(
          this.searchProductName,
          this.selectedOption,
          null,
          'name',
          this.pagination == null ? 1 : this.pagination.currentPage,
          this.pagination == null
            ? PaginationDefaults.ITEM_PER_PAGE
            : this.pagination.pageSize
        )
        // .pipe(finalize(() => (this.loaderService.loaderVisible = false)))
      ).then((data) => {
        this.products = data.items;
        this.pagination = data.pagination;
      });
    } catch (error) {
      handleError(null, error);
    }
  }

  async getCategories(): Promise<void> {
    try {
      const data = await firstValueFrom(
        this.categoryApi.getCategories(null, 1, -1)
        // .pipe(finalize(() => (this.loaderService.loaderVisible = false)))
      ).then((data) => {
        this.categories = data.items;
      });
    } catch (error) {
      handleError(null, error);
    }
  }
}
