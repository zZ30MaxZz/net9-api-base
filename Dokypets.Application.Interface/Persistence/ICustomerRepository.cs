using Dokypets.Domain.Entities;

namespace Dokypets.Application.Interface.Persistence
{
    public interface ICustomerRepository: IGenericRepository<Customer>
    {
        Task<int> CountAsync();
        Task<(IEnumerable<Customer> Records, int TotalRecords)> GetAllWithPaginationAsync(FilterCustomer filter);
    }
}
