using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using EF = Microsoft.EntityFrameworkCore;
using asset_amy.Models;

namespace asset_amy.DbContext;

public partial class AssetAmyContext : EF.DbContext
{
    public AssetAmyContext()
    {
    }

    public AssetAmyContext(DbContextOptions<AssetAmyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asset> Assets { get; set; }

    public virtual DbSet<Expense> Expenses { get; set; }

    public virtual DbSet<Revenue> Revenues { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("server=127.0.0.1;port=8889;database=asset-amy-dotnet;uid=root;pwd=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.7.34-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_unicode_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Asset>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Asset");

            entity.HasIndex(e => e.BelongsToId, "Asset_belongsToId_fkey");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.BelongsToId)
                .HasColumnType("int(11)")
                .HasColumnName("belongsToId");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(3)")
                .HasColumnType("datetime(3)")
                .HasColumnName("createdAt");
            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Type)
                .HasColumnType("enum('P2P','STOCK','BOND','CRYPTO','REAL_ESTATE','COMMODITY','CASH')")
                .HasColumnName("type");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime(3)")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Value).HasColumnName("value");

            entity.HasOne(d => d.BelongsTo).WithMany(p => p.Assets)
                .HasForeignKey(d => d.BelongsToId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Asset_belongsToId_fkey");
        });

        modelBuilder.Entity<Expense>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Expense");

            entity.HasIndex(e => e.BelongsToId, "Expense_belongsToId_fkey");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.BelongsToId)
                .HasColumnType("int(11)")
                .HasColumnName("belongsToId");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(3)")
                .HasColumnType("datetime(3)")
                .HasColumnName("createdAt");
            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime(3)")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Value).HasColumnName("value");

            entity.HasOne(d => d.BelongsTo).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.BelongsToId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Expense_belongsToId_fkey");
        });

        modelBuilder.Entity<Revenue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Revenue");

            entity.HasIndex(e => e.BelongsToId, "Revenue_belongsToId_fkey");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.BelongsToId)
                .HasColumnType("int(11)")
                .HasColumnName("belongsToId");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(3)")
                .HasColumnType("datetime(3)")
                .HasColumnName("createdAt");
            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime(3)")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Value).HasColumnName("value");

            entity.HasOne(d => d.BelongsTo).WithMany(p => p.Revenues)
                .HasForeignKey(d => d.BelongsToId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Revenue_belongsToId_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "User_email_key").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(3)")
                .HasColumnType("datetime(3)")
                .HasColumnName("createdAt");
            entity.Property(e => e.Email)
                .HasMaxLength(191)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("firstName");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("lastName");
            entity.Property(e => e.Password)
                .HasMaxLength(191)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasDefaultValueSql("'USER'")
                .HasColumnType("enum('USER','ADMIN')")
                .HasColumnName("role");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime(3)")
                .HasColumnName("updatedAt");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
