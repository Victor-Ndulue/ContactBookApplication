using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.Entities;
using RepositoryUnitOfWork.Contract;
using Service.Contracts.IEntityServices;
using Shared.DTOs;
using Shared.DTOs.UserDTOs;
using Shared.Utilities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service.Repositories.EntityServices
{
    public class UserAccountService : IUserAccountService
    {
        private readonly UserManager<User> _user;
        private readonly SignInManager<User> _signInManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserAccountService(IUnitOfWork unitOfWork, UserManager<User> user, SignInManager<User> signInManager, IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _user = user;
            _signInManager = signInManager;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<StandardResponse<UserDisplayDto>> CreateUser(UserDtoForCreation userDtoForCreation)
        {
            if (userDtoForCreation == null) 
                return StandardResponse<UserDisplayDto>.Failed("user creation request cannot be null");
            var res = _user.FindByNameAsync(userDtoForCreation.UserName);
            if(res!=null)
                return StandardResponse<UserDisplayDto>.Failed("username already exists");
            User user = _mapper.Map<User>(userDtoForCreation);
            var result = await _user.CreateAsync(user, userDtoForCreation.Password);
            
            if (result.Succeeded)
            {
                var response = _mapper.Map<UserDisplayDto>(user);
                return StandardResponse<UserDisplayDto>.Success("User account successfully created", response, 200);
            }
            return StandardResponse<UserDisplayDto>.Failed("faileed to create user account");
        }

        public async Task<StandardResponse<LoginResponse>> Login(UserDtoForLogin userDtoForLogin)
        {
            if (userDtoForLogin == null)
                return StandardResponse<LoginResponse>.Failed("login details cannot be left vacant");
            User user = await _unitOfWork.UserQueryRepository.GetUserByEmailAsync(userDtoForLogin.Email, false);
            if (user == null) return StandardResponse<LoginResponse>.Failed("Invalid email or password");
            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, userDtoForLogin.Password,lockoutOnFailure: false);
            if (signInResult.Succeeded)
            {
                var userClaims = await _user.GetClaimsAsync(user);

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["TokenKey"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(userClaims),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                // Create the LoginResponse object with the generated token
                LoginResponse loginResponse = new LoginResponse();
                loginResponse.UserName = user.UserName;
                loginResponse.Token = tokenString;

                return StandardResponse<LoginResponse>.Success($"welcome {user.UserName}", loginResponse, 200);
            }
            return StandardResponse<LoginResponse>.Failed("Invalid email or password");
        }
    }
 }



/*
 *         var signInResult = await _signInManager.PasswordSignInAsync(user.Email, userDtoForLogin.Password, isPersistent: false, lockoutOnFailure: false);
        if (signInResult.Succeeded)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["TokenKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(userClaims),
                Expires = DateTime.UtcNow.AddDays(7), // Token expiration time
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // Create the LoginResponse object with the generated token
            LoginResponse loginResponse = new LoginResponse();
            loginResponse.UserName = user.UserName;
            loginResponse.Token = tokenString;

            return StandardResponse<LoginResponse>.Success($"welcome {user.UserName}", loginResponse, 200);
        }

*/