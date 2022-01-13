'use strict'


function myFunction()
{
    document.getElementById("TEST").innerHTML = "this is a new paragraph";
}

function formFunction()
{
    var x = document.getElementById("form");
    var text = "";


    const person = {fname: "john" , lname: "doe"}

    for ( let i in person)
    {

    text += person[i] + "<br>";

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


let batch = '{"Associates" : [' +
        '{"name":["Travis"], "lname": ["Boskowitz"]},' + 
        '{"name":["Jing"], "lname": ["Zhuang"]},' +
        '{"name":["Melinda"], "lname": ["Waggoner"]}]}'

const obj = JSON.parse(batch);

const myJSON = JSON.stringify(obj);

    
    document.getElementById("formOut").innerHTML = myJSON;
};






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

var foo = function (x) {return x+5;}
alert("foo" + foo(2));

//Function Statement/Declaration

alert('bar' + bar(2));
function bar(x) {return x+5;}
