// See https://aka.ms/new-console-template for more information




static void Main()
{

    for (int counterForTheForLoop = 0; counterForTheForLoop < 5; counterForTheForLoop++)
    {
        int returnedValueFromSayHello = SayHello();
        Console.WriteLine(returnedValueFromSayHello);
    }

    int a,b;
    a=2;
    b=4;

    int c;

    c = NewFunc(a,b);  //call the function, send variables
    Console.WriteLine(c);

    c = NewFunc(5,6); //call the function, send values
    Console.WriteLine(c);

    Console.WriteLine(NewFunc(1,4));   //return function value to output. 

    c = Multiply(5,6);
    Console.WriteLine(c);
}

static int NewFunc(int x, int y)
{
 int z;
 z = x+y;   
 return z;

 //return (x+y);
}

static int Multiply(int x, int y)
{
 int z;
 z = x*y;   
 return z;
}

static int SayHello()
{
    Console.WriteLine("Hi There!");
    int j = NewFunc(7,4);
    return j;
}

Main();