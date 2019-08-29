var editorel = $('#editorSmall').val();
if (editorel != undefined) {
    var editor = CKEDITOR.replace('editorSmall', { toolbar: 'Basic', uicolor: '9AB8F3' });
    var editor2 = CKEDITOR.replace('editorSmall2', { toolbar: 'Basic', uicolor: '9AB8F3' });
    //$('form').submit(function (e) {
    //    for (instance in CKEDITOR.instances) {
    //        CKEDITOR.instances[instance].updateElement();
    //    }
    //    var content = unescape(CKEDITOR.instances.editorSmall.getData());
    //    if ($('form').valid() && content.length > 0) {
    //        return true;
    //    } else {
    //        alert('Кажется, есть ошибки при заполнении формы!');
    //        return false;
    //    }
    //});
}