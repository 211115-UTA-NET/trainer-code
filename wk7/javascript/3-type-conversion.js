'use strict';

// js does a lot of implicit type conversion.
console.log('adsf' + 123);
console.log(true + 123); // 124
// a lot of it is weird like that.

// better to be explicit, like:
console.log(Number(true) + 123); // 124

// except in if-conditions, no need to be explicit there
// so, what are the values that convert to true (truthy)
//  and to false (falsy)?
// almost all values convert to "true", but these ones are falsy:

// false
// 0
// -0
// NaN
// "" (empty string)
// null
// undefined

// NOT falsy: [], {}, [0], anything else

// when you use the == operator, implicit type conversion can occur.

// JS has double equals == and triple equals ===
// === checks type equality and value equality.
// == tries to check value equality regardless of type.

function printEqualsResults(a, b) {
    console.log(`${a} == ${b}:  ${a == b}`);
    console.log(`${a} === ${b}: ${a === b}`);
}

printEqualsResults(3, '3'); // ==
printEqualsResults(0, []); // ==
printEqualsResults([], [0]);
printEqualsResults(false, [0]); // ==

// avoid == pretty much always
// https://dorey.github.io/JavaScript-Equality-Table/
