using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Models.Entities;
using RepositoryUnitOfWork.Contract;
using Shared.DTOs;
using Shared.DTOs.UserDTOs;
using Shared.Utilities;

namespace Service.Repositories.EntityServices
{
    public class UserLoginService
    {
        private readonly UserManager<User> _user;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserLoginService(IUnitOfWork unitOfWork, UserManager<User> user, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _user = user;
            _mapper = mapper;
        }
        
        public async Task<StandardResponse<UserDisplayDto>> CreateUser(UserDtoForCreation userDtoForCreation)
        {
            if (userDtoForCreation == null) 
                return StandardResponse<UserDisplayDto>.Failed("user creation request cannot be null");

            User user = _mapper.Map<User>(userDtoForCreation);
            await _unitOfWork.UserRepository.CreateAsync(user);
            await _unitOfWork.SaveAsync();
            var response = _mapper.Map<UserDisplayDto>(user);
            return StandardResponse<UserDisplayDto>.Success("User account successfully created", response, 200);
        }

        public async Task<StandardResponse<LoginResponse>> Login(UserDtoForLogin userDtoForLogin) 
        {
            if (userDtoForLogin == null) 
                return StandardResponse<LoginResponse>.Failed("login details cannot be left vacant");
            User user = await _unitOfWork.UserQueryRepository.GetUserByEmailAsync(userDtoForLogin.Email, false);
            if (user == null) return StandardResponse<LoginResponse>.Failed("Invalid email or password");
            var result = await _user.CheckPasswordAsync(user, userDtoForLogin.Password);
            if (result == false) return StandardResponse<LoginResponse>.Failed("Invalid email or password");
            LoginResponse loginResponse = new LoginResponse();
            loginResponse.UserName = user.UserName;
            loginResponse.Token = _user.GenerateNewAuthenticatorKey();
            return StandardResponse<LoginResponse>.Success($"welcome {user.UserName}", loginResponse, 200);
        }
    }
}
