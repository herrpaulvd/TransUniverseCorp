using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SpaceRouteDAL;

public partial class SpaceRouteDbContext : DbContext
{
    public SpaceRouteDbContext()
    {
    }

    public SpaceRouteDbContext(DbContextOptions<SpaceRouteDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Edge> Edges { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<ScheduleElement> ScheduleElements { get; set; }

    public virtual DbSet<SpaceObject> SpaceObjects { get; set; }

    public virtual DbSet<SpacePort> SpacePorts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=host.docker.internal,22888;Database=SpaceRouteDB;User ID=sa;Password=\"228c@T228\";Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Edge>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Edges__3213E83FD4E6DDA2");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.End).HasColumnName("end");
            entity.Property(e => e.QualificationClasses).HasColumnName("qualificationClasses");
            entity.Property(e => e.SpaceshipClasses).HasColumnName("spaceshipClasses");
            entity.Property(e => e.Start).HasColumnName("start");
            entity.Property(e => e.Time).HasColumnName("time");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Orders__3213E83FF6EDFE45");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CurrentState).HasColumnName("currentState");
            entity.Property(e => e.Customer).HasColumnName("customer");
            entity.Property(e => e.Driver).HasColumnName("driver");
            entity.Property(e => e.LoadingPort).HasColumnName("loadingPort");
            entity.Property(e => e.LoadingTime).HasColumnName("loadingTime");
            entity.Property(e => e.Spaceship).HasColumnName("spaceship");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.TotalCost).HasColumnName("totalCost");
            entity.Property(e => e.TotalTime).HasColumnName("totalTime");
            entity.Property(e => e.UnloadingPort).HasColumnName("unloadingPort");
            entity.Property(e => e.UnloadingTime).HasColumnName("unloadingTime");
            entity.Property(e => e.Volume).HasColumnName("volume");
        });

        modelBuilder.Entity<ScheduleElement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Schedule__3213E83F97838E9C");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DepartureOrArrival).HasColumnName("departureOrArrival");
            entity.Property(e => e.DestinationOrStop).HasColumnName("destinationOrStop");
            entity.Property(e => e.Driver).HasColumnName("driver");
            entity.Property(e => e.IsStop).HasColumnName("isStop");
            entity.Property(e => e.Next).HasColumnName("next");
            entity.Property(e => e.Order).HasColumnName("order");
            entity.Property(e => e.PlannedDepartureOrArrival).HasColumnName("plannedDepartureOrArrival");
            entity.Property(e => e.Spaceship).HasColumnName("spaceship");
            entity.Property(e => e.Time).HasColumnName("time");
        });

        modelBuilder.Entity<SpaceObject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SpaceObj__3213E83FD33DED85");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Kind).HasColumnName("kind");
            entity.Property(e => e.Name)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.SystemCenter).HasColumnName("systemCenter");
            entity.Property(e => e.SystemPosition).HasColumnName("systemPosition");
        });

        modelBuilder.Entity<SpacePort>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SpacePor__3213E83FD8168A15");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Altitude).HasColumnName("altitude");
            entity.Property(e => e.Description)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Latitude).HasColumnName("latitude");
            entity.Property(e => e.Longtitude).HasColumnName("longtitude");
            entity.Property(e => e.Name)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Planet).HasColumnName("planet");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
