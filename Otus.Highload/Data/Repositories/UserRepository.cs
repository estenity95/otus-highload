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

    /// <inheritdoc />
    public async Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var getUserByEmailQuery = "select * from users where email = @email";
        
        await using var command = new NpgsqlCommand(getUserByEmailQuery, NpgsqlConnection);
        command.Parameters.AddWithValue("email", email);
        
        await NpgsqlConnection.OpenAsync(cancellationToken);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);

        User user = null;
        if (await reader.ReadAsync(cancellationToken))
        {
            user = new User
            {
                Id = reader.GetGuid(reader.GetOrdinal("id")),
                Password = reader.GetString(reader.GetOrdinal("password")),
                Email = reader.GetString(reader.GetOrdinal("email")),
                FirstName = reader.GetString(reader.GetOrdinal("first_name")),
                SecondName = reader.GetString(reader.GetOrdinal("second_name")),
                BirthDate = reader.GetDateTime(reader.GetOrdinal("birthdate")),
                Gender = (UserGender)reader.GetInt32(reader.GetOrdinal("gender")),
                Biography = reader.IsDBNull(reader.GetOrdinal("biography")) ? null : reader.GetString(reader.GetOrdinal("biography")),
                City = reader.IsDBNull(reader.GetOrdinal("city")) ? null : reader.GetString(reader.GetOrdinal("city")),
            };
        }

        return user;
    }

    /// <inheritdoc />
    public async Task<User> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var getUserByIdQuery = "select * from users where id = id";
        
        await using var command = new NpgsqlCommand(getUserByIdQuery, NpgsqlConnection);
        command.Parameters.AddWithValue("id", id);
        
        await NpgsqlConnection.OpenAsync(cancellationToken);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);

        User user = null;
        if (await reader.ReadAsync(cancellationToken))
        {
            user = new User
            {
                Id = reader.GetGuid(reader.GetOrdinal("id")),
                Password = reader.GetString(reader.GetOrdinal("password")),
                Email = reader.GetString(reader.GetOrdinal("email")),
                FirstName = reader.GetString(reader.GetOrdinal("first_name")),
                SecondName = reader.GetString(reader.GetOrdinal("second_name")),
                BirthDate = reader.GetDateTime(reader.GetOrdinal("birthdate")),
                Gender = (UserGender)reader.GetInt32(reader.GetOrdinal("gender")),
                Biography = reader.IsDBNull(reader.GetOrdinal("biography")) ? null : reader.GetString(reader.GetOrdinal("biography")),
                City = reader.IsDBNull(reader.GetOrdinal("city")) ? null : reader.GetString(reader.GetOrdinal("city")),
            };
        }

        return user;
    }
}