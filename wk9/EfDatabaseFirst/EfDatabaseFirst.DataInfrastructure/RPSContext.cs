using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EfDatabaseFirst.DataInfrastructure
{
    public partial class RPSContext : DbContext
    {
        public RPSContext()
        {
        }

        public RPSContext(DbContextOptions<RPSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Move> Moves { get; set; } = null!;
        public virtual DbSet<Player> Players { get; set; } = null!;
        public virtual DbSet<Round> Rounds { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Move>(entity =>
            {
                entity.ToTable("Move", "Rps");

                entity.HasIndex(e => e.Name, "UQ__Move__737584F62DA6727E")
                    .IsUnique();

                entity.Property(e => e.Name).HasMaxLength(30);
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.ToTable("Player", "Rps");

                entity.HasIndex(e => e.Name, "UQ__Player__737584F610BEE77C")
                    .IsUnique();

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Round>(entity =>
            {
                entity.ToTable("Round", "Rps");

                entity.HasIndex(e => e.Player1, "IX_Round_Player1");

                entity.HasIndex(e => e.Player1Move, "IX_Round_Player1Move");

                entity.HasIndex(e => e.Player2, "IX_Round_Player2");

                entity.HasIndex(e => e.Player2Move, "IX_Round_Player2Move");

                entity.Property(e => e.Timestamp).HasDefaultValueSql("(sysdatetimeoffset())");

                entity.HasOne(d => d.Player1Navigation)
                    .WithMany(p => p.RoundPlayer1Navigations)
                    .HasForeignKey(d => d.Player1)
                    .HasConstraintName("FK_Round_Player1");

                entity.HasOne(d => d.Player1MoveNavigation)
                    .WithMany(p => p.RoundPlayer1MoveNavigations)
                    .HasForeignKey(d => d.Player1Move)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Round_Player1Move");

                entity.HasOne(d => d.Player2Navigation)
                    .WithMany(p => p.RoundPlayer2Navigations)
                    .HasForeignKey(d => d.Player2)
                    .HasConstraintName("FK_Round_Player2");

                entity.HasOne(d => d.Player2MoveNavigation)
                    .WithMany(p => p.RoundPlayer2MoveNavigations)
                    .HasForeignKey(d => d.Player2Move)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Round_Player2Move");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
