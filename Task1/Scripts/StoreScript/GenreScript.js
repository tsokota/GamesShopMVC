var sel = $('select'),
    but = $('#ctrl button');
var actions = {
    opts: [[1, 0, ''], [1, 0, ':selected'], [0, 1, ':selected'], [0, 1, '']],
    resort: function (indx) {
        var opts = sel.eq(indx).find('option');
        opts.sort(function (a, b) {
            return $(a).text() > $(b).text();
        });
        sel.eq(indx).html(opts);
    },
    deselect: function () {
        $('option', sel).prop('selected', false);
    }
};
but.on('click', function () {
    var indx = $(this).index(),
        sets = actions.opts[indx];
    sel.eq(sets[0]).append(sel.eq(sets[1]).find('option' + sets[2]));
    actions.resort(sets[0]); // если нужно располагать по порядку
    actions.deselect(); // если снимать выделение
});

// submit button select all genres in right <select>
var butt = $('#subbutton');
butt.on('click', function () {
    $('#leftselect option').prop('selected', true);
});