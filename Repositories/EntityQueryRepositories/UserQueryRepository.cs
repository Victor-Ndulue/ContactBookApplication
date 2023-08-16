using Contracts.IEntityQueryRepository;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Repositories.Common;

namespace Repositories.EntityQueryRepositories
{
    public class UserQueryRepository : RepositoryBase<User>, IUserQueryRepository
    {
        public UserQueryRepository(DataContext context) : base(context)
        {
        }

        public IQueryable<User> GetAllUsers(bool trackChanges)
        {
            return GetAll(trackChanges);
        }

        public async Task<User> GetUserByEmailAsync(string emailAddress, bool trackChanges)
        {
            return await GetByCondition(u=>u.Email==emailAddress, trackChanges).Include(u=>u.Contacts).SingleOrDefaultAsync();
        }

        public async Task<User> GetUserByIdAsync(int id, bool trackChanges)
        {
            return await GetByCondition(u=>u.Id==id, trackChanges).SingleOrDefaultAsync();
        }
    }
}
