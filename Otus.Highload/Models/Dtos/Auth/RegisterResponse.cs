namespace Otus.Highload.Models.Dtos.Auth;

/// <summary>
/// Response model for register users.
/// </summary>
public class RegisterResponse
{
    /// <summary>
    /// New user id.
    /// </summary>
    public Guid UserId { get; set; }
}