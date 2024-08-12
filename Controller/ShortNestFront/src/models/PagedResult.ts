export class PagedResult<T> {
  public items: T[] = [];
  public totalCount: number = 0;
  public page: number = 1;
  public pageSize: number = 5;
}