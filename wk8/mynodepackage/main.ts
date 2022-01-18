interface Data {
  name: string;
  convert: () => string;
}

let data: Data = {
  name: '123',
  convert: () => '-123-'
};

function output(something: Data): number {
  return something.convert().length;
}

let result = output(data);

window.onload = () => {
  document.body.innerHTML = `from typescript ${result}`;
};
