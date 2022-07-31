using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CraftParty.Application.Interfaces.Authentication;
using CraftParty.Application.Interfaces.Data;
using CraftParty.Application.Interfaces.Services;
using CraftParty.Application.Models.Authentication;
using CraftParty.Domain.Entities;
using CraftParty.Domain.Enums;
using CraftParty.Infrastructure.Common;
using CraftParty.Infrastructure.Constants;
using CraftParty.Infrastructure.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CraftParty.Infrastructure.Services.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUnitOfWork _unitOfWork;

    public JwtTokenGenerator(
        IDateTimeProvider dateTimeProvider,
        IOptions<JwtSettings> jwtSettingsOptions,
        IUnitOfWork unitOfWork)
    {
        _dateTimeProvider = dateTimeProvider;
        _unitOfWork = unitOfWork;
        _jwtSettings = jwtSettingsOptions.Value;
    }

    public async Task<TokensDataModel> GenerateToken(IdentityUser user)
    {
        var jwtHandler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            }),
            Expires = _dateTimeProvider.UtcNow.Add(_jwtSettings.ExpiryTimeFrame),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = jwtHandler.CreateToken(tokenDescriptor);
        var jwtToken = jwtHandler.WriteToken(token);

        var refreshToken = new RefreshToken
        {
            AddedDate = DateTime.Now,
            Token = $"{StringService.GenerateRandomString(TokensConstants.RefreshTokenLength)}_{Guid.NewGuid()}",
            IdentityUserId = user.Id,
            IsRevoked = false,
            IsUsed = false,
            ActivityStatus = EntityActivityStatus.Active,
            JwtId = token.Id,
            ExpiryDate = DateTime.Now.AddMonths(TokensConstants.RefreshTokenLengthInMonth),
        };

        await _unitOfWork.RefreshTokens.AddAsync(refreshToken);

        return new TokensDataModel
        {
            JwtToken = jwtToken,
            RefreshToken = refreshToken.Token,
        };
    }
}