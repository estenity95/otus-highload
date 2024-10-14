using System.ComponentModel.DataAnnotations;

namespace Otus.Highload.Models.Dtos.Auth;

/// <summary>
/// Login request model.
/// </summary>
public class LoginRequest
{
    /// <summary>
    /// Password.
    /// </summary>
    [Required]
    public string Password { get; set; }
    
    /// <summary>
    /// Email.
    /// </summary>
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}