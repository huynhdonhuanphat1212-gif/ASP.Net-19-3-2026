using demo.Data;
using demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class TeacherController : ControllerBase
{
    private readonly AppDbContext _context;

    public TeacherController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetTeachers()
    {
        return Ok(await _context.Teachers
            .Include(t => t.Department)
            .ToListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> CreateTeacher(Teacher t)
    {
        _context.Teachers.Add(t);
        await _context.SaveChangesAsync();
        return Ok(t);
    }
}