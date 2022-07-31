using CraftParty.Application.Handlers.GetUsers;
using CraftParty.Application.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CraftParty.Api.Controllers;

[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(Routes.Users.GetUsers)]
    public Task<IEnumerable<UserModel>> GetUsers()
    {
        return _mediator.Send(new GetUsersQuery());
    }
}