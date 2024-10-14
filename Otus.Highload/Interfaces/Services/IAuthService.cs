using Otus.Highload.Models.Dtos.Auth;

namespace Otus.Highload.Interfaces.Services;

/// <summary>
/// Auth service logic.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Register new user.
    /// </summary>
    /// <param name="request">New user request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns></returns>
    Task<Guid?> RegisterUserAsync(RegisterRequest request, CancellationToken cancellationToken);
    
    /// <summary>
    /// Login.
    /// </summary>
    /// <param name="email">Email.</param>
    /// <param name="password">Password.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns></returns>
    Task<string> LoginAsync(string email, string password, CancellationToken cancellationToken);
}