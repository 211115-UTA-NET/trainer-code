'use strict'; // opt in to ES5 fixes

// in old JS, variables had two possible scopes.
// 1. global scope
// 2. function scope

// console.log(x);
var x = 123;
var z = 4;

function printStuff(condition) {
    let asdf;
    console.log(z);
    var z = 3; // local vars hiding outer vars
    console.log(z);
    if (condition) {
        var y = 123; // function scope
        // commenting ^that line out causes a ReferenceError...
        //  because of a behavior called "hoisting".
        // javascript effectively moves all "var" declarations to the
        //    top of the function.
    }
    asdf = 123; // assigning to undeclared variable... create that variable in global scope!
        // ES5 fixed that by making it an error instead...
    console.log(asdf);
    console.log(y);
}

printStuff(true);
printStuff(false);

console.log(asdf);

// JS calls them Errors, not Exceptions
// but we can do try catch finally in the same way
// in fact.. you can throw and catch any value in JS, not just Errors

// ES6 added a way to stop bothering with function scope,
// two new ways to declare variables: let, const
// both of these use block scope, not function scope

// const prevents reassigning the value after declaration

// the improved behavior in ES5 is opt-in using strict mode.
// you enable strict mode by making the first statement of each script file
//   be the string literal 'use strict'.
