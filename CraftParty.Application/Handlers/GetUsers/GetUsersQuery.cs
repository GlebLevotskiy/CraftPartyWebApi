using CraftParty.Application.Models.Users;
using MediatR;

namespace CraftParty.Application.Handlers.GetUsers;

public record GetUsersQuery() : IRequest<IEnumerable<UserModel>>;