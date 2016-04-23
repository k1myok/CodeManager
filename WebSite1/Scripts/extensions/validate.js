//验证社保编号 10位
function checkID(ID)
{
    if ((ID != null) && (ID != "") && (typeof (ID) != "undefined")) {
        if (ID.length == 10) {
            return true;
        }
        else {
            alert("社保编号格式不正确");
            return false;
        }
    }
    else
    {
        alert("请输入社保编号");
        return false;
    }
}
//验证身份证号
function checkIDCard(IDCard)
{
    if (IDCard != null && typeof (IDCard) != "undefined")
    {
        if ((IDCard.length == 18) || (IDCard.length == 15)) {
            var reg = /(^\d{15}$)|(^\d{17}(\d|X)$)/;
            if (reg.test(IDCard) === false) {
                alert("身份证格式错误，请重新输入！");
                return false;
            }
            return true;
        }
        else
        {
            alert("身份证格式不正确！请确认后重新输入！");
            return false;
        }
    }
    else
    {
        alert("请输入身份证号！");
        return false;
    }
}





