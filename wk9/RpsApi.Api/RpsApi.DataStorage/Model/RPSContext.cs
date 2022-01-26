using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RpsApi.DataStorage.Model
{
    public class RPSContext : DbContext
    {
        public RPSContext(DbContextOptions<RPSContext> options)
            : base(options)
        {
        }

        // if we don't want warnings about nullable stuff...
        //   (because the compiler can't tell that the base class uses reflection to set these DbSets
        // 1. could make this property type nullable (not great because they won't be ever null)
        // 2. assert that null isn't null "= null!" - a way of saying "yes it's null at first,
        //       but by the time anyone uses it, i promise it won't be"
        public DbSet<Move> Moves { get; set; } = null!;
        public DbSet<Player> Players { get; set; } = null!;
        public DbSet<Round> Rounds { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Move>(ConfigureMove);
            modelBuilder.Entity<Player>(ConfigurePlayer);
            modelBuilder.Entity<Round>(ConfigureRound);
        }

        // three sources for model configuration in EF.

        // 1. conventions (assumed defaults).
        //       a lot of stuff is assumed based on your entity classes.
        //       e.g. if a property in the entity class is named "Id" or "(classname)Id",
        //            it is assumed by convention to be the PK.
        //       if a PK is not discovered by convention (or if you want to override that default)
        //          you'll need to use one of the other two sources of configuration. (every entity does need a PK)
        //       also, lots of defaults for what SQL type corresponds to what .NET type.
        // 2. data annotations (attributes on the entity classes)
        //       some things can't be configured with data annotations (e.g. composite PK)
        //       dbcontext scaffold does not use data annotations by default,
        //       but you can add the "--data-annotations" option, and then it will, for everything it can.
        // 3. fluent API (the code in DbContext.OnModelCreating)
        //       everything can be configured with fluent API.

        // each source overrides the ones above it (so fluent api overrides data annotations and conventions)


        private static void ConfigureRound(EntityTypeBuilder<Round> entity)
        {
            entity.HasIndex(e => e.Player1Id);
            entity.HasIndex(e => e.Player1MoveId);
            entity.HasIndex(e => e.Player2Id);
            entity.HasIndex(e => e.Player2MoveId);

            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("(sysdatetimeoffset())");

            // EF sees a relationship by convention any time you put a navigation property on an entity.
            //   (what makes it a navigation property? simply the fact that it is of a type that EF doesn't recognize
            //    as mapping to one column, like string or DateTime)

            // this is how you configure a relationship explicitly
            // (have to here because otherwise the RoundsAsPlayer1 nav property would not be seen by conventions)
            entity.HasOne(r => r.Player1)
                .WithMany(p => p.RoundsAsPlayer1)
                .HasForeignKey(r => r.Player1Id)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(r => r.Player2)
                .WithMany(p => p.RoundsAsPlayer2)
                .HasForeignKey(r => r.Player2Id)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(r => r.Player1Move)
                .WithMany(m => m.RoundsUsedByPlayer1)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(r => r.Player2Move)
                .WithMany(m => m.RoundsUsedByPlayer2)
                .OnDelete(DeleteBehavior.NoAction);
        }

        private static void ConfigurePlayer(EntityTypeBuilder<Player> entity)
        {
            entity.HasIndex(e => e.Username)
                .IsUnique();

            entity.Property(e => e.Username).HasMaxLength(50);
        }

        private static void ConfigureMove(EntityTypeBuilder<Move> entity)
        {
            entity.HasIndex(e => e.Name)
                .IsUnique();

            entity.Property(e => e.Name).HasMaxLength(30);

            entity.HasData(
                new Move("Rock") { Id = 1 },
                new Move("Paper") { Id = 2 },
                new Move("Scissors") { Id = 3 });
        }
    }
}
