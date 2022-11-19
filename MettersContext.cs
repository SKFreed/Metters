using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApiSQLite;

public partial class MettersContext : DbContext
{
    public MettersContext()
    {
    }

    public MettersContext(DbContextOptions<MettersContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Metter> Metters { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Metter>(entity =>
        {
            entity.Property(e => e.Status).HasDefaultValueSql("0");
            entity.Property(e => e.Usages).HasDefaultValueSql("0");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
