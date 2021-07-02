<!DOCTYPE html>
<%@ Page Language="C#" Trace="False" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="register.aspx.cs" Inherits="_Default" Debug="true" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Assembly="Shield.Web.UI" Namespace="Shield.Web.UI" TagPrefix="shield" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <title>Cloud Nine Property</title>
    <link href="https://fonts.googleapis.com/css?family=Karla:400,700&display=swap" rel="stylesheet">
    <link rel="stylesheet" href="https://cdn.materialdesignicons.com/4.8.95/css/materialdesignicons.min.css">
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css">
    <link rel="stylesheet" href="assets/css/login.css">
</head>
<body>
    <main class="d-flex align-items-center min-vh-100 py-3 py-md-0">
        <div class="container">
            <form runat="server" >
            <div class="card login-card">
                <div class="row no-gutters">
                    <div class="col-md-5">
                        <img src="assets/img/login.jpg" alt="login" class="login-card-img">
                    </div>
                    <div class="col-md-7">
                        <div class="card-body">
                            <div class="brand-wrapper">
                                <img src="/assets/img/favicon.svg" alt="Cloud Nine Logo" height="25%" width="25%" align="center" />
                            </div>
                            <p class="login-card-description">Create CloudNine account</p>

                                <div class="form-group">
                                    <asp:TextBox ID="userName" placeholder="User Name" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:TextBox ID="emailAddress" placeholder="Email" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:TextBox ID="userPassword" placeholder="Password" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:TextBox ID="confirmPassword" placeholder="Confirm Password" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                    <!-- User Type input -->
                                    <label class="form-label" for="form1Example2">Select Account Type</label>
                                    <div class="form-outline mb-4">
	                        			<asp:DropDownList id="ddlUserType" placeholder="User Type" CssClass="form-control form-control-md" runat="server">
										    <asp:ListItem Value=""> </asp:ListItem>
										    <asp:ListItem Value="1"> Tenant</asp:ListItem>
										    <asp:ListItem Value="2"> Owner </asp:ListItem>
										    <asp:ListItem Value="3"> Property Manager </asp:ListItem>
										    <asp:ListItem Value="4"> Service Pro </asp:ListItem>
									    </asp:DropDownList>  
                                    </div>

                               <!-- Submit button -->
                                <button runat="server" id="Button5" class="btn btn-block login-btn mb-4" onserverclick="CreateUser_Click" title="Login">
                                    Create Account
                                </button>        
                                <asp:Label runat="server" id="ErrorText" Text="" />									
                            <nav class="login-card-footer-nav">
                                <a href="#!">Terms of use.</a>
                                <a href="#!">Privacy policy</a>
                            </nav>
                        </div>
                    </div>
                </div>
            </div>
          </form>
        </div>
    </main>
    <script src="https://code.jquery.com/jquery-3.4.1.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js"></script>
</body>
</html>
