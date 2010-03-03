<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SandwichServices._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>

    <script language="javascript" type="text/javascript">
        function Button1_onclick() {
            var service = new SandwichServices.CostService();
            service.getSandwich(onSuccess,onFailure,null);
        }

        function onSuccess() {
            alert("subway freshfit rocks!");
        }

        function onFailure() {
            alert("sucker punch!");
        }    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="CostService.svc" />
        </Services>
    </asp:ScriptManager>
    </form>
    <p>
        <input id="Button1" type="button" value="Get my Sub!" onclick="return Button1_onclick()" /></p>
</body>
</html>
