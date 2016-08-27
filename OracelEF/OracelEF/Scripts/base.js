String.prototype.format = function () {
    var args = arguments;
    return this.replace(/\{(\d+)\}/g,
        function (m, i) {
            return args[i];
        });
}

//获取QueryString的数组
function getQueryString() {
    var result = location.search.match(new RegExp("[\?\&][^\?\&]+=[^\?\&]+", "g"));
    if (result == null) {
        return "";
    }
    for (var i = 0; i < result.length; i++) {
        result[i] = result[i].substring(1);
    }
    return result;
}

//根据QueryString参数名称获取值
function getQueryStringByName(name) {

    var result = location.search.match(new RegExp("[\?\&]" + name + "=([^\&]+)", "i"));

    if (result == null || result.length < 1) {
        return "";
    }
    return result[1];
}

//根据QueryString参数索引获取值
function getQueryStringByIndex(index) {
    if (index == null) {

        return "";

    }

    var queryStringList = getQueryString();

    if (index >= queryStringList.length) {

        return "";

    }

    var result = queryStringList[index];

    var startIndex = result.indexOf("=") + 1;

    result = result.substring(startIndex);

    return result;

}


function findJsonItemOfList(list, func) {
    for (var i = 0; i < list.length; i++) {
        var item = list[i];
        if (!func(item))
            continue;
        return item;
    }
}
function findJsonItemsOfList(list, func) {
    var result = new Array();
    for (var i = 0; i < list.length; i++) {
        var item = list[i];
        if (!func(item))
            continue;

        result.push(item);
    }
    return result;
}


(function ($) {
    $.fn.serializeFormJSONFieldsValues = function (fieldFunc) {
    var o = {
        "fields": "",
        "values": ""
    };
    var a = this.serializeArray();
    $.each(a, function () {
        if (fieldFunc != null && !fieldFunc(this.name))
            return;
        var value = null;
        var elements = $('#' + this.name);
        if (elements.length == 1) {
            if (elements[0].type == "number")
                value = this.value;
            else
                value = "'" + this.value + "'";
        }
        else
            value = "'" + this.value + "'";

        if (o[this.name]) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o.fields += (o.fields == "" ? "" : ",") + this.name;
            o.values += (o.values == "" ? "" : ",") + value;
        } else {
            o.fields += (o.fields == "" ? "" : ",") + this.name;
            o.values += (o.values == "" ? "" : ",") + value;
        }
    });
    return o;
    };
})(jQuery);





(function ($) {
    $.fn.serializeFormJSON = function () {
        var o = {};
        var a = this.serializeArray();
        var getValue = function (id, arg) {

        };
        $.each(a, function () {
            var value = null;
            var elements = $('#' + this.name);
            if (elements.length == 1) {
                if (elements[0].type == "number")
                    value = parseFloat(this.value);
                else
                    value = this.value;
            }
            else
                value = this.value;

            if (o[this.name]) {
                if (!o[this.name].push) {
                    o[this.name] = [o[this.name]];
                }
                o[this.name].push(value || '');
            } else {
                o[this.name] = value || '';
            }
        });
        return o;
    };
})(jQuery);


