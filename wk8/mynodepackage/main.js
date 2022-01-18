let data = {
    name: '123',
    convert: () => '-123-'
};
function output(something) {
    return something.convert().length;
}
let result = output(data);
window.onload = () => {
    document.body.innerHTML = `from typescript ${result}`;
};
