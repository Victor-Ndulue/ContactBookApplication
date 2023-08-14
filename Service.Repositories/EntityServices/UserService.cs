using AutoMapper;
using RepositoryUnitOfWork.Contract;
using Shared.DTOs.UserDTOs;
using Shared.PaginationDefiners;
using Shared.Utilities;

namespace Service.Repositories.EntityServices
{
    public class UserService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<StandardResponse<UserDisplayDto>> GetUserByEmail(string email, bool trackChanges)
        {
            var user = await _unitOfWork.UserQueryRepository.GetUserByEmailAsync(email, trackChanges);
            if (user == null)  StandardResponse<UserDisplayDto>.Failed("User does not exist"); 
            var mapUser = _mapper.Map<UserDisplayDto>(user);
            return StandardResponse<UserDisplayDto>.Success("Successful", mapUser, 200);
        }

        public async Task<StandardResponse<PagedList<UserDisplayDto>>> GetAllUsers(PaginationParams paginationParams ,bool trackChanges) 
        {
            var users = _unitOfWork.UserQueryRepository.GetAllUsers(trackChanges);
            var mapUsers = _mapper.Map<IQueryable<UserDisplayDto>>(users);
            var pageListUsers = await PagedList<UserDisplayDto>.CreateAsync(mapUsers, paginationParams.PageNumber, paginationParams.PageSize);
            return StandardResponse<PagedList<UserDisplayDto>>.Success("successful", pageListUsers, 200);
        }
    }
}
