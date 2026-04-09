using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using demo.Data;
using demo.Models;

namespace demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Lấy danh sách sinh viên
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var data = await _context.Students
                .Select(s => new
                {
                    s.Id,
                    s.StudentCode,
                    s.FullName,
                    s.Birthday,
                    s.ClassId
                })
                .ToListAsync();

            return Ok(data);
        }

        /// <summary>
        /// Lấy chi tiết sinh viên theo ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
                return NotFound("Không tìm thấy sinh viên");

            return Ok(student);
        }

        /// <summary>
        /// Thêm sinh viên mới
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateStudent(Student student)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (student.Birthday > DateTime.Now)
                return BadRequest("Ngày sinh không hợp lệ");

            // Check trùng mã sinh viên
            var exists = await _context.Students
                .AnyAsync(s => s.StudentCode == student.StudentCode);

            if (exists)
                return BadRequest("Mã sinh viên đã tồn tại");

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return Ok(student);
        }

        /// <summary>
        /// Cập nhật sinh viên
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, Student student)
        {
            if (id != student.Id)
                return BadRequest("ID không khớp");

            var existing = await _context.Students.FindAsync(id);

            if (existing == null)
                return NotFound("Không tìm thấy sinh viên");

            existing.StudentCode = student.StudentCode;
            existing.FullName = student.FullName;
            existing.Birthday = student.Birthday;
            existing.ClassId = student.ClassId;

            await _context.SaveChangesAsync();

            return Ok(existing);
        }

        /// <summary>
        /// Xóa sinh viên
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students
                .Include(s => s.Enrollments)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
                return NotFound("Không tìm thấy sinh viên");

            // Check ràng buộc
            if (student.Enrollments != null && student.Enrollments.Any())
                return BadRequest("Không thể xóa sinh viên đã đăng ký môn học");

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return Ok("Đã xóa sinh viên");
        }

        /// <summary>
        /// Tìm kiếm sinh viên theo tên
        /// </summary>
        [HttpGet("search")]
        public async Task<IActionResult> Search(string? name)
        {
            var query = _context.Students.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(s => s.FullName.Contains(name));
            }

            var result = await query.ToListAsync();
            return Ok(result);
        }

        /// <summary>
        /// Phân trang sinh viên
        /// </summary>
        [HttpGet("paging")]
        public async Task<IActionResult> Paging(int page = 1, int pageSize = 5)
        {
            var total = await _context.Students.CountAsync();

            var data = await _context.Students
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(new
            {
                total,
                page,
                pageSize,
                data
            });
        }
    }
}