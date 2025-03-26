namespace Dokypets.Application.Dto.Pagination
{
    public class PaginationDto<T>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 3;
        public int TotalRecords { get; set; } = 0;
        public T Data { get; set; }
    }
}
