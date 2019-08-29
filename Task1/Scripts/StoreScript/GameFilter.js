$(function () {

    $('#applyfilter').click(function() {
        $('#Pagination_PageNumber').val(1);
        $('#gameFilters').submit();
    });

    $('#clearfilter').click(function() {
        $('#Pagination_PageNumber').val(1);
        var a = $('#gameFilters').attr('action');
        window.location.href = a;
    });

    $('#nextpage').click(function() {
        var curpage = Number($('#Pagination_PageNumber').val());
        $('#Pagination_PageNumber').val(curpage + 1);
        $('#gameFilters').submit();
    });

    $('#prevpage').click(function() {
        var curpage = Number($('#Pagination_PageNumber').val());
        if (curpage > 0) {
            $('#Pagination_PageNumber').val(curpage - 1);
        }
        $('#gameFilters').submit();
    });


    $('#gameFilters').submit(function () {
        $.ajax({
                url: $(this).attr('action') + '?' + $(this).serialize(),
                type: "Get",
            })
            .done(function(data) {
                var template = $.templates('#gameTemplate');
                var html = template.render(data, {
                    getDetailsLink: function(key) {
                        var url = $('#gameDetailsLinkTemplate').val() + key;
                        return url;
                    }
                });
                $('#gamesList').html(html);
            });
        return false;
    });
});