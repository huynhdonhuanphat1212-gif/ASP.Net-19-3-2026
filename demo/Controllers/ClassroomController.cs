using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using demo.Data;
using demo.Models;

namespace demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassroomController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClassroomController(AppDbContext context)
        {
            _context = context;
        }

        // GET ALL
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Classroom>>> GetClassrooms()
        {
            return await _context.Classrooms.ToListAsync();
        }

        // GET BY ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Classroom>> GetClassroom(int id)
        {
            var room = await _context.Classrooms.FindAsync(id);

            if (room == null)
                return NotFound();

            return room;
        }

        // CREATE
        [HttpPost]
        public async Task<ActionResult<Classroom>> PostClassroom(Classroom room)
        {
            _context.Classrooms.Add(room);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetClassroom), new { id = room.Id }, room);
        }

        // UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClassroom(int id, Classroom room)
        {
            if (id != room.Id)
                return BadRequest();

            var existing = await _context.Classrooms.FindAsync(id);
            if (existing == null)
                return NotFound();

            existing.RoomName = room.RoomName;
            existing.Capacity = room.Capacity;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClassroom(int id)
        {
            var room = await _context.Classrooms.FindAsync(id);
            if (room == null)
                return NotFound();

            _context.Classrooms.Remove(room);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}