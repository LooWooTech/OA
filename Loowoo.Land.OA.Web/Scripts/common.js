(function () {
    String.prototype.trimStart = function (trimStr) {
        if (!trimStr) { return this; }
        var temp = this;
        while (true) {
            if (temp.substr(0, trimStr.length) != trimStr) {
                break;
            }
            temp = temp.substr(trimStr.length);
        }
        return temp;
    };
    String.prototype.trimEnd = function (trimStr) {
        if (!trimStr) { return this; }
        var temp = this;
        while (true) {
            if (temp.substr(temp.length - trimStr.length, trimStr.length) != trimStr) {
                break;
            }
            temp = temp.substr(0, temp.length - trimStr.length);
        }
        return temp;
    };
    String.prototype.trim = function (trimStr) {
        var temp = trimStr;
        if (!trimStr) { temp = " "; }
        return this.trimStart(temp).trimEnd(temp);
    };

    $.fn.setUpload = function (uploadUrl, callback, beforeUpload) {
        var url = uploadUrl;
        if (typeof (uploadUrl) == "function") {
            url = uploadUrl();
        }
        var file = $(this);
        var fileId = file.attr("id");
        if (!fileId) {
            fileId = Math.random();
            file.attr("id", "file-" + fileId);
        }
        var controlName = file.attr("name");
        if (!controlName) {
            file.attr("name", fileId);
            controlName = fileId;
        }
        if (url.indexOf("?") == -1) {
            url += "?";
        }
        url += "&controlName=" + controlName;
        var form = file.parents("form");
        var formAction = form.attr("action");
        var formTarget = form.attr("target");

        file.change(function () {
            if (beforeUpload && !beforeUpload()) {
                reset();
                return;
            }
            var targetId = "iframe_upload" + Math.random();
            var iframe = $('<iframe width="0" height="0" frameborder="0" id="' + targetId + '" name="' + targetId + '">');
            document.body.appendChild(iframe[0]);
            form.attr({
                target: targetId,
                action: url,
                enctype: "multipart/form-data",
                method: "POST"

            });
            form.submit();
            iframe.load(function () {
                var content = $(this).contents().find("pre").html() || $(this).contents().find("body").html();
                try {
                    var json = eval("(" + content + ")");
                    callback(json);
                } catch (ex) {
                    alert("上传出错了" + ex.message);
                }
                reset();
                iframe.remove();
            });
        });

        function reset() {
            var fileId = file.attr("id");
            var newFile = file.clone();
            newFile.value = "";
            file.replaceWith(newFile);
            form.removeAttr("target");
            form.removeAttr("enctype");
            form.attr("action", formAction);
            if (formTarget) {
                form.attr("target", formTarget);
            }
            $("#" + fileId).setUpload(uploadUrl, callback, beforeUpload);
        }
    };

    $.wait = function (canDo, callback) {
        var waite = setInterval(function () {
            if (canDo()) {
                clearInterval(waite);
                callback();
            }
        }, 10);
    };

    $.fn.serializeObject = function () {
        var form = this[0];
        if (!form) return null;
        var data = {};

        var arr = $(this).serializeArray();
        $.each(arr, function (i, item) {
            setFields(data, item.name, item.value);
        });

        return data;

        function setFields(data, name, value) {
            if (name.indexOf('.') > 0) {
                var names = name.split('.');
                var subName = names[1];
                name = names[0];
                if (!data[name]) data[name] = {};
                setFields(data[name], subName, value);
                return;
            }

            if (data[name]) {
                data[name] += "," + value;
            }
            else {
                data[name] = value;
            }
        }
    };

    $.fn.setForm = function (options) {
        $(this).submit(function () {
            switch (typeof (options)) {
                case "string":
                    options = { url: options };
                    break;
                case "function":
                    options = { success: options };
                    break;
                default:
                    options = options || {};
                    break;
            }

            if (options.validate && !options.validate()) {
                return false;
            }

            options.url = $(this).attr("action");
            options.data = $(this).serializeObject();
            $.request(options);
            return false;
        });
    };

    $.request = function (url, data, success, error, global) {
        var options = null;
        if (arguments.length == 1) {
            switch (typeof (arguments[0])) {
                case "object":
                    options = arguments[0];
                    break;
                case "string":
                    options = { url: url };
            }
        } else {
            switch (typeof (arguments[1])) {
                case "function":
                    options = {
                        url: arguments[0],
                        data: null,
                        success: arguments[1],
                        error: arguments.length > 2 ? arguments[2] : null,
                        global: arguments.length > 3 ? arguments[3] : null,
                    };
                    break;
                default:
                    options = {
                        url: url,
                        data: data,
                        success: success,
                        error: error,
                        global: global
                    };
                    break;
            }
        }

        $.ajax({
            type: options.data ? "POST" : "GET",
            dataType: "text",
            global: options.global == undefined,
            url: options.url,
            data: options.data,
            success: function (responseText, statusText, xhr) {
                var json = {};
                if (responseText) {
                    json = $.parseJSON(responseText);
                }
                if (options.success) {
                    options.success(json, statusText, xhr);
                }
            }
        }).fail(function (xhr) {
            try {
                var data = {};
                if (xhr.responseText) {
                    data = $.parseJSON(xhr.responseText);
                    if (options.error) {
                        options.error(data);
                    }
                }
            } catch (err) {
                alert(err);
            }
        });
    };

    $.fn.loadUrl = function (href) {
        var self = $(this);
        href = href || self.attr("href") || "";
        if (href && href[0] == "#") {
            href = href.substring(1);
        }
        if (!href) {
            return false;
        }
        var selfId = "#" + self.attr("id");
        window.location.history[href] = selfId;

        self.attr("href", href);
        var hash = "#" + href;
        if (window.location.hash.trimEnd('/').toString() != hash.trimEnd('/').toString()) {
            window.location.hash = hash;
            return;
        }

        self.load(href, function (response, status, xhr) {
            window.location.hash = "#" + href;
            if (status == "error") {
                self.html("程序出错了");
            } else {
                try {
                    var target = $(response).filter(selfId);
                    if (target.length == 1) {
                        self.html(target.html());
                    }
                } catch (ex) {
                }
            }
        });
    };

    $.fn.reload = function (href) {
        var self = $(this);
        href = href || self.attr("href") || "";
        self.loadUrl(href);
    };
})();


$.ajaxSetup({
    beforeSend: function (xhr) {
        $("#loading").show();
    }
});

$(document).ajaxComplete(function () {
    $("#loading").hide();
});

$(document).ajaxError(function (event, jqxhr, settings, exception) {
    if (jqxhr.responseText) {
        try {
            var result = $.parseJSON(jqxhr.responseText);
            alert(result.message || result.content || "未知错误");
        } catch (ex) {

        }
    }
});
