// See https://aka.ms/new-console-template for more information
Console.WriteLine("Exceptions:");

bool loop = false;
string? myString;

do
{
    Console.WriteLine("Enter a string:");
    myString = Console.ReadLine();

    try
    {
        Console.WriteLine(myString[0]);
        loop = false;
    }
    catch (IndexOutOfRangeException e)
    {
        Console.WriteLine("Uh oh, you enterd no string!");
        loop = true;
    }
    finally
    {
        Console.WriteLine("Yup, it's saved");
    }


    // bool testOne = (myString == null);
    // bool testTwo = (a != b);
    // bool result = testOne || testTwo;
}
while( loop );








// int[] numbers = {0,1,2,3,4,5,6};
// Console.WriteLine("Enter and index between 0 and 6");
// int index = int.Parse(Console.ReadLine());

// try
// {
//     Console.WriteLine(numbers[index]);
// }
// catch (IndexOutOfRangeException e)
// {
//     Console.WriteLine("requested index not in array");
//     Console.WriteLine($"{e.GetType().Name}: {index} is outside the range of the array");
    
// }