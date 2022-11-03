namespace TasksWithRepositoryPattern.Data;
public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _context;
    public UnitOfWork(DataContext context)
    {
        _context = context;
    }
    public async Task CommitAsync()
    {
       await _context.SaveChangesAsync();
    }

    public void Rollback()
    {
        throw new NotImplementedException();
    }
}