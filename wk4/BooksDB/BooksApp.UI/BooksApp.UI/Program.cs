// not all Microsoft-provided libraries/classes are part of the Standard Libraries
// lots of them are just published as assemblies and you need to download them

// in the old days (.NET Framework), you would install assemblies to the
// Global Assembly Cache (GAC)
// these days, instead, .NET Core, 6, etc uses software called NuGet
// to download needed assemblies from package registries, especially the default one at nuget.org.

using System.Data.SqlClient;

// a connection string is a somewhat standardized format to represent all credentials and options
// needed to open a connection to some remote data source. (in the case of SQL, it will definitely
// have a server URL, user, password, and database name at least)
// to build from scratch, look at: either the ADO.NET documentation on SQL Server,
// or the SQL Server documentation on ADO.NET, or... https://www.connectionstrings.com/

// let's NOT put our connection strings on github
string connectionString = File.ReadAllText("C:/revature/richard-book-db.txt");
// instead... some reasonable possibilities: command-line args, environment variables
// easier for now - either a gitignored file in the project directory
// or a file somewhere outside the git repo entirely

using SqlConnection connection = new(connectionString);
connection.Open();

string commandText = "SELECT * FROM Books;";

using SqlCommand command = new(commandText, connection);

using SqlDataReader reader = command.ExecuteReader();

while (reader.Read())
{
    // goes through each row at a time

    // several ways to get the data in the current row (by column index)
    string title = reader.GetString(0);
    //string title2 = reader["Title"].ToString();
    //int pages = int.Parse(reader[2].ToString());
    int pages = reader.GetInt32(2);

    Console.WriteLine($"\"{title}\" with {pages} pages");
}
