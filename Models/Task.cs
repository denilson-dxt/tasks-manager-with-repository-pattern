namespace TasksWithRepositoryPattern.Models;
public class Task
{
    public int Id {get;set;}
    public string Title{get;set;}
    public string Description{get;set;}   
    public User Owner { get; set; }
}