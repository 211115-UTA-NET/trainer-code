'use strict';

let counter = 0;

// load event on whole window is pretty late
// window.onload = function () {
// DOMContentLoaded event fires as soon as all the elements are created on the page
// even if they haven't quite finished loading yet
// with this, it doesn't really matter where you put the script in the html.
// often it's put at the end of the HEAD.
document.addEventListener('DOMContentLoaded', function () {
  let formOut = document.getElementById('formOut');
  let newElement = document.createElement('div');
  newElement.textContent = 'added asap';
  formOut.before(newElement);
});

function myFunction() {
  // textContent can be used too
  // document.getElementById("TEST").innerHTML = "this is a new paragraph";

  let testDiv = document.getElementById('TEST');

  let newElement = document.createElement('p');
  newElement.textContent = `this is a new paragraph ${++counter}`;
  // testElement.after(newElement); // inserts the element into the DOM after another
  testDiv.append(newElement); //put the new element INSIDE another, at the end

  // can set up event handlers this way too
  // newElement.onclick = function () { console.log(`clicked ${counter}`); }

  // usually though, we do it this way, because it supports
  // multiple handlers on the same element and with the same event type
  newElement.addEventListener('click', function (event) {
    // event handler functions are called by the browser
    // with an argument: the event object itself.
    // if you don't need it, don't need to declare that parameter.

      console.log(event.target); // the element that triggered the event
      event.target == newElement;
    console.log(`clicked ${counter}`);
  });
  // newElement.addEventListener('click', function () {
  //   console.log(`and clicked ${counter}`);
  // });
}

function formFunction() {
  var x = document.getElementById('form');
  var text = '';

  const person = { fname: 'john', lname: 'doe' };

  for (let i in person) {
    text += person[i] + '<br>';
  }

  // let batch =
  // {
  //     "Associates" :
  //     [
  //         {
  //             "name":["Travis"],
  //             "lname": ["Boskowitz"]
  //         },
  //         {
  //             "name":["Jing"],
  //             "lname": ["Zhuang"]
  //         },
  //         {
  //             "name":["Melinda"],
  //             "lname": ["Waggoner"]
  //         }
  //     ]
  // }

  let batch =
    '{"Associates" : [' +
    '{"name":["Travis"], "lname": ["Boskowitz"]},' +
    '{"name":["Jing"], "lname": ["Zhuang"]},' +
    '{"name":["Melinda"], "lname": ["Waggoner"]}]}';

  const obj = JSON.parse(batch);

  const myJSON = JSON.stringify(obj);

  document.getElementById('formOut').innerHTML = myJSON;
}

// var myName = 'Tommy', age = 21, message = "hello";
// var 5windows
// var MyName != myName
// const birthday = '19.01.1991';
// console.log(birthday);

// let john = new user();

// let user =
// {
//     name = 'john',
//     age = 30
// };

// var test = '33';

// console.log("hi there, i'm " + test);

// var result = 6

//console.log(result ** 2 == 36)

// = , == , ===

// = is for assignment of a variable
// == comparison opperator of the values
// === is a comparison of the variable TYPE

// let a  = 1; //value assignment

// let b = 1;

// // var result = (a == b); //value comparison

// // console.log(result);

// var result = ("1" === 1); //type comparison

// console.log(result);

// result = (1 === 1); //type comparison

// console.log(result);

// result = (a === b); //type comparison

// console.log(result);

// var sending = 2;

// function newFunc(sent)
// {
//     console.log("hi there, you sent " + sent);
//     return "success"
// }

// var result = newFunc(sending);
// console.log( result)

//Function expression

// var foo = function (x) {return x+5;}
// alert(foo);

//Function Statement/Declaration

// alert('bar' + foo(2));
// function bar(x) {return x+5;}

function asdf(a, b = 0, dontprint = true) {
  if (typeof a === 'string' && typeof b === 'string') {
    // do something different...
    // in a language like C# this part
    // could just be a different overload with
    // different param types
  }
  if (b === undefined) {
    // way to have default values
    b = 0;
    // (but ES6 default values handle most of the use cases for this)
  }
  let result = a + b;
  if (!dontprint) {
    console.log(result);
  }
  return result;
}

asdf(1);
