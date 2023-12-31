using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace UserDAL;

public partial class UserDbContext : DbContext
{
    public UserDbContext()
    {
    }

    public UserDbContext(DbContextOptions<UserDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=host.docker.internal,22888;Database=UserDB;User ID=sa;Password=\"228c@T228\";Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3213E83F4478D9C0");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Customer).HasColumnName("customer");
            entity.Property(e => e.Driver).HasColumnName("driver");
            entity.Property(e => e.Login)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("login");
            entity.Property(e => e.PasswordHash).HasColumnName("passwordHash");
            entity.Property(e => e.Roles).HasColumnName("roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
