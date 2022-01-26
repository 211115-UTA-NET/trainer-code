using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RpsApi.DataStorage.Model;
using RpsApi.Logic;

namespace RpsApi.DataStorage
{
    public class EfRepository : IRepository
    {
        private readonly RPSContext _context;
        private readonly ILogger<EfRepository> _logger;

        public EfRepository(RPSContext context, ILogger<EfRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Logic.Round>> GetAllRoundsOfPlayerAsync(string name)
        {
            //_logger.LogInformation($"database connection test: {(await _context.Players.FirstAsync()).Name}");
            //await PlayerExistsAsync(name);

            //// example EF code for project 1 getting order history for one customer
            //_context.Customers
            //    .Include(c => c.Orders)
            //        .ThenInclude(o => o.Location)
            //    .First(c => c.Id == theId);

            //// example EF code for project 1 getting order details of one order
            //_context.Orders
            //    .Include(o => o.Location)
            //    .Include(o => o.Customer)
            //    //.Include(o => o.OrderProducts)
            //    //    .ThenInclude(op => op.Product)
            //    .Include(o => o.OrderProducts.Product) // alternative to above two lines
            //    .First(o => o.Id == theId);

            //await _context.Moves
            //    .FirstAsync(m => m.Name == "paper");

            List <Model.Round> rounds = await _context.Rounds
                .Include(r => r.Player1MoveNavigation)
                //.ThenInclude(m => m.RoundPlayer1MoveNavigations) // if i wanted to know all the rounds that player1 used that move in
                .Include(r => r.Player2MoveNavigation)
                .Where(r => r.Player1Navigation!.Name == name)
                .ToListAsync();

            // navigation properties represent the foreign-key-based relationships between entities
            //   they start out as null/empty because if EF loaded them by default, it might have to load 10000s of rows
            //    when you didn't need them all.
            // "eager loading" is our strategy to tell EF to load specific things with ".Include" and ".ThenInclude"

            // foreign key relationships between two rows are represented in EF in three distinct places.

            // 1. the foreign key value itself. (just like in the DB itself) (e.g. int Round.Player1)
            // 2. the (reference) navigation property on the entity that has the foreign key. (e.g. Player Round.Player1Navigation)
            // 3. the inverse of that navigation property, another (collection) navigation property, on the OTHER entity,
            //       the one referenced by the foreign key. (e.g. Player Player.RoundPlayer1Navigations)

            // you can use whichever one is most convenient in your query. (usually not the FK value)

            // at a simple level:
            //    the navigation properties are always null/empty if you don't load them with .Include.
            // but actually strictly speaking:
            //    any entities that this context instance has loaded, will be present in any navigation property on any other entity
            //    even if they weren't loaded in the same query or even if the property is not explicitly .Included.

            // another thing about querying data in EF:
            //   if you want to query only some columns of a row, you need to use anonymous type (or the column type if it's just one).
            //var names = await _context.Players.Select(p => new { p.Id, p.Name }).ToListAsync();

            // need to map from the EF model to the classes the consumers of this repository expect (Logic.Round)
            // (otherwise we could just do "return rounds;")
            return rounds.Select(r =>
            {
                // here, i have a string like "rock" and i want an enum value like Logic.Move.Rock
                var m1 = (Logic.Move)Enum.Parse(typeof(Logic.Move), r.Player1MoveNavigation!.Name);
                var m2 = (Logic.Move)Enum.Parse(typeof(Logic.Move), r.Player2MoveNavigation!.Name);
                return new Logic.Round(r.Timestamp, m1, m2);
            });
        }

        public async Task AddNewRoundAsync(string player1, string? player2, Logic.Round round)
        {
            Player firstPlayer = await EnsurePlayerExistsAsync(player1);
            Player? secondPlayer = null;
            if (player2 is not null)
            {
                secondPlayer = await EnsurePlayerExistsAsync(player2);
            }
            throw new NotImplementedException();
        }

        public async Task<bool> PlayerExistsAsync(string player)
        {
            //var x = player;
            //return await _context.Players.AnyAsync(p => p.Name == "nick') END; DROP TABLE Rps.Player;");

            // EF automatically SQL-escapes any strings involved, preventing SQL injection.
            // it also uses SqlParameters for any variables used in the query, further preventing SQL injection
            return await _context.Players.AnyAsync(p => p.Name == player);
            // little micro-optimization: if an async method's only "await" is right at the return statement
            //     then you can skip the await and the method doesn't need to be async (return the Task directly)
        }

        public async Task<Player> EnsurePlayerExistsAsync(string name)
        {
            if (await _context.Players.FirstOrDefaultAsync(p => p.Name == name) is Player player)
            {
                return player;
            }
            // if we get this far, FirstOrDefault returned null, meaning it doesn't exist in the DB

            // to insert data into the database, you instantiate the entity classes,
            // then call: DbContext.Add, DbContext.Update, DbSet.Add, DbSet.Update, any of those

            // don't set the ID... leave it at default 0
            // the context interprets default values of primary keys (like 0) as meaning "new row" instead of "modified row"
            var newPlayer = new Player { Name = name };

            // add entity to dbset. this does not generate any sql, the pending operation is stored ready to go in the context.
            _context.Players.Add(newPlayer);

            // apply all pending operations (in an ACID transaction together automatically)
            // (THIS generates and sends the sql)
            await _context.SaveChangesAsync();

            // the database is responsible for generating the ID (at least in THIS database)... EF knows this,
            //  and fixes up the entity to have the correct ID value as part of the overall insert.

            // also...
            // if you want to insert two rows to two tables, connected by a FK
            //  e.g., in p1, insert an Order AND an OrderProduct at the same time (to represent an order of one product)
            // in other words, i want to add a new order to the database of one product with a given quantity
            //  let's say, also, we have the customer entity already loaded in a variable, but not the location entity.

            // you can do it like this:
            //var order = new Order
            //{
            //    Customer = customer, // setting the navigation property betwen two entities WILL tell EF to set the right FK value.
            //                         // i don't have to set CustomerId also.
            //    LocationId = locationId, // setting the FK value. don't need to set the navigation property. this works too
            //};
            //// adding a new entity to a navigation property
            //order.OrderProducts.Add(new() { ProductId = product1Id, Quantity = quantity1 });
            ////order.OrderProducts.Add(new() { ProductId = product2Id, Quantity = quantity2 }); // if there were two products
            //_context.Orders.Add(order);
            //// ^ now the context knows that it needs to insert both the order and the orderproduct rows.
            //// dont have to explicitly Add the OrderProduct also, it's "visible" from the order that was Added.
            //await _context.SaveChangesAsync(); // if you forget to SaveChanges, there won't be any exceptions to let you know, the changes
            //                                   // just won't be saved.

            return newPlayer;
        }
    }
}
