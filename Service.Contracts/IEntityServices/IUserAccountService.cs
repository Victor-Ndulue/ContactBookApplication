﻿using Shared.DTOs;
using Shared.DTOs.UserDTOs;
using Shared.Utilities;

namespace Service.Contracts.IEntityServices
{
    public interface IUserAccountService
    {
        Task<StandardResponse<LoginResponse>> Login(UserDtoForLogin userDtoForLogin);
        Task<StandardResponse<UserDisplayDto>> CreateUser(UserDtoForCreation userDtoForCreation);
    }
}
