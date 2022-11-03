using Microsoft.EntityFrameworkCore;

using TasksWithRepositoryPattern.Data;
using TasksWithRepositoryPattern.Dtos;
using TasksWithRepositoryPattern.Models;

namespace TasksWithRepositoryPattern.Repositories;
public class TaskRepository : ITaskRepository
{
    private readonly DataContext _context;

    public TaskRepository(DataContext context)
    {
        _context = context;
    }
    public async Task<Models.Task> CreateAsync(TaskDto request)
    {
        var task = new Models.Task()
        {
            Title = request.Title,
            Description = request.Description
        };
        _context.Tasks.Add(task);
        return task;
    }

    public async Task<IEnumerable<Models.Task>> FilterAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Models.Task>> GetAllAsync()
    {
        var tasks = await _context.Tasks.ToListAsync();
        return tasks;
    }

    public async Task<Models.Task> GetByIdAsync(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if(task == null)
            throw new Exception("Post not found");
        return task;
    }
}