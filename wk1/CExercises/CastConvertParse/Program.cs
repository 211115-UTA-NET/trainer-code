// See https://aka.ms/new-console-template for more information



Console.WriteLine("Hello, Richard!");

Console.WriteLine("Hello again");


//Casting
//casting does not change the value stored
Console.WriteLine("####### Casting Example ######");

string start = "Begin";
object z = start;
string end = (string)z;

Console.WriteLine("starting string: " + start);
Console.WriteLine("ending string: " + end);


//Conversion
//conversion may change the value stored
Console.WriteLine("####### Conversion Example ######");

// int a = 78765432; // -2bil - +2bil
// byte b = (byte)a; // 0 - 255

double a = 5.5;
int b = (int)a;

Console.WriteLine("The value of a is: " + a);
Console.WriteLine("The value of b is: " + b);


//Parsing
//parsing reads a string as numerical value

bool res;
int parsedString;
string newString = "5thiscannotbeparsed678";
res = int.TryParse(newString, out parsedString);
Console.WriteLine("String could be parsed: " + res);

if (res == true)
{
    parsedString = int.Parse(newString);
    Console.WriteLine("The value of parsedString is: " + parsedString);
}
else
{
    Console.WriteLine("The value of newString could not be parsed.");
}

