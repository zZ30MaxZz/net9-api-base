using Dokypets.Application.Interface.Persistence.Identity;
using Dokypets.Domain.Entities.Identity;
using Dokypets.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Dokypets.Infrastructure.Repositories.Identity
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _applicationContext;

        public UserRepository(ApplicationDbContext applicationContext)
        {
            _applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
        {
            var users = await _applicationContext.Users.ToListAsync();

            return users;
        }

        public Task<ApplicationUser> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAsync(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }


        //public Task<User> GetAync(string client)
        //{
        //    using var connection = _applicationContext.CreateConnection();
        //    var query = "UsersGetByClient";

        //    var parameters = new DynamicParameters();
        //    parameters.Add("Client", client);

        //    var user = connection.QuerySingleOrDefaultAsync<User>(query, param: parameters, commandType: CommandType.StoredProcedure);
        //    return user;
        //}

        //public async Task<bool> InsertAync(User user)
        //{
        //    using var connection = _applicationContext.CreateConnection();
        //    var query = "UsersInsert";
        //    var parameters = new DynamicParameters();
        //    parameters.Add("Id", user.Id);
        //    parameters.Add("Company", user.Company);
        //    parameters.Add("Abbreviation", user.Abbreviation);
        //    parameters.Add("Client", user.Client);
        //    parameters.Add("Secret", user.Secret);

        //    var recordsAffected = await connection.ExecuteAsync(query, param: parameters, commandType: CommandType.StoredProcedure);
        //    return recordsAffected > 0;
        //}
    }
}
