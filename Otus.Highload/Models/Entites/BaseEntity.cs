namespace Otus.Highload.Models.Entites;

/// <summary>
/// Base entity.
/// </summary>
public abstract class BaseEntity<TId>
{
    /// <summary>
    /// Identifier.
    /// </summary>
    public TId Id { get; set; }
    
    /// <summary>
    /// Entity creation date.
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Entity update date.
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}