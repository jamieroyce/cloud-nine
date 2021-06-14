<!DOCTYPE html>

<%@ Page Language="C#" Trace="False" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="invoiced.aspx.cs" Inherits="_Default" Debug="true" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta http-equiv="x-ua-compatible" content="ie=edge">
    <title>Users</title>
    <link rel="icon" href="../img/favicon.png">
</head>
<body class="hold-transition sidebar-mini">
    <form id="form1" runat="server">
        <asp:GridView
            ID="GridViewInvoice"
            runat="server"
            Width="100%"
            CssClass="mGridAppt"
            AllowPaging="True"
            BorderWidth="0"
            AutoGenerateColumns="False"
            GridLines="None"
            PagerStyle-CssClass="pgr"
            AlternatingRowStyle-CssClass="alt"
            EmptyDataText="  (No records were found...)"
            CellPadding="0">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID">
                    <HeaderStyle CssClass="no-display"></HeaderStyle>
                    <ItemStyle CssClass="no-display"></ItemStyle>
                </asp:BoundField>
                <asp:TemplateField SortExpression="name" HeaderText="Name">
                    <ItemTemplate>
                        <asp:TextBox ID="name" CssClass="col_med" AutoPostBack="True" runat="server" Text='<%# Eval("name") %>' Width="175" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <div class="row">
            <asp:Label runat="server" ID="ErrorText" Text="" />
        </div>
    </form>
</body>
</html>
