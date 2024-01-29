import { Observable } from 'rxjs';
import { Material } from 'src/app/shared/model/material/material.model';
import { PaginatedResult } from '../../shared/model/pagination/pagination.model';

export abstract class IMaterialApi {
  abstract createMaterial(name: string, matImage: File): Observable<any>;
  abstract getMaterials(
    name: string | null,
    pageNumber: number | null,
    itemsPerPage: number | null
  ): Observable<PaginatedResult<Material[]>>;
  abstract getMaterial(id: number): Observable<Material>;
  abstract updateMaterial(
    id: string,
    name: string,
    matImage: File
  ): Observable<any>;
}
