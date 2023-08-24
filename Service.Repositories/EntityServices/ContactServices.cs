using AutoMapper;
using AutoMapper.QueryableExtensions;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using RepositoryUnitOfWork.Contract;
using Service.Contracts.IEntityServices;
using Shared.DTOs.ContactDTOs;
using Shared.PaginationDefiners;
using Shared.Utilities;

namespace Service.Repositories.EntityServices
{
    public class ContactServices : IContactServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Cloudinary _cloudinary;
        private readonly IMapper _mapper;

        public ContactServices(IUnitOfWork unitOfWork, Cloudinary cloudinary, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _cloudinary = cloudinary;
            _mapper = mapper;
        }

        public async Task<StandardResponse<ContactDisplayDto>> AddContactAsync(string email, ContactDtoForCreation dtoForCreation)
        {
            var user = await _unitOfWork.UserQueryRepository.GetUserByEmailAsync(email, false);
            if (dtoForCreation == null) return StandardResponse<ContactDisplayDto>.Failed("input field cannot be null");
            var contact = _mapper.Map<Contact>(dtoForCreation);
            await _unitOfWork.ContactRepository.CreateAsync(contact);
            await _unitOfWork.SaveAsync();
            user.Contacts.Add(contact);
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveAsync();
            var response = _mapper.Map<ContactDisplayDto>(contact);
            return StandardResponse<ContactDisplayDto>.Success("successfully uploaded", response, 201);
        }

        public async Task<StandardResponse<ContactDisplayDto>> UpdateContactAsync (string contactNameToUpdate, ContactDtoForUpdate dtoForUpdate)
        {
            if (contactNameToUpdate ==null || dtoForUpdate == null) return StandardResponse<ContactDisplayDto>.Failed("input fields cannot be empty");
            var contact = await _unitOfWork.ContactQueryRepository.GetContactByContactNameAsync(contactNameToUpdate, false);

           
            //if (contact.Photos.Count > 0 ) 
            //{
            //    var photos = contact.Photos;
            //    foreach (var pic in photos)
            //    {
            //        contact.Photos.Remove(pic);
            //        var deleteParams = new DeletionParams(pic.PublicId);
            //        _unitOfWork.ContactRepository.Update(contact);
            //        _unitOfWork.PhotoRepository.Delete(pic);
            //        await _cloudinary.DestroyAsync(deleteParams);
            //        await _unitOfWork.SaveAsync();
            //    }
            //}

            var updatedContact = _mapper.Map(dtoForUpdate, contact);
            _unitOfWork.ContactRepository.Update(updatedContact);
            await _unitOfWork.SaveAsync();
            var response = _mapper.Map<ContactDisplayDto>(updatedContact);
            return StandardResponse<ContactDisplayDto>.Success("Contact updated successfully", response, 201);
        }

        public async Task<String> AddContactPhoto(IFormFile PhotoFile, string contactNameToUpdate)
        {
            var contact = await _unitOfWork.ContactQueryRepository.GetContactByContactNameAsync(contactNameToUpdate, false);
            var uploadResult = new ImageUploadResult();

            var formFile = PhotoFile;
            if (formFile.Length > 0)
            {
                using var stream = formFile.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(formFile.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face"),
                    Folder = "da-net7"
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }
            var result = uploadResult;

            if (result.Error != null) return null;
            {
                var photo = new Photo
                {
                    Url = result.SecureUrl.AbsoluteUri,
                    PublicId = result.PublicId,
                };
                contact.Photos.Add(photo);
                _unitOfWork.ContactRepository.Update(contact);
                await _unitOfWork.SaveAsync();
            }
            return "update successful";
        }

        public async Task<StandardResponse<string>> DeleteContact (string userName, string contactName)
        {
            var users =  _unitOfWork.UserQueryRepository.GetAllUsers(false);
            var user = await users.SingleOrDefaultAsync(u => u.UserName == userName);
            var contact = user.Contacts.SingleOrDefault(t=>t.ContactName == contactName);
            if (contact == null) return StandardResponse<string>.Failed("contact name does not exist");
            if (contact.Photos.Count > 0)
            {
                var photos = contact.Photos;
                foreach (var pic in photos)
                {
                    contact.Photos.Remove(pic);
                    var deleteParams = new DeletionParams(pic.PublicId);
                    _unitOfWork.ContactRepository.Update(contact);
                    _unitOfWork.PhotoRepository.Delete(pic);
                    await _cloudinary.DestroyAsync(deleteParams);
                    await _unitOfWork.SaveAsync();
                }
            }
            user.Contacts.Remove(contact);
            _unitOfWork.ContactRepository.Delete(contact);
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveAsync();
            return StandardResponse<string>.Success("successful", $"{contactName} successfully deleted", 200);
        }

        public async Task<StandardResponse<PagedList<ContactDisplayDto>>> GetAllContacts (PaginationParams paginationParams, bool trackChanges) 
        {
            var contacts = _unitOfWork.ContactQueryRepository.GetAllContacts(trackChanges);
            var mapContacts = contacts.ProjectTo<ContactDisplayDto>(_mapper.ConfigurationProvider);
            var pagedContacts = await PagedList<ContactDisplayDto>.CreateAsync(mapContacts, paginationParams.PageNumber, paginationParams.PageSize);
            return StandardResponse<PagedList<ContactDisplayDto>>.Success("successful", pagedContacts, 200);
        }

        public async Task<StandardResponse<PagedList<ContactDisplayDto>>> GetContactsByKeyWord(string keyword ,PaginationParams paginationParams, bool trackChanges)
        {
            var contacts = _unitOfWork.ContactQueryRepository.GetContactByKeyword(keyword, trackChanges);
            var mapContacts = _mapper.Map<IQueryable<ContactDisplayDto>>(contacts);
            var pagedContacts = await PagedList<ContactDisplayDto>.CreateAsync(mapContacts, paginationParams.PageNumber, paginationParams.PageSize);
            return StandardResponse<PagedList<ContactDisplayDto>>.Success("successful", pagedContacts, 200);
        }

        public async Task<StandardResponse<ContactDisplayDto>> GetContactByContactName(string contactName, bool trackChanges)
        {
            var contacts = await _unitOfWork.ContactQueryRepository.GetContactByContactNameAsync(contactName, trackChanges);
            var response = _mapper.Map<ContactDisplayDto>(contacts);
            return StandardResponse<ContactDisplayDto>.Success("successful", response, 200);
        }
    }
}
