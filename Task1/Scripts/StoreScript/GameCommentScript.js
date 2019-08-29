
var idComment = 0;
var userId = 0;
function setID(newID, Id)
{
    idComment = newID;
    userId = Id;
}


$.ajaxSetup({ cache: false });
$(function () {
   
    $("#dialog").dialog({
        autoOpen: false,
        modal: true,
        title:"Ban or Delete Comment",
        buttons: {
            "Delete": function () {
                $('#removeinput').val(idComment);
                $(this).dialog("close");
                $('#remove').click();

            },
            "Ban": function () {
                $('#baninput').val(idComment);
                $('#banUser').val(userId);
                $(this).dialog("close");
                $('#ban').click();
            }
        }
    });
            
    $(".openId").click(function () {
       
        $("#dialog").dialog("open");
    });
});
