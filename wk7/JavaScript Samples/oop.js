// Object Oriented Porgramming in JavaScript example
// by Jacob Davis
// Revature 2020

// In this example we'll be going through how to achieve some
//  OOP concepts in basic JavaSript. The ideas we will cover include:
//   1. Defining a class
//   2. Instantiating a class
//   3. Inheriting from a class

// The first lesson to consider in basic JS is that everything
// is an object: strings, numbers, and even functions. If you
// know about closures, then treating functions as full-
// fledged classes is only a small stretch.

// Here's an example class plus usage.
function Computer(name, ramSizeGb, cpuCores, cpuSpeedGhz) {
    this.name = name;
    this.ramSizeGb = ramSizeGb;
    this.cpuCores = cpuCores;
    this.cpuSpeedGhz = cpuSpeedGhz;

    this.printSpecs = function() {
        console.log("This computer is named " + name + ". It has " + ramSizeGb + " GB RAM, and a " + cpuCores + "-core, " + cpuSpeedGhz + "GHz processor.");
    }
}

var myComputer = new Computer("MyPC", 4, 4, 3.2);
myComputer.printSpecs();

// Things to note:
//  1. The function Computer is known as a constructor function. It 
//      may look quite familiar to Java programmers, as it takes
//      properties and sets them using 'this'. However, in JS,
//      functions are also properties to be set, so the constructor
//      doubles as a class definition, practically speaking.
//
//  2. The function is called with 'new', also reminiscent of Java.
//      Without 'new', nothing would happen, as Computer actually
//      doesn't ever use 'return'. New creates an empty object and
//      calls Constructor in the context of that object, thereby
//      attaching 'this' to the object and setting those properties.

// In regards to #2 above, we could rewrite the constructor like so:
function ComputerRewrite(name, ramSizeGb, cpuCores, cpuSpeedGhz) {
    var obj = {};

    obj.name = name;
    obj.ramSizeGb = ramSizeGb;
    obj.cpuCores = cpuCores;
    obj.cpuSpeedGhz = cpuSpeedGhz;

    obj.printSpecs = function() {
        console.log("This computer is named " + this.name + ". It has " + this.ramSizeGb + " GB RAM, and a " + this.cpuCores + "-core, " + cpuSpeedGhz + "GHz processor.");
    }

    return obj;
}

// And use it like so:
var myComputerRw = ComputerRewrite("MyBetterPC", 8, 8, 4.2);
myComputerRw.printSpecs();

// However, we prefer the first version. Its cleaner, less verbose,
//  and clearly shows we meant to write a constructor. In addition,
//  'new' clearly shows we meant to instantiate an object. These
//  are important considerations when writing easily understood
//  code.

// A major problem with the way we've constructed objects so far
//  is that we've put functions inside our constructors. This
//  is functionally OK, and will work, but it's bad style. Go
//  ahead and run the following line after the last example:
console.log(myComputerRw);

// It can be hard to see, but the function is defined inside 
//  the object; that is, in JSON form, we would see all 
//  properties defined, including the function. We don't
//  want to serialize the function definitions everytime
//  we send the object somewhere for instance, only the 
//  data, so we need to change how we define our objects.

// This is what the prototype property is for.

// Whenever we want every instance of am object to have
//  the same functionality, we attach those functions to
//  the prototype, like so:
function ComputerProto(name, ramSizeGb, cpuCores, cpuSpeedGhz) {
    this.name = name;
    this.ramSizeGb = ramSizeGb;
    this.cpuCores = cpuCores;
    this.cpuSpeedGhz = cpuSpeedGhz;
}

ComputerProto.prototype.printSpecs = function() {
    console.log("This computer is named " + this.name + ". It has " + this.ramSizeGb + " GB RAM, and a " + this.cpuCores + "-core, " + this.cpuSpeedGhz + "GHz processor.");
}

var myComputerProto = new ComputerProto("MyPC", 4, 4, 3.2);
myComputerProto.printSpecs();

// Now that we have an object with a configured prototype,
//  we can even inherit from it. To do this, we use the
//  call() function, which allows you to define 'this'
//  in the context of the function. Lets' define a laptop
//  child class:
function Laptop(name, ramSizeGb, cpuCores, cpuSpeedGhz, weight) {
    ComputerProto.call(this, name, ramSizeGb, cpuCores, cpuSpeedGhz);

    this.weight = weight;
}

var myLaptop = new Laptop("JacobsLatop", 4, 4, 3.0, 5);

myLaptop.printSpecs();

// If you run the above, you may notice that the Laptop
//  object now has the properties we defined for Computer,
//  but not the functions. That's because we defined the
//  functions on the prototype of Computer. In order to
//  accomplish this, several things must happen:
//   1. We must set the prototype of our child to equal 
//       that of our parent using Object.create()
//   2. We must set our constructor to equal that
//       of our child, rather than the parent

// This is called "parasitic combination inheritance."

// There are a couple of ways to do this. Mozilla uses
//  Object.defineProperty(), but I will show a way
//  that's a little cleaner.
var computerCopy = Object.create(ComputerProto.prototype);
computerCopy.constructor = Laptop;
Laptop.prototype = computerCopy;

// Here, we copy the Computer prototype into a new 
//  object, modify it so that the prototype uses
//  Laptop's constructor instead of Computer's,
//  and finally set that object, which is a 
//  prototype, as the value of Laptop's prototype.

// Here is Mozilla's method, for reference:
/* Laptop.prototype = Object.create(ComputerProto.prototype);
Object.defineProperty(Laptop.prototype, 'constructor', {
    value: 'Laptop',
    enumerable: false,
    writable: true
}); */

// Now we can printSpecs() on Laptop!
var myLaptop2 = new Laptop("JacobsLatop", 4, 4, 3.0, 5);
myLaptop2.printSpecs();

// The final topic we'll discuss here is overriding functions.
//  printSpecs() is the perfect example, because we added
//  weight and changed the thing from computer to laptop,
//  so the text needs a-changing.

Laptop.prototype.printSpecs() = function() {
    console.log("This laptop is named " + this.name + " and weighs " + weight + "lbs. It has " + ramSizeGb + "Gb RAM, and has a " + cpuCores + "-core, " + cpuSpeedGhz + "GHz processor.");
}

// Simply put, we overwrite, rather than override, the 
//  function in Laptop's prototype. 