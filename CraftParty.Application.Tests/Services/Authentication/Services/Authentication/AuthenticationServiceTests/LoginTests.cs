using System.Threading.Tasks;
using AutoMapper;
using CraftParty.Application.Interfaces.Authentication;
using CraftParty.Application.Interfaces.Data;
using CraftParty.Application.Models.Authentication;
using CraftParty.Application.Services.Authentication;
using CraftParty.Domain.Common.Errors;
using CraftParty.Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using Moq.AutoMock;
using Tests.Infrastructure.Factories;
using Tests.Infrastructure.Factories.Models.Authentication;
using Tests.Infrastructure.Factories.Models.Entities;
using Xunit;

namespace CraftParty.Application.Tests.Services.Authentication.Services.Authentication.AuthenticationServiceTests;

public class LoginTests
{
    private readonly AuthenticationService _authenticationService;
    
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<UserManager<IdentityUser>> _userManagerMock;
    private readonly Mock<IJwtTokenGenerator> _jwtTokenGeneratorMock;

    public LoginTests()
    {
        var mocker = new AutoMocker();
        _mapperMock = mocker.GetMock<IMapper>();
        _unitOfWorkMock = mocker.GetMock<IUnitOfWork>();
        _userManagerMock = mocker.GetMock<UserManager<IdentityUser>>();
        _jwtTokenGeneratorMock = mocker.GetMock<IJwtTokenGenerator>();
        _authenticationService = mocker.CreateInstance<AuthenticationService>();
    }

    [Fact]
    public async Task IfUserIsNull_ShouldReturnInvalidCredentialsError()
    {
        // Arrange
        var loginModel = TestData.Create.LoginModel();

        _unitOfWorkMock
            .Setup(m => m.UsersRepository.GetUserByEmailAsync(loginModel.Email))
            .ReturnsAsync((User) null);

        // Act
        var result = await _authenticationService.Login(loginModel);

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(Errors.Authentication.InvalidCredentials);
        
        _userManagerMock.Verify(
            m => m.CheckPasswordAsync(
                It.IsAny<IdentityUser>(),
                It.IsAny<string>()),
            Times.Never);
        
        _jwtTokenGeneratorMock.Verify(
            m => m.GenerateToken(It.IsAny<IdentityUser>()),
            Times.Never);
        
        _mapperMock.Verify(
            m => m.Map<AuthenticationResult>(It.IsAny<IdentityUser>()),
            Times.Never);
    }

    [Fact]
    public async Task IfUserPasswordIsInvalid_ShouldReturnInvalidCredentialsError()
    {
        // Arrange
        var loginModel = TestData.Create.LoginModel();
        var user = TestData.Create.User();

        _unitOfWorkMock
            .Setup(m => m.UsersRepository.GetUserByEmailAsync(loginModel.Email))
            .ReturnsAsync(user);

        _userManagerMock
            .Setup(m => m.CheckPasswordAsync(user.IdentityUser, loginModel.Password))
            .ReturnsAsync(false);

        // Act
        var result = await _authenticationService.Login(loginModel);

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(Errors.Authentication.InvalidCredentials);

        _jwtTokenGeneratorMock.Verify(
            m => m.GenerateToken(It.IsAny<IdentityUser>()),
            Times.Never);
        
        _mapperMock.Verify(
            m => m.Map<AuthenticationResult>(It.IsAny<IdentityUser>()),
            Times.Never);
    }

    [Theory]
    [InlineData("jwtToken", "refreshToken")]
    public async Task IfUserIsValid_ShouldReturnAuthenticationResult(
        string jwtToken,
        string refreshToken)
    {
        // Arrange
        var loginModel = TestData.Create.LoginModel();
        var user = TestData.Create.User();
        var tokensDataModel = TestData.Create.TokensDataModel(
            jwtToken: jwtToken,
            refreshToken: refreshToken);
        var authenticationResult = TestData.Create.AuthenticationResult();

        _unitOfWorkMock
            .Setup(m => m.UsersRepository.GetUserByEmailAsync(loginModel.Email))
            .ReturnsAsync(user);

        _userManagerMock
            .Setup(m => m.CheckPasswordAsync(user.IdentityUser, loginModel.Password))
            .ReturnsAsync(true);

        _jwtTokenGeneratorMock
            .Setup(m => m.GenerateToken(user.IdentityUser))
            .ReturnsAsync(tokensDataModel);

        _mapperMock
            .Setup(m => m.Map<AuthenticationResult>(user))
            .Returns(authenticationResult);

        // Act
        var result = await _authenticationService.Login(loginModel);

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().BeOfType<AuthenticationResult>();
        result.Value.Token.Should().Be(tokensDataModel.JwtToken);
        result.Value.RefreshToken.Should().Be(tokensDataModel.RefreshToken);
    }
}