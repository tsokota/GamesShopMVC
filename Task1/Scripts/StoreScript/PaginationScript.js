$(function () {
    var curpage = Number($("#Pagination_PageNumber").val());
    var maxpage = Number($("#Pagination_TotalPageCount").val());

    $("#applyfilter").click(function () {
        $("#Pagination_PageNumber").val(1);

        $("#filterform").submit();
    });

    $("#nextpage").click(function () {

        $("#Pagination_PageNumber").val(curpage + 1);
        $("#filterform").submit();
    });
    $("#prevpage").click(function () {

        $("#Pagination_PageNumber").val(curpage - 1);
        $("#filterform").submit();
    });


    if (curpage >= maxpage) {
        $("#nextpage").prop('disabled', true);
    } else {
        $("#nextpage").prop('disabled', false);
    }


    if (curpage <= 1) {
        $("#prevpage").prop('disabled', true);
    } else {
        $("#prevpage").prop('disabled', false);
    }

});
