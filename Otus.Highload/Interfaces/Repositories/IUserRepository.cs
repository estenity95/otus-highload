using Otus.Highload.Models.Entites;

namespace Otus.Highload.Interfaces.Repositories;

/// <summary>
/// Repository pattern for User entity.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Add user.
    /// </summary>
    /// <param name="user"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Guid?> AddUserAsync(User user, CancellationToken cancellationToken);
}