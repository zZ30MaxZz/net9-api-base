using Dokypets.Application.Interface.Persistence.Identity;

namespace Dokypets.Application.Interface.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        #region
        IUserRepository Users { get; }
        IUserTokenRepository UserTokens { get; }
        #endregion

        ICustomerRepository Customers { get; }
    }
}
