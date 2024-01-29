import { Component, OnInit } from '@angular/core';
import { Material } from './../../../shared/model/material/material.model';
import { IMaterialApi } from 'src/app/api';
import {
  Pagination,
  PaginationDefaults
} from 'src/app/shared/model/pagination/pagination.model';
import { firstValueFrom } from 'rxjs';
import { handleError } from 'src/app/shared/functions/error-handler';

@Component({
  selector: 'app-material-list',
  templateUrl: './material-list.component.html',
  styleUrls: ['./material-list.component.css']
})
export class MaterialListComponent implements OnInit {
  pagination!: Pagination;
  materials!: Material[];
  searchMaterial: string = '';

  constructor(private materialApi: IMaterialApi) {}

  ngOnInit() {
    this.getMaterials();
  }

  async getMaterials() {
    try {
      const data = await firstValueFrom(
        this.materialApi.getMaterials(
          this.searchMaterial,
          this.pagination == null ? 1 : this.pagination.currentPage,
          this.pagination == null
            ? PaginationDefaults.ITEM_PER_PAGE
            : this.pagination.pageSize
        )
        // .pipe(finalize(() => (this.loaderService.loaderVisible = false)))
      ).then((data) => {
        this.materials = data.items;
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
    await this.getMaterials();
  }
}
