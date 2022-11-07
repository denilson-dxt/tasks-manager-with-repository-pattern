using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using TasksWithRepositoryPattern.Dtos;
using TasksWithRepositoryPattern.Models;
using TasksWithRepositoryPattern.Data;
using TasksWithRepositoryPattern.Repositories;
using TasksWithRepositoryPattern.Services;

namespace TasksWithRepositoryPattern.Controllers;
[ApiController]
[Authorize]
[Route("api/[controller]")]
public class TaskController: ControllerBase
{
    private readonly DataContext _context;
    private readonly ITaskService _service;
    public TaskController(DataContext context, ITaskService service)
    {
        _context = context;
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<TaskDto>> List()
    {
        var tasks = await _service.FindAll();
        return Ok(tasks);
    }
    [HttpPost]
    public async Task<ActionResult> Create(TaskDto request)
    {
        var task = await _service.CreateAsync(request);
        return Ok(task);    
    }

    [HttpPut]
    public async Task<ActionResult> Update(TaskDto request)
    {
       var task = await _service.UpdateAsync(request);
        return Ok(task);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTask(int id)
    {
        var result = await _service.DeleteAsync(id);
        return Ok(result);
    }


}