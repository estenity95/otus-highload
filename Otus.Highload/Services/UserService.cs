using Otus.Highload.Interfaces.Repositories;
using Otus.Highload.Interfaces.Services;
using Otus.Highload.Models.Dtos.User;

namespace Otus.Highload.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="userRepository">User repository.</param>
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    /// <inheritdoc />
    public async Task<UserDto> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByIdAsync(id, cancellationToken);

        if (user is null)
            return null;

        return new UserDto()
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            SecondName = user.SecondName,
            BirthDate = user.BirthDate,
            UserGender = user.Gender,
            Biography = user.Biography,
            City = user.City
        };
    }
}