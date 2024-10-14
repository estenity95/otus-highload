using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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
    
    /// <inheritdoc />
    public async Task<string> LoginAsync(string email, string password, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByEmailAsync(email, cancellationToken);
        
        if (user == null)
        {
            // Пользователь отсутствует
            return null;
        }

        // Проверяем пароль
        var isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.Password);
        if (!isPasswordValid)
        {
            // Неверный пароль
            return null;
        }
        
        // Генерация JWT
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            }),
            Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
        var token = tokenHandler.WriteToken(jwtSecurityToken);

        return token;
    }
}
