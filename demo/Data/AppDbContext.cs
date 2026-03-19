using Microsoft.EntityFrameworkCore;
using demo.Models;

namespace demo.Data
{
    public class AppDbContext : DbContext
    {
        // Constructor bắt buộc
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Bảng Students
        public DbSet<Student> Students { get; set; }
    }
}