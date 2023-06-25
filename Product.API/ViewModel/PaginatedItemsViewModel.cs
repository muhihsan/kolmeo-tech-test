namespace API.ViewModel;

public class PaginatedItemsViewModel<TEntity> where TEntity : class
{
    public int PageSize { get; }

    public int PageIndex { get; }

    public long Count { get; }

    public IEnumerable<TEntity> Data { get; }

    public PaginatedItemsViewModel(int pageSize, int pageIndex, long count, IEnumerable<TEntity> data)
    {
        PageSize = pageSize;
        PageIndex = pageIndex;
        Count = count;
        Data = data;
    }
}
