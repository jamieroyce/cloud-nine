<!DOCTYPE html>
<%@ Page Language="C#" Trace="False" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="dashboard.aspx.cs" Inherits="_Default" Debug="true" %>
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
	
	<title>Mind Cloud</title>

		<link rel="stylesheet" href="../css/all.min.css" type="text/css"  />
		<link rel="stylesheet" href="../css/font-awesome.min.css">
		<link rel="stylesheet" href="../css/adminlte.css">
		<link rel="stylesheet" href="../css/jquery-ui.css">
		<!-- <link rel="stylesheet" href="css/bootstrap.css"> -->
		<link rel="stylesheet" href="../css/bootstrap-select.css">
		<link rel="stylesheet" href="../css/bootstrap-datepicker.css">
		<!-- <link rel="stylesheet" type="text/css" href="css/style.css" /> -->
		<!-- <link rel="stylesheet" href="/css/bootstrap_min.css"> -->

		<script src="../js/jquery-3.3.1.min.js"></script>

		<script src="../js/bootstrap.js"></script>
		<script type="text/javascript" src="../js/moment.js"></script>
		<script type="text/javascript" src="../js/bootstrap-select.js"></script>
		<script type="text/javascript" src="../js/bootstrap-datetimepicker.min.js"></script>		
		<script type="text/javascript" src="../js/bootstrap-datepicker.js"></script>
		<script src="../js/jquery.min.js"></script>
		<script src="../js/bootstrap.bundle.min.js"></script>
		<script src="../js/adminlte.js"></script>
		<script src="../js/shieldui-all.min.js" type="text/javascript">//</script>
		<script>	
            $('.btn-group a').on('click', function () {
                $('a').removeClass('active');
                $(this).addClass('active');
            });
        </script>	
	
	<link rel="icon" href="../assets/img/favicon.png">
	 
