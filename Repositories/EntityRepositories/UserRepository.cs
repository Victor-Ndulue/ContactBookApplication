using Contracts;
using Contracts.Common;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Repositories.Common;

namespace Repositories.EntityRepositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly IRepositoryBase<User> _repository;

        public UserRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public IQueryable<User> GetAllUsers(bool trackChanges)
        {
             var users = _repository.GetAll(trackChanges);
            return users;
        }

        public async Task<User> GetUserByEmailAsync(string emailAddress, bool trackChanges)
        {
            var user = await _repository.GetByCondition(u => u.Email == emailAddress, trackChanges).SingleAsync();
            return user;
        }

        public async Task<User> GetUserByIdAsync(int id, bool trackChanges)
        {
            var user = await _repository.GetByCondition(u => u.Id == id, trackChanges).SingleAsync();
            return user;
        }
    }
}
