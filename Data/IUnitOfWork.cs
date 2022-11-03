namespace TasksWithRepositoryPattern.Data;
public interface IUnitOfWork
{
    public Task CommitAsync();
    public void Rollback();
}