</head>
<body class="hold-transition sidebar-mini">
<div class="wrapper">
	<!-- Navbar -->
	<nav class="main-header navbar navbar-expand bg-white navbar-light border-bottom">
	<!-- Left navbar links -->
	<ul class="navbar-nav">
	  <li class="nav-item">
		<a class="nav-link" data-widget="pushmenu" href="#"><i class="fa fa-bars"></i></a>
	  </li>
	  <li class="nav-item d-none d-sm-inline-block">
		<a href="home.aspx" class="nav-link">Home</a>
	  </li>
	</ul>

	<!-- Right navbar links -->
	<ul class="navbar-nav ml-auto">
	  <li class="nav-item">
		<a href="#" class="nav-link">	  
		  <i class="fa fa-question fa-lg text-info" title="Refresh"></i>
		</a>
	  </li>	  
	  <li class="nav-item">
		<a href="home.aspx" class="nav-link">	  
		  <i class="fa fa-refresh fa-lg text-info" title="Refresh"></i>
		</a>
	  </li>	  
	  <li class="nav-item">
		<a href="settings/registrars.aspx" class="nav-link">	  
		  <i class="fa fa-cog fa-lg text-info" title="Setup Registrars"></i>
		</a>
	  </li>	  
	  </ul>
	</nav>
	<!-- /.navbar -->

	<!-- Main Sidebar Container -->
	<aside class="main-sidebar sidebar-light-primary elevation-1">
		<!-- Brand Logo -->
		<a href="home.aspx" class="brand-link">
		  <img src="../assets/img/favicon.png" alt="CLoudNine Logo" class="brand-image img-circle"
			   style="opacity: .8">
		  <span class="brand-text font-weight-light">CloudNine</span>
		</a>

    <!-- Sidebar -->
    <div class="sidebar">
		<!-- Sidebar Menu -->
		<nav class="mt-2">
			<ul class="nav nav-pills nav-sidebar flex-column" data-widget="treeview" role="menu" data-accordion="false">
			  <!-- Add icons to the links using the .nav-icon class
				   with font-awesome or any other icon font library -->
				   <li class="nav-item">
					   <a href="gi/log.aspx" class="nav-link" >
						   <i class="nav-icon fa fa-address-card text-info"></i>
						   <p>
							   Production Board
							</p>
						</a>
						<hr>
				<li class="nav-item">
					<a href="gi/invoiced.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-info"></i>
						<p>Invoiced</p>
					</a>
				</li>
				<li class="nav-item">
					<a href="gi/confirmed.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-secondary"></i>
						<p>Confirmed</p>
					</a>
				</li>
				<li class="nav-item">
					<a href="gi/definite.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-secondary"></i>
						<p>Definite</p>
					</a>
				</li>			
				<li class="nav-item">
					<a href="gi/possible.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-secondary"></i>
						<p>Possible</p>
					</a>
				</li>			
				<li class="nav-item">
					<a href="gi/opencycle.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-secondary"></i>
						<p>Open Cycle</p>
					</a>
				</li>			
				<li class="nav-item">
					<a href="gi/now_prospects.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-secondary"></i>
						<p>Prospects</p>
					</a>
				</li>			
				<li class="nav-item">
					<a href="gi/archive.aspx" class="nav-link">
						<i class="fa fa-archive nav-icon text-secondary"></i>
						<p>Archive</p>
					</a>
				</li>	  
				<li class="nav-item">
					<a href="gi/graph.aspx" class="nav-link">
						<i class="fa fa-pie-chart nav-icon text-info"></i>
						<p>Income Report</p>
					</a>
				</li>
				<li class="nav-item">
					<a href="gi/graphbyreg.aspx" class="nav-link">
						<i class="fa fa-pie-chart nav-icon text-info"></i>
						<p>Salesperson Report</p>
					</a>
				</li>	
			</li>	  
			</ul>
		</nav>
		<!-- /.sidebar-menu -->
    </div>
    <!-- Sidebar -->	</aside>

  	<form id="form1" runat="server">

	<!-- Content Wrapper. Contains page content -->
	<div class="content-wrapper">
		<!-- Content Header (Page header) -->
		<div class="content-header">
		  <div class="container-fluid">
			<!-- <div class="row mb-2"> -->
			  <!-- <div class="col-sm-6"> -->
				<!-- <h1 class="m-0 text-dark">Reg Tracking</h1> -->
			   <!-- </div> -->
			  <!-- <div class="col-sm-6"> -->
				<!-- <ol class="breadcrumb float-sm-right"> -->
				  <!-- <li class="breadcrumb-item"><a href="#">Home</a></li> -->
				  <!-- <li class="breadcrumb-item active">Reg Tracking</li> -->
				<!-- </ol> -->
			  <!-- </div> -->
			<!-- </div> -->
		  </div><!-- /.container-fluid -->
		</div>
		<!-- /.content-header -->
	
		<!-- Main content -->
		<section class="content">
			<div class="container-fluid">
				<div class="row">
					<div class="col-3">
						<div class="card">		
							<div class="card-body">
								<div class="row">
									<div class="col-12">
										<div class="row">
											<div class="col-12">
												<h1 class="m-0 text-dark">Payments</h1>
												<asp:Label id="OrgText" visible="false" runat="server" Text="Combined" />												
											</div>
											<div class="col-6 text-info">
												<h5>In</br>
												In/Confirmed</br>
												Last Week</h5>
											</div>
											<div class="col-6 text-info">
												<h5><asp:Label id="CardInvoiced" runat="server" /></br>
												<asp:Label id="CardInConfirmed" runat="server" /></br>
												<asp:Label id="LastWeekGI" runat="server" /></h5>
											</div>
										</div>
										<div class="row">
											<div class="col-12">				
																					
											</div>
										</div>	
										<div class="float-right">
											<a class="btn btn-outline-info" href="gi/graph.aspx">
											  <i class="fa fa-pie-chart text-info fa-lg"></i>
											</a>
											<a class="btn btn-outline-info" href="gi/log.aspx">
											  View Log
											</a>
										</div>										
									</div>
								</div>
								
							</div>
						</div>
					</div>				
				</div>
				<div hidden class="row">
				
				</div>				
										
				<p class="text-right">Mind Cloud / Version 0.1 / JSR / 2 June 2021</p>
				<asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
				<asp:Label visible="false" id="HeadText" style="padding-left:15px" runat="server" />
				<asp:Label runat="server" id="ErrorText" Text="" />		
				<asp:TextBox ID="lblWeekending" hidden runat="server" Text="" CssClass="form-control date_we" ></asp:TextBox>
							

			</div>
		</section>
	</div>
	<!-- content-wrapper -->
	</form>	
	<!-- ./form  -->
</div>
<!-- wrapper -->
</body>
</html>