using TasksWithRepositoryPattern.Data;
using TasksWithRepositoryPattern.Dtos;
using TasksWithRepositoryPattern.Repositories;

namespace TasksWithRepositoryPattern.Services;
public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;
    private readonly DataContext _context;
    private readonly IUnitOfWork _unitOfWork;
    public TaskService(ITaskRepository repository, DataContext context, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _context = context;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Models.Task> CreateAsync(TaskDto request)
    {
        var post = await _repository.CreateAsync(request);
        await _unitOfWork.CommitAsync();
        return post;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var task = await _repository.GetByIdAsync(id);
        _context.Tasks.Remove(task);
        await _unitOfWork.CommitAsync();
        return true;
    }

    public async Task<IEnumerable<Models.Task>> FindAll()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Models.Task> FindById(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Models.Task> UpdateAsync(TaskDto request)
    {
        var task = await _repository.GetByIdAsync(request.Id);
        task.Title = request.Title;
        task.Description = request.Description;

        await _unitOfWork.CommitAsync();
        return task;
    }
}