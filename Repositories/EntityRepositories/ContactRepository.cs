using Contracts;
using Contracts.Common;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Repositories.Common;

namespace Repositories.EntityRepositories
{
    public class ContactRepository : GenericRepository<Contact>, IContactRepository
    {

        public ContactRepository(DataContext dataContext) : base(dataContext)
        {
        }        
    }
}
