namespace Dima.Core.Requests;

public abstract class PagedRequest : Requests.Request
{
    public int PageNumber { get; set; } = Configurations.DefaultPageNumber;
    public int PageSize { get; set; } = Configurations.DefaultPageSize;
}