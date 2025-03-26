
using Dokypets.Application.Interface.Persistence;
using Dokypets.Application.Interface.Persistence.Identity;

namespace Dokypets.Infrastructure.Repositories
{
    internal class UnitOfWork : IUnitOfWork
    {
        public IUserRepository Users { get; }

        public ICustomerRepository Customers { get; }

        public IUserTokenRepository UserTokens { get; }

        public UnitOfWork(IUserRepository users, ICustomerRepository customers, IUserTokenRepository userTokens)
        {
            Users = users ?? throw new ArgumentNullException(nameof(users));
            Customers = customers ?? throw new ArgumentNullException(nameof(customers));
            UserTokens = userTokens ?? throw new ArgumentNullException(nameof(userTokens));
        }

        public void Dispose()
        {
            System.GC.SuppressFinalize(this);
        }
    }
}
