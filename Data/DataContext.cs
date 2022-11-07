using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TasksWithRepositoryPattern.Models;

namespace TasksWithRepositoryPattern.Data;
public class DataContext: IdentityDbContext<User>
{
    private IConfiguration _configuration;
    public DataContext(DbContextOptions options, IConfiguration configuration):base(options)
    {
        _configuration = configuration;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
    public DbSet<TasksWithRepositoryPattern.Models.Task> Tasks{get;set;}
}