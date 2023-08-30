using Microsoft.EntityFrameworkCore;
using PI6.Shared.Data.Dtos;
using PI6.Shared.Data.Entities;

namespace PI6.Shared.Data;

public partial class DBContext : DbContext
{
    public DBContext(DbContextOptions<DBContext> options) : base(options) { }

    public virtual DbSet<formularz_typ> formularz_typ { get; set; }
    public virtual DbSet<formularz> formularz { get; set; }
    public virtual DbSet<formularz_podejscie> formularz_podejscie { get; set; }
    public virtual DbSet<formularz_pytanie> formularz_pytanie { get; set; }
    public virtual DbSet<formularz_pytanie_opcja> formularz_pytanie_opcja { get; set; }
    public virtual DbSet<formularz_podejscie_odpowiedz> formularz_podejscie_odpowiedz { get; set; }

    public virtual DbSet<FormularzDto> formularz_dto { get; set; }
    public virtual DbSet<FormularzKafelekDto> formularz_kafelek_dto { get; set; }
    public virtual DbSet<FormularzPodejscieDto> formularz_podejscie_dto { get; set; }

    public virtual DbSet<account> account { get; set; }
    public virtual DbSet<account_type> account_type { get; set; }
    public virtual DbSet<student_group> student_group { get; set; }
    public virtual DbSet<student_group_map> student_group_map { get; set; }
    public virtual DbSet<StudentGroupMapDto> student_group_map_dto { get; set; }
    public virtual DbSet<group_assigned_forms> group_assigned_forms { get; set; }
    public virtual DbSet<GroupAssignedFormCheckDto> group_assigned_form_check_dto { get; set; }
    public virtual DbSet<FormDatesDto> form_dates_dto { get; set; }

    public virtual DbSet<FormResultDto> form_result_dto { get; set; }



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

        modelBuilder.Entity<formularz_podejscie_odpowiedz>(entity =>
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

        modelBuilder.Entity<account>(entity =>
        {
            entity.HasKey(e => e.us_id);
        });

        modelBuilder.Entity<account_type>(entity =>
        {
            entity.HasKey(e => e.ust_id);
        });

        modelBuilder.Entity<student_group>(entity =>
        {
            entity.HasKey(e => e.sgr_id);
        });

        modelBuilder.Entity<student_group_map>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<StudentGroupMapDto>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<group_assigned_forms>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<GroupAssignedFormCheckDto>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<FormDatesDto>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<FormResultDto>(entity =>
        {
            entity.HasNoKey();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}