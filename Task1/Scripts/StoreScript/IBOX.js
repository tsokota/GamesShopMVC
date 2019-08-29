$(function () {
    var document = { body: {} };
    var window = {};
    eval(doAction.toString());

    try {
        var result = doAction(23123, 2);
        $('#IBOXID').text(result);
    }
    catch (e) {
        (function (exception) {
            $('#IBOXID').text(exception.toString());
        })(e);
    }
});