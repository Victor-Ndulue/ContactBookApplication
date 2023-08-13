using Contracts.Common;
using Models.Entities;

namespace Contracts
{
    public interface IContactRepository : IGenericRepository<Contact>
    {
        Task<Contact> GetContactByIdAsync(int id, bool trackChanges);
        Task<Contact> GetContactByEmailAsync(string emailAddress, bool trackChanges);
        IQueryable<Contact> GetAllContacts(bool trackChanges);
    }
}
