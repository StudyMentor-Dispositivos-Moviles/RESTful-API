using _1._API.Request;
using _2._Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace _1._API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthDomain _authDomain;
    private readonly IConfiguration _configuration;

    public AuthController(IAuthDomain authDomain, IConfiguration configuration)
    {
        _authDomain = authDomain;
        _configuration = configuration;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest loginRequest)
    {
        var student = _authDomain.AuthenticateStudent(loginRequest.Email, loginRequest.Password);
        var tutor = _authDomain.AuthenticateTutor(loginRequest.Email, loginRequest.Password);

        if (student == null && tutor == null)
        {
            return Unauthorized();
        }
        
        var id = student?.Id ?? tutor?.TutorId;
        var token = GenerateJwtToken(loginRequest.Email);
        return Ok(new { Token = token, Id = id });
    }

    private string GenerateJwtToken(string email)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}