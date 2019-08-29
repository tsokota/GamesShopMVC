var template = $.templates("#commentTemplate");
var isModer = $('#IsModerator').val();
$(window).ready(UpdateComments());
$('#show_comments').click(function () { UpdateComments(); });

$('#del').click(function() {
    window.frames[0].document.body.innerHTML = "";
    $('#AuthorName').val('');
    $('#ParentId').val('');
    $('#QuteId').val('');
    $('#title').text("");
});

$("#addComment").click(AddComment);
function AddComment() {
    $.post($('#comlink').val(), {
        body: window.frames[0].document.body.innerHTML, parentId: $('#ParentId').val(), gamekey: $('#gamekey').val(),
        authorName: $('#authorName').val(), quoteId: $('#QuteId').val()
    })
        .done(function() {
            window.frames[0].document.body.innerText = "";
            $('#authorName').val('');
            UpdateComments();
        })
        .fail(function (data) {
            $('#addComment').on('submit');
            if (data.status == 201) {
                UpdateComments();
                window.frames[0].document.body.innerHTML = "";
                $('#authorName').val('');
                $('#ParentId').val('');
                $('#QuteId').val('');
                $('.newcomment').hide();
                $('#title').text("");
            }
            else {
                alert('There is some error occured.' + data.responseText);
            }

        });
    $('#addComment').off('submit');
    return false;
}



function UpdateComments() {
    var comments = $.get('/api/games/' + $('#GameKey').val() + '/comments');
    comments
     .done(function (data) {
         $('#show_comments').addClass("hidden");
         $('#hide_comments').removeClass("hidden");
         $('#comments').removeClass('hidden');
         $('#inputComment').removeClass('hidden');
         if (data.length != 0) {
             var html = template.render(data, {
                 isModerator: function () {
                     return isModer;
                 }

              
             });
             $('#comments').html(html);
             ButtonsInit();
         }
     })
     .fail(function () {
         $('#comments').html('Error getting comments.');
     });
}
$('#hide_comments').click(function () {
    $('#hide_comments').addClass("hidden");
    $('#show_comments').removeClass("hidden");
    $('#comments').addClass('hidden');
    $('#inputComment').addClass('hidden');
});

var idComment = 0;
var userId = 0;
function setID(newID, Id) {
    idComment = newID;
    userId = Id;
    $("#dialog").dialog("open");
}



$(function () {

    $("#dialog").dialog({
        autoOpen: false,
        modal: true,
        title: "Ban or Delete Comment",
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
    
    $(".dialog").click(function () {
        debugger;
        $("#dialog").dialog("open");
    });
});




function ButtonsInit() {
    $('.quoteButton').click(function () {
        var id = $(this).attr('id').substr(6, 9999999999);
        $('#QuteId').val(id);
        $('#ParentId').val(id);
        $('#ParentId').val(id);
        $('#title').text('Quote to "' + $("#author_" + id).text() + '"');
        var text = '<blockquote">' + $('#comment_' + id).text() + "</blockquote>";
        window.frames[0].document.body.innerHTML = text;
     
    });
    $('.answerButton').click(function () {
        var id = $(this).attr('id').substr(7, 9999999999);
        $('#ParentId').val(id);      
        $('#title').text('Answer to "' + $("#author_" + id).text() + '"');
        window.frames[0].document.body.innerHTML = "";

    });
}