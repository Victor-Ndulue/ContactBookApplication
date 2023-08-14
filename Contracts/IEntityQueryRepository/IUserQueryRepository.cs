using Contracts.Common;
using Models.Entities;

namespace Contracts.IEntityQueryRepository
{
    public interface IUserQueryRepository:IRepositoryBase<User>
    {
        IQueryable<User> GetAllUsers(bool trackChanges);
        Task<User> GetUserByEmailAsync(string emailAddress, bool trackChanges);
        Task<User> GetUserByIdAsync(int id, bool trackChanges);
    }
}
