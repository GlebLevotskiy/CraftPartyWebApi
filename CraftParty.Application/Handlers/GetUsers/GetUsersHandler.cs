using CraftParty.Application.Interfaces.Users;
using CraftParty.Application.Models.Users;
using MediatR;

namespace CraftParty.Application.Handlers.GetUsers;

public class GetUsersHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserModel>>
{
    private readonly IUsersRetrievingService _usersRetrievingService;

    public GetUsersHandler(IUsersRetrievingService usersRetrievingService)
    {
        _usersRetrievingService = usersRetrievingService;
    }

    public Task<IEnumerable<UserModel>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
    {
        return _usersRetrievingService.GetUsers();
    }
}