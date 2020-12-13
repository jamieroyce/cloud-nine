<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
	<script src="./js/jquery.min.js"></script>
    <script type="text/javascript" src="../dev/js/MaxLength.min.js"></script>
    <script type="text/javascript">
        $(function () {
            //Normal Configuration
            $("[id*=TextBox1]").MaxLength({ MaxLength: 10 });

            //Specifying the Character Count control explicitly
            $("[id*=TextBox2]").MaxLength(
            {
                MaxLength: 15,
                CharacterCountControl: $('#counter')
            });

            //Disable Character Count
            $("[id*=TextBox3]").MaxLength(
            {
                MaxLength: 20,
                DisplayCharacterCount: false
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Width="300" Height="100"></asp:TextBox>
    <br />
    <br />
    <div id="counter">
    </div>
    <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine" Width="300" Height="100"
        Text="Mudassar Khan"></asp:TextBox>
    <br />
    <br />
    
    <asp:TextBox ID="TextBox3" runat="server" TextMode="MultiLine" Width="300" Height="100"></asp:TextBox>
    </form>
</body>
</html>
