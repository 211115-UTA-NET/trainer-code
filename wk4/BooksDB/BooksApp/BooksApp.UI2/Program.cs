using System.Data.SqlClient;

// next steps...
// we'd like to keep all the database-specific implementation details in their own classes
//     and even if its own project
// today we learned what's called the "connected architecture" in ADO.NET for queries
//     tomorrow we'll see "disconnected architecture" for when using less memory is less important than closing the connection quicker
// we'll probably want to have classes to represent the data we read and write with the database.
//    we might even want a separate set of classes for the DB compared to the rest of our code

string connectionString = File.ReadAllText("C:/revature/richard-book-db.txt");

ListBooks();

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
    using SqlCommand command = new("SELECT * FROM Books;", connection);
    // 2. execute the command
    using SqlDataReader reader = command.ExecuteReader();

    // 3. process the results
    while (reader.Read())
    {
        string title = reader.GetString(0);
        int pages = reader.GetInt32(2);

        Console.WriteLine($"\"{title}\" with {pages} pages");
    }
    // 4. close the connection
    connection.Close(); // good practice to close connection ASAP, and explicitly
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
