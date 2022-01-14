'use strict';

// what we're trying to do is a technique called AJAX
//   (asynchronous javascript and xml)
//   : instead of just using links, forms, etc to
//     send data to the server and get a new page at the same time...
//    instead, we can send requests at will without replacing the current page. then, with the response, we'll modify the DOM to show it or otherwise react accordingly

// the old school way to do this is a object called
//   XMLHttpRequest supported by virtually all browsers

document.addEventListener('DOMContentLoaded', () => {
  const loadButton = document.getElementById('load-button');
  const loadRpsButton = document.getElementById('load-rps-button');
  const dataContainer = document.getElementById('data-container');
  const errorDisplay = document.getElementById('error-display');
  const whichDataInput = document.getElementById('which-data');


  loadRpsButton.addEventListener('click', () => {
    fetch(`https://localhost:7175/api/Rounds?player=${whichDataInput.value}`)
      .then(res => {
        if (!res.ok) throw new Error(`server error ${res.status}`);
        return res.json();
      })
      .then(obj => {
        let html = '<ul>' + obj.map(x => `<li>${x.date}</li>`).join() + '</ul>';
        dataContainer.innerHTML = html;
      });
  });

  loadButton.addEventListener('click', () => {
    // be careful letting user input construct your URLs without validation
    let url = `https://jsonplaceholder.typicode.com/users/${whichDataInput.value}`;

    // fetch(url, {
    //   // method: 'GET', // default
    //   headers: { Accept: 'application/json' },
    //   // body: {}
    // })
    fetch(url)
      .then(response => {
        if (!response.ok) {
          throw new Error(`server error ${response.status}`);
        }
        return response.json(); // another promise
      })
      .then(obj => {
        errorDisplay.hidden = true;
        displayData(obj, dataContainer);
      })
      .catch(error => {
        errorDisplay.hidden = false;
        errorDisplay.textContent = error.message;
        dataContainer.textContent = '';
      });

    // sendHttpRequest(url, xhr => {
    //   // check status code
    //   if (xhr.status >= 200 && xhr.status < 300) {
    //     // success
    //     errorDisplay.hidden = true;
    //     console.log(xhr.status);
    //     console.log(xhr.responseText);
    //     let responseObj = JSON.parse(xhr.responseText);
    //     displayData(responseObj, dataContainer);
    //   } else {
    //     // failure
    //     errorDisplay.hidden = false;
    //     console.log(xhr.status);
    //     console.log(xhr.responseText);
    //     errorDisplay.textContent = `server error: ${xhr.status}`;
    //     dataContainer.textContent = '';
    //   }
    // });
  });
});

function sendHttpRequest(url, callback) {
  const xhr = new XMLHttpRequest();

  // define what will happen when the response is received
  //   XHR has a readyState property - goes from 0 to 4
  //     as the request is sent and response is received
  //   XHR has a "readystatechange" event when it changes
  xhr.onreadystatechange = () => {
    // console.log(xhr.readyState);
    if (xhr.readyState === 4) {
      // when the response is finished downloading
      callback(xhr);
    }
  };

  // set up the request (including headers, body, etc)
  xhr.open('GET', url);
  xhr.setRequestHeader('Accept', 'application/json');

  // send the request
  xhr.send();
  // next thing to happen would be the readystatechange handler
}

function displayData(users, dataContainer) {
  if (!(users instanceof Array)) {
    users = [users];
  }
  // let content = todoItem.title;
  // if (todoItem.completed) {
  //   content += ' (completed)';
  // } else {
  //   content += ' (incomplete)';
  // }
  // dangerous to use innerHTML based on the http response directly... maybe
  // potential XSS risk
  //   especially if the server's response is based ultimately on some user data
  let html = '<ul>';
  for (let user of users) {
    // debugger; // breakpoint
    html += `<li>${user.name}</li>`;
  }
  html += '</ul>';
  dataContainer.innerHTML = html;
}

function add(a, b) {
  return a + b;
}

let result = add(1, 2);
console.log(result); // the thing i want to do with the result

//------------------------------------

function printSomething(x) {
  console.log(x);
}

function addWithCallback(a, b, callback) {
  let result = a + b;
  callback(result);
}

addWithCallback(1, 2, result => {
  console.log(result); // the thing i want to do with the result
});
// or
addWithCallback(1, 2, printSomething);

// addWithCallback('1', 2, 3); // throw an error,
//  can't call a number like a function


// c#
// public class Program {
//     public static void Main() {
//         addWithCallback(1, 2, result => {
//             Console.WriteLine(result); // the thing i want to do with the result
//         });
//         // or
//         addWithCallback(1, 2, printSomething);
//     }

//     static void printSomething(int x) {
//         Console.WriteLine(x);
//     }

//     static void addWithCallback(int a, int b, Action<int> callback) {
//         let result = a + b;
//         callback(result);
//     }
// }
