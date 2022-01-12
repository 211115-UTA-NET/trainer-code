// JavaScript is weakly-typed object-based language
// originally made for giving dynamic behavior to webpages
// and now used in that role as well as many others

// weakly-typed
// in JS, variables do not have a specific type
// values have types, but variables do not
// also, objects can gain and lose properties
//   through their lifetime

// object-based
//   objects aren't fundamentally based on classes,
//   they can just be created and evolve
//   structure independently.

// JS is interpreted, not compiled... whatever that means

// JS runs primarily in the browser
//   it started life with Netscape
//   Brendan Eich

// standardized as ECMAScript (ES)
// ES3, in the days of pre-HTML 4
// ES5 - fix a lot of weird inconsistent behavior,
//   turn silent errors into thrown errors
// <-- common browser baseline in somewhere here
// ES6 - added LOTS of things, including classes
// ES2016
// ES2017... etc not too many new features per version
//           async

console.log('Hello world')

// data types in JS
// 6 main data types
// 2 that have been added in later versions

// number
// in JS, just one data type for all numbers
// equiv. to double in C-based languages
let x = 0;
x = Infinity;
x = 3 * 6;
// arithmetic operators just like C# and any C-based language
x = 1 / 0; // infinity, not an error
x = 'asdf' - 5; // NaN, not an error

// boolean (true/false)
x = 3 == 3;
x = false;
// all the familiar boolean operators
x = true && false;
x = NaN == NaN; // false - have to use isNaN
x = isNaN(NaN); // true

// string
x = 'as"df\'';
x = "as'df\"";
// backtick string syntax
// added in ES6
// multiline, supports template literal (string interpolation)
x = `asdf
asdf
asdf`;
x = `3 + 3 == ${3 + 3}`;

// object
x = {}; // object literal
x.name = 'fred';
delete x.name;
x = {
    name: 'fred', // property of the object (like C# field, not C# property)
    'property with spaces': 5,
    3: 5
};
// x = x.name;
x['property with spaces'];
x = x[3];
// functions are object type, but typeof calls them "function"
//   for historical reasons
x = function () { console.log('asdf'); };
// that is "function expression" syntax.
// this is "function statement":
x();
function y(a) {
    console.log(a);
}
// added in ES6, "arrow function" syntax, basically the same as lambda expression in C#
x = () => { console.log('asdf'); };
x = a => a + a;
x(2, 2);
x('a', 'b');
y('qwer');
x = y();
x = [1, 2, 3]; // an Array - object type

// undefined
// special data type with only one possible value, undefined
// if you don't pass something for a function parameter... it will be undefined.
// if you don't return a value from a function, it returns undefined.
x = y();
x = undefined;

// null
// special data type with only one possible value, null
// represent missing data
// typeof says null is an object, wrongly
x = null;

// ES6 added Symbol data type
// a way of making guaranteed-unique identifiers

// ES2020 added BigInt
// storing arbitrary-size whole numbers

console.log(x);
console.log(typeof x);
