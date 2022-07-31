using AutoMapper;
using CraftParty.Application.Interfaces.Data;
using CraftParty.Application.Interfaces.Users;
using CraftParty.Application.Models.Users;

namespace CraftParty.Application.Services.Users;

public class UsersRetrievingService : IUsersRetrievingService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UsersRetrievingService(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserModel>> GetUsers()
    {
        return _mapper.Map<IEnumerable<UserModel>>(await _unitOfWork.UsersRepository.GetAllAsync());
    }
}