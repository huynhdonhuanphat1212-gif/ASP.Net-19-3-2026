using demo.Data;
using demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class DepartmentController : ControllerBase
{
    private readonly AppDbContext _context;

    public DepartmentController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetDepartments()
    {
        return Ok(await _context.Departments.ToListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> CreateDepartment(Department d)
    {
        _context.Departments.Add(d);
        await _context.SaveChangesAsync();
        return Ok(d);
    }
}