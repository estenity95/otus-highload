namespace Otus.Highload.Models.Dtos;

/// <summary>
/// Error response model.
/// </summary>
public class ErrorResponse
{
    /// <summary>
    /// Message description.
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Request identifier.
    /// </summary>
    public string RequestId { get; set; }

    /// <summary>
    /// Error code.
    /// </summary>
    public int? Code { get; set; }
}