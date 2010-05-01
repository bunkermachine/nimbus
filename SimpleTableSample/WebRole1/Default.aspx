<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebRole1._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView
        id="contactsView"
        DataSourceId="contactData"
        DataKeyNames="PartitionKey"
        AllowPaging="False"
        AutoGenerateColumns="True"
        GridLines="Vertical"
        Runat="server" 
        BackColor="White" ForeColor="Black"
        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4">
        <Columns>
            <asp:CommandField ShowDeleteButton="true"  />
        </Columns>
        <RowStyle BackColor="#F7F7DE" />
        <FooterStyle BackColor="#CCCC99" />
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>    

    <br />        
    <asp:FormView
        id="frmAdd"
        DataSourceId="contactData"
        DefaultMode="Insert"
        Runat="server">
        <InsertItemTemplate>
            <asp:Label
                    id="nameLabel"
                    Text="Name:"
                    AssociatedControlID="nameBox"
                    Runat="server" />
            <asp:TextBox
                    id="nameBox"
                    Text='<%# Bind("Name") %>'
                    Runat="server" />
            <br />
            <asp:Label
                    id="addressLabel"
                    Text="Address:"
                    AssociatedControlID="addressBox"
                    Runat="server" />
            <asp:TextBox
                    id="addressBox"
                    Text='<%# Bind("Address") %>'
                    Runat="server" />
            <br />
            <asp:Button
                    id="insertButton"
                    Text="Add"
                    CommandName="Insert"
                    Runat="server"/>
        </InsertItemTemplate>
    </asp:FormView>

    <%-- Data Sources --%>
    <asp:ObjectDataSource runat="server" ID="contactData"     TypeName="WebRole1.ContactDataSource"
        DataObjectTypeName="WebRole1.ContactDataModel" 
        SelectMethod="Select" DeleteMethod="Delete" InsertMethod="Insert">    
    </asp:ObjectDataSource>

    </div>
    </form>
</body>
</html>
