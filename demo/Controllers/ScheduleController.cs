using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using demo.Data;
using demo.Models;

namespace demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ScheduleController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Lấy danh sách lịch học
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetSchedules()
        {
            var data = await _context.Schedules
                .Include(s => s.Course)
                .Include(s => s.Classroom)
                .Include(s => s.Class)
                .Select(s => new
                {
                    s.Id,
                    s.DayOfWeek,
                    s.TimeSlot,

                    s.ClassId,
                    Class = s.Class, // 👈 không dùng Name nữa

                    s.CourseId,
                    Course = s.Course,

                    s.ClassroomId,
                    Classroom = s.Classroom
                })
                .ToListAsync();

            return Ok(data);
        }

        /// <summary>
        /// Lấy chi tiết lịch học
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSchedule(int id)
        {
            var s = await _context.Schedules
                .Include(x => x.Course)
                .Include(x => x.Classroom)
                .Include(x => x.Class)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (s == null)
                return NotFound("Không tìm thấy");

            return Ok(s);
        }

        /// <summary>
        /// Thêm lịch học
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateSchedule(Schedule schedule)
        {
            if (string.IsNullOrEmpty(schedule.DayOfWeek))
                return BadRequest("Thiếu ngày học");

            if (string.IsNullOrEmpty(schedule.TimeSlot))
                return BadRequest("Thiếu ca học");

            // ❗ Check trùng lịch lớp
            var exists = await _context.Schedules.AnyAsync(s =>
                s.ClassId == schedule.ClassId &&
                s.DayOfWeek == schedule.DayOfWeek &&
                s.TimeSlot == schedule.TimeSlot
            );

            if (exists)
                return BadRequest("Lớp đã có lịch trong thời gian này");

            // ❗ Check trùng phòng
            var roomConflict = await _context.Schedules.AnyAsync(s =>
                s.ClassroomId == schedule.ClassroomId &&
                s.DayOfWeek == schedule.DayOfWeek &&
                s.TimeSlot == schedule.TimeSlot
            );

            if (roomConflict)
                return BadRequest("Phòng đã được sử dụng");

            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();

            return Ok(schedule);
        }

        /// <summary>
        /// Cập nhật lịch học
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSchedule(int id, Schedule schedule)
        {
            if (id != schedule.Id)
                return BadRequest("ID không khớp");

            var existing = await _context.Schedules.FindAsync(id);

            if (existing == null)
                return NotFound("Không tìm thấy");

            existing.DayOfWeek = schedule.DayOfWeek;
            existing.TimeSlot = schedule.TimeSlot;
            existing.ClassId = schedule.ClassId;
            existing.CourseId = schedule.CourseId;
            existing.ClassroomId = schedule.ClassroomId;

            await _context.SaveChangesAsync();

            return Ok(existing);
        }

        /// <summary>
        /// Xóa lịch học
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            var s = await _context.Schedules.FindAsync(id);

            if (s == null)
                return NotFound("Không tìm thấy");

            _context.Schedules.Remove(s);
            await _context.SaveChangesAsync();

            return Ok("Đã xóa lịch");
        }

        /// <summary>
        /// Lọc theo lớp
        /// </summary>
        [HttpGet("by-class/{classId}")]
        public async Task<IActionResult> GetByClass(int classId)
        {
            var data = await _context.Schedules
                .Where(s => s.ClassId == classId)
                .ToListAsync();

            return Ok(data);
        }

        /// <summary>
        /// Lọc theo ngày
        /// </summary>
        [HttpGet("by-day")]
        public async Task<IActionResult> GetByDay(string day)
        {
            var data = await _context.Schedules
                .Where(s => s.DayOfWeek == day)
                .ToListAsync();

            return Ok(data);
        }
    }
}