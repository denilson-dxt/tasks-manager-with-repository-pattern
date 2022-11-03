using TasksWithRepositoryPattern.Dtos;

namespace TasksWithRepositoryPattern.Services;
public interface ITaskService
{
    public Task<Models.Task> CreateAsync(TaskDto request);
    public Task<IEnumerable<Models.Task>> FindAll();
    public Task<Models.Task> FindById(int id);
    //public Task<IEnumerable<Models.Task>> FindByUser();
    public Task<Models.Task> UpdateAsync(TaskDto request);
    public Task<bool> DeleteAsync(int id);

}