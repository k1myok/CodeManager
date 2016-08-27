LG.login = function ()
{
    $(document).bind('keydown.login', function (e)
    {
        if (e.keyCode == 13)
        {
            dologin();
        }
    });

    if (!window.loginWin)
    {
        var loginPanle = $("<form></form>");
        loginPanle.ligerForm({
            fields: [
                { display: '用户名', name: 'LoginUserName' },
                { display: '密码', name: 'LoginPassword', type: 'password' },
            ]
        });

        window.loginWin = $.ligerDialog.open({
            width: 400,
            height: 140, top: 200,
            isResize: true,
            title: '用户登录',
            target: loginPanle,
            buttons: [
            { text: '登录', onclick: function ()
            {
                dologin();
            } 
            },
            { text: '取消', onclick: function ()
            {
                window.loginWin.hide();
                $(document).unbind('keydown.login');
            } 
            }
            ]
        });
    }
    else
    {
        window.loginWin.show();
    }

    function dologin()
    {
        var username = $("#LoginUserName").val();
        var password = $("#LoginPassword").val();

        $.ajax({
            type: 'post', cache: false, dataType: 'json',
            url: '/Admin/Manage/Login',
            data: [
                    { name: 'UserName', value: username },
                    { name: 'Password', value: password }
                    ],
            success: function (result)
            {
                if (!result)
                {
                    LG.showError('登陆失败,账号或密码有误!');
                    $("#LoginUserName").focus();
                    return;
                } else
                {
                    location.href = location.href;
                }
            },
            error: function ()
            {
                LG.showError('发送系统错误,请与系统管理员联系!');
            },
            beforeSend: function ()
            {
                LG.showLoading('正在登录中...');
            },
            complete: function ()
            {
                LG.hideLoading();
            }
        });
    }

};
LG.Logount = function () {
    LG.ajax({
        url: '/Admin/Manage/Logout',
        success: function () {
           // LG.showSuccess('安全退出成功！');
            location.href = "/Admin/Manage/UserLogin";
        },
        error: function (message) {
            LG.showError(message);
        }
    });
};

LG.changepassword = function () {
    $(document).bind('keydown.changepassword', function (e) {
        if (e.keyCode == 13) {
            doChangePassword();
        }
    });

    if (!window.changePasswordWin) {
        var changePasswordPanle = $("<form></form>");
        changePasswordPanle.ligerForm({
            fields: [
                { display: '旧密码', name: 'OldPassword', type: 'password', validate: { maxlength: 50, required: true, messages: { required: '请输入密码'}} },
                { display: '新密码', name: 'NewPassword', type: 'password', validate: { maxlength: 50, required: true, messages: { required: '请输入密码'}} },
                { display: '确认密码', name: 'NewPassword2', type: 'password', validate: { maxlength: 50, required: true, equalTo: '#NewPassword', messages: { required: '请输入密码', equalTo: '两次密码输入不一致'}} }
            ]
        });

        //验证
        jQuery.metadata.setType("attr", "validate");
        LG.validate(changePasswordPanle);

        window.changePasswordWin = $.ligerDialog.open({
            width: 400,
            height: 190, top: 200,
            isResize: true,
            title: '用户修改密码',
            target: changePasswordPanle,
            buttons: [
            { text: '确定', onclick: function () {
                doChangePassword();
            }
            },
            { text: '取消', onclick: function () {
                window.changePasswordWin.hide();
                $(document).unbind('keydown.changepassword');
            }
            }
            ]
        });
    }
    else {
        window.changePasswordWin.show();
    }

    function doChangePassword() {
        var OldPassword = $("#OldPassword").val();
        var LoginPassword = $("#NewPassword").val();
        if (changePasswordPanle.valid()) {
            LG.ajax({
               url:'/Admin/Manage/ChangePassword',
                data: { OldPassword: OldPassword, NewPassword: LoginPassword },
                success: function () {
                    LG.showSuccess('密码修改成功');
                    window.changePasswordWin.hide();
                    $(document).unbind('keydown.changepassword');
                },
                error: function (message) {
                    LG.showError(message);
                }
            });
        }
    }

};






LG.addfavorite = function (success) {
    $(document).bind('keydown.addfavorite', function (e) {
        if (e.keyCode == 13) {
            doAddFavorite();
        }
    });

    if (!window.addfavoriteWin) {
        var addfavoritePanle = $("<form></form>");

        var menusTree = {
            id: 'addfavoriteMenusTree',
            url: '/Admin/Manage/GetMenus',
            checkbox: false,
            nodeWidth: 220
        };

        addfavoritePanle.ligerForm({
            fields: [
                 { display: "页面", name: "MenuNo", newline: true, labelWidth: 100, width: 220, space: 30, type: "select", comboboxName: "MyMenusMenuID",
                     options: { id: 'MyMenusMenuID', treeLeafOnly: true, tree: menusTree, valueFieldID: "MenuNo", valueField: "id" },
                     validate: { required: true, messages: { required: '请选择页面'} }
                 },

                 { display: "收藏备注", name: "FavoriteContent", newline: true, labelWidth: 100, width: 220, space: 30, type: "textarea" }

            ]
        });

        //验证
        jQuery.metadata.setType("attr", "validate");
        LG.validate(addfavoritePanle);

        window.addfavoriteWin = $.ligerDialog.open({
            width: 400,
            height: 190, top: 150, left: 230,
            isResize: true,
            title: '增加收藏',
            target: addfavoritePanle,
            buttons: [
            { text: '确定', onclick: function () {
                doAddFavorite();
            }
            },
            { text: '取消', onclick: function () {
                window.addfavoriteWin.hide();
                $(document).unbind('keydown.addfavorite');
            }
            }
            ]
        });
    }
    else {
        window.addfavoriteWin.show();
    }

    function doAddFavorite() {
        var manager = $.ligerui.get("MyMenusMenuID");
        if (addfavoritePanle.valid() && manager) {
            LG.ajax({
                url: '/Admin/Manage/AddFavorite',
                data: { MenuNo: manager.getValue(), FavoriteContent: $("#FavoriteContent").val() },
                success: function () {
                    LG.showSuccess('收藏成功');
                    window.addfavoriteWin.hide();
                    $(document).unbind('keydown.addfavorite');
                    if (success) {
                        success();
                    }
                },
                error: function (message) {
                    LG.showError(message);
                }
            });
        }

    }

};