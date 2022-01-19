"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.output = void 0;
var data = {
    name: '123',
    convert: function () { return '-123-'; }
};
function output(something) {
    return something.convert().length;
}
exports.output = output;
var result = output(data);
window.onload = function () {
    document.body.innerHTML = "from typescript ".concat(result);
};
