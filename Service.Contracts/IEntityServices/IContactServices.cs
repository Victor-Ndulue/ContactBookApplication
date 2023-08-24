using Microsoft.AspNetCore.Http;
using Shared.DTOs.ContactDTOs;
using Shared.PaginationDefiners;
using Shared.Utilities;

namespace Service.Contracts.IEntityServices
{
    public interface IContactServices
    {
        Task<StandardResponse<ContactDisplayDto>> GetContactByContactName(string contactName, bool trackChanges);
        Task<StandardResponse<PagedList<ContactDisplayDto>>> GetContactsByKeyWord(string keyword, PaginationParams paginationParams, bool trackChanges);
        Task<StandardResponse<PagedList<ContactDisplayDto>>> GetAllContacts(PaginationParams paginationParams, bool trackChanges);
        Task<StandardResponse<string>> DeleteContact(string email, string contactName);
        Task<String> AddContactPhoto(IFormFile PhotoFile, string contactNameToUpdate);
        Task<StandardResponse<ContactDisplayDto>> UpdateContactAsync(string contactNameToUpdate, ContactDtoForUpdate dtoForUpdate);
        Task<StandardResponse<ContactDisplayDto>> AddContactAsync(string email, ContactDtoForCreation dtoForCreation);
    }
}
