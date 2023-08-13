using Contracts.Common;
using Models.Entities;

namespace Contracts
{
    public interface IUserRepository : IGenericRepository<User>
    {
        IQueryable<User> GetAllUsers(bool trackChanges);
        Task<User> GetUserByIdAsync(int id, bool trackChanges);
        Task<User> GetUserByEmailAsync(string emailAddress, bool trackChanges);
    }
}
