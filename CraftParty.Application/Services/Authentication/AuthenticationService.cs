using AutoMapper;
using CraftParty.Application.Interfaces.Authentication;
using CraftParty.Application.Interfaces.Data;
using CraftParty.Application.Models.Authentication;
using CraftParty.Domain.Common.Errors;
using CraftParty.Domain.Entities;
using ErrorOr;
using Microsoft.AspNetCore.Identity;

namespace CraftParty.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthenticationService(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        UserManager<IdentityUser> userManager,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ErrorOr<AuthenticationResult>> Login(LoginModel model)
    {
        var user = await _unitOfWork.UsersRepository.GetUserByEmailAsync(model.Email);

        if (user is null)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        if (!await _userManager.CheckPasswordAsync(user.IdentityUser, model.Password))
        {
            return Errors.Authentication.InvalidCredentials;
        }

        var tokensDataModel = await _jwtTokenGenerator.GenerateToken(user.IdentityUser);
        
        return _mapper.Map<AuthenticationResult>(user) with
        {
            Token = tokensDataModel.JwtToken,
            RefreshToken = tokensDataModel.RefreshToken,
        };
    }

    public async Task<ErrorOr<AuthenticationResult>> Register(RegisterModel model)
    {
        if (await _unitOfWork.UsersRepository.GetUserByEmailAsync(model.Email) is not null)
        {
            return Errors.Authentication.DuplicateEmail;
        }

        var identityUser = _mapper.Map<IdentityUser>(model);

        var userCreationResult = await _userManager.CreateAsync(identityUser, model.Password);

        if (!userCreationResult.Succeeded)
        {
            return Errors.User.CreationIssue;
        }

        var user = _mapper.Map<User>(model);
        user.IdentityId = identityUser.Id;

        await _unitOfWork.UsersRepository.AddAsync(user);

        var tokensDataModel = await _jwtTokenGenerator.GenerateToken(user.IdentityUser);

        return _mapper.Map<AuthenticationResult>(user) with
        {
            Token = tokensDataModel.JwtToken,
            RefreshToken = tokensDataModel.RefreshToken,
        };
    }
}