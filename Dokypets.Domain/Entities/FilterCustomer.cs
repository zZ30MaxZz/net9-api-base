namespace Dokypets.Domain.Entities
{
    public class FilterCustomer
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 3;
        public string? CompanyName { get; set; }
        public string? ContactName { get; set; }
        public string? ContactTitle { get; set; }
    }
}