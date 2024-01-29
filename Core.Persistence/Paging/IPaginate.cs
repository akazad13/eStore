using eStore.Domain.Paging;

public interface IPaginate<T>
{
    public Pagination Pagination { get; set; }
    public IList<T> Items { get; set; }
}
