using System.ComponentModel.DataAnnotations;
using Otus.Highload.Models.Entites;

namespace Otus.Highload.Models.Dtos.Auth;

/// <summary>
/// Model for register users.
/// </summary>
public class RegisterRequest
{
    /// <summary>
    /// Password.
    /// </summary>
    [Required]
    [MinLength(6, ErrorMessage = "{0} should be more then {1} characters")]
    public string Password { get; set; }
    
    /// <summary>
    /// Email.
    /// </summary>
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    /// <summary>
    /// First name.
    /// </summary>
    [Required]
    public string FirstName { get; set; }
    
    /// <summary>
    /// Second name.
    /// </summary>
    [Required]
    public string SecondName { get; set; }
    
    /// <summary>
    /// Birthdate.
    /// </summary>
    [Required]
    public DateTime BirthDate { get; set; }
    
    /// <summary>
    /// Gender.
    /// </summary>
    [Required]
    public UserGender Gender { get; set; }
    
    /// <summary>
    /// Biography.
    /// </summary>
    public string Biography { get; set; }
    
    /// <summary>
    /// City.
    /// </summary>
    public string City { get; set; }
}