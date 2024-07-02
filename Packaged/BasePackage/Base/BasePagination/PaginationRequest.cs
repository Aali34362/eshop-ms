namespace Base.BasePagination;

public record PaginationRequest(int PageIndex = 0, int PageSize = 10);
