using Microsoft.EntityFrameworkCore;

namespace LabManager.Models;

public class SystemContext : DbContext 
{
    public DbSet<Computer> Computers { get; set; }
    // public SystemContext(DbContextOptions<SystemContext> options) : base(options)
    // { }

    public SystemContext() { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL("server=localhost;database=estudante;user=estudante;password=Estudante@!2");
    }
}