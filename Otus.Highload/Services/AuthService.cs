using Microsoft.Extensions.Options;
using Otus.Highload.Configurations;
using Otus.Highload.Interfaces.Repositories;
using Otus.Highload.Interfaces.Services;
using Otus.Highload.Models.Dtos.Auth;
using Otus.Highload.Models.Entites;

namespace Otus.Highload.Services;

/// <inheritdoc />
public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly JwtSettings _jwtSettings;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="userRepository">User repository.</param>
    /// <param name="jwtSettings">JWT settings.</param>
    public AuthService(IUserRepository userRepository, IOptions<JwtSettings> jwtSettings)
    {
        _userRepository = userRepository;
        _jwtSettings = jwtSettings.Value;
    }
    
    /// <inheritdoc />
    public async Task<Guid?> RegisterUserAsync(RegisterRequest request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Email = request.Email,
            FirstName = request.FirstName,
            SecondName = request.SecondName,
            BirthDate = request.BirthDate,
            Gender = request.Gender,
            Biography = request.Biography,
            City = request.City,
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
            CreatedAt = DateTime.UtcNow
        };

        return await _userRepository.AddUserAsync(user, cancellationToken);
    }
}