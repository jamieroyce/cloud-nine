<%@ Page Language="C#" AutoEventWireup="true" CodeFile="main_form.aspx.cs" Inherits="main_form" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title></title>
    <script language="javascript" type="text/javascript">

    function Button1_onclick()
    {
        var currWin = window.open(window.location, '_self');
        currWin.close();
    }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height: 537px; width: 859px" title="DEMO APP">

        <asp:DropDownList ID="statesDropDownList" runat="server" Height="21px" 
            Width="192px" Font-Size="8.25pt"              
            style="top: 81px; left: 43px; position: absolute" AutoPostBack="True"
            ontextchanged = "statesDropDownList_TextChanged">
        </asp:DropDownList>

        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" 
            Font-Size="Small" 
            style="z-index: 1; left: 43px; top: 140px; position: absolute" 
            Text="COUNTIES:"></asp:Label>

        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Arial" 
            Font-Size="Small" 
            style="z-index: 1; left: 42px; top: 61px; position: absolute" 
            Text="STATES:"></asp:Label>

        <asp:DropDownList ID="countiesDropDownList" runat="server" Enabled="False"
            style="top: 160px; left: 42px; height: 21px; width: 192px; position: absolute" 
            AutoPostBack="True" Font-Size="8.25pt" Width="192px" 
            ontextchanged="countiesDropDownList_TextChanged">
        </asp:DropDownList>

        <input type="button" value="CLOSE
 APP" onclick="Button1_onclick()" 
            style="z-index: 1; left: 79px; top: 306px; position: absolute;
            width: 122px; height: 53px; font: bold 14px Arial"/>
            
        <asp:Button ID="btnBack" 
            runat="server" Font-Bold="True" Font-Names="Arial Black" 
            style="z-index: 1; left: 257px; top: 65px; position: absolute; width: 85px; height: 54px" 
            Text="&lt;- BACK" onclick="btnBack_Click" Enabled="False"/>
            
        <asp:GridView ID="citiesOfCountyDG" runat="server" 
            style="top: 65px; left: 353px; position: absolute; height: 226px; width: 355px" 
            onrowdatabound="citiesOfCountyDG_RowDataBound" 
            onrowcommand="citiesOfCountyDG_RowCommand">
            <Columns>
                <asp:CommandField ButtonType="Button" CancelText="" CausesValidation="False" 
                    DeleteText="" EditText="" InsertText="" InsertVisible="False" NewText="" 
                    SelectText="Counties Dropdown" ShowCancelButton="False" ShowSelectButton="True" 
                    UpdateText="">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
                </asp:CommandField>
            </Columns>
        </asp:GridView>

        </div>
    </form>
</body>
</html>