using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Otus.Highload.Interfaces.Services;
using Otus.Highload.Models.Dtos;
using Otus.Highload.Models.Dtos.Auth;
using Otus.Highload.Models.Dtos.User;

namespace Otus.Highload.Controllers;

/// <summary>
/// API Controller for user operations.
/// </summary>
[ApiController]
[Route("[controller]")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<UserController> _logger;

    /// <inheritdoc />
    public UserController(IUserService userService, ILogger<UserController> logger)
    {
        _userService = userService;
        _logger = logger;
    }
    
    /// <summary>
    /// Get user by id.
    /// </summary>
    /// <param name="id">User id</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns></returns>
    /// <remarks>Get user info by user identifier.</remarks>
    [HttpGet("get/{id:guid}")]
    [ProducesResponseType(typeof(UserDto), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    [ProducesResponseType(typeof(ErrorResponse), 401)]
    [ProducesResponseType(typeof(ErrorResponse), 404)]
    [ProducesResponseType(typeof(ErrorResponse), 500)]
    public async Task<IActionResult> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Невалидный входной запрос на получение анкеты пользователя");
            var errorResponse = new ErrorResponse
            {
                Message = "Невалидный входной запрос",
                Code = 400,
                RequestId = HttpContext.TraceIdentifier
            };
            return BadRequest(errorResponse);
        }
        
        var user = await _userService.GetUserByIdAsync(id, cancellationToken);
        
        if (user == null)
        {
            _logger.LogError("Пользователь не найден");
            var errorResponse = new ErrorResponse
            {
                Message = "Пользователь не найден",
                Code = 404,
                RequestId = HttpContext.TraceIdentifier
            };
            return NotFound(errorResponse);
        }

        return Ok(user);
    }
}