'use strict';

// common in JS to pass functions as arguments
// especially in an asynchronous context.
// in that case they are called "callback functions"

// with callback functions:
function slowCalculation(a, b, successCallback, failureCallback) {
  let result = a + b;
  successCallback(result);
  // setTimeout(() => successCallback(result), 1);
}

// ^ "library code"
//  library code is calling back to the calling code
// v "calling code"
slowCalculation(2, 3, result => {
  console.log(result);
});
console.log('asdf');

// another example, more like LINQ
[1, 2, 3].forEach(x => console.log(x));

// ES6 added "for..of" loop which is like C#'s foreach.
for (const x of [1, 2, 3]) {
  console.log(x);
}

console.log('-----------------------');

// we'll see next week, an object called Promise
// helps us manage callbacks more effectively
// in an asynchronous context.

let count = 123;

function newCounter() {
  let count = 0;

  return () => ++count;
} // this is the end of the block,
// so isn't that local variable gone / out of scope now?
// shouldn't it see the global variable.

// JS designers decided that
// if you reference a local variable from an
// "inner function", that inner function will
// keep that variable alive as long as it itself remains

// this behavior is called closure -
// the inner function "closes over" the variables
// it references.

function newCounter2() {
  let count = 0;

  return function () {
    count += 1;
    return count;
  }
}

// "what data type is counter"
const counter = newCounter();
// it's a function that returns a number

console.log(counter()); // 1
console.log(counter()); // 2
console.log(counter()); // 3
console.log(counter()); // 4

const counter2 = newCounter();

console.log(counter2()); // 1
console.log(counter2()); // 2


console.log(counter()); // 5

// --------------------

// another technique involving variable scope

// it's not good to pollute the global scope with
// your stuff
// we want more encapsulation than that.

// immediately-invoked function expression
// (IIFE)

(function () {
  // stuff
  // i can define temporary variables in here without
  // polluting the global scope
})();

let library = (function () {
  let privateData = 1;

  let privateFunction = function (value) {
    privateData += value;
  }

  return {
    publicData: 0,

    publicFunction(data) {
      this.publicData = privateFunction(data) + privateData;
      console.log(privateData);
    }
  }
})();
library.publicData = 5;
console.log(library.publicFunction(123));

// new in ES6
// http://es6-features.org/
