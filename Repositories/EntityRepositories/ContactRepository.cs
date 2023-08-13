using Contracts;
using Contracts.Common;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Repositories.Common;

namespace Repositories.EntityRepositories
{
    public class ContactRepository : GenericRepository<Contact>, IContactRepository
    {
        private readonly IRepositoryBase<Contact> _repository;

        public ContactRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public IQueryable<Contact> GetAllContacts(bool trackChanges) 
        {
            var contacts = _repository.GetAll(trackChanges);
            return contacts;
        }

        public async Task<Contact> GetContactByEmailAsync(string emailAddress, bool trackChanges)
        {
            var contact = await _repository.GetByCondition(u => u.ContactEmail == emailAddress, trackChanges).SingleAsync();
            return contact;
        }

        public async Task<Contact> GetContactByIdAsync(int id, bool trackChanges)
        {
            var contact = await _repository.GetByCondition(u => u.Id == id, trackChanges).SingleAsync();
            return contact;
        }
    }
}
