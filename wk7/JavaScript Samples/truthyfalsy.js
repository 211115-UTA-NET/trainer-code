// Truthy/Falsy example
// by Jacob Davis
// Revature 2020

// In this example, we will examine the concept of
//  truthiness and falsiness in JavaScript.
//  Originally, I thought this would be too simple
//  to really make a file out of, yet here we are.

// In JavaScript, some things are always falsy:
//  1. NaN, or Not a Number;
//  2. "", or an empty string;
//  3. null,
//  4. false,
//  5. 0,
//  6. undefined

// There's no wierdness here. These really are always 
//  false when compared to other things. Sometimes,
//  they are false even compared to themselves, like
//  NaN. However, the truly wierd comes to light
//  when trying to get a 'true' result.

// Let's run some tests, shall we?
if ("f") console.log("f, no ops, is true!"); // True
if ("f" == true) console.log("f == true is true!"); // False
if (!!"f" == true) console.log("f double negated == true is true!!!"); // True

// If you run these tests, you will find that the first
//  test is true, but the second is false. The third is
//  true.

// We, as wily JS programmers, may expect JS to coerce 
//  "f" to true based on the idea that, as a non-
//  empty string, "f" is truthy. It does not.
// The third test works because we explicitly coerce
//  "f" into a true boolean, which we negate twice.

// But wait! you might cry. Doesn't the order matter?
//  Let's check!
if (true == "f") console.log("true == f is true!"); // False

// This is still false. Coercion is driven by JavaScript's
//  own secret black wizardry, so we cannot implicitly
//  coerce "f" into a boolean just by switching the order
//  around. Sometimes we can, but not in cases where
//  we want to take advantage of truthiness. In these
//  cases, both values are likely converted into numbers,
//  and then compared. Unless the string is actually
//  a bunch of numbers, it will equal NaN, and even
//  if it is a number, if that number isn't 1, it
//  ain't true. 
console.log(Number("f")); // NaN
if ("2" == true) console.log("2 is true!"); // False
if ("1" == true) console.log("1 is true!"); // True

// Be careful out there with your boolean conversions!
// Hope this helps!