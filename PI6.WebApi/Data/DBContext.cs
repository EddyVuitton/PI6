using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using PI6.Shared.Dtos;
using PI6.Shared.Entities;

namespace PI6.WebApi.Data;

public partial class DBContext : DbContext
{
    public DBContext(DbContextOptions<DBContext> options) : base(options) {}

    public virtual DbSet<formularz_typ> formularz_typ { get; set; }
    public virtual DbSet<formularz> formularz { get; set; }
    public virtual DbSet<FormularzDto> formularz_dto { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<formularz_typ>(entity =>
        {
            entity.HasKey(e => e.fort_id);
        });

        modelBuilder.Entity<formularz>(entity =>
        {
            entity.HasKey(e => e.for_id);
            entity.HasOne(e => e.formularz_typ);
        });

        modelBuilder.Entity<FormularzDto>(entity =>
        {
            entity.HasNoKey();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}