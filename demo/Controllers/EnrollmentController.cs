using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using demo.Data;
using demo.Models;

namespace demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EnrollmentController(AppDbContext context)
        {
            _context = context;
        }

        // GET ALL
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enrollment>>> GetEnrollments()
        {
            return await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .ToListAsync();
        }

        // GET BY ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Enrollment>> GetEnrollment(int id)
        {
            var enroll = await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (enroll == null)
                return NotFound();

            return enroll;
        }

        // CREATE
        [HttpPost]
        public async Task<ActionResult<Enrollment>> PostEnrollment(Enrollment enroll)
        {
            _context.Enrollments.Add(enroll);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEnrollment), new { id = enroll.Id }, enroll);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnrollment(int id)
        {
            var enroll = await _context.Enrollments.FindAsync(id);
            if (enroll == null)
                return NotFound();

            _context.Enrollments.Remove(enroll);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}