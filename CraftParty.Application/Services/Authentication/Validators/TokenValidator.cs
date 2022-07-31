using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CraftParty.Application.Interfaces.Authentication.Validators;
using CraftParty.Application.Interfaces.Authentication.Validators.RefreshToken;
using CraftParty.Application.Interfaces.Data;
using CraftParty.Application.Models.Authentication;
using CraftParty.Domain.Common.Errors;
using ErrorOr;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace CraftParty.Application.Services.Authentication.Validators;

public class TokenValidator : ITokenValidator
{
    private readonly TokenValidationParameters _tokenValidationParameters;
    private readonly ILogger<TokenValidator> _logger;
    private readonly IEnumerable<IRefreshTokenValidator> _refreshTokenValidators;
    private readonly IJwtSecurityTokenValidator _jwtSecurityTokenValidator;
    private readonly ITokenExpiryDateClaimValidator _tokenExpiryDateClaimValidator;
    private readonly ITokenJtiClaimValidator _tokenJtiClaimValidator;
    private readonly IUnitOfWork _unitOfWork;

    public TokenValidator(
        TokenValidationParameters tokenValidationParameters,
        ILogger<TokenValidator> logger,
        IEnumerable<IRefreshTokenValidator> refreshTokenValidators,
        IUnitOfWork unitOfWork,
        IJwtSecurityTokenValidator jwtSecurityTokenValidator,
        ITokenExpiryDateClaimValidator tokenExpiryDateClaimValidator,
        ITokenJtiClaimValidator tokenJtiClaimValidator)
    {
        _tokenValidationParameters = tokenValidationParameters;
        _logger = logger;
        _refreshTokenValidators = refreshTokenValidators;
        _unitOfWork = unitOfWork;
        _jwtSecurityTokenValidator = jwtSecurityTokenValidator;
        _tokenExpiryDateClaimValidator = tokenExpiryDateClaimValidator;
        _tokenJtiClaimValidator = tokenJtiClaimValidator;
    }

    public async Task<ErrorOr<Domain.Entities.RefreshToken>> Validate(RefreshTokenModel refreshTokenModel)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        ClaimsPrincipal principal;
        
        try
        {
            principal = tokenHandler.ValidateToken(
                refreshTokenModel.Token,
                _tokenValidationParameters,
                out var validatedToken);
            
            if (!_jwtSecurityTokenValidator.IsValid(validatedToken))
            {
                return Errors.Authentication.InvalidToken;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while validating token");
            return Errors.Authentication.InvalidToken;
        }

        if (!_tokenExpiryDateClaimValidator.IsValid(principal))
        {
            return Errors.Authentication.InvalidTokenExpiryDate;
        }
        
        var refreshToken = await _unitOfWork.RefreshTokens.GetByRefreshTokenAsync(refreshTokenModel.RefreshToken);

        if (!_refreshTokenValidators.All(v => v.IsValid(refreshToken)))
        {
            return Errors.Authentication.InvalidRefreshToken;
        }

        if (!_tokenJtiClaimValidator.IsValid(refreshToken.JwtId, principal))
        {
            return Errors.Authentication.InvalidTokenJti;
        }

        return refreshToken;
    }
}