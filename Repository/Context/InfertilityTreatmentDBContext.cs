using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Model.Models;

namespace Repository.Context;

public partial class InfertilityTreatmentDBContext : DbContext
{
    public InfertilityTreatmentDBContext()
    {
    }

    public InfertilityTreatmentDBContext(DbContextOptions<InfertilityTreatmentDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BlogPost> BlogPosts { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<MedicalRecord> MedicalRecords { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Services> Services { get; set; }

    public virtual DbSet<TreatmentBooking> TreatmentBookings { get; set; }

    public virtual DbSet<TreatmentSchedule> TreatmentSchedules { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VwTreatmentStat> VwTreatmentStats { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured) optionsBuilder.UseSqlServer(GetConnectionString());
    }

    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();

        var connectionString = config.GetConnectionString("DefaultConnectionString");
        return connectionString;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BlogPost>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PK__BlogPost__AA126018ABF48D53");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.BlogPosts)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__BlogPosts__Creat__59063A47");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.DoctorId).HasName("PK__Doctors__2DC00EBF5DA15B09");

            entity.HasIndex(e => e.UserId, "UQ__Doctors__1788CC4DBF749680").IsUnique();

            entity.Property(e => e.Degree).HasMaxLength(100);
            entity.Property(e => e.Specialization).HasMaxLength(100);
            entity.Property(e => e.WorkSchedule).HasMaxLength(255);

            entity.HasOne(d => d.User).WithOne(p => p.Doctor)
                .HasForeignKey<Doctor>(d => d.UserId)
                .HasConstraintName("FK__Doctors__UserId__403A8C7D");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__Feedback__6A4BEDD62F3D536A");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK__Feedbacks__Docto__5441852A");

            entity.HasOne(d => d.User).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Feedbacks__UserI__534D60F1");
        });

        modelBuilder.Entity<MedicalRecord>(entity =>
        {
            entity.HasKey(e => e.RecordId).HasName("PK__MedicalR__FBDF78E983EF35F2");

            entity.Property(e => e.VisitDate).HasColumnType("datetime");

            entity.HasOne(d => d.Booking).WithMany(p => p.MedicalRecords)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK__MedicalRe__Booki__4F7CD00D");

            entity.HasOne(d => d.Doctor).WithMany(p => p.MedicalRecords)
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK__MedicalRe__Docto__5070F446");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1AF37375F0");

            entity.HasIndex(e => e.Name, "UQ__Roles__737584F63C229D06").IsUnique();

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<Services>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__Services__C51BB00A3DB3DDA0");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.MethodType).HasMaxLength(20);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<TreatmentBooking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Treatmen__73951AEDB9AA9518");

            entity.Property(e => e.BookingDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Doctor).WithMany(p => p.TreatmentBookings)
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK__Treatment__Docto__46E78A0C");

            entity.HasOne(d => d.Service).WithMany(p => p.TreatmentBookings)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK__Treatment__Servi__47DBAE45");

            entity.HasOne(d => d.User).WithMany(p => p.TreatmentBookings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Treatment__UserI__45F365D3");
        });

        modelBuilder.Entity<TreatmentSchedule>(entity =>
        {
            entity.HasKey(e => e.ScheduleId).HasName("PK__Treatmen__9C8A5B490D8182C6");

            entity.Property(e => e.EventDate).HasColumnType("datetime");
            entity.Property(e => e.EventType).HasMaxLength(100);
            entity.Property(e => e.Reminder).HasDefaultValue(true);

            entity.HasOne(d => d.Booking).WithMany(p => p.TreatmentSchedules)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK__Treatment__Booki__4BAC3F29");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C489496D9");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105348244471C").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__RoleId__3B75D760");
        });

        modelBuilder.Entity<VwTreatmentStat>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_TreatmentStats");

            entity.Property(e => e.MethodType).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}