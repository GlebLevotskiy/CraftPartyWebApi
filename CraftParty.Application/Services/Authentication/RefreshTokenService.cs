using AutoMapper;
using CraftParty.Application.Interfaces.Authentication;
using CraftParty.Application.Interfaces.Authentication.Validators;
using CraftParty.Application.Interfaces.Data;
using CraftParty.Application.Models.Authentication;
using CraftParty.Domain.Common.Errors;
using ErrorOr;
using Microsoft.AspNetCore.Identity;

namespace CraftParty.Application.Services.Authentication;

public class RefreshTokenService : IRefreshTokenService
{
    private readonly ITokenValidator _tokenValidator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IMapper _mapper;

    public RefreshTokenService(
        ITokenValidator tokenValidator,
        IUnitOfWork unitOfWork,
        UserManager<IdentityUser> userManager,
        IJwtTokenGenerator jwtTokenGenerator,
        IMapper mapper)
    {
        _tokenValidator = tokenValidator;
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _jwtTokenGenerator = jwtTokenGenerator;
        _mapper = mapper;
    }

    public async Task<ErrorOr<TokenRefreshResult>> RefreshToken(RefreshTokenModel refreshTokenModel)
    {
        var tokenValidationResult = await _tokenValidator.Validate(refreshTokenModel);

        if (tokenValidationResult.IsError)
        {
            return tokenValidationResult.Errors;
        }   

        var refreshToken = tokenValidationResult.Value;

        refreshToken.IsUsed = true;
        _unitOfWork.RefreshTokens.Update(refreshToken);

        var user = await _userManager.FindByIdAsync(refreshToken.IdentityUserId);

        if (user == null)
        {
            return Errors.Authentication.InvalidTokenUser;
        }

        return _mapper.Map<TokenRefreshResult>(await _jwtTokenGenerator.GenerateToken(user));
    }
}