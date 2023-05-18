using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Entity;

public partial class SuitYouDbContext : DbContext
{
    public SuitYouDbContext()
    {
    }

    public SuitYouDbContext(DbContextOptions<SuitYouDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Skirt> Skirts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SuitYouDB;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Skirt>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Skirt__3214EC0760FADBB8");

            entity.ToTable("Skirt");

            entity.Property(e => e.ImgName).HasMaxLength(20);
            entity.Property(e => e.SkirtCutImgName).HasMaxLength(100);

            //entity.HasOne(d => d.User).WithMany(p => p.Skirts)
               // .HasForeignKey(d => d.UserId)
                //.HasConstraintName("FK__Skirt__UserId__3C34F16F");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC079CC690EC");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "UQ__User__AB6E6164BE443C86").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(35)
                .HasColumnName("email");
            entity.Property(e => e.Name).HasMaxLength(20);
            entity.Property(e => e.Password).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
