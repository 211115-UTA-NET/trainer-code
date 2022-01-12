'use strict';

// object literal syntax
let obj = {
  property: 'value',
  behavior: function (a) {
    // debugger; // breakpoint
    return a * this.property.length;
  }
};

console.log(obj.behavior(3)); // 15
//           |
//          this

let func = obj.behavior;

console.log(obj.behavior === func);

try {
  console.log(func(3));
} catch (error) {
  console.log(error);
}

// in JS, "this" refers to whatever object was to the left of the dot
//   when the current function was called.
console.log(this);
// out here in global scope, "this" refers to a special object
// called the global object.
// "var" variables in global scope wind up as properties of this global object.

// debugger;
console.log(obj === this.obj);

// the difference between regular functions and arrow functions
// is when "this" gets bound.
// with arrow functions, it gets its value locked in at the time of
// defining the function value.
obj.behavior = a => a * this.property.length;
// console.log(obj.behavior(3)); // no length of undefined
// regular functions are useful as "methods" (behavior tied to one object)
// arrow functions are not, arrow functions are useful in contexts
//    more like LINQ in C#.

// another syntax for creating functions was addd in ES6 besides arrow function
// called method syntax

obj = {
  property: 'value',
  behavior(a) {
    // method syntax
    // debugger; // breakpoint
    return a * this.property.length;
  }
};

// how to make objects follow some common structure
// we can use the "new" operator in JS to treat functions as "constructors"
// by convention, functions intended to work this way are named in TitleCase

function Product(name, price) {
  this.name = name;
  this.price = price;

  this.formatPrice = function () {
    return `$${this.price}`;
  };
}

// when you do "new Function()" in JS, JS makes a new object, and
//   calls the constructor on it
let product = new Product('cup', 5.0);
// let product = new Product('cup', 5.0);
// let product = new Product('cup', 5.0);

// inheritance in JS is not class-based, it's "prototypal"
// which means directly from object to object.

// you can set an object's prototype by setting the special
// "__proto__" property that all objects have.
let obj1 = {
  number: 6,
  number2: 8,
  func() {
    return 6;
  }
};
let obj2 = { number: 3 };
obj2.__proto__ = obj1;

function SaleProduct(name, price, salePrice) {
  this.__proto__ = new Product(name, price);
  this.salePrice = salePrice;

  this.formatPrice = function (sale) {
    if (sale) {
      return `$${this.salePrice}`;
    }
    return this.__proto__.formatPrice();
  }
}

let saleProduct = new SaleProduct('plate', 6, 4);

console.log(saleProduct.formatPrice(true));
console.log(saleProduct.formatPrice(false));

// that's how you have to do it in pre-ES6 JS
// ES6 added classes


class Item {
  constructor(name, price) {
    this.name = name;
    this.price = price;
  }

  formatPrice() {
    return `$${this.price}`;
  }
}

class SaleItem extends Item {
  constructor(name, price, salePrice) {
    super(name, price);
    this.salePrice = salePrice;
  }

  formatPrice(sale) {
    if (sale) {
      return `$${this.salePrice}`;
    }
    return super.formatPrice();
  }
}

let saleItem = new SaleItem('plate', 6, 4);

console.log(saleItem.formatPrice(true));
console.log(saleItem.formatPrice(false));

// it's still not real OOP, classes
// are just syntactic sugar for prototypal inheritance.

// https://caniuse.com/
// https://kangax.github.io/compat-table/

// transpilation
//    (compiling, when the target language is similar
//     to the source language)
// new JS features (+ new web APIs like fetch)
//   are a combination of stuff that can be rewritten in old syntax
//    (let, const, classes, template literal, arrow functions, etc)
//   and stuff that can't (like new functions, new kinds of objects)

// for the stuff that can't, we have "polyfills"

// with those tools, we can write new JS and have a "build step"
// that transpiles to old JS for more browser compat.

// once people started doing that, they added new features of their own
// new languages that are meant to be compiled/transpiled to JS.
//  TypeScript
//  Elm, CoffeeScript
