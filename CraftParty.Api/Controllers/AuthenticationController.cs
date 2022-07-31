using CraftParty.Application.Handlers.Login;
using CraftParty.Application.Handlers.RefreshToken;
using CraftParty.Application.Handlers.Register;
using CraftParty.Contracts.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CraftParty.Api.Controllers;

[Route("auth")]
public class WeatherForecastController : ApiController
{
    private readonly IMediator _mediator;

    public WeatherForecastController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(Routes.Authentication.Login)]
    public async Task<IActionResult> Login(LoginRequestModel requestModel)
    {
        var loginResult = await _mediator.Send(new LoginQuery(requestModel));

        return loginResult.Match(
            result => Ok(result),
            errors => Problem(errors));
    }

    [HttpPost(Routes.Authentication.Register)]
    public async Task<IActionResult> Register(RegisterRequestModel requestModel)
    {
        var registrationResult = await _mediator.Send(new RegisterCommand(requestModel));

        return registrationResult.Match(
            result => Ok(result),
            errors => Problem(errors));
    }

    [HttpPost(Routes.Authentication.RefreshToken)]
    public async Task<IActionResult> RefreshToken(TokenRequestModel requestModel)
    {
        var refreshTokenResult = await _mediator.Send(new RefreshTokenCommand(requestModel));
        
        return refreshTokenResult.Match(
            result => Ok(result),
            errors => Problem(errors));
    }
}
