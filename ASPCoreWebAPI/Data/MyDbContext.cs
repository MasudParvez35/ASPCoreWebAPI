using ASPCoreWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPCoreWebAPI.Data;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
    }

    public DbSet<Student> Students { get; set; }
}
