using Contracts.Common;
using Models.Entities;

namespace Contracts.IEntityQueryRepository
{
    public interface IContactQueryRepository : IRepositoryBase<Contact>
    {
        IQueryable<Contact> GetAllContacts(bool trackChanges);
        Task<Contact> GetContactByEmailAsync(string emailAddress, bool trackChanges);

        Task<Contact> GetContactByContactNameAsync(string contactName, bool trackChanges);
        Task<Contact> GetContactByIdAsync(int id, bool trackChanges);
        IQueryable<Contact> GetContactByKeyword(string keyword, bool trackChanges);
    }
}
