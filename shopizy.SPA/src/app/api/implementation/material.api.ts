import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';

import { environment } from '../../../environments/environment';

import { IMaterialApi } from '../base';
import { PaginatedResult } from '../../shared/model/pagination/pagination.model';
import { Material } from 'src/app/shared/model/material/material.model';

@Injectable()
export class MaterialApi extends IMaterialApi {
  materialUrl = environment.apiUrl + '/api/material/';
  constructor(private http: HttpClient) {
    super();
  }

  createMaterial(name: string, matImage: File): Observable<any> {
    const formData = new FormData();

    formData.append('name', name);
    formData.append('materialImage', matImage);

    return this.http.post<any>(this.materialUrl, formData);
  }
  getMaterials(
    name: string | null,
    pageNumber: number | null,
    itemsPerPage: number | null
  ): Observable<PaginatedResult<Material[]>> {
    let params = new HttpParams();

    if (name != null) {
      params = params.append('name', name);
    }

    if (pageNumber != null && itemsPerPage != null) {
      params = params.append('pageNumber', pageNumber.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }
    return this.http.get<PaginatedResult<Material[]>>(
      this.materialUrl + 'search',
      {
        params
      }
    );
  }
  getMaterial(id: number): Observable<Material> {
    return this.http.get<Material>(this.materialUrl + id);
  }

  updateMaterial(id: string, name: string, matImage: File): Observable<any> {
    const formData = new FormData();

    formData.append('id', id);
    formData.append('name', name);
    formData.append('materialImage', matImage);

    return this.http.patch<any>(this.materialUrl, formData);
  }
}
