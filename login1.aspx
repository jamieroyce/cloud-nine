<!DOCTYPE html>
<%@ Page Language="C#" Trace="False" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="login1.aspx.cs" Inherits="_Default" Debug="true" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"  
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<%@ Register Assembly="Shield.Web.UI" Namespace="Shield.Web.UI" TagPrefix="shield" %>	
	
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
	<meta charset="utf-8">
	<meta name="viewport" content="width=device-width, initial-scale=1">
	<meta http-equiv="x-ua-compatible" content="ie=edge">
	<meta http-equiv="refresh" content="300">
	
	<title>FLOW</title>

		<link rel="stylesheet" href="css/all.min.css" type="text/css"  />
		<link rel="stylesheet" href="css/font-awesome.min.css">
		<link rel="stylesheet" href="css/adminlte.css">
		<link rel="stylesheet" href="css/jquery-ui.css">
		<!-- <link rel="stylesheet" href="css/bootstrap.css"> -->
		<link rel="stylesheet" href="css/bootstrap-select.css">
		<link rel="stylesheet" href="css/bootstrap-datepicker.css">
		<!-- <link rel="stylesheet" type="text/css" href="css/style.css" /> -->
		<!-- <link rel="stylesheet" href="/css/bootstrap_min.css"> -->

		<script src="js/jquery-3.3.1.min.js"></script>

		<script src="js/bootstrap.js"></script>
		<script type="text/javascript" src="js/moment.js"></script>
		<script type="text/javascript" src="js/bootstrap-select.js"></script>
		<script type="text/javascript" src="js/bootstrap-datetimepicker.min.js"></script>		
		<script type="text/javascript" src="js/bootstrap-datepicker.js"></script>
		<script src="js/jquery.min.js"></script>
		<script src="js/bootstrap.bundle.min.js"></script>
		<script src="js/adminlte.js"></script>
		<script src="js/shieldui-all.min.js" type="text/javascript">//</script>
		<script>	
		$('.btn-group a').on('click',function(){
		  $('a').removeClass('active');
		  $(this).addClass('active');
		});	
		</script>	
	
	<link rel="icon" href="/img/favicon.png">
	 
</head>
<body>
    <form id="form2" runat="server" class="frmalg">

        <div class="container">
            <center>
                <h3>Asp.net Login form </h3>
            </center>
            <label for="uname"><b>Username</b></label>
            <asp:TextBox runat="server" ID="txt_Username" placeholder="Enter Username"></asp:TextBox>
            <label for="psw"><b>Password</b></label>
            <asp:TextBox runat="server" ID="txt_password" TextMode="Password" placeholder="Enter Password"></asp:TextBox>
            <asp:Button runat="server" ID="btn_Login" CssClass="lgnbtn" Text="Login" />
            <asp:Button runat="server" ID="btn_cancel" Text="Cancel" class="cnbtn" />
        </div>
    </form>
</body>
</html>