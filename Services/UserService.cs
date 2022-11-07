using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TasksWithRepositoryPattern.Configs;
using TasksWithRepositoryPattern.Dtos;
using TasksWithRepositoryPattern.Exceptions;
using TasksWithRepositoryPattern.Models;

namespace TasksWithRepositoryPattern.Services;
public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly JwtBearerTokenSettings _jwtBearerTokenSettings;

    public UserService(UserManager<User> userManager, IOptions<JwtBearerTokenSettings> jwtBearerTokenSettings)
    {
        _userManager = userManager;
        _jwtBearerTokenSettings = jwtBearerTokenSettings.Value;
    }
    public async Task<UserDto> Register(UserDto request)
    {
        var user = new User()
        {
            UserName = request.UserName,
            Email = request.Email,
            PasswordHash = request.Password
        };
        
        var result = await _userManager.CreateAsync(user, request.Password);
        if(!result.Succeeded)
            throw new Exception(result.Errors.FirstOrDefault().Description);
        return request;

    }
    public async Task<string> Login(LoginDto request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
            throw new AuthException("User not found", new List<string>()
            {
                "The user given does not exist in the system"
            });
        var result = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
        if (result == PasswordVerificationResult.Failed)
            throw new AuthException("Invaid credentials", new List<string>()
            {
                "The given credentials dont match with any of our registered users"
            });
        return _generateToken(user);
    }

    private string _generateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtBearerTokenSettings.SecretKey);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.UserName.ToString()),
                new Claim(ClaimTypes.Email, user.Email.ToString())
            }),
            Expires = DateTime.Now.AddSeconds(_jwtBearerTokenSettings.ExpirationTime),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Audience = _jwtBearerTokenSettings.Audience,
            Issuer = _jwtBearerTokenSettings.Issuer
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    
}