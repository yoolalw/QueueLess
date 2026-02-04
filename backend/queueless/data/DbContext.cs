using Microsoft.EntityFrameworkCore;

namespace SnackBar_system.Data;

public class SnackBarContext : DbContext
{
    // public SnackBarContext(DbContextOptions<SnackBarContext> options)
    //     : base(options)
    // {
        
    // }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

    }
}