﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Otus.Highload.Interfaces.Services;
using Otus.Highload.Models;
using Otus.Highload.Models.Dtos;
using Otus.Highload.Models.Dtos.Auth;

namespace Otus.Highload.Controllers;

/// <summary>
/// API Controller for auth operations.
/// </summary>
[ApiController]
// TODO: Настрой везде ProducesReponseType и для авторизации мидлварю может тоже?
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    /// <inheritdoc />
    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }
    
    /// <summary>
    /// Register new user.
    /// </summary>
    /// <param name="request">Input request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns></returns>
    /// <remarks>Register new user for social network.</remarks>
    [AllowAnonymous]
    [HttpPost("user/register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Невалидный входной запрос на регистрацию нового пользователя");
            var errorResponse = new ErrorResponse
            {
                Message = "Невалидный входной запрос",
                Code = 400,
                RequestId = HttpContext.TraceIdentifier
            };
            return BadRequest(errorResponse);
        }
        
        var userId = await _authService.RegisterUserAsync(request, cancellationToken);
        
        if (userId == null)
        {
            _logger.LogError("Ошибка при регистрации нового пользователя");
            var errorResponse = new ErrorResponse
            {
                Message = "Ошибка при регистрации нового пользователя",
                Code = 500,
                RequestId = HttpContext.TraceIdentifier
            };
            return StatusCode(500, errorResponse);
        }
        
        return Ok(new RegisterResponse
        {
            UserId = userId.Value
        });
    }
    
    /// <summary>
    /// Login.
    /// </summary>
    /// <param name="request">Input request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns></returns>
    /// <remarks>Login to social network.</remarks>
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Невалидные входной запрос при попытке входа");
            var errorResponse = new ErrorResponse
            {
                Message = "Невалидный входной запрос",
                Code = 400,
                RequestId = HttpContext.TraceIdentifier
            };
            return BadRequest(errorResponse);
        }

        var token = await _authService.LoginAsync(request.Email, request.Password, cancellationToken);

        if (token == null)
        {
            _logger.LogWarning("Неверные данные пользователя при регистрации");
            var errorResponse = new ErrorResponse
            {
                Message = "Неверные данные пользователя при регистрации",
                Code = 404,
                RequestId = HttpContext.TraceIdentifier
            };
            return NotFound(errorResponse);
        }

        return Ok(new LoginResponse()
        {
            Token = token
        });
    }
}