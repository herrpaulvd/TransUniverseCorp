using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Entities;

public partial class TransUniverseDbContext : DbContext
{
    public TransUniverseDbContext()
    {
    }

    public TransUniverseDbContext(DbContextOptions<TransUniverseDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<Edge> Edges { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<ScheduleElement> ScheduleElements { get; set; }

    public virtual DbSet<SpaceObject> SpaceObjects { get; set; }

    public virtual DbSet<SpacePort> SpacePorts { get; set; }

    public virtual DbSet<Spaceship> Spaceships { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\main;Database=TransUniverseDB;Trusted_Connection=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3213E83FE96D13AD");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Corporative).HasColumnName("corporative");
            entity.Property(e => e.Email)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Drivers__3213E83FE9210BDC");

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

            entity.HasOne(d => d.CurrentStateNavigation).WithMany(p => p.Drivers)
                .HasForeignKey(d => d.CurrentState)
                .HasConstraintName("Drivers_FK_CurrentState");
        });

        modelBuilder.Entity<Edge>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Edges__3213E83FF8DF27D1");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.End).HasColumnName("end");
            entity.Property(e => e.QualificationClasses).HasColumnName("qualificationClasses");
            entity.Property(e => e.SpaceshipClasses).HasColumnName("spaceshipClasses");
            entity.Property(e => e.Start).HasColumnName("start");
            entity.Property(e => e.Time).HasColumnName("time");

            entity.HasOne(d => d.EndNavigation).WithMany(p => p.EdgeEndNavigations)
                .HasForeignKey(d => d.End)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Edges_FK_End");

            entity.HasOne(d => d.StartNavigation).WithMany(p => p.EdgeStartNavigations)
                .HasForeignKey(d => d.Start)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Edges_FK_Start");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Orders__3213E83FF6900C93");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CurrentState).HasColumnName("currentState");
            entity.Property(e => e.Driver).HasColumnName("driver");
            entity.Property(e => e.Customer).HasColumnName("customer");
            entity.Property(e => e.LoadingPort).HasColumnName("loadingPort");
            entity.Property(e => e.LoadingTime).HasColumnName("loadingTime");
            entity.Property(e => e.Spaceship).HasColumnName("spaceship");
            entity.Property(e => e.TotalCost).HasColumnName("totalCost");
            entity.Property(e => e.TotalTime).HasColumnName("totalTime");
            entity.Property(e => e.UnloadingPort).HasColumnName("unloadingPort");
            entity.Property(e => e.UnloadingTime).HasColumnName("unloadingTime");
            entity.Property(e => e.Volume).HasColumnName("volume");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.CurrentStateNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CurrentState)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Orders_FK_CurrentState");

            entity.HasOne(d => d.DriverNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Driver)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Orders_FK_Driver");

            entity.HasOne(d => d.CustomerNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Customer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Orders_FK_Customer");

            entity.HasOne(d => d.LoadingPortNavigation).WithMany(p => p.OrderLoadingPortNavigations)
                .HasForeignKey(d => d.LoadingPort)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Orders_FK_LoadingPort");

            entity.HasOne(d => d.SpaceshipNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Spaceship)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Orders_FK_Spaceship");

            entity.HasOne(d => d.UnloadingPortNavigation).WithMany(p => p.OrderUnloadingPortNavigations)
                .HasForeignKey(d => d.UnloadingPort)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Orders_FK_UnloadingPort");
        });

        modelBuilder.Entity<ScheduleElement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Schedule__3213E83F7E29119E");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DepartureOrArrival).HasColumnName("departureOrArrival");
            entity.Property(e => e.DestinationOrStop).HasColumnName("destinationOrStop");
            entity.Property(e => e.Driver).HasColumnName("driver");
            entity.Property(e => e.IsStop).HasColumnName("isStop");
            entity.Property(e => e.Order).HasColumnName("order");
            entity.Property(e => e.PlannedDepartureOrArrival).HasColumnName("plannedDepartureOrArrival");
            entity.Property(e => e.Spaceship).HasColumnName("spaceship");
            entity.Property(e => e.Time).HasColumnName("time");
            entity.Property(e => e.Next).HasColumnName("next");

            entity.HasOne(d => d.NextNavigation).WithMany(p => p.Prevs)
                .HasForeignKey(d => d.Next)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ScheduleElements_FK_ScheduleElements");

            entity.HasOne(d => d.DestinationOrStopNavigation).WithMany(p => p.ScheduleElements)
                .HasForeignKey(d => d.DestinationOrStop)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ScheduleElements_FK_DestinationOrStop");

            entity.HasOne(d => d.DriverNavigation).WithMany(p => p.ScheduleElements)
                .HasForeignKey(d => d.Driver)
                .HasConstraintName("ScheduleElements_FK_Driver");

            entity.HasOne(d => d.OrderNavigation).WithMany(p => p.ScheduleElements)
                .HasForeignKey(d => d.Order)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ScheduleElements_FK_Order");

            entity.HasOne(d => d.SpaceshipNavigation).WithMany(p => p.ScheduleElements)
                .HasForeignKey(d => d.Spaceship)
                .HasConstraintName("ScheduleElements_FK_Spaceship");
        });

        modelBuilder.Entity<SpaceObject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SpaceObj__3213E83F5A0680F0");

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

            entity.HasOne(d => d.SystemCenterNavigation).WithMany(p => p.InverseSystemCenterNavigation)
                .HasForeignKey(d => d.SystemCenter)
                .HasConstraintName("SpaceObjects_FK_SystemCenter");
        });

        modelBuilder.Entity<SpacePort>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SpacePor__3213E83F93BE01B5");

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

            entity.HasOne(d => d.PlanetNavigation).WithMany(p => p.SpacePorts)
                .HasForeignKey(d => d.Planet)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SpacePorts_FK_Planet");
        });

        modelBuilder.Entity<Spaceship>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Spaceshi__3213E83FA4949E06");

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

            entity.HasOne(d => d.CurrentStateNavigation).WithMany(p => p.Spaceships)
                .HasForeignKey(d => d.CurrentState)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Spaceships_FK_CurrentState");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3213E83F885FC247");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Customer).HasColumnName("customer");
            entity.Property(e => e.Driver).HasColumnName("driver");
            entity.Property(e => e.Login)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("login");
            entity.Property(e => e.PasswordHash).HasColumnName("passwordHash");
            entity.Property(e => e.Roles).HasColumnName("roles");

            entity.HasOne(d => d.CustomerNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Customer)
                .HasConstraintName("Users_FK_Customer");

            entity.HasOne(d => d.DriverNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Driver)
                .HasConstraintName("Users_FK_Driver");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public static void Do(Action<TransUniverseDbContext> action)
    {
        using TransUniverseDbContext db = new();
        action(db);
    }

    public static T Get<T>(Func<TransUniverseDbContext, T> func)
    {
        using TransUniverseDbContext db = new();
        return func(db);
    }
}
