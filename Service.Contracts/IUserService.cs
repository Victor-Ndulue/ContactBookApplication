using Shared.DTOs.UserDTOs;
using Shared.PaginationDefiners;
using Shared.Utilities;

namespace Service.Contracts
{
    public interface IUserService
    {
        Task<StandardResponse<UserDisplayDto>> GetUserById(int id, bool trackChanges);
        Task<StandardResponse<PagedList<UserDisplayDto>>> GetAllUsers(PaginationParams paginationParams, bool trackChanges);
        Task<StandardResponse<UserDisplayDto>> GetUserByEmail(string email, bool trackChanges);
    }
}
