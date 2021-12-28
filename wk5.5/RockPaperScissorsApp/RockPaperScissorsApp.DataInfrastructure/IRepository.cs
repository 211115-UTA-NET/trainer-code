using RockPaperScissorsApp.Logic;

namespace RockPaperScissorsApp.DataInfrastructure
{
    // repository design pattern: implement basic create, read, update, delete
    // operations that the rest of your app needs, hiding the implementation details
    // advantages: it makes the rest of your code more unit-testable

    // often repositories don't "save changes" immediately, they have a special Save method
    // to wrap the accumulated changes in a transaction.
    
    // sometimes we have one repository per "entity", per type of thing we want to track in the DB
    // if we want to have a transaction across multiple entities then
    // we need the help of an additional design pattern called "unit of work"
    //  which manages multiple repositories and saves the changes of all of them at once.

    // be careful working on P0... you might read about "Entity Framework" online,
    // but P0 requires using basic ADO.NET (sqlconnection, sqlcommand, sqldatareader/sqldataadapter etc)
    public interface IRepository
    {
        IEnumerable<Round> GetAllRoundsOfPlayer(string name);
        void AddNewRound(string? player1, string? player2, Round round);

        //void Save();
    }
}
