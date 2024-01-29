export interface Pagination {
  currentPage: number;
  pageSize: number;
  totalItems: number;
  totalPages: number;
}

export class PaginatedResult<T> {
  items!: T;
  pagination!: Pagination;
}

export class PaginationDefaults {
  public static readonly ITEM_PER_PAGE = 10;
}
