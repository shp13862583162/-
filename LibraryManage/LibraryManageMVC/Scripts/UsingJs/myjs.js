function data_string_Date(str) {
    var d = eval('new ' + str.replace(/[/]/g, ''));
    return d;
}

Date.prototype.FormatStr = function (y) {
    var handle = this;
    var z = { M: handle.getMonth() + 1, d: handle.getDate(), h: handle.getHours(), m: handle.getMinutes(), s: handle.getSeconds() };
    y = y.replace(/(M+|d+|h+|m+|s+)/g, function (v) { return ((v.length > 1 ? "0" : "") + eval('z.' + v.slice(-1))).slice(-2) });
    return y.replace(/(y+)/g, function (v) { return handle.getFullYear().toString().slice(-v.length) });
}
