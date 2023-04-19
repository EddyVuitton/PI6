using Microsoft.EntityFrameworkCore;
using PI6.Shared.Data.Dtos;
using PI6.Shared.Data.Entities;

namespace PI6.Shared.DataSource;

public partial class DBContext : DbContext
{
    public DBContext(DbContextOptions<DBContext> options) : base(options) {}

    public virtual DbSet<formularz_typ> formularz_typ { get; set; }
    public virtual DbSet<formularz> formularz { get; set; }
    public virtual DbSet<formularz_podejscie> formularz_podejscie { get; set; }
    public virtual DbSet<formularz_pytanie> formularz_pytanie { get; set; }
    public virtual DbSet<formularz_pytanie_opcja> formularz_pytanie_opcja { get; set; }
    public virtual DbSet<formularz_odpowiedz> formularz_odpowiedz { get; set; }

    public virtual DbSet<FormularzDto> formularz_dto { get; set; }
    public virtual DbSet<FormularzKafelekDto> formularz_kafelek_dto { get; set; }
    public virtual DbSet<FormularzPodejscieDto> formularz_podejscie_dto { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<formularz_typ>(entity =>
        {
            entity.HasKey(e => e.fort_id);
        });

        modelBuilder.Entity<formularz>(entity =>
        {
            entity.HasKey(e => e.for_id);
        });

        modelBuilder.Entity<formularz_podejscie>(entity =>
        {
            entity.HasKey(e => e.fpod_id);
        });

        modelBuilder.Entity<formularz_pytanie>(entity =>
        {
            entity.HasKey(e => e.forp_id);
        });

        modelBuilder.Entity<formularz_pytanie_opcja>(entity =>
        {
            entity.HasKey(e => e.fpop_id);
        });

        modelBuilder.Entity<formularz_odpowiedz>(entity =>
        {
            entity.HasKey(e => e.fodp_id);
        });


        modelBuilder.Entity<FormularzDto>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<FormularzKafelekDto>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<FormularzPodejscieDto>(entity =>
        {
            entity.HasNoKey();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}