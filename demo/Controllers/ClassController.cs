using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using demo.Data;
using demo.Models;

namespace demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClassController(AppDbContext context)
        {
            _context = context;
        }

        // GET ALL
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Class>>> GetClasses()
        {
            return await _context.Classes
                .Include(c => c.Department)
                .ToListAsync();
        }

        // GET BY ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Class>> GetClass(int id)
        {
            var cls = await _context.Classes
                .Include(c => c.Department)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cls == null)
                return NotFound();

            return cls;
        }

        // CREATE
        [HttpPost]
        public async Task<ActionResult<Class>> PostClass(Class cls)
        {
            _context.Classes.Add(cls);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetClass), new { id = cls.Id }, cls);
        }

        // UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClass(int id, Class cls)
        {
            if (id != cls.Id)
                return BadRequest();

            var existing = await _context.Classes.FindAsync(id);
            if (existing == null)
                return NotFound();

            existing.ClassName = cls.ClassName;
            existing.DepartmentId = cls.DepartmentId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClass(int id)
        {
            var cls = await _context.Classes.FindAsync(id);
            if (cls == null)
                return NotFound();

            _context.Classes.Remove(cls);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}