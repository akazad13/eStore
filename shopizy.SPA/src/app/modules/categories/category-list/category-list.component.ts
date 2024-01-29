import { Component, OnInit } from '@angular/core';
import { ICategoryApi } from 'src/app/api';
import { Category } from './../../../shared/model/category/category.model';
import {
  Pagination,
  PaginationDefaults
} from 'src/app/shared/model/pagination/pagination.model';
import { firstValueFrom } from 'rxjs';
import { handleError } from 'src/app/shared/functions/error-handler';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.css']
})
export class CategoryListComponent implements OnInit {
  pagination!: Pagination;
  categories!: Category[];
  searchCategory: string = '';

  constructor(private CategoryApi: ICategoryApi) {}

  ngOnInit() {
    this.getCategories();
  }

  async getCategories() {
    try {
      const data = await firstValueFrom(
        this.CategoryApi.getCategories(
          this.searchCategory,
          this.pagination == null ? 1 : this.pagination.currentPage,
          this.pagination == null
            ? PaginationDefaults.ITEM_PER_PAGE
            : this.pagination.pageSize
        )
        // .pipe(finalize(() => (this.loaderService.loaderVisible = false)))
      ).then((data) => {
        this.categories = data.items;
        this.pagination = data.pagination;
      });
    } catch (error) {
      handleError(null, error);
    }
  }

  async pageChanged(event: any): Promise<void> {
    if (this.pagination != null) {
      this.pagination.currentPage = event.page;
    }
    await this.getCategories();
  }
}
