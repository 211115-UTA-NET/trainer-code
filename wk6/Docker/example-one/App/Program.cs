﻿var counter = 0;
//var max = args.Length != 0 ? Convert.ToInt32(args[0]) : -1;
var max = 60;
while (max == -1 || counter < max)
{
    Console.WriteLine($"Counter: {++counter}");
    await Task.Delay(1000);
}