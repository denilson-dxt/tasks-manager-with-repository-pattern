using Microsoft.EntityFrameworkCore;

namespace TasksWithRepositoryPattern.Data;
public class DataContext: DbContext
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