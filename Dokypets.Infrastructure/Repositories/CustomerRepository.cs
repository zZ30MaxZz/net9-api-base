using Dokypets.Application.Interface.Persistence;
using Dokypets.Domain.Entities;
using Dokypets.Domain.Entities.Pagination;
using Dokypets.Infrastructure.Contexts;
using Dokypets.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Dokypets.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _applicationContext;

        public CustomerRepository(ApplicationDbContext applicationContext)
        {
            _applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
        }

        #region Queries
        /*Queries*/
        public async Task<int> CountAsync()
        {
            var count = await _applicationContext.Customers.CountAsync();

            return count;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            var customers = await _applicationContext.Customers.ToListAsync();

            return customers;
        }

        public async Task<(IEnumerable<Customer> Records, int TotalRecords)> GetAllWithPaginationAsync(FilterCustomer filter)
        {
            var customerQueryable = _applicationContext.Customers.AsQueryable();

            

            if (!string.IsNullOrWhiteSpace(filter.CompanyName))
            {
                customerQueryable = customerQueryable
                    .Where(w => w.CompanyName.ToLower().Contains(filter.CompanyName.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(filter.ContactName))
            {
                customerQueryable = customerQueryable
                    .Where(w => w.ContactName.ToLower().Contains(filter.ContactName.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(filter.ContactTitle))
            {
                customerQueryable = customerQueryable
                    .Where(w => w.ContactTitle.ToLower().Contains(filter.ContactTitle.ToLower()));
            }

            var records = await customerQueryable.Pagination(new Pagination { PageNumber = filter.PageNumber, PageSize = filter.PageSize }).ToListAsync();

            int totalRecords = await customerQueryable.CountAsync();

            return (records, totalRecords);
        }

        public async Task<Customer> GetAsync(Guid id)
        {
            var customer = await _applicationContext.Customers.FirstOrDefaultAsync(x => x.Id == id);

            return customer;
        }
        #endregion

        #region Commands
        /*Commands*/

        public async Task<bool> InsertAsync(Customer entity)
        {
            _applicationContext.Add(entity);

            await _applicationContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateAsync(Customer entity)
        {
            var entityDb = await _applicationContext.Customers.FindAsync(entity.Id);

            entityDb.CompanyName = entity.CompanyName;
            entityDb.ContactName = entity.ContactName;
            entityDb.ContactTitle = entity.ContactTitle;
            entityDb.Address = entity.Address;
            entityDb.City = entity.City;
            entityDb.Region = entity.Region;
            entityDb.PostalCode = entity.PostalCode;
            entityDb.Country = entity.Country;
            entityDb.Phone = entity.Phone;
            entityDb.Fax = entity.Fax;

            await _applicationContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var customer = await _applicationContext.Customers.FindAsync(id);
            if (customer == null) return false;

            _applicationContext.Customers.Remove(customer);
            await _applicationContext.SaveChangesAsync();

            return true;
        }

        #endregion
    }
}
