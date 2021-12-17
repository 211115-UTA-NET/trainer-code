using System.Data;
using System.Data.SqlClient;

// next steps...
// we'd like to keep all the database-specific implementation details in their own classes
//     and even if its own project
// today we learned what's called the "connected architecture" in ADO.NET for queries
//     tomorrow we'll see "disconnected architecture" for when using less memory is less important than closing the connection quicker
// we'll probably want to have classes to represent the data we read and write with the database.
//    we might even want a separate set of classes for the DB compared to the rest of our code

string connectionString = File.ReadAllText("C:/revature/richard-book-db.txt");

//ListBooks();
ListBooksDisconnected();

Console.WriteLine("Enter a book title: ");
string title = Console.ReadLine()!;
Console.WriteLine("How many pages: ");
int pages = int.Parse(Console.ReadLine()!);

AddNewBook(title, pages);

void ListBooks()
{
    // steps of connected architecture
    // 1. open the connection
    using SqlConnection connection = new(connectionString);
    connection.Open();
    using IDbCommand command = new SqlCommand("SELECT * FROM Books;", connection);
    // 2. execute the command
    using IDataReader reader = command.ExecuteReader();

    // 3. process the results
    while (reader.Read())
    {
        string title = reader.GetString(0);
        int pages = reader.GetInt32(2);

        Console.WriteLine($"\"{title}\" with {pages} pages");
    }
    // 4. close the connection
    connection.Close(); // good practice to close connection ASAP, and explicitly

    // we can reopen that same connection and reuse it
    // it's no big deal to not do that and just instantiate a new one though
    //    because the SqlClient ADO.NET code maintains a "connection pool"
}

void ListBooksDisconnected()
{
    // disconnected architecture (for queries) focuses on closing the connection ASAP,
    //   by loading all the results into some general .NET objects (DataSet),
    // then closing the connection, then processing the results.

    // 1. open the connection
    using SqlConnection connection = new(connectionString);
    connection.Open();
    string commandText = @"SELECT B.Title, B.Author, B.Pages, G.Genre, FP.PrintFormat, FP.Price
        FROM Books AS B
        INNER JOIN FormatPrice AS FP ON B.Title = FP.Title
        INNER JOIN Genres AS G ON G.ID = B.GenreID;";
    using SqlCommand command = new(commandText, connection);
    // instead of directly managing the datareader, we have a dataadapter fill a dataset with the command
    using SqlDataAdapter adapter = new(command);
    DataSet dataSet = new();
    // 2. execute the command
    adapter.Fill(dataSet);
    // 3. close the connection
    connection.Close();
    // 4. process the results

    // inside the DataSet are DataTables (one per result set)
    // inside the DataTable are DataColumns and DataRows
    // DataSet was made before we had generics
    // in C#, you can use a foreach loop on anything that implements IEnumerable<T> (and you'll get a T out)
    // ...OR IEnumerable (and you get to implicitly downcast in the foreach loop)
    DataColumn? titleColumn = dataSet.Tables[0].Columns[0];
    foreach (DataRow row in dataSet.Tables[0].Rows)
    {
        Console.WriteLine($"{row["Author"]}: \"{row["Title"]}\" ({row["Pages"]} p.) [{row["Genre"]}]");
    }
}

    void AddNewBook(string title, int pages)
{
    using SqlConnection connection = new(connectionString);
    connection.Open();
    // vulnerable to sql injection
    //   that means: a malicious user is able to manipulate his input to create unexpected sql queries
    //   two main mitigations:
    //   (1) never put user input directly into a SqlCommand. always use a SqlParameter
    //   (2) your app should be connecting to the database with the minimum necessary permissions on that database user.
    //       (we would use DCL (Data Control Language) to manage that (CREATE USER, GRANT, REVOKE))
    using SqlCommand command = new(
        $"INSERT INTO Books (Title, Author, Pages, GenreID, PublisherID) VALUES (@title, 'E.F.Codd', @pages, 1, 1);",
        connection);
    command.Parameters.AddWithValue("@title", title);
    command.Parameters.AddWithValue("@pages", pages);
    command.ExecuteNonQuery();
    connection.Close();
}
