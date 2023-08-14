using Contracts;
using Contracts.Common;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Repositories.Common;

namespace Repositories.EntityRepositories
{
    public class UserRepository : GenericRepository<User>,IUserRepository
    {
        public UserRepository(DataContext dataContext) : base(dataContext)
        {
        }       
    }
}
