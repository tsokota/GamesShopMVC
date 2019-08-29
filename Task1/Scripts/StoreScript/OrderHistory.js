$(document).ready(function () {
    $("#StartDateDTP").attr('value', $('#StartDateToString').attr('value'));
    $("#EndDateDTP").attr('value', $('#EndDateToString').attr('value'));

    $('#show').click(function () {
        $('#StartDateToString').attr('value', $('#StartDateDTP').attr('value'));
        $('#EndDateToString').attr('value', $('#EndDateDTP').attr('value'));
    });
});