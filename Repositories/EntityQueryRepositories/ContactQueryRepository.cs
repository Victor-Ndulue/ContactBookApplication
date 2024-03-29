﻿using Contracts.IEntityQueryRepository;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Repositories.Common;

namespace Repositories.EntityQueryRepositories
{
    public class ContactQueryRepository : RepositoryBase<Contact>, IContactQueryRepository
    {
        public ContactQueryRepository(DataContext context) : base(context)
        {
        }

        public IQueryable<Contact> GetAllContacts(bool trackChanges)
        {
            var contacts = GetAll(trackChanges).Include(p => p.Photos);
            return contacts;
        }

        public async Task<Contact> GetContactByEmailAsync(string emailAddress, bool trackChanges)
        {
            var contact = await GetByCondition(u => u.ContactEmail == emailAddress, trackChanges).Include(p=>p.Photos).SingleOrDefaultAsync();
            return contact;
        }
        public async Task<Contact> GetContactByContactNameAsync(string contactName, bool trackChanges)
        {
            var contact = await GetByCondition(u => u.ContactName == contactName, trackChanges).Include(p => p.Photos).SingleOrDefaultAsync();
            return contact;
        }

        public async Task<Contact> GetContactByIdAsync(int id, bool trackChanges)
        {
            var contact = await GetByCondition(u => u.Id == id, trackChanges).Include(p => p.Photos).SingleOrDefaultAsync();
            return contact;
        }

        public IQueryable<Contact> GetContactByKeyword(string keyword, bool trackChanges)
        {
            return GetByCondition(c => c.ContactName.Contains(keyword) || c.ContactEmail.Contains(keyword), trackChanges).Include(p => p.Photos);
        }
    }
}