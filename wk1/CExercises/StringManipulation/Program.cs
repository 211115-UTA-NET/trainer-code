string testString = "abcdefghijklmnopqrstuvwxyz";
Console.WriteLine(testString);


//read individual characters
Console.WriteLine("########### Individual Character #########");
char myChar = testString[25];
Console.WriteLine(myChar);



//find the length of a string of characters
Console.WriteLine("########### Length #########");
//length function will return the length of the string
Console.WriteLine(testString.Length);


//Concatenation - n a series of interconnected things or events
// line up or join multiple strings one after another.
Console.WriteLine("########### Concat #########");

string one = "this is a string";

string two = "this is a different string";

Console.WriteLine( one + " " + two );

Console.WriteLine(one);
Console.WriteLine(two);



//Change cases to upper case, to lower case
Console.WriteLine("########### Case Changes #########");
testString = testString.ToUpper();
Console.WriteLine(testString);
testString = testString.ToLower();
Console.WriteLine(testString);




//chage case of a specific letter to upper or to lower case.






//trim end, trim start, trim
Console.WriteLine("########### Trim #########");
string badSpacing = "          hello         world        ";
Console.WriteLine("start" + badSpacing + "end");
Console.WriteLine("start" + badSpacing.TrimStart() + "end");
Console.WriteLine("start" + badSpacing.TrimEnd() + "end");
Console.WriteLine("start" + badSpacing.Trim() + "end");


//break down a string into component characters
Console.WriteLine("########### SubStrings #########");
string betterSpacing = "hello world";
Console.WriteLine(betterSpacing);
Console.WriteLine( betterSpacing.Substring(6));


//compare the contents of a string




