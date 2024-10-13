using Npgsql;
using Otus.Highload.Interfaces.Repositories;
using Otus.Highload.Models.Entites;

namespace Otus.Highload.Data.Repositories;

/// <inheritdoc />
public class UserRepository : IUserRepository
{
    private NpgsqlConnection NpgsqlConnection { get; set; }
    
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="npgsqlConnection">DB connection.</param>
    public UserRepository(NpgsqlConnection npgsqlConnection)
    {
        NpgsqlConnection = npgsqlConnection;
    }


    /// <inheritdoc />
    public async Task<Guid?> AddUserAsync(User user, CancellationToken cancellationToken)
    {
        var insertQuery =
            "insert into users (password, email, first_name, second_name, birthdate, gender, biography, city, created_at)" +
            "values (@password, @email, @first_name, @second_name, @birthdate, @gender, @biography, @city, @created_at) returning id";
            
        await using var command = new NpgsqlCommand(insertQuery, NpgsqlConnection);
        
        command.Parameters.Add(new NpgsqlParameter("password", user.Password));
        command.Parameters.Add(new NpgsqlParameter("email", user.Email));
        command.Parameters.Add(new NpgsqlParameter("first_name", user.FirstName));
        command.Parameters.Add(new NpgsqlParameter("second_name", user.SecondName));
        command.Parameters.Add(new NpgsqlParameter("birthdate", user.BirthDate));
        command.Parameters.Add(new NpgsqlParameter("gender", (int)user.Gender));
        command.Parameters.Add(new NpgsqlParameter("biography", user.Biography));
        command.Parameters.Add(new NpgsqlParameter("city", user.City));
        command.Parameters.Add(new NpgsqlParameter("created_at", user.CreatedAt));
        
        await NpgsqlConnection.OpenAsync(cancellationToken);
        var result = await command.ExecuteScalarAsync(cancellationToken);
        
        return result as Guid?;
    }
}