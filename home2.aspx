<!DOCTYPE html>
<%@ Page Language="C#" Trace="False" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="home2.aspx.cs" Inherits="_Default" Debug="true" %>
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
	  <li class="nav-item d-none d-sm-inline-block">
		<a href="delivery.aspx" class="nav-link">Delivery View</a>
	  </li>
	</ul>

	<!-- Right navbar links -->
	<ul class="navbar-nav ml-auto">
	  <li class="nav-item">
		<a href="FLOW System Manual.docx" class="nav-link">	  
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
	<aside class="main-sidebar sidebar-light-primary elevation-4">
		<!-- Brand Logo -->
		<a href="home.aspx" class="brand-link">
		  <img src="img/scn_sm.png" alt="AdminLTE Logo" class="brand-image img-circle"
			   style="opacity: .8">
		  <span class="brand-text font-weight-light">FLOW</span>
		</a>
	  <div class="btn-group btn-group-sm btn-block">
		<a class="btn btn-default" onserverclick="Day_Click" id="day" runat="server" >Day</a>
		<a class="btn btn-default" onserverclick="Fdn_Click" id="fdn" runat="server" >Fdn</a>
		<a class="btn btn-default btn-block" onserverclick="Combined_Click" id="cmb" runat="server" >Combined</a>
	  </div>

    <!-- Sidebar -->
    <div class="sidebar">
		<!-- Sidebar Menu -->
		<nav class="mt-2">
			<ul class="nav nav-pills nav-sidebar flex-column" data-widget="treeview" role="menu" data-accordion="false">
			  <li class="nav-item has-treeview">
				<a href="#" class="nav-link">
				  <i class="nav-icon fa fa-money text-info"></i>
				  <p>
					Income
					<i class="right fa fa-angle-left"></i>
				  </p>
				</a>
				<ul class="nav nav-treeview">
				<a href="gi/log.aspx" class="nav-link" >
				  <i class="nav-icon fa fa-address-card-o text-info"></i>
				  <p>
					GI Log
				  </p>
				</a>
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
					<a href="gi/account.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-secondary"></i>
						<p>Account Total</p>
					</a>
				</li>
				<li class="nav-item">
					<a href="gi/fppp.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-secondary"></i>
						<p>Fully Partially Paid</p>
					</a>
				</li>
				<li class="nav-item">
					<a href="gi/pastInvoices.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-secondary"></i>
						<p>Past Invoices</p>
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
						<p>Registrar Report</p>
					</a>
				</li>					
				</ul>
				<hr>
			  </li>
				<li class="nav-item">
					<a href="starts/log.aspx" class="nav-link">
					  <i class="fa fa-play-circle nav-icon text-info"></i>
					  <p>Starts Log</p>
					</a>
				</li>				
				<hr>
			  
			  <li class="nav-item has-treeview">
				<a href="#" class="nav-link">
				  <i class="nav-icon fa fa-group text-info"></i>
				  <p>
					Bodies In The Shop
					<i class="right fa fa-angle-left"></i>
				  </p>
				</a>
				<ul class="nav nav-treeview">
				<li class="nav-item">
					<a href="bis/log.aspx" class="nav-link">
					  <i class="fa fa-address-card-o nav-icon text-info"></i>
					  <p>BIS Log</p>
					</a>
				</li>				
				<li class="nav-item">
					<a href="bis/intheshop.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-info"></i>
						<p>In The Shop</p>
					</a>
				</li>
				<li class="nav-item">
					<a href="bis/scheduled.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-secondary"></i>
						<p>Scheduled</p>
					</a>
				</li>
				<li class="nav-item">
					<a href="bis/named.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-warning"></i>
						<p>Fallen Off BIS</p>
					</a>
				</li>		
				<li class="nav-item">
					<a href="bis/archive.aspx" class="nav-link">
						<i class="fa fa-archive nav-icon text-secondary"></i>
						<p>Archive</p>
					</a>
				</li>	 
				<li class="nav-item">
					<a href="bis/graph.aspx" class="nav-link">
						<i class="fa fa-pie-chart nav-icon text-info"></i>
						<p>BIS Report</p>
					</a>
				</li>	
				</ul>
			  </li>		  
				<hr>			
			  <li class="nav-item has-treeview">
				<a href="#" class="nav-link">
				  <i class="nav-icon fa fa-check-square-o text-info"></i>
				  <p>
					In and Started
					<i class="right fa fa-angle-left"></i>
				  </p>
				</a>
				<ul class="nav nav-treeview">
				<li class="nav-item">
					<a href="inandstarted/log.aspx" class="nav-link">
					  <i class="fa fa-address-card-o nav-icon text-info"></i>
					  <p>In and Started Log</p>
					</a>
                </li>
				<li class="nav-item">
					<a href="inandstarted/inandstarted.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-info"></i>
						<p>In and Started</p>
					</a>
				</li>
				<li class="nav-item">
					<a href="inandstarted/scheduled.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-secondary"></i>
						<p>Scheduled</p>
					</a>
				</li>
				<li class="nav-item">
					<a href="inandstarted/named.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-secondary"></i>
						<p>Named</p>
					</a>
				</li>		
				<li class="nav-item">
					<a href="inandstarted/archive.aspx" class="nav-link">
						<i class="fa fa-archive nav-icon text-secondary"></i>
						<p>Archive</p>
					</a>
				</li>	  
				<li class="nav-item">
					<a href="reports/inandstarted.aspx" class="nav-link">
						<i class="fa fa-pie-chart nav-icon text-info"></i>
						<p>In and Started Report</p>
					</a>
				</li>	
				</ul>
			  </li>
				<hr>			  
			  <li class="nav-item has-treeview">
				<a href="#" class="nav-link">
				  <i class="nav-icon fa fa-graduation-cap text-info"></i>
				  <p>
					Comp Resign
					<i class="right fa fa-angle-left"></i>
				  </p>
				</a>
				<ul class="nav nav-treeview">
				<li class="nav-item">
					<a href="comprsn/log.aspx" class="nav-link">
					  <i class="fa fa-address-card-o nav-icon text-info"></i>
					  <p>Comp Resign Log</p>
					</a>
                </li>
				<li class="nav-item">
					<a href="comprsn/compresign.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-info"></i>
						<p>Comp Resign</p>
					</a>
				</li>
				<li class="nav-item">
					<a href="comprsn/scheduled.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-secondary"></i>
						<p>Scheduled</p>
					</a>
				</li>
				<li class="nav-item">
					<a href="comprsn/named.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-secondary"></i>
						<p>Named</p>
					</a>
				</li>		
				<li class="nav-item">
					<a href="comprsn/failedresign.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-danger"></i>
						<p>Failed to Resign</p>
					</a>
				</li>		
				<li class="nav-item">
					<a href="comprsn/archive.aspx" class="nav-link">
						<i class="fa fa-archive nav-icon text-secondary"></i>
						<p>Archive</p>
					</a>
				</li>	  
				<li class="nav-item">
					<a href="comprsn/graph.aspx" class="nav-link">
						<i class="fa fa-pie-chart nav-icon text-info"></i>
						<p>Comp Resign Report</p>
					</a>
				</li>	
				</ul>
			  </li>
				<hr>			  
			  <li class="nav-item has-treeview">
				<a href="#" class="nav-link">
				  <i class="nav-icon fa fa-handshake-o text-info"></i>
				  <p>
					Recruitment
					<i class="right fa fa-angle-left"></i>
				  </p>
				</a>
				<ul class="nav nav-treeview">
				<li class="nav-item">
					<a href="recruit/log.aspx" class="nav-link">
					  <i class="fa fa-address-card-o nav-icon text-info"></i>
					  <p>Recruitment Log</p>
					</a>
                </li>
				<li class="nav-item">
					<a href="recruit/arrived.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-info"></i>
						<p>Arrived</p>
					</a>
				</li>
				<li class="nav-item">
					<a href="recruit/signed.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-secondary"></i>
						<p>Project Prepares</p>
					</a>
				</li>
				<li class="nav-item">
					<a href="recruit/prospect.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-secondary"></i>
						<p>Prospects</p>
					</a>
				</li>		
				<li class="nav-item">
					<a href="recruit/archive.aspx" class="nav-link">
						<i class="fa fa-archive nav-icon text-secondary"></i>
						<p>Archive</p>
					</a>
				</li>	  
				<li class="nav-item">
					<a href="recruit/graph.aspx" class="nav-link">
						<i class="fa fa-pie-chart nav-icon text-info"></i>
						<p>Recruitment Report</p>
					</a>
				</li>	
				</ul>
			  </li>		
				<hr>	
			  <li class="nav-item has-treeview">
				<a href="#" class="nav-link">
				  <i class="nav-icon fa fa-user-plus text-info"></i>
				  <p>
					First Service Starts
					<i class="right fa fa-angle-left"></i>
				  </p>
				</a>
				<ul class="nav nav-treeview">
				<li class="nav-item">
					<a href="fss/log.aspx" class="nav-link">
					  <i class="fa fa-address-card-o nav-icon text-info"></i>
					  <p>FSS Log</p>
					</a>
				</li>				
				<li class="nav-item">
					<a href="fss/firststarts.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-info"></i>
						<p>Started</p>
					</a>
				</li>
				<li class="nav-item">
					<a href="fss/scheduled.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-secondary"></i>
						<p>Scheduled</p>
					</a>
				</li>
				<li class="nav-item">
					<a href="fss/named.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-secondary"></i>
						<p>Named</p>
					</a>
				</li>		
				<li class="nav-item">
					<a href="fss/archive.aspx" class="nav-link">
						<i class="fa fa-archive nav-icon text-secondary"></i>
						<p>Archive</p>
					</a>
				</li>	 
				<li class="nav-item">
					<a href="fss/graph.aspx" class="nav-link">
						<i class="fa fa-pie-chart nav-icon text-info"></i>
						<p>First Starts Report</p>
					</a>
				</li>	
				</ul>
			  </li>		  
				<hr>
			  <li class="nav-item has-treeview">
				<a href="#" class="nav-link">
				  <i class="nav-icon fa fa-shower text-info"></i>
				  <p>
					Purif Starts
					<i class="right fa fa-angle-left"></i>
				  </p>
				</a>
				<ul class="nav nav-treeview">
				<li class="nav-item">
					<a href="purif/log.aspx" class="nav-link">
					  <i class="fa fa-address-card-o nav-icon text-info"></i>
					  <p>Purif Starts Log</p>
					</a>
				</li>				
				<li class="nav-item">
					<a href="purif/firststarts.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-info"></i>
						<p>Started</p>
					</a>
				</li>
				<li class="nav-item">
					<a href="purif/scheduled.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-secondary"></i>
						<p>Scheduled</p>
					</a>
				</li>
				<li class="nav-item">
					<a href="purif/named.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-secondary"></i>
						<p>Named</p>
					</a>
				</li>		
				<li class="nav-item">
					<a href="purif/archive.aspx" class="nav-link">
						<i class="fa fa-archive nav-icon text-secondary"></i>
						<p>Archive</p>
					</a>
				</li>	 
				<li class="nav-item">
					<a href="purif/graph.aspx" class="nav-link">
						<i class="fa fa-pie-chart nav-icon text-info"></i>
						<p>Purif Starts Report</p>
					</a>
				</li>	
				</ul>
			  </li>		  
				<hr>				
			  <li class="nav-item has-treeview">
				<a href="#" class="nav-link">
				  <i class="nav-icon fa fa-calendar text-info"></i>
				  <p>
					Event Confirms
					<i class="right fa fa-angle-left"></i>
				  </p>
				</a>
				<ul class="nav nav-treeview">
				<li class="nav-item">
					<a href="event/log.aspx" class="nav-link">
					  <i class="fa fa-address-card-o nav-icon text-info"></i>
					  <p>Event Confirms Log</p>
					</a>
                </li>
				<li class="nav-item">
					<a href="event/graph.aspx" class="nav-link">
						<i class="fa fa-pie-chart nav-icon text-info"></i>
						<p>Event Report</p>
					</a>
				</li>	
				</ul>
			  </li>					  
			<hr>
			  
			</ul>
		</nav>
		<!-- /.sidebar-menu -->
    </div>
    <!-- Sidebar -->
	</aside>

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
												<h1 class="m-0 text-dark">Income</h1>
												<asp:Label id="OrgText" visible="false" runat="server" Text="Combined" />												
											</div>
											<div class="col-6 text-info">
												<h5>Invoiced GI</br>
												In/Confirmed</br>
												Last Week GI </h5>
											</div>
											<div class="col-6 text-info">
												<h5><asp:Label id="CardInvoiced" runat="server" /></br>
												<asp:Label id="CardInConfirmed" runat="server" /></br>
												<asp:Label id="LastWeekGI" runat="server" /></h5>
											</div>
										</div>
										<div class="row">
											<div class="col-12">				
												<shield:ShieldChart 
												ID="ShieldChart7R" 
												runat="server" AutoPostBack="true" Width="100%" Height="240px" 
												OnTakeDataSource="ShieldChart7R_TakeDataSource"> 
												<PrimaryHeader Text="Daily"> 
												</PrimaryHeader> 
												<ExportOptions AllowExportToImage="false" AllowPrint="false" /> 
												<TooltipSettings CustomPointText="{point.dataSeries.collectionAlias}: <b>{point.y}</b>"></TooltipSettings> 
												<Axes> 
													<shield:ChartAxisX 
														CategoricalValuesField="dayofweek"> 
													</shield:ChartAxisX> 
													<shield:ChartAxisY min=0> 
													</shield:ChartAxisY> 
												</Axes> 
												<DataSeries> 
													<shield:ChartLineSeries DataFieldY="thisweekinv" CollectionAlias="Inv"> 
														<Settings seriesDashStyle="Dash" color="#3DA5D9" EnablePointSelection="true" EnableAnimation="false"> 
															<DataPointText BorderWidth=""> 
															</DataPointText> 
															<PointMark Enabled="false"></PointMark>
														</Settings> 
													</shield:ChartLineSeries> 
													<shield:ChartLineSeries DataFieldY="thisweekinvcum" CollectionAlias="Inv"> 
														<Settings EnablePointSelection="true" color="#3DA5D9" EnableAnimation="false" > 
															<DataPointText BorderWidth=""> 
															</DataPointText> 
															<PointMark Enabled="false"></PointMark>
														</Settings> 
													</shield:ChartLineSeries> 
													<shield:ChartLineSeries DataFieldY="thisweekgi" CollectionAlias="InConf"> 
														<Settings seriesDashStyle="Dot" color="#FFB30F" EnablePointSelection="true" EnableAnimation="false"> 
															<DataPointText BorderWidth=""> 
															</DataPointText> 
															<PointMark Enabled="false"></PointMark>
														</Settings> 
													</shield:ChartLineSeries> 
													<shield:ChartLineSeries DataFieldY="thisweekcum" CollectionAlias="InConf"> 
														<Settings EnablePointSelection="true" color="#FFB30F" EnableAnimation="false" > 
															<DataPointText BorderWidth=""> 
															</DataPointText> 
															<PointMark Enabled="false"></PointMark>
														</Settings> 
													</shield:ChartLineSeries> 
													<shield:ChartLineSeries DataFieldY="lastweekcum" CollectionAlias="LastWk"> 
														<Settings EnablePointSelection="true" color="#AFC6E0" EnableAnimation="false"> 
															<DataPointText BorderWidth=""> 
															</DataPointText> 
															<PointMark Enabled="false"></PointMark>
														</Settings> 
													</shield:ChartLineSeries> 
												</DataSeries> 
												<Legend Align="Center" BorderWidth=""></Legend> 
											</shield:ShieldChart> 																					
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
					<div class="col-3">
						<div class="card">		
							<div class="card-body">
								<div class="row">
									<div class="col-12">
										<div class="row">
											<div class="col-12">
												<h1 class="m-0 text-dark">BIS</h1>
												<asp:Label visible="false" id="OrgText2" runat="server" Text="Combined" />												
											</div>
											<div class="col-6 text-info">
												<h5>In The Shop</br>
												Scheduled</br>
												Last Week BIS</h5>
											</div>
											<div class="col-6 text-info">
												<h5><asp:Label id="ThisWeek" runat="server" /></br>
												<asp:Label id="ThisWeekSched" runat="server" /></br>
												<asp:Label id="LastWeekBIS" runat="server" /></h5>
											</div>
										</div>
										<div class="row">
											<div class="col-12">				
												<shield:ShieldChart 
												ID="ShieldChartBIS7R" 
												runat="server" AutoPostBack="true" Width="100%" Height="240px" 
												OnTakeDataSource="ShieldChartBIS7R_TakeDataSource"> 
												<PrimaryHeader Text="Daily"> 
												</PrimaryHeader> 
												<ExportOptions AllowExportToImage="false" AllowPrint="false" /> 
												<TooltipSettings CustomPointText="{point.dataSeries.collectionAlias}: <b>{point.y}</b>"></TooltipSettings> 
												<Axes> 
													<shield:ChartAxisX 
														CategoricalValuesField="dayofweek"> 
													</shield:ChartAxisX> 
													<shield:ChartAxisY min=0> 
													</shield:ChartAxisY> 
												</Axes> 
												<DataSeries> 
													<shield:ChartLineSeries DataFieldY="thisweek" CollectionAlias="BIS (Daily)"> 
														<Settings seriesDashStyle="Dash" color="#3DA5D9" EnablePointSelection="true" EnableAnimation="false"> 
															<DataPointText BorderWidth=""> 
															</DataPointText> 
															<PointMark Enabled="false"></PointMark>
														</Settings> 
													</shield:ChartLineSeries> 
													<shield:ChartLineSeries DataFieldY="thisweekcum" CollectionAlias="BIS (Cum.)"> 
														<Settings EnablePointSelection="true" color="#3DA5D9" EnableAnimation="false" > 
															<DataPointText BorderWidth=""> 
															</DataPointText> 
															<PointMark Enabled="false"></PointMark>
														</Settings> 
													</shield:ChartLineSeries> 
													<shield:ChartLineSeries DataFieldY="lastweekcum" CollectionAlias="Last Week (Cum.)"> 
														<Settings EnablePointSelection="true" color="#AFC6E0" EnableAnimation="false"> 
															<DataPointText BorderWidth=""> 
															</DataPointText> 
															<PointMark Enabled="false"></PointMark>
														</Settings> 
													</shield:ChartLineSeries> 
												</DataSeries> 
												<Legend Align="Center" BorderWidth=""></Legend> 
											</shield:ShieldChart> 																					
											</div>
										</div>		
										<div class="float-right">
											<a class="btn btn-outline-info" href="bis/graph.aspx">
											  <i class="fa fa-pie-chart text-info fa-lg"></i>
											</a>
											<a class="btn btn-outline-info" href="bis/log.aspx">
											  View Log
											</a>
										</div>										
									</div>
								</div>
								
							</div>
						</div>
					</div>
					<div class="col-3">
						<div class="card">		
							<div class="card-body">
								<div class="row">
									<div class="col-12">
										<div class="row">
											<div class="col-12">
												<h1 class="m-0 text-dark">In and Started</h1>
												<asp:Label id="OrgText3" visible="false" runat="server" Text="Combined" />												
											</div>
											<div class="col-6 text-info">
												<h5>In and Started</br>
												Scheduled</br>
												Last Week</h5>
											</div>
											<div class="col-6 text-info">
												<h5><asp:Label id="ThisWeekInStarted" runat="server" /></br>
												<asp:Label id="ThisWeekSchedInStarted" runat="server" /></br>
												<asp:Label id="LastWeekInStarted" runat="server" /></h5>
											</div>
										</div>
										<div class="row">
											<div class="col-12">				
												<shield:ShieldChart 
												ID="ShieldChartInStarted7R" 
												runat="server" AutoPostBack="true" Width="100%" Height="240px" 
												OnTakeDataSource="ShieldChartInStarted7R_TakeDataSource"> 
												<PrimaryHeader Text="Daily"> 
												</PrimaryHeader> 
												<ExportOptions AllowExportToImage="false" AllowPrint="false" /> 
												<TooltipSettings CustomPointText="{point.dataSeries.collectionAlias}: <b>{point.y}</b>"></TooltipSettings> 
												<Axes> 
													<shield:ChartAxisX 
														CategoricalValuesField="dayofweek"> 
													</shield:ChartAxisX> 
													<shield:ChartAxisY min=0> 
													</shield:ChartAxisY> 
												</Axes> 
												<DataSeries> 
													<shield:ChartLineSeries DataFieldY="thisweek" CollectionAlias="(Daily)"> 
														<Settings seriesDashStyle="Dash" color="#3DA5D9" EnablePointSelection="true" EnableAnimation="false"> 
															<DataPointText BorderWidth=""> 
															</DataPointText> 
															<PointMark Enabled="false"></PointMark>
														</Settings> 
													</shield:ChartLineSeries> 
													<shield:ChartLineSeries DataFieldY="thisweekcum" CollectionAlias="(Cum)"> 
														<Settings EnablePointSelection="true" color="#3DA5D9" EnableAnimation="false" > 
															<DataPointText BorderWidth=""> 
															</DataPointText> 
															<PointMark Enabled="false"></PointMark>
														</Settings> 
													</shield:ChartLineSeries> 
													<shield:ChartLineSeries DataFieldY="lastweekcum" CollectionAlias="Last Week (Cum.)"> 
														<Settings EnablePointSelection="true" color="#AFC6E0" EnableAnimation="false"> 
															<DataPointText BorderWidth=""> 
															</DataPointText> 
															<PointMark Enabled="false"></PointMark>
														</Settings> 
													</shield:ChartLineSeries> 
												</DataSeries> 
												<Legend Align="Center" BorderWidth=""></Legend> 
											</shield:ShieldChart> 												 																					
											</div>
										</div>		
										<div class="float-right">
											<a class="btn btn-outline-info" href="inandstarted/graph.aspx">
											  <i class="fa fa-pie-chart text-info fa-lg"></i>
											</a>
											<a class="btn btn-outline-info" href="inandstarted/log.aspx">
											  View Log
											</a>
										</div>										
									</div>
								</div>
								
							</div>
						</div>
					</div>
					<div class="col-3">
						<div class="card">		
							<div class="card-body">
								<div class="row">
									<div class="col-12">
										<div class="row">
											<div class="col-12">
												<h1 class="m-0 text-dark">Comp & Resign</h1>
												<asp:Label id="OrgText4" visible="false" runat="server" Text="Combined" />												
											</div>
											<div class="col-6 text-info">
												<h5>Comp Resign</br>
												Scheduled</br>
												Last Week</h5>
											</div>
											<div class="col-6 text-info">
												<h5><asp:Label id="ThisWeekCompResign" runat="server" /></br>
												<asp:Label id="ThisWeekSchedCompResign" runat="server" /></br>
												<asp:Label id="LastWeekCompResign" runat="server" /></h5>
											</div>
										</div>
										<div class="row">
											<div class="col-12">				
												<shield:ShieldChart 
												ID="ShieldChartCompResign7R" 
												runat="server" AutoPostBack="true" Width="100%" Height="240px" 
												OnTakeDataSource="ShieldChartCompResign7R_TakeDataSource"> 
												<PrimaryHeader Text="Daily"> 
												</PrimaryHeader> 
												<ExportOptions AllowExportToImage="false" AllowPrint="false" /> 
												<TooltipSettings CustomPointText="{point.dataSeries.collectionAlias}: <b>{point.y}</b>"></TooltipSettings> 
												<Axes> 
													<shield:ChartAxisX 
														CategoricalValuesField="dayofweek"> 
													</shield:ChartAxisX> 
													<shield:ChartAxisY min=0> 
													</shield:ChartAxisY> 
												</Axes> 
												<DataSeries> 
													<shield:ChartLineSeries DataFieldY="thisweek" CollectionAlias="(Daily)"> 
														<Settings seriesDashStyle="Dash" color="#3DA5D9" EnablePointSelection="true" EnableAnimation="false"> 
															<DataPointText BorderWidth=""> 
															</DataPointText> 
															<PointMark Enabled="false"></PointMark>
														</Settings> 
													</shield:ChartLineSeries> 
													<shield:ChartLineSeries DataFieldY="thisweekcum" CollectionAlias="(Cum)"> 
														<Settings EnablePointSelection="true" color="#3DA5D9" EnableAnimation="false" > 
															<DataPointText BorderWidth=""> 
															</DataPointText> 
															<PointMark Enabled="false"></PointMark>
														</Settings> 
													</shield:ChartLineSeries> 
													<shield:ChartLineSeries DataFieldY="lastweekcum" CollectionAlias="Last Week (Cum.)"> 
														<Settings EnablePointSelection="true" color="#AFC6E0" EnableAnimation="false"> 
															<DataPointText BorderWidth=""> 
															</DataPointText> 
															<PointMark Enabled="false"></PointMark>
														</Settings> 
													</shield:ChartLineSeries> 
												</DataSeries> 
												<Legend Align="Center" BorderWidth=""></Legend> 
											</shield:ShieldChart> 	
											</div>
										</div>		
										<div class="float-right">
											<a class="btn btn-outline-info" href="comprsn/graph.aspx">
											  <i class="fa fa-pie-chart text-info fa-lg"></i>
											</a>
											<a class="btn btn-outline-info" href="comprsn/log.aspx">
											  View Log
											</a>
										</div>										
									</div>
								</div>
								
							</div>
						</div>
					</div>					
				</div>
				<div class="row">
					<div class="col-3">
						<div class="card">		
							<div class="card-body">
								<div class="row">
									<div class="col-12">
										<div class="row">
											<div class="col-12">
												<h1 class="m-0 text-dark">Recruitment</h1>
												<asp:Label id="OrgText5" visible="false" runat="server" Text="Combined" />												
											</div>
											<div class="col-6 text-info">
												<h5>PHS</br>
												Sign Ups</br>
												Last Week PHS</h5>
											</div>
											<div class="col-6 text-info">
												<h5><asp:Label id="ThisWeekPHS" runat="server" /></br>
												<asp:Label id="ThisWeekSignups" runat="server" /></br>
												<asp:Label id="LastWeekPHS" runat="server" /></h5>
											</div>
										</div>
										<div class="row">
											<div class="col-12">				
												<shield:ShieldChart 
												ID="ShieldChartRecruit7R" 
												runat="server" AutoPostBack="true" Width="100%" Height="240px" 
												OnTakeDataSource="ShieldChartRecruit7R_TakeDataSource"> 
												<PrimaryHeader Text="Daily"> 
												</PrimaryHeader> 
												<ExportOptions AllowExportToImage="false" AllowPrint="false" /> 
												<TooltipSettings CustomPointText="{point.dataSeries.collectionAlias}: <b>{point.y}</b>"></TooltipSettings> 
												<Axes> 
													<shield:ChartAxisX 
														CategoricalValuesField="dayofweek"> 
													</shield:ChartAxisX> 
													<shield:ChartAxisY min=0> 
													</shield:ChartAxisY> 
												</Axes> 
												<DataSeries> 
													<shield:ChartLineSeries DataFieldY="thisweek" CollectionAlias="Arrived(Daily)"> 
														<Settings seriesDashStyle="Dash" color="#3DA5D9" EnablePointSelection="true" EnableAnimation="false"> 
															<DataPointText BorderWidth=""> 
															</DataPointText> 
															<PointMark Enabled="false"></PointMark>
														</Settings> 
													</shield:ChartLineSeries> 
													<shield:ChartLineSeries DataFieldY="thisweeksched" CollectionAlias="Signed(Daily)"> 
														<Settings seriesDashStyle="Dash" color="#FFB30F" EnablePointSelection="true" EnableAnimation="false"> 
															<DataPointText BorderWidth=""> 
															</DataPointText> 
															<PointMark Enabled="false"></PointMark>
														</Settings> 
													</shield:ChartLineSeries> 
													<shield:ChartLineSeries DataFieldY="thisweekcum" CollectionAlias="Arrived(Cum.)"> 
														<Settings EnablePointSelection="true" color="#3DA5D9" EnableAnimation="false" > 
															<DataPointText BorderWidth=""> 
															</DataPointText> 
															<PointMark Enabled="false"></PointMark>
														</Settings> 
													</shield:ChartLineSeries> 
													<shield:ChartLineSeries DataFieldY="thisweekschedcum" CollectionAlias="Signed(Cum.)"> 
														<Settings EnablePointSelection="true" color="#FFB30F" EnableAnimation="false" > 
															<DataPointText BorderWidth=""> 
															</DataPointText> 
															<PointMark Enabled="false"></PointMark>
														</Settings> 
													</shield:ChartLineSeries> 
													<shield:ChartLineSeries DataFieldY="lastweekcum" CollectionAlias="Last Week (Cum.)"> 
														<Settings EnablePointSelection="true" color="#AFC6E0" EnableAnimation="true"> 
															<DataPointText BorderWidth=""> 
															</DataPointText> 
															<PointMark Enabled="false"></PointMark>
														</Settings> 
													</shield:ChartLineSeries> 
												</DataSeries> 
												<Legend Align="Center" BorderWidth=""></Legend> 
											</shield:ShieldChart> 																						
											</div>
										</div>	
										<div class="float-right">
											<a class="btn btn-outline-info" href="recruit/graph.aspx">
											  <i class="fa fa-pie-chart text-info fa-lg"></i>
											</a>
											<a class="btn btn-outline-info" href="recruit/log.aspx">
											  View Log
											</a>
										</div>										
									</div>
								</div>
								
							</div>
						</div>
					</div>
					<div class="col-3">
						<div class="card">		
							<div class="card-body">
								<div class="row">
									<div class="col-12">
										<div class="row">
											<div class="col-12">
												<h1 class="m-0 text-dark">First Starts</h1>
												<asp:Label id="OrgText6" visible="false" runat="server" Text="Combined" />												
											</div>
											<div class="col-6 text-info">
												<h5>First Starts</br>
												Scheduled</br>
												Last Week</h5>
											</div>
											<div class="col-6 text-info">
												<h5><asp:Label id="ThisWeekFSS" runat="server" /></br>
												<asp:Label id="ThisWeekScheduled" runat="server" /></br>
												<asp:Label id="LastWeekFSS" runat="server" /></h5>
											</div>
										</div>
										<div class="row">
											<div class="col-12">				
												<shield:ShieldChart 
												ID="ShieldChartFSS7R" 
												runat="server" AutoPostBack="true" Width="100%" Height="240px" 
												OnTakeDataSource="ShieldChartFSS7R_TakeDataSource"> 
												<PrimaryHeader Text="Daily"> 
												</PrimaryHeader> 
												<ExportOptions AllowExportToImage="false" AllowPrint="false" /> 
												<TooltipSettings CustomPointText="{point.dataSeries.collectionAlias}: <b>{point.y}</b>"></TooltipSettings> 
												<Axes> 
													<shield:ChartAxisX 
														CategoricalValuesField="dayofweek"> 
													</shield:ChartAxisX> 
													<shield:ChartAxisY min=0> 
													</shield:ChartAxisY> 
												</Axes> 
												<DataSeries> 
													<shield:ChartLineSeries DataFieldY="thisweek" CollectionAlias="Starts(Daily)"> 
														<Settings seriesDashStyle="Dash" color="#3DA5D9" EnablePointSelection="true" EnableAnimation="false"> 
															<DataPointText BorderWidth=""> 
															</DataPointText> 
															<PointMark Enabled="false"></PointMark>
														</Settings> 
													</shield:ChartLineSeries> 
													<shield:ChartLineSeries DataFieldY="thisweekcum" CollectionAlias="Starts(Cum.)"> 
														<Settings EnablePointSelection="true" color="#3DA5D9" EnableAnimation="false" > 
															<DataPointText BorderWidth=""> 
															</DataPointText> 
															<PointMark Enabled="false"></PointMark>
														</Settings> 
													</shield:ChartLineSeries> 
													<shield:ChartLineSeries DataFieldY="lastweekcum" CollectionAlias="Last Week (Cum.)"> 
														<Settings EnablePointSelection="true" color="#AFC6E0" EnableAnimation="true"> 
															<DataPointText BorderWidth=""> 
															</DataPointText> 
															<PointMark Enabled="false"></PointMark>
														</Settings> 
													</shield:ChartLineSeries> 
												</DataSeries> 
												<Legend Align="Center" BorderWidth=""></Legend> 
											</shield:ShieldChart> 																						
											</div>
										</div>	
										<div class="float-right">
											<a class="btn btn-outline-info" href="fss/graph.aspx">
											  <i class="fa fa-pie-chart text-info fa-lg"></i>
											</a>
											<a class="btn btn-outline-info" href="fss/log.aspx">
											  View Log
											</a>
										</div>										
									</div>
								</div>
								
							</div>
						</div>
					</div>
	
					<div class="col-6">
						<div class="card">		
							<div class="card-body">
								<div class="row">
									<div class="col-12">
										<div class="row">
											<div class="col-12">
												<h1 class="m-0 text-dark">Event Confirms</h1>
												<asp:Label id="OrgText7" runat="server" visible="false" Text="Combined" />												
											</div>
										</div>
										<div class="row">
											<div class="col-12">				
												<shield:ShieldChart ID="ShieldChartThisWeek" Width="100%" Height="320px" runat="server"
													OnTakeDataSource="ShieldChartThisWeek_TakeDataSource"
													CssClass="chart">
													<PrimaryHeader Text="Event Confirms Report"></PrimaryHeader>
													<ExportOptions AllowExportToImage="false" AllowPrint="false" />
													<Axes>
														<shield:ChartAxisX 
															CategoricalValuesField="Area">
														</shield:ChartAxisX> 
														<shield:ChartAxisY>
															<Title Text="Confirms"></Title>
														</shield:ChartAxisY>
													</Axes>
													<DataSeries>
														<shield:ChartBarSeries DataFieldY="Sales" CollectionAlias="Confirms">
														</shield:ChartBarSeries>
													</DataSeries>
												</shield:ShieldChart>
											</div>
										</div>	
										<div class="float-right">
											<a class="btn btn-outline-info" href="event/graph.aspx">
											  <i class="fa fa-pie-chart text-info fa-lg"></i>
											</a>
											<a class="btn btn-outline-info" href="event/log.aspx">
											  View Log
											</a>
										</div>										
									</div>
								</div>
								
							</div>
						</div>
					</div>					
				</div>				
				<div class="row">
					<div class="col-sm-12">
						<label for="lblWeekending" class="col-sm-2">Weekending: </label>
					</div>
				</div>
				<div class="row">
					<div class="col-sm-2">
						<asp:TextBox ID="lblWeekending" runat="server" Text="" CssClass="form-control date_we" ></asp:TextBox>
					</div>
				</div>				
				<div class="row">
					<!-- ERROR -->				
					<div class="col-12">
						<div class="card">		
							<div class="card-body">
								<div class="row">
									<div class="col-12">	
										<asp:Label runat="server" id="ErrorText" Text="" />									
									</div>				
								</div>
							</div>
						</div>
					</div>
				</div>									
				<p class="text-right">FLOW System / Version 2.1 / JSR / 16 Nov 2018</p>
				<asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
				<asp:Label visible="false" id="HeadText" style="padding-left:15px" runat="server" />
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