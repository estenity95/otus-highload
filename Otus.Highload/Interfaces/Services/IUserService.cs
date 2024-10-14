using Otus.Highload.Models.Dtos.User;

namespace Otus.Highload.Interfaces.Services;

/// <summary>
/// User service logic.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Get user by id.
    /// </summary>
    /// <param name="id">User id.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns></returns>
    Task<UserDto> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);
}