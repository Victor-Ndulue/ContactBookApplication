using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts.IEntityServices;
using Shared.DTOs.ContactDTOs;
using Shared.PaginationDefiners;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Authorize]
    public class ContactController : BaseController
    {
        private readonly IContactServices _contactServices;
        public ContactController(IContactServices contactServices)
        {
            _contactServices = contactServices;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddContactAsync([FromForm] ContactDtoForCreation contactDto)
        {
            try
            {
                var userName = User.GetUsername();
                var result = await _contactServices.AddContactAsync(userName, contactDto);
                return Ok(result);
            }
            catch (Exception ex) { return Ok(ex.ToString()); }
        }

        [HttpPut]
        [Route("update/{oldContactName}")]
        public async Task<IActionResult> UpdateContact(string oldContactName, ContactDtoForUpdate contactDto )
        {
            var result = await _contactServices.UpdateContactAsync(oldContactName, contactDto);
            return Ok(result);
        }


        [HttpPut]
        [Route("addcontactphoto/{oldContactName}")]
        public async Task<IActionResult> AddContactPhoto(IFormFile PhotoFile, string oldContactName)
        {
            var result = await _contactServices.AddContactPhoto(PhotoFile, oldContactName);
            return Ok(result);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteContact(string contactNameToDelete)
        {
            var user = User.GetUsername();
            var result = await _contactServices.DeleteContact(user, contactNameToDelete);
            return Ok(result);
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetAllContacts([FromQuery] PaginationParams paginationParams, bool trackChanges)
        {
            var result = await _contactServices.GetAllContacts(paginationParams, trackChanges);
            return Ok(result);
        }

        [HttpGet]
        [Route("getcontactbykeyword/filter/{keyword}")]
        public async Task<IActionResult> GetContactByKeyword([FromQuery] string keyword, PaginationParams paginationParams, bool trackChanges)
        {
            var result = await _contactServices.GetContactsByKeyWord(keyword, paginationParams, trackChanges);
            return Ok(result);
        }

        [HttpGet]
        [Route("contact/filter/{contactname}")]
        public async Task<IActionResult> GetContactByContactName([FromQuery] string contactname, bool trackChanges)
        {
            var result = await _contactServices.GetContactByContactName(contactname, trackChanges);
            return Ok(result);
        }
    }
}
