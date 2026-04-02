using demo.Data;
using demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class ClassController : ControllerBase
{
    private readonly AppDbContext _context;

    public ClassController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetClasses()
    {
        return Ok(await _context.Classes
            .Include(c => c.Department)
            .ToListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> CreateClass(Class c)
    {
        _context.Classes.Add(c);
        await _context.SaveChangesAsync();
        return Ok(c);
    }
}