<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login1.aspx.cs" Inherits="Login1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta content="initial-scale=1.0, maximum-scale=1.0, user-scalable=no" name="viewport" />
    <meta name="msapplication-tap-highlight" content="no" />
    <title>用户登录</title>


</head>
<body>

<form id="form1" runat="server">
    <div>
     <asp:Login ID="Login2" runat="server" OnAuthenticate="Login2_Authenticate"></asp:Login>
    </div>
    </form>
</body>
</html>
