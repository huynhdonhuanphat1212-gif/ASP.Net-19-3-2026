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

        // GET all enrollment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enrollment>>> GetEnrollments()
        {
            return await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .ToListAsync();
        }

        // Student đăng ký Course
        [HttpPost]
        public async Task<IActionResult> EnrollStudent(Enrollment enrollment)
        {
            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();

            return Ok(enrollment);
        }

        // Lấy course theo student
        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetCoursesByStudent(int studentId)
        {
            var courses = await _context.Enrollments
                .Where(e => e.StudentId == studentId)
                .Include(e => e.Course)
                .Select(e => e.Course)
                .ToListAsync();

            return Ok(courses);
        }

        // Lấy student theo course
        [HttpGet("course/{courseId}")]
        public async Task<IActionResult> GetStudentsByCourse(int courseId)
        {
            var students = await _context.Enrollments
                .Where(e => e.CourseId == courseId)
                .Include(e => e.Student)
                .Select(e => e.Student)
                .ToListAsync();

            return Ok(students);
        }

        // Xóa enrollment
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnrollment(int id)
        {
            var en = await _context.Enrollments.FindAsync(id);
            if (en == null)
                return NotFound();

            _context.Enrollments.Remove(en);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}