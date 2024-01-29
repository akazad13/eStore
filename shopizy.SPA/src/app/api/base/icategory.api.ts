import { Observable } from 'rxjs';
import { Category } from 'src/app/shared/model/category/category.model';
import { PaginatedResult } from '../../shared/model/pagination/pagination.model';

export abstract class ICategoryApi {
  abstract createCategory(name: string): Observable<any>;
  abstract getCategories(
    name: string | null,
    page: number | null,
    itemsPerPage: number | null
  ): Observable<PaginatedResult<Category[]>>;
  abstract getCategory(id: number): Observable<Category>;
  abstract updateCategory(id: number, name: string): Observable<any>;
}
