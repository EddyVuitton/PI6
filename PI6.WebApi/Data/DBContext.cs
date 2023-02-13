using Microsoft.EntityFrameworkCore;
using PI6.Shared.Entities;

namespace PI6.WebApi.Data;

public partial class DBContext : DbContext
{
    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<formularz_typ> formularz_typ { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<formularz_typ>(entity =>
        {
            entity.HasKey(e => e.fort_id);
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}