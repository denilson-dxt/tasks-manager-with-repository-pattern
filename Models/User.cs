using Microsoft.AspNetCore.Identity;
namespace TasksWithRepositoryPattern.Models;

public class User:IdentityUser
{
    public List<Models.Task> Tasks { get; set; }
}