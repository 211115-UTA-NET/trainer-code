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
  const dataContainer = document.getElementById('data-container');
  const errorDisplay = document.getElementById('error-display');
  const whichDataInput = document.getElementById('which-data');

  loadButton.addEventListener('click', () => {
    const xhr = new XMLHttpRequest();

    // define what will happen when the response is received
    //   XHR has a readyState property - goes from 0 to 4
    //     as the request is sent and response is received
    //   XHR has a "readystatechange" event when it changes
    xhr.onreadystatechange = () => {
      // console.log(xhr.readyState);
      // when the response is finished downloading
      if (xhr.readyState === 4) {
        // check status code
        if (xhr.status >= 200 && xhr.status < 300) {
          // success
          errorDisplay.hidden = true;
          console.log(xhr.status);
          console.log(xhr.responseText);
          let responseObj = JSON.parse(xhr.responseText);
          displayData(responseObj, dataContainer);
        } else {
          // failure
          errorDisplay.hidden = false;
          console.log(xhr.status);
          console.log(xhr.responseText);
          errorDisplay.textContent = `server error: ${xhr.status}`;
          dataContainer.textContent = '';
        }
      }
    };

    // set up the request (including headers, body, etc)
    // be careful letting user input construct your URLs without validation
    let url = `https://jsonplaceholder.typicode.com/users/${whichDataInput.value}`;
    xhr.open('GET', url);
    xhr.setRequestHeader('Accept', 'application/json');

    // send the request
    xhr.send();
    // next thing to happen would be the readystatechange handler
  });
});

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
    html += `<li>${user.name}</li>`
  }
  html += '</ul>';
  dataContainer.innerHTML = html;
}
