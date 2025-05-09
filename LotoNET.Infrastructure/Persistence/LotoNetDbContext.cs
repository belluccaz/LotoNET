using System;
using Microsoft.EntityFrameworkCore;
using LotoNET.Domain.Entities;

namespace LotoNET.Infrastructure.Persistence;

public class LotoNetDbContext : DbContext
{
    public LotoNetDbContext(DbContextOptions<LotoNetDbContext> options)
        : base(options)
    {
    }

    public DbSet<Lottery> Lotteries => Set<Lottery>();
    public DbSet<Draw> Draws => Set<Draw>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Lottery>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();

        });

        modelBuilder.Entity<Draw>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.DrawNumber).IsRequired();
            entity.Property(e => e.DrawDate).IsRequired();
            entity.Property(e => e.NumbersDrawn)
                  .HasColumnType("integer[]");
            entity.Property(e => e.NumbersInOrder)
                  .HasColumnType("integer[]");

            entity.HasOne(d => d.Lottery)
                  .WithMany(l => l.Draws)
                  .HasForeignKey(d => d.LotteryId);
        });

        base.OnModelCreating(modelBuilder);
    }
}
