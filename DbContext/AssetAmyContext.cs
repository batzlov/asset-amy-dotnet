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

    public virtual DbSet<Asset> assets { get; set; }

    public virtual DbSet<Expense> expenses { get; set; }

    public virtual DbSet<Revenue> revenues { get; set; }

    public virtual DbSet<User> users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("server=127.0.0.1;port=8889;database=asset-amy-dotnet;uid=root;pwd=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.7.34-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_unicode_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Asset>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.ToTable("Asset");

            entity.HasIndex(e => e.belongsToId, "Asset_belongsToId_fkey");

            entity.Property(e => e.id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.belongsToId)
                .HasColumnType("int(11)")
                .HasColumnName("belongsToId");
            entity.Property(e => e.createdAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(3)")
                .HasColumnType("datetime(3)")
                .HasColumnName("createdAt");
            entity.Property(e => e.description)
                .HasMaxLength(1000)
                .HasColumnName("description");
            entity.Property(e => e.name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.type)
                .HasColumnType("enum('P2P','STOCK','BOND','CRYPTO','REAL_ESTATE','COMMODITY','CASH')")
                .HasColumnName("type");
            entity.Property(e => e.updatedAt)
                .HasColumnType("datetime(3)")
                .HasColumnName("updatedAt");
            entity.Property(e => e.value).HasColumnName("value");

            entity.HasOne(d => d.belongsTo).WithMany(p => p.assets)
                .HasForeignKey(d => d.belongsToId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Asset_belongsToId_fkey");
        });

        modelBuilder.Entity<Expense>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.ToTable("Expense");

            entity.HasIndex(e => e.belongsToId, "Expense_belongsToId_fkey");

            entity.Property(e => e.id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.belongsToId)
                .HasColumnType("int(11)")
                .HasColumnName("belongsToId");
            entity.Property(e => e.createdAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(3)")
                .HasColumnType("datetime(3)")
                .HasColumnName("createdAt");
            entity.Property(e => e.description)
                .HasMaxLength(1000)
                .HasColumnName("description");
            entity.Property(e => e.name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.updatedAt)
                .HasColumnType("datetime(3)")
                .HasColumnName("updatedAt");
            entity.Property(e => e.value).HasColumnName("value");

            entity.HasOne(d => d.belongsTo).WithMany(p => p.expenses)
                .HasForeignKey(d => d.belongsToId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Expense_belongsToId_fkey");
        });

        modelBuilder.Entity<Revenue>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.ToTable("Revenue");

            entity.HasIndex(e => e.belongsToId, "Revenue_belongsToId_fkey");

            entity.Property(e => e.id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.belongsToId)
                .HasColumnType("int(11)")
                .HasColumnName("belongsToId");
            entity.Property(e => e.createdAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(3)")
                .HasColumnType("datetime(3)")
                .HasColumnName("createdAt");
            entity.Property(e => e.description)
                .HasMaxLength(1000)
                .HasColumnName("description");
            entity.Property(e => e.name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.updatedAt)
                .HasColumnType("datetime(3)")
                .HasColumnName("updatedAt");
            entity.Property(e => e.value).HasColumnName("value");

            entity.HasOne(d => d.belongsTo).WithMany(p => p.revenues)
                .HasForeignKey(d => d.belongsToId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Revenue_belongsToId_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.ToTable("User");

            entity.HasIndex(e => e.email, "User_email_key").IsUnique();

            entity.Property(e => e.id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.createdAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(3)")
                .HasColumnType("datetime(3)")
                .HasColumnName("createdAt");
            entity.Property(e => e.email)
                .HasMaxLength(191)
                .HasColumnName("email");
            entity.Property(e => e.firstName)
                .HasMaxLength(100)
                .HasColumnName("firstName");
            entity.Property(e => e.lastName)
                .HasMaxLength(100)
                .HasColumnName("lastName");
            entity.Property(e => e.password)
                .HasMaxLength(191)
                .HasColumnName("password");
            entity.Property(e => e.role)
                .HasDefaultValueSql("'USER'")
                .HasColumnType("enum('USER','ADMIN')")
                .HasColumnName("role");
            entity.Property(e => e.updatedAt)
                .HasColumnType("datetime(3)")
                .HasColumnName("updatedAt");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
