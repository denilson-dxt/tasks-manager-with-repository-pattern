using TasksWithRepositoryPattern.Dtos;
using TasksWithRepositoryPattern.Models;

namespace TasksWithRepositoryPattern.Services;
public interface IUserService
{
    public Task<UserDto> Register(UserDto request);
    public Task<string> Login(LoginDto request);
    public Task<User> GetAuthenticatedUser(string username);
}