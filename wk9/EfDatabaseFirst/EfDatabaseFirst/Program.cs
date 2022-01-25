using System.Diagnostics;
using System.Linq.Expressions;
using EfDatabaseFirst.DataInfrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

// the workflow with entity framework
//   (the first one we'll learn, anyway, "database-first")
// ... is, set up the right nuget packages, then use a separate entity framework command-line utility
//   to generate code that will basically serve as our repositories, which it will make by
//   connecting to our database and examining the tables. this first step is "design-time"
//       the second step is have code that uses that dbcontext and those dbsets, and run it.
//       you'll need to provide the connection string to the dbcontext object somehow;
//       in an asp.net core app, runtime configuration (user secrets, environment variables) is the best way

string connectionString = await File.ReadAllTextAsync("C:/revature/richard-rps-db.txt");

// to make a dbcontext properly, first set up its DbContextOptions using a DbContextOptionsBuilder,
// then pass that to the context's constructor

// getting the options ready can be "one-time setup"
// this is an example of factory pattern - useful when object's config has too many possibilities to fit nicely in constructor parameters
DbContextOptionsBuilder<RPSContext> builder = new();
builder
    .UseSqlServer(connectionString)
    .LogTo(message => Debug.WriteLine(message), LogLevel.Information);
DbContextOptions<RPSContext> options = builder.Options;

// the dbcontext object is meant to be used for an operation or two, and then the next time we want to connect,
// you'll construct a new one
using (RPSContext context = new(options))
{
    // we can treat the DbSets (like context.Players) as collections of our "entities"
    //  (the entities are the objects that are also rows)

    // the DbSets and DbContext work together such that, when you try to access things in the DbSet,
    //   the DbContext will generate the right command, send it, receive the response, deserialize it into your objects.

    // EF provides async versions of any LINQ method that would result in needing to execute a SQL query,
    //   available on the DbSet.
    List<Player> players = await context.Players.ToListAsync();
    // we saw previously LINQ working with just .NET objects, using the IEnumerable interface.
    //   now we see LINQ is not just for that, there is also an IQueryable interface, with all the same LINQ methods,
    //    but this one supports translating the "query" implied by the LINQ calls into something else that's not .NET.
    // DbSet implements IQueryable.

    Console.WriteLine("-- players: --");
    foreach (var player in players)
    {
        Console.WriteLine($"{player.Name} ({player.Id})");
    }

    List<Round> myRounds = await context.Rounds
        .Where(r => r.Player1 == 1)
        .ToListAsync();

    Player specificPlayer = await context.Players
        .FirstAsync(p => p.Name == "nick");
    bool isIt = specificPlayer.Name == "nick";

    // context is disposed; connection is closed
}

using (RPSContext context2 = new(options))
{
    // a new connection to do the next thing
}
