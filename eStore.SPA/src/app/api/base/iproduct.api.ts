import { Observable } from 'rxjs';
import { PaginatedResult } from '../../shared/model/pagination/pagination.model';
import { Product } from '../../shared/model/Product/Product.model';

export abstract class IProductApi {
  abstract createProduct(
    name: string,
    description: string,
    productDetails: string,
    categoryId: number,
    sku: string,
    price: number,
    discount: number,
    tags: string,
    productImages: File[]
  ): Observable<any>;
  abstract getProducts(
    name: string | null,
    categories: string | null,
    materials: string | null,
    sortby: string | null,
    pageNumber: number | null,
    itemsPerPage: number | null
  ): Observable<PaginatedResult<Product[]>>;
  // abstract getEventDetails(id: number): Observable<GameDetails>;
  abstract updateProduct(
    id: number,
    title: string,
    location: string,
    scheduledOn: Date,
    maxPlayers: number,
    isGameStarted: boolean,
    signupStartsFrom?: Date
  ): Observable<any>;
}
