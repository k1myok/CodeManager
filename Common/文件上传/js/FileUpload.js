// 源文件头信息：
// <copyright file="FileUpload.js">
// Copyright(c)2014-2034 Kencery.All rights reserved.
// 个人博客：http://www.cnblogs.com/hanyinglong
// 创建人：韩迎龙(kencery)
// 创建时间：2015-6-24
// </copyright>

;
(function($) {
    $.fn.SalesMOUNDUpload = function(options) {
        var defaults =
        {
            saveUrl: '',
            jqInput: '',
            maxSize: 1024 * 1024 * 100, //100M
            fnRemove: '', //移除文件 ，参数：文件名
            fnComplete: '' //每个文件成功 ，参数：服务器端返回内容
        };

        var opt = $.extend(defaults, options);

        function getByteToM(val) {
            if (isNaN(val)) return val;
            val = val / (1024 * 1024);
            val = Math.round(val * 100) / 100;
            return val;
        }

        return this.each(function() {
            var $this = $(this);
            $this.empty();

            if (typeof FormData == 'undefine') {
                alert('浏览器版本太低，不支持改上传！');
                return;
            }

            //表头
            if ($this.find('thead').length == 0) {
                var $thead = $('<thead>');
                var $th_tr = $('<tr>');
                $th_tr.append('<th>文件名</th>');
                $th_tr.append('<th>类型</th>');
                $th_tr.append('<th>大小</th>');
                $th_tr.append('<th>状态</th>');
                $th_tr.append('<th>操作</th>');
                $th_tr.appendTo($thead);
                $this.append($thead);
            }

            opt.jqInput[0].addEventListener('change', function(e) {
                var file = this.files[0];

                if (!file) {
                    return;
                }
                if (file.size > opt.maxSize) {
                    window.alert('文件超过最大');
                    return;
                }
                var fd = new FormData();
                var $table = $this;

                fd.append("uploadFile", file);
                var xhr = new XMLHttpRequest();
                xhr.open('POST', opt.saveUrl, true);

                xhr.upload.addEventListener("progress", uploadProgress, false);
                xhr.addEventListener("load", uploadComplete, false);
                xhr.addEventListener("error", uploadFailed, false);
                xhr.addEventListener("abort", uploadCanceled, false);

                //表中内容
                var $tr = $('<tr>');
                $tr.append('<td class="upload_name">' + file.name + '</td>');
                $tr.append('<td class="upload_type">' + file.type + '</td>');
                $tr.append('<td class="upload_size">' + getByteToM(file.size) + 'M' + '</td>');
                $tr.append('<td class="upload_status">' + 0 + '</td>');
                $tr.append('<td class="upload_action"><a href="javascript:void(0);">' + '取消' + '</a></td>');
                $tr.find('.upload_action a').unbind('click').bind('click', function() {
                    xhr.abort();
                });

                $table.append($tr);

                function uploadProgress(evt) {
                    if (evt.lengthComputable) {
                        var percentComplete = Math.round(evt.loaded * 100 / evt.total);
                        $tr.find('.upload_status').html(Math.round(percentComplete) + '%');
                    } else {
                        $tr.find('.upload_status').html('unable to compute');
                    }
                }

                function uploadComplete(evt) {
                    if (evt.target.status == 200) {
                        $tr.find('.upload_status').html('已完成');
                        $tr.find('.upload_action a').html('删除');
                        if (typeof opt.fnComplete == 'function') {
                            opt.fnComplete(evt.target.response);

                        }
                        $tr.find('.upload_action').unbind('click').bind('click', removeFile);
                    }
                }

                function uploadFailed() {
                    $tr.find('.upload_status').html('<a href="javascript:void(0);">×</a>');
                    $tr.find('.upload_status a').unbind('click').bind('click', function() {
                        $tr.remove();
                    });
                    $tr.find('.upload_action a').html('重试');
                    $tr.find('.upload_action a').unbind('click').bind('click', function() {
                        xhr.send(fd);
                    });
                }

                function uploadCanceled() {
                    $tr.remove();
                }

                function removeFile() {
                    $tr.remove();
                    if (typeof opt.fnRemove == 'function') {
                        opt.fnRemove(file.name);
                    }
                }

                xhr.send(fd);
            }, false);
        });
    };
}(jQuery));