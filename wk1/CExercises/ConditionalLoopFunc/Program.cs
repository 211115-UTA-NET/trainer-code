// // See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

// Console.WriteLine("####### Switch ########");



// Console.WriteLine("[1] - condition 1");
// Console.WriteLine("[2] - condition 2");
// Console.WriteLine("[3] - condition 3");
// Console.WriteLine("[4] - condition 4");
// Console.WriteLine("Please input a value");

// int test = int.Parse(Console.ReadLine());

// switch (test)
// {
//     case 1:
//         Console.WriteLine("Condition 1");
//     break;

//     case 2:
//         Console.WriteLine("Condition 2");
//     break;

//     case 3:
//         Console.WriteLine("Condition 3");
//     break;

//     case 4:
//         Console.WriteLine("Condition 4");
//     break;

//     default:
//         Console.WriteLine("Not a valid condition");
//     break;
// }






bool newTest = true;

// Console.WriteLine("this happends before the loop");
// while (newTest == true)
// {
//     Console.WriteLine("your test was true");
//     newTest = false;
// }
// Console.WriteLine("This happens after the loop");


Console.WriteLine("####### Do While #######");

int j = 2;
do
{
    Console.WriteLine(j);
    j++;

    if(j <= 4)
    {
        Console.WriteLine("Did the if work?");
    }
}
while(j<5);


Console.WriteLine("####### While #######");

j=5;
while(j<5)
{
    Console.WriteLine(j);
    j++;
}


// j=2;
// while( (j<5) && (j>1) )
// {

//     int q = 1;
//     Console.WriteLine(q);

//     q += 10;
//     Console.WriteLine(q);


//     Console.WriteLine(j);
//     j++;
// }