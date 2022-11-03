using TasksWithRepositoryPattern.Dtos;
using TasksWithRepositoryPattern.Models;

namespace TasksWithRepositoryPattern.Repositories;
public interface ITaskRepository
{
    public Task<Models.Task> CreateAsync(TaskDto request);
    public Task<Models.Task> GetByIdAsync(int id);
    public Task<IEnumerable<Models.Task>> GetAllAsync();
    public Task<IEnumerable<Models.Task>> FilterAsync();
}