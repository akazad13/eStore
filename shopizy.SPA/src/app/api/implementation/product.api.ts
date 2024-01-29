import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';

import { environment } from '../../../environments/environment';

import { AuthService } from '../../shared/services/auth.service';

import { IProductApi } from '../base';
import { PaginatedResult } from '../../shared/model/pagination/pagination.model';
import { Product } from '../../shared/model/Product/Product.model';

@Injectable()
export class ProductApi extends IProductApi {
  productUrl = environment.apiUrl + '/api/product/';
  constructor(private http: HttpClient, private authService: AuthService) {
    super();
  }

  createProduct(
    name: string,
    description: string,
    productDetails: string,
    categoryId: number,
    sku: string,
    price: number,
    discount: number,
    tags: string,
    productImages: File[]
  ): Observable<any> {
    debugger;
    const formData = new FormData();

    for (var i = 0; i < productImages.length; i++) {
      formData.append('images', productImages[i]);
    }
    formData.append('name', name);
    formData.append('description', description);
    formData.append('price', price?.toString());
    formData.append('size', '7"');
    formData.append('color', 'red');
    formData.append('sku', sku);
    formData.append('stock', '100');
    formData.append('categoryId', categoryId?.toString());

    return this.http.post<any>(this.productUrl, formData);
  }

  getProducts(
    name: string | null,
    categories: string | null,
    materials: string | null,
    sortby: string | null,
    pageNumber: number | null,
    itemsPerPage: number | null
  ): Observable<PaginatedResult<Product[]>> {
    let params = new HttpParams();

    if (name != null) {
      params = params.append('name', name);
    }
    if (categories != null) {
      params = params.append('categories', categories);
    }
    if (materials != null) {
      params = params.append('materials', materials);
    }
    if (sortby != null) {
      params = params.append('sortBy', sortby);
    }

    if (pageNumber != null && itemsPerPage != null) {
      params = params.append('pageNumber', pageNumber.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }
    return this.http.get<PaginatedResult<Product[]>>(
      this.productUrl + 'search',
      {
        params
      }
    );
  }

  updateProduct(
    id: number,
    title: string,
    location: string,
    scheduledOn: Date,
    maxPlayers: number,
    isGameStarted: boolean,
    signupStartsFrom?: Date
  ): Observable<any> {
    return this.http.patch<any>(this.productUrl + id, {
      title,
      location,
      scheduledOn,
      signupStartsFrom,
      maxPlayers,
      isGameStarted,
      modifiedBy: this.authService.getCurrentUserId(),
      modifiedOn: new Date()
    });
  }
}
