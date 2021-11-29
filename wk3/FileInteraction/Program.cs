// See https://aka.ms/new-console-template for more information
using System.IO;
using System;


string path = "./testFile.txt";

// /c/Users/Richard/revature/trainer-code/wk3/FileInteraction/testFile.txt


string[] text = {"hi", "hello", "How's it going" };

File.AppendAllLines(path, text);






// StreamReader reader = new StreamReader(path);
// while(reader.Peek() != -1)
// {
//     Console.WriteLine(reader.ReadLine());
// }
// Console.WriteLine("End of file reached");
// reader.Close();





// StreamWriter writer = new StreamWriter(path);
// writer.WriteLine("This is a new line.");
// writer.Close();
