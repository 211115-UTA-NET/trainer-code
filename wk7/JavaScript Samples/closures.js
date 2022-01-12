// Closures example
// by Jacob Davis
// Revature 2020

// Closures in JavaScript can be confusing, and may seem like black magic.
// Before we get to what a closure is, we should first consider the idea of a
//  self-invoking function, as these ideas often appear together. 

// Consider the following:
( function() { }) ();

// "What is that garbage?" you may ask. The function is defined inside the 
//  first set of parentheses, and then called, with the second set of parens
//  being where the parameters would go, were there any.

// Consider this:
var five = ( function(n) { return n + 1; }) (5);

console.log(five); // Output: 6

// Five will hold the value 6! The function is run immediately, and no
//  reference to it is kept. How does this play into closures?

// What if we had the following:
var addIncreasing = ( function() {
    var increasingCounter = 0;
    return function() { increasingCounter += 1; return increasingCounter; };
}) ();

console.log(addIncreasing()); // Output: 1
console.log(addIncreasing()); // Output: 2

// A self invoking function which returns a function which returns a number...
// I know. Nuts. But if you run this, notice that the value of increasingCounter
//  is saved! That is, the function returned by addIncreasing has its own personal
//  space where it can store persistent variables. An enCLOSED space, if you will.
// That's a JavaScript closure.

// What if we want to pass parameters inside the closure? Things get a little tricky.

// Consider the following example:
var addEverMore = ( function() {
    var everIncreasingAdd = 0;
    var returnVal = function(num) { everIncreasingAdd += 1; return num + everIncreasingAdd; };

    return returnVal;
});

// This works:
var func = addEverMore();
console.log(func(5)); // Output: 6
console.log(func(5)); // Output: 7

// This doesn't:
console.log(addEverMore(5)); // Output: 6
console.log(addEverMore(5)); // Output: 6

// A key difference here compared to the previous code bit is that addEverMore is 
//  lacking a pair of parens at the end of its definition. This means we are no
//  longer defining a self-invoking funciton; it's a regular function def now.
//  And THAT means that when we call addEverMore, it initializes a new closure,
//  in essence, preventing us from using the previous method to access those
//  variables. Now we have to store the function reference somewhere.

// Hope this helps! Here's some helpful links:
// https://hackernoon.com/how-to-use-javascript-closures-with-confidence-85cd1f841a6b