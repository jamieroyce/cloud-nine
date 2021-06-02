<!DOCTYPE html>

<%@ Page Language="C#" Trace="False" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="_Default" Debug="true" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<%@ Register Assembly="Shield.Web.UI" Namespace="Shield.Web.UI" TagPrefix="shield" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>


    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta http-equiv="x-ua-compatible" content="ie=edge" />
    <title>Cloud Nine Property</title>
    <meta content="" name="description">
    <meta content="" name="keywords">

    <!-- Favicons -->
    <link href="assets/img/favicon.png" rel="icon">
    <link href="assets/img/apple-touch-icon.png" rel="apple-touch-icon">

    <!-- Google Fonts -->
    <link href="https://fonts.googleapis.com/css?family=Open+Sans:300,300i,400,400i,600,600i,700,700i|Raleway:300,300i,400,400i,500,500i,600,600i,700,700i|Poppins:300,300i,400,400i,500,500i,600,600i,700,700i" rel="stylesheet">


    <!-- Vendor CSS Files -->

    <link href="assets/vendor/aos/aos.css" rel="stylesheet">
    <link href="assets/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet">
    <link href="assets/vendor/bootstrap-icons/bootstrap-icons.css" rel="stylesheet">
    <link href="assets/vendor/boxicons/css/boxicons.min.css" rel="stylesheet">
    <link href="assets/vendor/glightbox/css/glightbox.min.css" rel="stylesheet">
    <link href="assets/vendor/swiper/swiper-bundle.min.css" rel="stylesheet">

    <!-- Template Main CSS File -->
    <link href="assets/css/style.css" rel="stylesheet">

    <!-- MDB -->
    <link rel="stylesheet" href="css/mdb.min.css" />
    <style>
        body {
            font: Poppins, sans-serif;
        }
    </style>
</head>
<body>
    <!--Main Navigation-->
  <!-- ======= Top Bar ======= -->
  <section id="topbar" class="d-flex align-items-center">
    <div class="container d-flex justify-content-center justify-content-md-between">
      <div class="contact-info d-flex align-items-center">
        <i class="bi bi-envelope-fill"></i><a href="mailto:contact@example.com">jamie@cloudnineproperty.com</a>
        <i class="bi bi-phone-fill phone-icon"></i> +1 555 1212
      </div>
      <div class="social-links d-none d-md-block">
        <a href="#" class="twitter"><i class="bi bi-twitter"></i></a>
        <a href="#" class="facebook"><i class="bi bi-facebook"></i></a>
        <a href="#" class="instagram"><i class="bi bi-instagram"></i></a>
        <a href="#" class="linkedin"><i class="bi bi-linkedin"></i></i></a>
        <a href="#">Schedule A Demo</a>
      </div>
    </div>
  </section>
    <!-- ======= Header ======= -->
    <header id="header" class="d-flex align-items-center">
        <div class="container d-flex align-items-center justify-content-between">
            <!--<h1 class="logo"><a href="index.html">Cloud Nine Property</a></h1>-->
            <!-- Uncomment below if you prefer to use an image logo -->
            <a href="index.html" class="logo">
                <img src="assets/img/android-chrome-512x512.png" alt="" class="img-fluid"></a>

            <nav id="navbar" class="navbar">
                <ul>
                    <li><a class="nav-link scrollto active" href="Index.html">Home</a></li>
                    <li> <asp:HyperLink runat="server" id="signout" onserverclick="BtnSignOut_Click" href="Index.html">Sign Out</asp:HyperLink></li>
                   

                </ul>
                <i class="bi bi-list mobile-nav-toggle"></i>
            </nav>
            <!-- .navbar -->
        </div>
    </header>
    <!-- End Header -->
    <!-- ======= Hero Section ======= -->
    <div id="hero" class="bg-image shadow-2-strong">
        <div class="mask d-flex align-items-center h-100" style="background-color: white;">

            <div class="container" >
                <div class="row justify-content-center">
                    <div class="col-xl-5 col-md-8">
                        <form runat="server" class="bg-white rounded shadow-5-strong p-5">
                            <div style="text-align: center; margin-bottom: 10%">
                                <img src="/assets/img/favicon.svg" alt="Cloud Nine Logo" height="25%" width="25%" align="center"/>
                            </div>
                            <!-- Email input -->
                            <div class="form-outline mb-4">
                                <asp:TextBox ID="userEmail" TextMode="Email" CssClass="form-control" runat="server"></asp:TextBox>
                                <label class="form-label" for="form1Example1">Email address</label>
                            </div>

                            <!-- Password input -->
                            <div class="form-outline mb-4">
                                <asp:TextBox ID="userPassword" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                                <label class="form-label" for="form1Example2">Password</label>
                            </div>

                            <!-- 2 column grid layout for inline styling -->
                            <div class="row mb-4">
                                <div class="col d-flex justify-content-center">
                                    <!-- Checkbox -->
                                    <div class="form-check">
                                        <input class="form-check-input" type="checkbox" value="" id="form1Example3" checked />
                                        <label class="form-check-label" for="form1Example3">
                                            Remember me
                                        </label>
                                    </div>
                                </div>

                                <div class="col text-center">
                                    <!-- Simple link -->
                                    <a href="#!">Forgot password?</a>
                                </div>
                            </div>
                            <!-- Submit button -->
                            <button runat="server" id="Button5" class="btn btn-primary btn-block" onserverclick="BtnLogin_Click" title="Login">
                                Sign in
                            </button>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- End Hero -->

    <!--Main Navigation-->
    <script type="text/javascript" src="js/mdb.min.js"></script>
    <!-- Custom scripts -->
    <script type="text/javascript" src="js/script.js"></script>
</body>
</html>
