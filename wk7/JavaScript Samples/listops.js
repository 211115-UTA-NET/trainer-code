// List Operations example
// by Jacob Davis
// Revature 2020

// In this document, we'll be covering three main functions:
//  map(), which applies the same effect to every item in an array using a callback and puts it into a new array, which is returned;
//  filter(), which keeps some elements and removes some based on a callback predicate, and returns a new, filtered array;
//  and reduce(), which reduces an array down into a single value using a callback and returns that value.

// Finally, we will explore chaining these operations together, a powerful way to leverage JS.

// Let's look at some examples.

// We will start with map(). Say we have an array of ages in months:
var ages = [ 520, 790, 651, 900 ];

// We want to convert this into a new array, where all the values are in years.

// We could do a for loop:
var agesInYears = [];
for (let i = 0; i < ages.length; ++i) {
    let inYears = ages[i]/12;
    agesInYears.push(inYears);
}
console.log("For loop - ages in years: " + agesInYears);

// And here is the equivalent with map()!
var agesInYearsMap = ages.map((value, index, array) => value/12);
console.log("Map - ages in years: " + agesInYearsMap);

// As you can see, the result is the same. It is interesting to note
//  that the map callback has three parameters: value is the value
//  at the 'current' element, index is the index of the element,
//  and array holds the value of the array that was passed in.

// You can rename these however you want, as it is the order that
// matters. Observe the naming below:
var agesInYearsButSilly = ages.map((haha) => haha/12);
console.log("Map - ages in years: " + agesInYearsButSilly);

// Next, let's look at filter() using the same array. Let's say
//  we want all of the ages below 700 months.
var agesBelow700 = ages.filter( (value) => value < 700);
console.log("Ages below 700: " + agesBelow700);

// Another interesting note is that an arrow funciton isnt
//  the only way to do this, though its very clean and 
//  commonly used. You can also pass a function object.
var agesBelow700Func = ages.filter( function(value) {
    return value < 700;
});
console.log("Ages below 700 w/ function: " + agesBelow700Func);

// When writing the function, though, you must use the return
//  keyword. This is because arrow functions are read "given this => return this"
//  and thus do not need the return statement. Functions, on the other hand, may
//  return nothiing.

// For more on arrow functions: https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Functions/Arrow_functions

// On to reduce(). Let's say we want the average age in months. We can sum the ages
//  with reduce and then average that sum.
var ageSum = ages.reduce((previousValue, currentValue) => previousValue + currentValue);
console.log("Average age in months: " + ageSum/ages.length);

// We can do the averaging in the reducer, but we need to provide an initial value
//  for previousValue, also known as the accumulator, or the callback won't get
//  run for the first element of the array. The initial value goes after the
//  arrow function!
var averageAgeInMo = ages.reduce((previousValue, currentValue, currentIndex, array) => previousValue + (currentValue/ages.length), 0);
console.log("Alternate average age in months: " + averageAgeInMo);

// Now that we've gone through the thee list operations, let's discuss chaining them
//  with an example. Suppose we want to identify all the above-60s in the group,
//  then take the average age of that subgroup. Why? No questions.
var above60sAvgAge = ages.map((value) => value/12)
    .filter((value) => value > 60)
    .reduce((previousValue, currentValue, currentIndex, array) => previousValue + (currentValue/array.length), 0);

console.log("Average age of people above 60: " + above60sAvgAge);