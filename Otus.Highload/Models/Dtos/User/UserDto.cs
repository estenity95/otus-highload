using Otus.Highload.Models.Entites;

namespace Otus.Highload.Models.Dtos.User;

/// <summary>
/// User DTO.
/// </summary>
public class UserDto
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public DateTime BirthDate { get; set; }
    public UserGender UserGender { get; set; }
    public string Biography { get; set; }
    public string City { get; set; }
}