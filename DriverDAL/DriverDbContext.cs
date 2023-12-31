using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DriverDAL;

public partial class DriverDbContext : DbContext
{
    public DriverDbContext()
    {
    }

    public DriverDbContext(DbContextOptions<DriverDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<Spaceship> Spaceships { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=host.docker.internal,22888;Database=DriverDB;User ID=sa;Password=\"228c@T228\";Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Drivers__3213E83FCE78A527");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.CurrentState).HasColumnName("currentState");
            entity.Property(e => e.Email)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.HiringCost).HasColumnName("hiringCost");
            entity.Property(e => e.Name)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.QualificationClasses).HasColumnName("qualificationClasses");
            entity.Property(e => e.SpaceshipClasses).HasColumnName("spaceshipClasses");
        });

        modelBuilder.Entity<Spaceship>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Spaceshi__3213E83FD924F124");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Class).HasColumnName("class");
            entity.Property(e => e.CurrentState).HasColumnName("currentState");
            entity.Property(e => e.Model)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("model");
            entity.Property(e => e.Name)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.UsageCost).HasColumnName("usageCost");
            entity.Property(e => e.Volume).HasColumnName("volume");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
