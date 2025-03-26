using Dokypets.Application.Interface.Persistence;
using Dokypets.Application.Interface.Persistence.Identity;
using Dokypets.Domain.Entities.Identity;
using Dokypets.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Dokypets.Infrastructure.Repositories.Identity
{
    public class UserTokenRepository : IUserTokenRepository
    {
        private readonly ApplicationDbContext _applicationContext;

        public UserTokenRepository(ApplicationDbContext applicationContext)
        {
            _applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ApplicationUserToken>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationUserToken> GetAsync(Guid id)
        {
            var response = await _applicationContext.UserTokens.FirstOrDefaultAsync(s => s.UserId == id.ToString());

            return response;
        }

        public async Task<bool> InsertAsync(ApplicationUserToken entity)
        {
            _applicationContext.UserTokens.Add(entity);

            await _applicationContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateAsync(ApplicationUserToken entity)
        {
            _applicationContext.UserTokens.Update(entity);

            await _applicationContext.SaveChangesAsync();

            return true;
        }
    }
}
