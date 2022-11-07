using Microsoft.AspNetCore.Identity;
using TasksWithRepositoryPattern.Configs;
using TasksWithRepositoryPattern.Repositories;
using TasksWithRepositoryPattern.Models;
using TasksWithRepositoryPattern.Data;
using TasksWithRepositoryPattern.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<DataContext>( options => {

});

builder.Services.AddIdentity<User, IdentityRole>(options=>{
    options.SignIn.RequireConfirmedAccount = true;
}).AddEntityFrameworkStores<DataContext>();


// Configure jwt to services
var jwtSection = builder.Configuration.GetSection("JwtBearerTokenSettings");
builder.Services.Configure<JwtBearerTokenSettings>(jwtSection);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add repositories
builder.Services.AddTransient<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
