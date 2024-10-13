namespace Otus.Highload.Models.Entites;

/// <summary>
/// User.
/// </summary>
public class User : BaseEntity<Guid>
{ 
    /// <summary>
    /// Password.
    /// </summary>
    public string Password { get; set; }
    
    /// <summary>
    /// Email.
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    /// First name.
    /// </summary>
    public string FirstName { get; set; }
    
    /// <summary>
    /// Second name.
    /// </summary>
    public string SecondName { get; set; }
    
    /// <summary>
    /// Birthdate.
    /// </summary>
    public DateTime BirthDate { get; set; }
    
    /// <summary>
    /// Gender.
    /// </summary>
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