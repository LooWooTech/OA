$(document).ready(function () {
    $.openWindow = function (href) {
        $("#modal").modal({
            show: true,
            remote: href
        });
    };

    $.closeWindow = function () {
        $("#modal").modal("hide");
    };

    //弹窗
    $('.modal').on('hidden.bs.modal', function () {
        $(this).removeData('bs.modal');
    });

    $(".nano").nanoScroller();

});


