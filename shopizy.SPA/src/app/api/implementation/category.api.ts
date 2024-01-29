import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';

import { environment } from '../../../environments/environment';

import { ICategoryApi } from '../base';
import { PaginatedResult } from '../../shared/model/pagination/pagination.model';
import { Category } from 'src/app/shared/model/category/category.model';

@Injectable()
export class CategoryApi extends ICategoryApi {
  categoryUrl = environment.apiUrl + '/api/category/';
  constructor(private http: HttpClient) {
    super();
  }
  createCategory(name: string): Observable<any> {
    return this.http.post<any>(this.categoryUrl, {
      name: name
    });
  }
  getCategories(
    name: string | null,
    page: number | null,
    itemsPerPage: number | null
  ): Observable<PaginatedResult<Category[]>> {
    let params = new HttpParams();
    if (name != null) {
      params = params.append('name', name);
    }
    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }
    return this.http.get<PaginatedResult<Category[]>>(
      this.categoryUrl + 'search',
      {
        params
      }
    );
  }
  getCategory(id: number): Observable<Category> {
    return this.http.get<Category>(this.categoryUrl + id);
  }
  updateCategory(id: number, name: string): Observable<any> {
    return this.http.patch<any>(this.categoryUrl, {
      id: id,
      name: name
    });
  }
}
