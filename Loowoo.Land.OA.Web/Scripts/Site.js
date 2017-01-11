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

    $("#contact").load("/contact")

    $(".nano").nanoScroller();

    $("#btn-contact").click(function () {
        var contact = $("#contact");
        var right = contact.css("right");
        contact.animate({ right: right == '5px' ? '-245px' : '5px' }, 100);
    });

});

function UpdateCalendar(list) {

    $(".calendar .date").each(function () {
        var self = $(this);
        var date = self.attr("data-date");
        var val = getDateValue(date);
        if (val) {
            self.append(val);
        }
    });

    function getDateValue(date) {
        for (var i = 0; i < list.length; i++) {
            var item = list[i];
            if (item.date == date) {
                return item.value;
            }
        }
    }
};


