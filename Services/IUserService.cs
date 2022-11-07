using TasksWithRepositoryPattern.Dtos;

namespace TasksWithRepositoryPattern.Services;
public interface IUserService
{
    public Task<UserDto> Register(UserDto request);
    public Task<string> Login(LoginDto request);
}