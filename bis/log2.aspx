<!DOCTYPE html>
<%@ Page Language="C#" Trace="False" enableEventValidation="false" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="log2.aspx.cs" Inherits="_Default" Debug="false" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"  
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

	
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
	<meta charset="utf-8" />
	<meta name="viewport" content="width=device-width, initial-scale=1" />
	<meta http-equiv="x-ua-compatible" content="ie=edge" />
		<meta http-equiv="refresh" content="5000" />
	<title>
		Bodies in the Shop
	</title>

	<link rel="stylesheet" href="../css/font-awesome.min.css" />
	<link rel="stylesheet" href="../css/adminlte.css" />
	<link rel="stylesheet" href="../css/jquery-ui.css">

	<link rel="icon" href="../img/favicon.png" />
	<style type="text/css">
		a {text-decoration: none}
	</style>	
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
		<a href="../home.aspx" class="nav-link">Home</a>
	  </li>
	</ul>

	<!-- Right navbar links -->
	<ul class="navbar-nav ml-auto">
	  <li class="nav-item">
		<a runat="server" id="excel" onserverclick="ExportToExcel_Click" class="nav-link">  <i class="fa fa-download fa-lg text-info" title="Download"></i> 
		</a>
	  </li>	  
	  <li class="nav-item">
		<a href="log.aspx" class="nav-link">	  
		  <i class="fa fa-refresh fa-lg text-info" title="Refresh"></i>
		</a>
	  </li>	  
    </ul>		
	</nav>
	<!-- /.navbar -->

	<!-- Main Sidebar Container -->
	<aside class="main-sidebar sidebar-light-primary elevation-4">
		<!-- Brand Logo -->
		<a href="../home.aspx" class="brand-link">
		  <img src="../img/scn_sm.png" alt="AdminLTE Logo" class="brand-image img-circle" style="opacity: .8">
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
			  <!-- Add icons to the links using the .nav-icon class
				   with font-awesome or any other icon font library -->

			  <li class="nav-item">
				<a href="log.aspx" class="nav-link" >
				  <i class="nav-icon fa fa-address-card text-info"></i>
				  <p>
					BIS Log
				  </p>
				</a>
				<li class="nav-item">
					<a href="intheshop.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-info"></i>
						<p>In The Shop</p>
					</a>
				</li>
				<li class="nav-item">
					<a href="scheduled.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-secondary"></i>
						<p>Scheduled</p>
					</a>
				</li>
				<li class="nav-item">
					<a href="lastweek.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-warning"></i>
						<p>Last Week BIS</p>
					</a>
				</li>			
				<li class="nav-item">
					<a href="named.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-danger"></i>
						<p>Named/Fallen Off BIS</p>
					</a>
				</li>			
				<li class="nav-item">
					<a href="archive.aspx" class="nav-link">
						<i class="fa fa-archive nav-icon text-secondary"></i>
						<p>Archive</p>
					</a>
				</li>	 
			<hr>
			<li class="nav-item">
				<a href="graph.aspx" class="nav-link">
					<i class="fa fa-pie-chart nav-icon text-info"></i>
					<p>BIS Report</p>
				</a>
			</li>					
			</li>
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
			<div class="row mb-2">
			  <div class="col-sm-10">
				<h1 class="m-0 text-dark">
					<asp:Label id="HeaderText" runat="server" />								
				</h1>
				<asp:Label visible="false" id="OrgText" runat="server" Text="Combined" />												
			  </div><!-- /.col -->
			  <div class="col-sm-2">
				<div class="row">
					<div class="col-sm-3">
						<p class=text-right> W/E: </p>
					</div>
					<div class="col-sm-9">
						<asp:TextBox ID="lblWeekending" runat="server" autocomplete="off" Text="" CssClass="form-control date_we" ></asp:TextBox>
					</div>
				</div>
			  </div><!-- /.col -->			  
			</div><!-- /.row -->
		  </div><!-- /.container-fluid -->
		</div>
		<!-- /.content-header -->
		<ajaxToolKit:ToolkitScriptManager EnablePartialRendering="true" runat="server" />
			
		<!-- Main content -->
		<section class="content">
			<div class="container-fluid">
				<asp:UpdatePanel ID="UpdatePanelTotal" runat="server" UpdateMode="Conditional">
				<ContentTemplate>
			
				<!-- TOTALS -->
				<div class="row">
				  <div class="col-3">
					<div class="small-box bg-info-gradient">
					<!-- <div class="info-box"> -->
					  <div class="info-box-content">
						<span class="info-box-number">
						IN THE SHOP
						</span>
						<span class="info-box-text">
						<h3>
							<asp:Label id="BothBIS" runat="server" /> 
						</h3>
						</span>
						<span class="progress-description">
							<asp:Label id="DayInv" runat="server" /> 
						</span>
						<span class="progress-description">
							<asp:Label id="FdnInv" runat="server" /> 
						</span>
					  </div>
					  <!-- /.info-box-content -->
					</div>
					<!-- /.info-box -->
				  </div>
				  <!-- /.col -->				  	
				  <div class="col-3">
					<div class="small-box bg-info-gradient">
					  <div class="info-box-content">
						<a href="#scheduled">
						<font color="white">						
						<span class="info-box-number">SCHEDULED</span>
						<span class="info-box-text">
							<h3> 
							<asp:Label id="BothScheduled" runat="server" /> 
							</h3>
						</span>
						<span class="progress-description">
							<asp:Label id="DayConf" runat="server" /> 
						</span>
						<span class="progress-description">
							<asp:Label id="FdnConf" runat="server" /> 
						</span>
						</font>						
						</a>
					  </div>
					  <!-- /.info-box-content -->
					</div>
					<!-- /.info-box -->
				  </div>
				  <!-- /.col -->
				  <div class="col-3">
					<div class="small-box bg-warning-gradient">
					  <div class="info-box-content">
						<a href="#lastweek">
						<span class="info-box-number">LAST WEEK</span>
						<span class="info-box-text">
							<h3> 
							<asp:Label id="BothLastWeek" runat="server" /> 
							</h3>
						</span>
						<span class="progress-description">
							<asp:Label id="DayLastWeek" runat="server" /> 
						</span>
						<span class="progress-description">
							<asp:Label id="FdnLastWeek" runat="server" /> 
						</span>
						</a>

					</div>
					  <!-- /.info-box-content -->
					</div>
					<!-- /.info-box -->
				  </div>				  
				  <div class="col-3">
					<div class="small-box bg-danger-gradient">
					  <div class="info-box-content">
						<a href="#felloff">
						<font color="white">						
						<span class="info-box-number">NAMED/FELL OFF</span>
						<span class="info-box-text">
							<h3> 
							<asp:Label id="BothNamed" runat="server" /> 
							</h3>
						</span>
						<span class="progress-description">
							<asp:Label id="DayNamed" runat="server" /> 
						</span>
						<span class="progress-description">
							<asp:Label id="FdnNamed" runat="server" /> 
						</span>
						</font>						
						</a>
					</div>
					
					  <!-- /.info-box-content -->
					</div>
					<!-- /.info-box -->
				  </div>
				</div>
				<!-- /.row -->	

				<div class="row no-print">						
				  <div class="col-md-2">
					<asp:LinkButton runat="server" CommandArgument="DIV6" OnClick="LinkButtonArea_Command">					
					<div class="small-box bg-default-gradient">
					  <div id="boxDiv6" runat="server" class="info-box-content">
						<span class="info-box-number">DIV6</span>
						<span class="info-box-text">
						</span>
						<span class="progress-description">
							<asp:Label id="div6Day" runat="server" /> 
						</span>
						<span class="progress-description">
							<asp:Label id="div6Fdn" runat="server" /> 
						</span>
					  </div>
					  <!-- /.info-box-content -->
					</div>
					</asp:LinkButton>							
					<!-- /.info-box -->
				  </div>
				  <div class="col-md-2">
						<asp:LinkButton runat="server" CommandArgument="Purif" OnClick="LinkButtonArea_Command">					
						<div class="small-box bg-default-gradient">
						  <div id="boxPurif" runat="server" class="info-box-content">
							<span class="info-box-number">PURIF</span>
							<span class="info-box-text">
							<span class="progress-description">
								<asp:Label id="purifDay" runat="server" /> 
							</span>
							<span class="progress-description">
								<asp:Label id="purifFdn" runat="server" /> 
							</span>
						   </div>
						</div>
						</asp:LinkButton>							
				  </div>
				  <div class="col-md-2">
					<asp:LinkButton runat="server" CommandArgument="SRD" OnClick="LinkButtonArea_Command">					
					<div class="small-box bg-default-gradient">
						<div id="boxSRD" runat="server" class="info-box-content">					
						<span class="info-box-number">SRD</span>
						<span class="info-box-text">
						</span>
						<span class="progress-description">
							<asp:Label id="srdDay" runat="server" /> 
						</span>
						<span class="progress-description">
							<asp:Label id="srdFdn" runat="server" /> 
						</span>
					  </div>
					  <!-- /.info-box-content -->
					</div>
					</asp:LinkButton>							
					<!-- /.info-box -->
				  </div>
				  <div class="col-md-2">
					<asp:LinkButton runat="server" CommandArgument="HGC" OnClick="LinkButtonArea_Command">					
					<div class="small-box bg-default-gradient">
					  <div id="boxHGC" runat="server" class="info-box-content">
						<span class="info-box-number">HGC</span>
						<span class="info-box-text">
						</span>
						<span class="progress-description">
							<asp:Label id="hgcDay" runat="server" /> 
						</span>
						<span class="progress-description">
							<asp:Label id="hgcFdn" runat="server" /> 
						</span>
					  </div>
					  <!-- /.info-box-content -->
					</div>
					</asp:LinkButton>							
					<!-- /.info-box -->
				  </div>
				  <div class="col-md-2">
					<asp:LinkButton runat="server" CommandArgument="ACAD" OnClick="LinkButtonArea_Command">					
					<div class="small-box bg-default-gradient">
					  <div id="boxACADEMY" runat="server" class="info-box-content">
						<span class="info-box-number">ACADEMY</span>
						<span class="info-box-text">
						</span>
						<span class="progress-description">
							<asp:Label id="academyDay" runat="server" /> 
						</span>
						<span class="progress-description">
							<asp:Label id="academyFdn" runat="server" /> 
						</span>
					  </div>
					  <!-- /.info-box-content -->
					</div>
					</asp:LinkButton>							
					<!-- /.info-box -->
				  </div>
				  <div class="col-md-2">
					<asp:LinkButton runat="server" CommandArgument="GAK" OnClick="LinkButtonArea_Command">					
					<div class="small-box bg-default-gradient">
					  <div id="boxDiv6Processing" runat="server" class="info-box-content">
						<span class="info-box-number">GAK</span>
						<span class="info-box-text">
						</span>
						<span class="progress-description">
							<asp:Label id="div6processingDay" runat="server" /> 
						</span>
						<span class="progress-description">
							<asp:Label id="div6processingFdn" runat="server" /> 
						</span>
					  </div>
					  <!-- /.info-box-content -->
					</div>
					</asp:LinkButton>							
					<!-- /.info-box -->
				  </div>
				</div>					
				
				
				
				
				<div class="row no-print">
				  <div class="col-md-1 col-sm-2">
						<div class="small-box bg-default-gradient">
						<asp:LinkButton ID="areaSearch" runat="server" CommandArgument="PE" OnClick="LinkButton_Command">					
						  <div class="info-box-content">
							<span class="info-box-number">PE BIS</span>
							<span class="info-box-text">
							<span class="progress-description">
								<asp:Label id="ped" runat="server" /> 
							</span>
							<span class="progress-description">
								<asp:Label id="pef" runat="server" /> 
							</span>
						</div>
						</asp:LinkButton>							
						  <!-- /.info-box-content -->
					</div>

					<!-- /.info-box -->
				  </div>
				  <div class="col-md-1 col-sm-2">
					<div class="small-box bg-default-gradient">
						<asp:LinkButton runat="server" CommandArgument="DIV6" OnClick="LinkButton_Command">					
					  <div class="info-box-content">
						<span class="info-box-number">DIV 6 BIS</span>
						<span class="info-box-text">
						</span>
						<span class="progress-description">
							<asp:Label id="div6d" runat="server" /> 
						</span>
						<span class="progress-description">
							<asp:Label id="div6f" runat="server" /> 
						</span>
					  </div>
						</asp:LinkButton>							
					  <!-- /.info-box-content -->
					</div>
					<!-- /.info-box -->
				  </div>
				  <div class="col-md-1 col-sm-2">
					<div class="small-box bg-default-gradient">
						<asp:LinkButton runat="server" CommandArgument="LI" OnClick="LinkButton_Command">					
					  <div class="info-box-content">
						<span class="info-box-number">LI BIS</span>
						<span class="info-box-text">
						</span>
						<span class="progress-description">
							<asp:Label id="lid" runat="server" /> 
						</span>
						<span class="progress-description">
							<asp:Label id="lif" runat="server" /> 

						</span>
					  </div>
						</asp:LinkButton>							
					  <!-- /.info-box-content -->
					</div>
					<!-- /.info-box -->
				  </div>
				  <div class="col-md-1 col-sm-2">
					<div class="small-box bg-default-gradient">
						<asp:LinkButton runat="server" CommandArgument="DN" OnClick="LinkButton_Command">					
					  <div class="info-box-content">
						<span class="info-box-number">DN BIS</span>
						<span class="info-box-text">
						</span>
						<span class="progress-description">
							<asp:Label id="dnd" runat="server" /> 
						</span>
						<span class="progress-description">
							<asp:Label id="dnf" runat="server" /> 
						</span>
					  </div>
						</asp:LinkButton>							
					  <!-- /.info-box-content -->
					</div>
					<!-- /.info-box -->
				  </div>
				  <div class="col-md-1 col-sm-2">
					<div class="small-box bg-default-gradient">
						<asp:LinkButton runat="server" CommandArgument="STCC" OnClick="LinkButton_Command">					
					  <div class="info-box-content">
						<span class="info-box-number">STCC BIS</span>
						<span class="info-box-text">
						</span>
						<span class="progress-description">
							<asp:Label id="stccd" runat="server" /> 
						</span>
						<span class="progress-description">
							<asp:Label id="stccf" runat="server" /> 
						</span>
					  </div>
						</asp:LinkButton>							
					  <!-- /.info-box-content -->
					</div>
					<!-- /.info-box -->
				  </div>
				  <div class="col-md-1 col-sm-2">
					<div class="small-box bg-default-gradient">
						<asp:LinkButton runat="server" CommandArgument="HQS" OnClick="LinkButton_Command">					
					  <div class="info-box-content">
						<span class="info-box-number">HQS BIS</span>
						<span class="info-box-text">
						</span>
						<span class="progress-description">
							<asp:Label id="hqsd" runat="server" /> 
						</span>
						<span class="progress-description">
							<asp:Label id="hqsf" runat="server" /> 
						</span>
					  </div>
						</asp:LinkButton>							
					  <!-- /.info-box-content -->
					</div>
					<!-- /.info-box -->
				  </div>				  
				  <div class="col-md-1 col-sm-2">
					<div class="small-box bg-default-gradient">
						<asp:LinkButton runat="server" CommandArgument="PURIF" OnClick="LinkButton_Command">					
					  <div class="info-box-content">
						<span class="info-box-number">PURIF BIS</span>
						<span class="info-box-text">
						</span>
						<span class="progress-description">
							<asp:Label id="purifd" runat="server" /> 
						</span>
						<span class="progress-description">
							<asp:Label id="puriff" runat="server" /> 
						</span>
					  </div>
						</asp:LinkButton>							
					  <!-- /.info-box-content -->
					</div>
					<!-- /.info-box -->
				  </div>
				  <div class="col-md-1 col-sm-2">
					<div class="small-box bg-default-gradient">
						<asp:LinkButton runat="server" CommandArgument="SRD" OnClick="LinkButton_Command">					
					  <div class="info-box-content">
						<span class="info-box-number">SRD BIS</span>
						<span class="info-box-text">
						</span>
						<span class="progress-description">
							<asp:Label id="srdd" runat="server" /> 
						</span>
						<span class="progress-description">
							<asp:Label id="srdf" runat="server" /> 
						</span>
					  </div>
						</asp:LinkButton>							
					  <!-- /.info-box-content -->
					</div>
					<!-- /.info-box -->
				  </div>				  
				  <div class="col-md-1 col-sm-2">
					<div class="small-box bg-default-gradient">
						<asp:LinkButton runat="server" CommandArgument="HGC" OnClick="LinkButton_Command">					
					  <div class="info-box-content">
						<span class="info-box-number">HGC BIS</span>
						<span class="info-box-text">
						</span>
						<span class="progress-description">
							<asp:Label id="hgcd" runat="server" /> 
						</span>
						<span class="progress-description">
							<asp:Label id="hgcf" runat="server" /> 
						</span>
					  </div>
						</asp:LinkButton>							
					  <!-- /.info-box-content -->
					</div>
					<!-- /.info-box -->
				  </div>
			      <div class="col-md-1 col-sm-2">
					<div class="small-box bg-default-gradient">
						<asp:LinkButton runat="server" CommandArgument="ACAD" OnClick="LinkButton_Command">					
					  <div class="info-box-content">
						<span class="info-box-number">ACAD BIS</span>
						<span class="info-box-text">
						</span>
						<span class="progress-description">
							<asp:Label id="acadd" runat="server" /> 
						</span>
						<span class="progress-description">
							<asp:Label id="acadf" runat="server" /> 
						</span>
					  </div>
						</asp:LinkButton>							
					  <!-- /.info-box-content -->
					</div>
					<!-- /.info-box -->
				  </div>				  
				  <div class="col-md-1 col-sm-2">
					<div class="small-box bg-default-gradient">
						<asp:LinkButton runat="server" CommandArgument="INTERN" OnClick="LinkButton_Command">					
					  <div class="info-box-content">
						<span class="info-box-number">INTERN</span>
						<span class="info-box-text">
						</span>
						<span class="progress-description">
							<asp:Label id="internd" runat="server" /> 
						</span>
						<span class="progress-description">
							<asp:Label id="internf" runat="server" /> 
						</span>
					  </div>
						</asp:LinkButton>							
					  <!-- /.info-box-content -->
					</div>
					<!-- /.info-box -->
				  </div>
				  <div class="col-md-1 col-sm-2">
					<div class="small-box bg-default-gradient">
						<asp:LinkButton runat="server" CommandArgument="KNOW" OnClick="LinkButton_Command">					
					  <div class="info-box-content">
						<span class="info-box-number">KNOW BIS</span>
						<span class="info-box-text">
						</span>
						<span class="progress-description">
							<asp:Label id="knowd" runat="server" /> 
						</span>
						<span class="progress-description">
							<asp:Label id="knowf" runat="server" /> 
						</span>
					  </div>
						</asp:LinkButton>							
					  <!-- /.info-box-content -->
					</div>
					<!-- /.info-box -->
				  </div>
				</div>				
		    </ContentTemplate>
		</asp:UpdatePanel>
			
				<!-- SEARCH BOX -->
				<div class="row">
					<div class="col-md-6 col-sm-6 col-12">
						<div class="card">		
							<div class="card-body">
								<div class="row">
									<div class="col-3">
										<asp:DropDownList id="ddlSearchGI" runat="server" CssClass="form-control form-control-md">
											<asp:ListItem Value="name"> Name </asp:ListItem>
											<asp:ListItem Value="area"> Area </asp:ListItem>
											<asp:ListItem Value="service"> Service </asp:ListItem>
											<asp:ListItem Value="fsm"> FSM </asp:ListItem>
										</asp:DropDownList>  
									</div>				
									<div class="col-4">
										<asp:TextBox ID="txtBIS" CssClass="form-control form-control-md" runat="server"></asp:TextBox>
									</div>				
									<div class="col-2">
										<button runat="server" id="Button5" class="btn btn-block btn-outline-info btn-md" onserverclick="BtnSearch_Click" title="Search">
										Search
										</button>							
									</div>	
									<div class="col-3">
										<a runat="server" href="./add.aspx" id="btnAddName" class="btn btn-block btn-info btn-md">Add Name</a>
									</div>	
								</div>
							</div>
						</div>
					<!-- /.col -->		
					</div>
					<!-- /.col -->	
					<div class="col-md-6 col-sm-3 col-6">
						<div class="card">		
							<div class="card-body">
								<div class="row">
									<div class="col-8">
										<asp:TextBox ID="weekendingText" autocomplete="off" CssClass="form-control date_we form-control-md" placeholder="Weekending:" runat="server"></asp:TextBox>
									</div>				
									<div class="col-4">
										<button runat="server" id="btnOpenModal" class="btn btn-block btn-outline-info btn-md" onserverclick="OpenModal" title="Archive">
											Archive
										</button>							
									</div>	
									<!-- <div class="col-md-4 col-sm-3 col-4"> -->
										<!-- <button type="button" id="btnArchiveModal" runat="server" class="form-control btn btn-outline-warning" data-toggle="modal" data-target="#archiveModal"> -->
											<!-- Archive Week -->
										<!-- </button>								 -->
									<!-- </div>	 -->
								</div>
							</div>
						</div>
					</div>
				</div>
				
		<asp:UpdatePanel ID="UpdatePanelStarted" runat="server" UpdateMode="Conditional">
		    <ContentTemplate>
				<!-- GRID -->
				<div class="row">
				  <div class="col-md-12">
					<div class="card">
					  <div class="card-header">
						<h5 class="card-title">
						In The Shop
						<asp:Label id="BothInLbl" runat="server" /> 						
						</h5>
						<div class="card-tools">
						  <button type="button" class="btn btn-tool" data-widget="collapse">
							<i class="fa fa-minus"></i>
						  </button>
						</div>
					  </div>
					  <!-- /.card-header -->
					  <div class="card-body">
							<div class="row">
								<div class="col-lg-12 ">  
									<div class="table-responsive">
									<asp:GridView 
										ID="GridViewInTheShop" 
										runat="server" 
										OnSorting="TaskGridView_Sorting" 
										AllowSorting="True" 
										AutoGenerateColumns="False" 
										BorderWidth="0" 
										CellPadding="2" 
										CssClass="mGridAppt"
										ForeColor="Black" 
										GridLines="None" 
										Width="100%" >
										<Columns>
											<asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID">
												<HeaderStyle CssClass="no-display"></HeaderStyle>
												<ItemStyle CssClass="no-display"></ItemStyle>
											</asp:BoundField>
											<asp:BoundField DataField="addo_ID">
												<HeaderStyle CssClass="no-display"></HeaderStyle>
												<ItemStyle CssClass="no-display"></ItemStyle>
											</asp:BoundField>  
											<asp:BoundField DataField="org" HeaderText="Org" SortExpression="org" >
												<HeaderStyle HorizontalAlign="Left" CssClass="col_xs" />
											</asp:BoundField>
											<asp:TemplateField HeaderText="ID">
												<ItemTemplate>
													<asp:HyperLink runat="server" class="no-print" Text='<%# Eval("Addo_ID") %>' Target="_blank" NavigateUrl='<%# "http://webapp2a.scientology.net/portal/cars/cosCarsPersonReport.jsp?hideLinks=true&addoId="+ Eval("Addo_ID") +"&orgId=" + System.Configuration.ConfigurationManager.AppSettings["orgid"] + "&browser=&userId=allhandsharlem&password=harlembas1"%>' />
												</ItemTemplate>
											</asp:TemplateField>                    										
											<asp:TemplateField SortExpression="name" HeaderText="Name">
												<HeaderStyle HorizontalAlign="Left" CssClass="col_name" />
												<ItemTemplate>
													<asp:TextBox ID="name" AutoPostBack="True" CssClass="col_name" runat="server" Text='<%# Eval("name") %>' Width="175px" OnTextChanged="text_change" />
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField SortExpression="area" HeaderText="Area">
												<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
												<ItemTemplate>
													<asp:DropDownList id="ddlReg" name="ddlReg" AutoPostBack="True" runat="server" Width=125 OnSelectedIndexChanged="Selection_Change_Reg"
														CssClass="col_med" SelectedValue='<%# Eval("area") %>' >
														<asp:ListItem Value="ACAD"> ACAD </asp:ListItem>
														<asp:ListItem Value="DIV6"> DIV6 </asp:ListItem>
														<asp:ListItem Value="DN"> DN </asp:ListItem>
														<asp:ListItem Value="HGC"> HGC </asp:ListItem>
														<asp:ListItem Value="HQS"> HQS </asp:ListItem>
														<asp:ListItem Value="KNOW"> GAK </asp:ListItem>
														<asp:ListItem Value="INTERN"> INTERN </asp:ListItem>
														<asp:ListItem Value="LI"> LI </asp:ListItem>
														<asp:ListItem Value="PE"> PE </asp:ListItem>
														<asp:ListItem Value="PURIF"> PURIF </asp:ListItem>
														<asp:ListItem Value="SRD"> SRD </asp:ListItem>
														<asp:ListItem Value="STCC"> STCC </asp:ListItem>
														<asp:ListItem Value="- UNK"> UNK </asp:ListItem>
													</asp:DropDownList>
												</ItemTemplate>
												<ControlStyle width="100%" />																
											</asp:TemplateField>
											<asp:TemplateField SortExpression="service" HeaderText="Service">
												<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
												<ItemTemplate>
												<asp:TextBox ID="service" AutoPostBack="True" CssClass="col_med" runat="server" Text='<%# Eval("service") %>' Width=100 OnTextChanged="text_change" />
												</ItemTemplate>
												<ControlStyle width="100%" />																
											</asp:TemplateField>
											<asp:TemplateField SortExpression="reg" HeaderText="Terminal">
												<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
												<ItemTemplate>
												<asp:TextBox ID="reg" AutoPostBack="True" CssClass="col_med" runat="server" Text='<%# Eval("reg") %>' Width=75 OnTextChanged="text_change" />
												</ItemTemplate>
												<ControlStyle width="100%" />																
											</asp:TemplateField>
											<asp:TemplateField SortExpression="fsm" Visible="false" HeaderText="FSM">
												<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
												<ItemTemplate>
												<asp:TextBox ID="fsm" AutoPostBack="True" CssClass="col_med" runat="server" Text='<%# Eval("fsm") %>' Width=75 OnTextChanged="text_change" />
												</ItemTemplate>
												<ControlStyle width="100%" />																
											</asp:TemplateField>
											<asp:TemplateField visible="False" HeaderText="Org">
												<ItemTemplate>
													<asp:DropDownList id="ddlOrg" AutoPostBack="True" OnSelectedIndexChanged="Selection_Change_Org" runat="server" 
													CssClass="col_med" SelectedValue='<%# Eval("org") %>' TabIndex='<%# TabIndex %>'>
														<asp:ListItem Value=""> </asp:ListItem>
														<asp:ListItem Value="Day"> Day </asp:ListItem>
														<asp:ListItem Value="Fdn"> Fdn </asp:ListItem>
													</asp:DropDownList>
												</ItemTemplate>
												<ControlStyle Width="100%" />								
											</asp:TemplateField>
											<asp:TemplateField Visible="true" SortExpression="Scheduled" HeaderText="BIS Date">												
												<HeaderStyle HorizontalAlign="Left" />												
												<ItemTemplate>
													<asp:TextBox ID="scheduled" AutoPostBack="True" runat="server" class="date_thisweek col_med" Text='<%# Eval("Scheduled", "{0:dd-MMM-yyyy}") %>' Width=100 OnTextChanged="text_change_date" ></asp:TextBox>
												</ItemTemplate>
											</asp:TemplateField>			
											<asp:TemplateField SortExpression="status" HeaderText="Status" >
												<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
												<ItemTemplate>
												<asp:DropDownList id="ddlStatus" AutoPostBack="True" OnSelectedIndexChanged="Selection_Change_Status" Width=100 runat="server" 
												CssClass="col_med" SelectedValue='<%# Eval("status") %>' TabIndex='<%# TabIndex %>'>
													<asp:ListItem Value="In The Shop"> In The Shop </asp:ListItem>
													<asp:ListItem Value="Scheduled"> Scheduled </asp:ListItem>
													<asp:ListItem Value="Last Week"> Last Week </asp:ListItem>
													<asp:ListItem Value="Named"> Named </asp:ListItem>
												</asp:DropDownList>
												</ItemTemplate>
												<ControlStyle width="100%" />																
											</asp:TemplateField>
											<asp:TemplateField SortExpression="notes" HeaderText="Notes">
												<HeaderStyle HorizontalAlign="Left" CssClass="col_large" />
												<ItemTemplate>
												<asp:TextBox ID="notes" AutoPostBack="True" CssClass="col_large" runat="server" Text='<%# Eval("notes") %>' Width="400" OnTextChanged="text_change" />
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField>
												<ItemTemplate>
													<asp:LinkButton ID="addToNamedInStarted1" Tooltip="Add to (Named) In and Started" Class="fa fa-check-square-o text-secondary fa-lg" runat="server" OnClick="Click_AddToNamedInStarted"></asp:LinkButton>
												</ItemTemplate>						
											</asp:TemplateField>
											<asp:TemplateField>
												<ItemTemplate>
													<asp:LinkButton ID="addToInStarted1" Tooltip="Add to In and Started" Class="fa fa-check-square-o text-info fa-lg" runat="server" OnClick="Click_AddToInStarted"></asp:LinkButton>
												</ItemTemplate>						
											</asp:TemplateField>
											<asp:TemplateField>
												<ItemTemplate>
													<asp:LinkButton ID="addToNamedCompResign1" Tooltip="Add to (Named) Comp Resign" Class="fa fa-graduation-cap text-secondary fa-lg" runat="server" OnClick="Click_AddToNamedCompResign"></asp:LinkButton>
												</ItemTemplate>						
											</asp:TemplateField>
											<asp:TemplateField>
												<ItemTemplate>
													<asp:LinkButton ID="addToCompResign1" Tooltip="Add to Comp Resign" Class="fa fa-graduation-cap text-info fa-lg" runat="server" OnClick="Click_AddToCompResign"></asp:LinkButton>
												</ItemTemplate>						
											</asp:TemplateField>
											<asp:TemplateField>
												<ItemTemplate>
													<asp:LinkButton ID="lnkBtnEdit" Tooltip="Edit Record" Class="fa fa-pencil-square-o text-info fa-lg" runat="server" OnClick="Display"></asp:LinkButton>
												</ItemTemplate>						
											</asp:TemplateField>
											<asp:TemplateField>
												<ItemTemplate>
													<asp:LinkButton ID="lnkBtnDelete" Tooltip="Delete Record" Class="fa fa-times text-danger fa-lg" runat="server" OnClick="DeleteRow"></asp:LinkButton>
												</ItemTemplate>						
											</asp:TemplateField>										
										</Columns>
									</asp:GridView>										
									<ajaxToolkit:ModalPopupExtender runat="server" 
										ID="ModalPopupExtender1" 
										TargetControlID="hiddenButton"
										PopupControlID="PanelDel2" X="770" Y="100" CancelControlID="ButtonDeleteCancel2"
										BackgroundCssClass="modalBackground">
									</ajaxToolkit:ModalPopupExtender>
									<asp:Button id="hiddenButton" runat="server" style="display:none" />
									<asp:Panel ID="PanelDel2" runat="server" Style="display: none;" Width="400px" > 
										<div class="modal-dialog modal-lg" role="document">
											<div class="modal-content">
												<div class="modal-header modal-header-danger">
													<h3 class="modal-title">Warning!</h3>
												</div>
												<div class="modal-body">
													<div class="row">
														<div class="col-md-12">
															<fieldset>
																<p> Do you want to permanently delete this record?</p>
															</fieldset>
															<div class="modal-footer">
																<asp:Button ID="ButtonDeleteCancel2" class="btn btn-default" runat="server" Text="Cancel" />
																<asp:Button ID="ButtonDeleteOkay2" OnClientClick="<% %>" class="btn btn-info" runat="server" Text="Delete" CommandArgument='<%# Eval("id") %>' OnCommand="btnDelete_Click" />
															</div>	
															<div class="row">
																<asp:TextBox ID="IdToDelete" type="hidden" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
															</div>										
														</div>
													</div>
												</div><!-- /.modal-body -->
											</div><!-- /.modal-content -->
										</div><!-- /.modal-dialog -->
									</asp:Panel>
							
									</div>
								</div>	
							</div>
						</div>
					  </div>
					</div>
				</div>
		    </ContentTemplate>
		</asp:UpdatePanel>

		<asp:UpdatePanel ID="UpdatePanelScheduled" runat="server" UpdateMode="Conditional">
		    <ContentTemplate>

				<div class="row">
				  <div class="col-md-12">
					<div class="card">
					  <div class="card-header">
						<a name="scheduled">
						<h5 class="card-title">
						Scheduled
						<asp:Label id="SchedLbl" runat="server" /> 						
						</h5>
						</a> 
						<div class="card-tools">
						  <button type="button" class="btn btn-tool" data-widget="collapse">
							<i class="fa fa-minus"></i>
						  </button>
						</div>
					  </div>
					  <!-- /.card-header -->
					  <div class="card-body">
						<div class="row">
								<div class="col-lg-12 ">  
									<div class="table-responsive">  
									<asp:GridView 
										ID="GridViewScheduled" 
										runat="server" 
										OnSorting="TaskGridView_Sorting" 
										AllowSorting="True" 
										AutoGenerateColumns="False" 
										BorderWidth="0" 
										CellPadding="2" 
										CssClass="mGridAppt"
										ForeColor="Black" 
										GridLines="None" 
										Width="100%" >
										<Columns>
											<asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID">
												<HeaderStyle CssClass="no-display"></HeaderStyle>
												<ItemStyle CssClass="no-display"></ItemStyle>
											</asp:BoundField>
											<asp:BoundField DataField="addo_ID">
												<HeaderStyle CssClass="no-display"></HeaderStyle>
												<ItemStyle CssClass="no-display"></ItemStyle>
											</asp:BoundField>  
											<asp:BoundField DataField="org" HeaderText="Org" SortExpression="org" >
												<HeaderStyle HorizontalAlign="Left" CssClass="col_xs" />
											</asp:BoundField>
											<asp:TemplateField HeaderText="ID">
												<ItemTemplate>
													<asp:HyperLink runat="server" class="no-print" Text='<%# Eval("Addo_ID") %>' Target="_blank" NavigateUrl='<%# "http://webapp2a.scientology.net/portal/cars/cosCarsPersonReport.jsp?hideLinks=true&addoId="+ Eval("Addo_ID") +"&orgId=" + System.Configuration.ConfigurationManager.AppSettings["orgid"] + "&browser=&userId=allhandsharlem&password=harlembas1"%>' />
												</ItemTemplate>
											</asp:TemplateField>                    										
											<asp:TemplateField SortExpression="name" HeaderText="Name">
												<HeaderStyle HorizontalAlign="Left" CssClass="col_name" />
												<ItemTemplate>
													<asp:TextBox ID="name" AutoPostBack="True" CssClass="col_name" runat="server" Text='<%# Eval("name") %>' Width="175px" OnTextChanged="text_change" />
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField SortExpression="area" HeaderText="Area">
												<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
												<ItemTemplate>
													<asp:DropDownList id="ddlReg" name="ddlReg" AutoPostBack="True" runat="server" Width=125 OnSelectedIndexChanged="Selection_Change_Reg"
														CssClass="col_med" SelectedValue='<%# Eval("area") %>' >
														<asp:ListItem Value="ACAD"> ACAD </asp:ListItem>
														<asp:ListItem Value="DIV6"> DIV6 </asp:ListItem>
														<asp:ListItem Value="DN"> DN </asp:ListItem>
														<asp:ListItem Value="HGC"> HGC </asp:ListItem>
														<asp:ListItem Value="HQS"> HQS </asp:ListItem>
														<asp:ListItem Value="KNOW"> GAK </asp:ListItem>
														<asp:ListItem Value="INTERN"> INTERN </asp:ListItem>
														<asp:ListItem Value="LI"> LI </asp:ListItem>
														<asp:ListItem Value="PE"> PE </asp:ListItem>
														<asp:ListItem Value="PURIF"> PURIF </asp:ListItem>
														<asp:ListItem Value="SRD"> SRD </asp:ListItem>
														<asp:ListItem Value="STCC"> STCC </asp:ListItem>
														<asp:ListItem Value="- UNK"> UNK </asp:ListItem>
													</asp:DropDownList>
												</ItemTemplate>
												<ControlStyle width="100%" />																
											</asp:TemplateField>
											<asp:TemplateField SortExpression="service" HeaderText="Service">
												<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
												<ItemTemplate>
												<asp:TextBox ID="service" AutoPostBack="True" CssClass="col_med" runat="server" Text='<%# Eval("service") %>' Width=100 OnTextChanged="text_change" />
												</ItemTemplate>
												<ControlStyle width="100%" />																
											</asp:TemplateField>
											<asp:TemplateField SortExpression="reg" HeaderText="Terminal">
												<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
												<ItemTemplate>
												<asp:TextBox ID="reg" AutoPostBack="True" CssClass="col_med" runat="server" Text='<%# Eval("reg") %>' Width=75 OnTextChanged="text_change" />
												</ItemTemplate>
												<ControlStyle width="100%" />																
											</asp:TemplateField>
											<asp:TemplateField SortExpression="fsm" Visible="false" HeaderText="FSM">
												<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
												<ItemTemplate>
												<asp:TextBox ID="fsm" AutoPostBack="True" CssClass="col_med" runat="server" Text='<%# Eval("fsm") %>' Width=75 OnTextChanged="text_change" />
												</ItemTemplate>
												<ControlStyle width="100%" />																
											</asp:TemplateField>
											<asp:TemplateField visible="False" HeaderText="Org">
												<ItemTemplate>
													<asp:DropDownList id="ddlOrg" AutoPostBack="True" OnSelectedIndexChanged="Selection_Change_Org" runat="server" 
													CssClass="col_med" SelectedValue='<%# Eval("org") %>' TabIndex='<%# TabIndex %>'>
														<asp:ListItem Value=""> </asp:ListItem>
														<asp:ListItem Value="Day"> Day </asp:ListItem>
														<asp:ListItem Value="Fdn"> Fdn </asp:ListItem>
													</asp:DropDownList>
												</ItemTemplate>
												<ControlStyle Width="100%" />								
											</asp:TemplateField>
											<asp:TemplateField Visible="true" SortExpression="Scheduled" HeaderText="Scheduled">												
												<HeaderStyle HorizontalAlign="Left" />												
												<ItemTemplate>
													<asp:TextBox ID="scheduled" AutoPostBack="True" runat="server" class="date_future col_med" Text='<%# Eval("Scheduled", "{0:dd-MMM-yyyy}") %>' Width=100 OnTextChanged="text_change_date" ></asp:TextBox>
												</ItemTemplate>
											</asp:TemplateField>			
											<asp:TemplateField SortExpression="status" HeaderText="Status" >
												<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
												<ItemTemplate>
												<asp:DropDownList id="ddlStatus" AutoPostBack="True" OnSelectedIndexChanged="Selection_Change_Status" Width=100 runat="server" 
												CssClass="col_med" SelectedValue='<%# Eval("status") %>' TabIndex='<%# TabIndex %>'>
													<asp:ListItem Value="In The Shop"> In The Shop </asp:ListItem>
													<asp:ListItem Value="Scheduled"> Scheduled </asp:ListItem>
													<asp:ListItem Value="Last Week"> Last Week </asp:ListItem>
													<asp:ListItem Value="Named"> Named </asp:ListItem>
												</asp:DropDownList>
												</ItemTemplate>
												<ControlStyle width="100%" />																
											</asp:TemplateField>
											<asp:TemplateField SortExpression="notes" HeaderText="Notes">
												<HeaderStyle HorizontalAlign="Left" CssClass="col_large" />
												<ItemTemplate>
												<asp:TextBox ID="notes" AutoPostBack="True" CssClass="col_large" runat="server" Text='<%# Eval("notes") %>' Width="400" OnTextChanged="text_change" />
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField>
												<ItemTemplate>
													<asp:LinkButton ID="addToNamedInStarted1" Tooltip="Add to (Named) In and Started" Class="fa fa-check-square-o text-secondary fa-lg" runat="server" OnClick="Click_AddToNamedInStarted"></asp:LinkButton>
												</ItemTemplate>						
											</asp:TemplateField>
											<asp:TemplateField>
												<ItemTemplate>
													<asp:LinkButton ID="addToInStarted1" Tooltip="Add to In and Started" Class="fa fa-check-square-o text-info fa-lg" runat="server" OnClick="Click_AddToInStarted"></asp:LinkButton>
												</ItemTemplate>						
											</asp:TemplateField>
											<asp:TemplateField>
												<ItemTemplate>
													<asp:LinkButton ID="addToNamedCompResign1" Tooltip="Add to (Named) Comp Resign" Class="fa fa-graduation-cap text-secondary fa-lg" runat="server" OnClick="Click_AddToNamedCompResign"></asp:LinkButton>
												</ItemTemplate>						
											</asp:TemplateField>
											<asp:TemplateField>
												<ItemTemplate>
													<asp:LinkButton ID="addToCompResign1" Tooltip="Add to Comp Resign" Class="fa fa-graduation-cap text-info fa-lg" runat="server" OnClick="Click_AddToCompResign"></asp:LinkButton>
												</ItemTemplate>						
											</asp:TemplateField>
											<asp:TemplateField>
												<ItemTemplate>
													<asp:LinkButton ID="lnkBtnEdit" Tooltip="Edit Record" Class="fa fa-pencil-square-o text-info fa-lg" runat="server" OnClick="Display"></asp:LinkButton>
												</ItemTemplate>						
											</asp:TemplateField>
											<asp:TemplateField>
												<ItemTemplate>
													<asp:LinkButton ID="lnkBtnDelete" Tooltip="Delete Record" Class="fa fa-times text-danger fa-lg" runat="server" OnClick="DeleteRow"></asp:LinkButton>
												</ItemTemplate>						
											</asp:TemplateField>										
										</Columns>
									</asp:GridView>			
									
									</div>
								</div>	
							</div>
						</div>
					  </div>
					</div>
				</div>
		    </ContentTemplate>
		</asp:UpdatePanel>

		<asp:UpdatePanel ID="UpdatePanelLastWeek" runat="server" UpdateMode="Conditional">
		    <ContentTemplate>
				<div class="row">
				  <div class="col-md-12">
					<div class="card">
					  <div class="card-header bg-warning">
						<a name="lastweek">
						<h5 class="card-title">
						Last Week BIS
						<asp:Label id="LastWeekLbl" runat="server" /> 						
						</h5>
						</a> 
						<div class="card-tools">
						  <button type="button" class="btn btn-tool" data-widget="collapse">
							<i class="fa fa-minus"></i>
						  </button>
						</div>
					  </div>
					  <!-- /.card-header -->
					  <div class="card-body">
						<div class="row">
								<div class="col-lg-12 ">  
									<div class="table-responsive"> 
									<asp:GridView 
										ID="GridViewLastWeek" 
										runat="server" 
										OnSorting="TaskGridView_Sorting" 
										AllowSorting="True" 
										AutoGenerateColumns="False" 
										BorderWidth="0" 
										CellPadding="2" 
										CssClass="mGridAppt"
										ForeColor="Black" 
										GridLines="None" 
										Width="100%" >
										<Columns>
											<asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID">
												<HeaderStyle CssClass="no-display"></HeaderStyle>
												<ItemStyle CssClass="no-display"></ItemStyle>
											</asp:BoundField>
											<asp:BoundField DataField="addo_ID">
												<HeaderStyle CssClass="no-display"></HeaderStyle>
												<ItemStyle CssClass="no-display"></ItemStyle>
											</asp:BoundField>  
											<asp:BoundField DataField="org" HeaderText="Org" SortExpression="org" >
												<HeaderStyle HorizontalAlign="Left" CssClass="col_xs" />
											</asp:BoundField>
											<asp:TemplateField HeaderText="ID">
												<ItemTemplate>
													<asp:HyperLink runat="server" class="no-print" Text='<%# Eval("Addo_ID") %>' Target="_blank" NavigateUrl='<%# "http://webapp2a.scientology.net/portal/cars/cosCarsPersonReport.jsp?hideLinks=true&addoId="+ Eval("Addo_ID") +"&orgId=" + System.Configuration.ConfigurationManager.AppSettings["orgid"] + "&browser=&userId=allhandsharlem&password=harlembas1"%>' />
												</ItemTemplate>
											</asp:TemplateField>                    										
											<asp:TemplateField SortExpression="name" HeaderText="Name">
												<HeaderStyle HorizontalAlign="Left" CssClass="col_name" />
												<ItemTemplate>
													<asp:TextBox ID="name" AutoPostBack="True" CssClass="col_name" runat="server" Text='<%# Eval("name") %>' Width="175px" OnTextChanged="text_change" />
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField SortExpression="area" HeaderText="Area">
												<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
												<ItemTemplate>
													<asp:DropDownList id="ddlReg" name="ddlReg" AutoPostBack="True" runat="server" Width=125 OnSelectedIndexChanged="Selection_Change_Reg"
														CssClass="col_med" SelectedValue='<%# Eval("area") %>' >
														<asp:ListItem Value="ACAD"> ACAD </asp:ListItem>
														<asp:ListItem Value="DIV6"> DIV6 </asp:ListItem>
														<asp:ListItem Value="DN"> DN </asp:ListItem>
														<asp:ListItem Value="HGC"> HGC </asp:ListItem>
														<asp:ListItem Value="HQS"> HQS </asp:ListItem>
														<asp:ListItem Value="KNOW"> GAK </asp:ListItem>
														<asp:ListItem Value="INTERN"> INTERN </asp:ListItem>
														<asp:ListItem Value="LI"> LI </asp:ListItem>
														<asp:ListItem Value="PE"> PE </asp:ListItem>
														<asp:ListItem Value="PURIF"> PURIF </asp:ListItem>
														<asp:ListItem Value="SRD"> SRD </asp:ListItem>
														<asp:ListItem Value="STCC"> STCC </asp:ListItem>
														<asp:ListItem Value="- UNK"> UNK </asp:ListItem>
													</asp:DropDownList>
												</ItemTemplate>
												<ControlStyle width="100%" />																
											</asp:TemplateField>
											<asp:TemplateField SortExpression="service" HeaderText="Service">
												<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
												<ItemTemplate>
												<asp:TextBox ID="service" AutoPostBack="True" CssClass="col_med" runat="server" Text='<%# Eval("service") %>' Width=100 OnTextChanged="text_change" />
												</ItemTemplate>
												<ControlStyle width="100%" />																
											</asp:TemplateField>
											<asp:TemplateField SortExpression="reg" HeaderText="Terminal">
												<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
												<ItemTemplate>
												<asp:TextBox ID="reg" AutoPostBack="True" CssClass="col_med" runat="server" Text='<%# Eval("reg") %>' Width=75 OnTextChanged="text_change" />
												</ItemTemplate>
												<ControlStyle width="100%" />																
											</asp:TemplateField>
											<asp:TemplateField SortExpression="fsm" Visible="false" HeaderText="FSM">
												<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
												<ItemTemplate>
												<asp:TextBox ID="fsm" AutoPostBack="True" CssClass="col_med" runat="server" Text='<%# Eval("fsm") %>' Width=75 OnTextChanged="text_change" />
												</ItemTemplate>
												<ControlStyle width="100%" />																
											</asp:TemplateField>
											<asp:TemplateField visible="False" HeaderText="Org">
												<ItemTemplate>
													<asp:DropDownList id="ddlOrg" AutoPostBack="True" OnSelectedIndexChanged="Selection_Change_Org" runat="server" 
													CssClass="col_med" SelectedValue='<%# Eval("org") %>' TabIndex='<%# TabIndex %>'>
														<asp:ListItem Value=""> </asp:ListItem>
														<asp:ListItem Value="Day"> Day </asp:ListItem>
														<asp:ListItem Value="Fdn"> Fdn </asp:ListItem>
													</asp:DropDownList>
												</ItemTemplate>
												<ControlStyle Width="100%" />								
											</asp:TemplateField>
											<asp:TemplateField Visible="true" SortExpression="Scheduled" HeaderText="Scheduled">												
												<HeaderStyle HorizontalAlign="Left" />												
												<ItemTemplate>
													<asp:TextBox ID="scheduled" AutoPostBack="True" runat="server" class="date_future col_med" Text='<%# Eval("Scheduled", "{0:dd-MMM-yyyy}") %>' Width=100 OnTextChanged="text_change_date" ></asp:TextBox>
												</ItemTemplate>
											</asp:TemplateField>			
											<asp:TemplateField SortExpression="status" HeaderText="Status" >
												<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
												<ItemTemplate>
												<asp:DropDownList id="ddlStatus" AutoPostBack="True" OnSelectedIndexChanged="Selection_Change_Status" Width=100 runat="server" 
												CssClass="col_med" SelectedValue='<%# Eval("status") %>' TabIndex='<%# TabIndex %>'>
													<asp:ListItem Value="In The Shop"> In The Shop </asp:ListItem>
													<asp:ListItem Value="Scheduled"> Scheduled </asp:ListItem>
													<asp:ListItem Value="Last Week"> Last Week </asp:ListItem>
													<asp:ListItem Value="Named"> Named </asp:ListItem>
												</asp:DropDownList>
												</ItemTemplate>
												<ControlStyle width="100%" />																
											</asp:TemplateField>
											<asp:TemplateField SortExpression="notes" HeaderText="Notes">
												<HeaderStyle HorizontalAlign="Left" CssClass="col_large" />
												<ItemTemplate>
												<asp:TextBox ID="notes" AutoPostBack="True" CssClass="col_large" runat="server" Text='<%# Eval("notes") %>' Width="400" OnTextChanged="text_change" />
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField>
												<ItemTemplate>
													<asp:LinkButton ID="addToNamedInStarted1" Tooltip="Add to (Named) In and Started" Class="fa fa-check-square-o text-secondary fa-lg" runat="server" OnClick="Click_AddToNamedInStarted"></asp:LinkButton>
												</ItemTemplate>						
											</asp:TemplateField>
											<asp:TemplateField>
												<ItemTemplate>
													<asp:LinkButton ID="addToInStarted1" Tooltip="Add to In and Started" Class="fa fa-check-square-o text-info fa-lg" runat="server" OnClick="Click_AddToInStarted"></asp:LinkButton>
												</ItemTemplate>						
											</asp:TemplateField>
											<asp:TemplateField>
												<ItemTemplate>
													<asp:LinkButton ID="addToNamedCompResign1" Tooltip="Add to (Named) Comp Resign" Class="fa fa-graduation-cap text-secondary fa-lg" runat="server" OnClick="Click_AddToNamedCompResign"></asp:LinkButton>
												</ItemTemplate>						
											</asp:TemplateField>
											<asp:TemplateField>
												<ItemTemplate>
													<asp:LinkButton ID="addToCompResign1" Tooltip="Add to Comp Resign" Class="fa fa-graduation-cap text-info fa-lg" runat="server" OnClick="Click_AddToCompResign"></asp:LinkButton>
												</ItemTemplate>						
											</asp:TemplateField>
											<asp:TemplateField>
												<ItemTemplate>
													<asp:LinkButton ID="lnkBtnEdit" Tooltip="Edit Record" Class="fa fa-pencil-square-o text-info fa-lg" runat="server" OnClick="Display"></asp:LinkButton>
												</ItemTemplate>						
											</asp:TemplateField>
											<asp:TemplateField>
												<ItemTemplate>
													<asp:LinkButton ID="lnkBtnDelete" Tooltip="Delete Record" Class="fa fa-times text-danger fa-lg" runat="server" OnClick="DeleteRow"></asp:LinkButton>
												</ItemTemplate>						
											</asp:TemplateField>										
										</Columns>
									</asp:GridView>	
									</div>
								</div>	
							</div>
						</div>
					  </div>
					</div>
				</div>		
		    </ContentTemplate>
		</asp:UpdatePanel>

		<asp:UpdatePanel ID="UpdatePanelNamed" runat="server" UpdateMode="Conditional">
		    <ContentTemplate>
				<div class="row">
				  <div class="col-md-12">
					<div class="card">
					  <div class="card-header bg-danger">
						<a name="felloff">
						<h5 class="card-title">
						Named/Fallen Off BIS
						<asp:Label id="NamedLbl" runat="server" /> 						
						</h5>
						</a> 
						<div class="card-tools">
						  <button type="button" class="btn btn-tool" data-widget="collapse">
							<i class="fa fa-minus"></i>
						  </button>
						</div>
					  </div>
					  <!-- /.card-header -->
					  <div class="card-body">
						<div class="row">
								<div class="col-lg-12 ">  
									<div class="table-responsive"> 
									<asp:GridView 
										ID="GridViewNamed" 
										runat="server" 
										OnSorting="TaskGridView_Sorting" 
										AllowSorting="True" 
										AutoGenerateColumns="False" 
										BorderWidth="0" 
										CellPadding="2" 
										CssClass="mGridAppt"
										ForeColor="Black" 
										GridLines="None" 
										Width="100%" >
										<Columns>
											<asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID">
												<HeaderStyle CssClass="no-display"></HeaderStyle>
												<ItemStyle CssClass="no-display"></ItemStyle>
											</asp:BoundField>
											<asp:BoundField DataField="addo_ID">
												<HeaderStyle CssClass="no-display"></HeaderStyle>
												<ItemStyle CssClass="no-display"></ItemStyle>
											</asp:BoundField>  
											<asp:BoundField DataField="org" HeaderText="Org" SortExpression="org" >
												<HeaderStyle HorizontalAlign="Left" CssClass="col_xs" />
											</asp:BoundField>
											<asp:TemplateField HeaderText="ID">
												<ItemTemplate>
													<asp:HyperLink runat="server" class="no-print" Text='<%# Eval("Addo_ID") %>' Target="_blank" NavigateUrl='<%# "http://webapp2a.scientology.net/portal/cars/cosCarsPersonReport.jsp?hideLinks=true&addoId="+ Eval("Addo_ID") +"&orgId=" + System.Configuration.ConfigurationManager.AppSettings["orgid"] + "&browser=&userId=allhandsharlem&password=harlembas1"%>' />
												</ItemTemplate>
											</asp:TemplateField>                    										
											<asp:TemplateField SortExpression="name" HeaderText="Name">
												<HeaderStyle HorizontalAlign="Left" CssClass="col_name" />
												<ItemTemplate>
													<asp:TextBox ID="name" AutoPostBack="True" CssClass="col_name" runat="server" Text='<%# Eval("name") %>' Width="175px" OnTextChanged="text_change" />
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField SortExpression="area" HeaderText="Area">
												<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
												<ItemTemplate>
													<asp:DropDownList id="ddlReg" name="ddlReg" AutoPostBack="True" runat="server" Width=125 OnSelectedIndexChanged="Selection_Change_Reg"
														CssClass="col_med" SelectedValue='<%# Eval("area") %>' >
														<asp:ListItem Value="ACAD"> ACAD </asp:ListItem>
														<asp:ListItem Value="DIV6"> DIV6 </asp:ListItem>
														<asp:ListItem Value="DN"> DN </asp:ListItem>
														<asp:ListItem Value="HGC"> HGC </asp:ListItem>
														<asp:ListItem Value="HQS"> HQS </asp:ListItem>
														<asp:ListItem Value="KNOW"> GAK </asp:ListItem>
														<asp:ListItem Value="INTERN"> INTERN </asp:ListItem>
														<asp:ListItem Value="LI"> LI </asp:ListItem>
														<asp:ListItem Value="PE"> PE </asp:ListItem>
														<asp:ListItem Value="PURIF"> PURIF </asp:ListItem>
														<asp:ListItem Value="SRD"> SRD </asp:ListItem>
														<asp:ListItem Value="STCC"> STCC </asp:ListItem>
														<asp:ListItem Value="- UNK"> UNK </asp:ListItem>
													</asp:DropDownList>
												</ItemTemplate>
												<ControlStyle width="100%" />																
											</asp:TemplateField>
											<asp:TemplateField SortExpression="service" HeaderText="Service">
												<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
												<ItemTemplate>
												<asp:TextBox ID="service" AutoPostBack="True" CssClass="col_med" runat="server" Text='<%# Eval("service") %>' Width=100 OnTextChanged="text_change" />
												</ItemTemplate>
												<ControlStyle width="100%" />																
											</asp:TemplateField>
											<asp:TemplateField SortExpression="reg" HeaderText="Terminal">
												<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
												<ItemTemplate>
												<asp:TextBox ID="reg" AutoPostBack="True" CssClass="col_med" runat="server" Text='<%# Eval("reg") %>' Width=75 OnTextChanged="text_change" />
												</ItemTemplate>
												<ControlStyle width="100%" />																
											</asp:TemplateField>
											<asp:TemplateField SortExpression="fsm" Visible="false" HeaderText="FSM">
												<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
												<ItemTemplate>
												<asp:TextBox ID="fsm" AutoPostBack="True" CssClass="col_med" runat="server" Text='<%# Eval("fsm") %>' Width=75 OnTextChanged="text_change" />
												</ItemTemplate>
												<ControlStyle width="100%" />																
											</asp:TemplateField>
											<asp:TemplateField visible="False" HeaderText="Org">
												<ItemTemplate>
													<asp:DropDownList id="ddlOrg" AutoPostBack="True" OnSelectedIndexChanged="Selection_Change_Org" runat="server" 
													CssClass="col_med" SelectedValue='<%# Eval("org") %>' TabIndex='<%# TabIndex %>'>
														<asp:ListItem Value=""> </asp:ListItem>
														<asp:ListItem Value="Day"> Day </asp:ListItem>
														<asp:ListItem Value="Fdn"> Fdn </asp:ListItem>
													</asp:DropDownList>
												</ItemTemplate>
												<ControlStyle Width="100%" />								
											</asp:TemplateField>
											<asp:TemplateField Visible="true" SortExpression="Scheduled" HeaderText="Scheduled">												
												<HeaderStyle HorizontalAlign="Left" />												
												<ItemTemplate>
													<asp:TextBox ID="scheduled" AutoPostBack="True" runat="server" class="date_future col_med" Text='<%# Eval("Scheduled", "{0:dd-MMM-yyyy}") %>' Width=100 OnTextChanged="text_change_date" ></asp:TextBox>
												</ItemTemplate>
											</asp:TemplateField>			
											<asp:TemplateField SortExpression="status" HeaderText="Status" >
												<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
												<ItemTemplate>
												<asp:DropDownList id="ddlStatus" AutoPostBack="True" OnSelectedIndexChanged="Selection_Change_Status" Width=100 runat="server" 
												CssClass="col_med" SelectedValue='<%# Eval("status") %>' TabIndex='<%# TabIndex %>'>
													<asp:ListItem Value="In The Shop"> In The Shop </asp:ListItem>
													<asp:ListItem Value="Scheduled"> Scheduled </asp:ListItem>
													<asp:ListItem Value="Last Week"> Last Week </asp:ListItem>
													<asp:ListItem Value="Named"> Named </asp:ListItem>
												</asp:DropDownList>
												</ItemTemplate>
												<ControlStyle width="100%" />																
											</asp:TemplateField>
											<asp:TemplateField SortExpression="notes" HeaderText="Notes">
												<HeaderStyle HorizontalAlign="Left" CssClass="col_large" />
												<ItemTemplate>
												<asp:TextBox ID="notes" AutoPostBack="True" CssClass="col_large" runat="server" Text='<%# Eval("notes") %>' Width="400" OnTextChanged="text_change" />
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField>
												<ItemTemplate>
													<asp:LinkButton ID="addToNamedInStarted1" Tooltip="Add to (Named) In and Started" Class="fa fa-check-square-o text-secondary fa-lg" runat="server" OnClick="Click_AddToNamedInStarted"></asp:LinkButton>
												</ItemTemplate>						
											</asp:TemplateField>
											<asp:TemplateField>
												<ItemTemplate>
													<asp:LinkButton ID="addToInStarted1" Tooltip="Add to In and Started" Class="fa fa-check-square-o text-info fa-lg" runat="server" OnClick="Click_AddToInStarted"></asp:LinkButton>
												</ItemTemplate>						
											</asp:TemplateField>
											<asp:TemplateField>
												<ItemTemplate>
													<asp:LinkButton ID="addToNamedCompResign1" Tooltip="Add to (Named) Comp Resign" Class="fa fa-graduation-cap text-secondary fa-lg" runat="server" OnClick="Click_AddToNamedCompResign"></asp:LinkButton>
												</ItemTemplate>						
											</asp:TemplateField>
											<asp:TemplateField>
												<ItemTemplate>
													<asp:LinkButton ID="addToCompResign1" Tooltip="Add to Comp Resign" Class="fa fa-graduation-cap text-info fa-lg" runat="server" OnClick="Click_AddToCompResign"></asp:LinkButton>
												</ItemTemplate>						
											</asp:TemplateField>
											<asp:TemplateField>
												<ItemTemplate>
													<asp:LinkButton ID="lnkBtnEdit" Tooltip="Edit Record" Class="fa fa-pencil-square-o text-info fa-lg" runat="server" OnClick="Display"></asp:LinkButton>
												</ItemTemplate>						
											</asp:TemplateField>
											<asp:TemplateField>
												<ItemTemplate>
													<asp:LinkButton ID="lnkBtnDelete" Tooltip="Delete Record" Class="fa fa-times text-danger fa-lg" runat="server" OnClick="DeleteRow"></asp:LinkButton>
												</ItemTemplate>						
											</asp:TemplateField>										
										</Columns>
									</asp:GridView>	
															
									</div>
								</div>	
							</div>
						</div>
					  </div>
					</div>
				</div>		
		    </ContentTemplate>
		</asp:UpdatePanel>
		
		<asp:UpdatePanel ID="UpdatePanelAlerts" runat="server" UpdateMode="Always">
		    <ContentTemplate>
				<asp:Button id="hiddenButton2" runat="server" style="display:none" />
				<ajaxToolkit:ModalPopupExtender runat="server" 
					ID="ModalPopupExtender2" 
					TargetControlID="hiddenButton2"
					PopupControlID="PanelAlert" X="770" Y="100" CancelControlID="ButtonOk"
					BackgroundCssClass="modalBackground">
				</ajaxToolkit:ModalPopupExtender>
				<asp:Panel ID="PanelAlert" runat="server" Style="display: none;" Width="400px" > 
					<div class="modal-dialog modal-lg" role="document">
						<div class="modal-content">
							<div class="modal-header modal-header-danger">
								<h3 class="modal-title">Info</h3>
							</div>
							<div class="modal-body">
								<div class="row">
									<div class="col-md-12">
										<fieldset>
											<p><asp:Label id="AlertPanel" runat="server" /> </p>
										</fieldset>
										<div class="modal-footer">
											<asp:Button ID="ButtonOk" class="btn btn-default" runat="server" Text="OK" />
										</div>	
									</div>
								</div>
							</div><!-- /.modal-body -->
						</div><!-- /.modal-content -->
					</div><!-- /.modal-dialog -->
				</asp:Panel> 
				
				<ajaxToolkit:ModalPopupExtender runat="server" 
					ID="ModalPopupExtender3" 
					TargetControlID="id"
					PopupControlID="PanelUpdate" X="570" Y="100" CancelControlID="ButtonOk2"
					BackgroundCssClass="modalBackground">
				</ajaxToolkit:ModalPopupExtender>
				
				<asp:Panel ID="PanelUpdate" runat="server" Style="display: none;" Width="800px" > 
					<div class="modal-dialog modal-lg" role="document">
						<div class="modal-content">
							<div class="modal-header">
								<button type="button" class="close" data-dismiss="modal">&times;</button>
								<h3 class="modal-title">Update Data</h3>
							</div>
							<div class="modal-body">
								<div class="row">
									<div class="col-md-12">
										<div class="row">
											<label for="lblnameid" class="col-sm-2 control-label">Name</label>
											<div class="col-sm-6">
												<asp:TextBox ID="lblnameid" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
											</div>
											<label for="statusid" class="col-sm-1 control-label">Status</label>
											<div class="col-sm-3">
												<asp:DropDownList id="statusid" runat="server" CssClass="form-control" 
												SelectedValue='<%# Eval("status") %>' TabIndex='<%# TabIndex %>'>
													<asp:ListItem Value="In The Shop"> In The Shop </asp:ListItem>
													<asp:ListItem Value="Scheduled"> Scheduled </asp:ListItem>
													<asp:ListItem Value="Last Week"> Last Week </asp:ListItem>
													<asp:ListItem Value="Named"> Named </asp:ListItem>
												</asp:DropDownList>
											</div>								
										</div>
										<div class="row">
											<label for="serviceid" class="col-sm-2 control-label">Service</label>
											<div class="col-sm-6">
												<asp:TextBox ID="serviceid" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
											</div>
											<label class="col-sm-1 control-label" for="ddlReg">Area</label>
											<div class="col-sm-3">
												<asp:DropDownList id="ddlReg" name="ddlReg" runat="server" CssClass="form-control"></asp:DropDownList>												
											</div>
										</div>
										<div class="row">
											<label for="regid" class="col-sm-2 control-label">Terminal</label>
											<div class="col-sm-6">
												<asp:TextBox ID="regid" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
											</div>
											<label for="orgid" class="col-sm-1 control-label">Org</label>
											<div class="col-sm-3">
												<asp:DropDownList id="orgid" runat="server" CssClass="form-control" 
												SelectedValue='<%# Eval("org") %>' TabIndex='<%# TabIndex %>'>
													<asp:ListItem Value="Day"> Day </asp:ListItem>
													<asp:ListItem Value="Fdn"> Fdn </asp:ListItem>							
												</asp:DropDownList>	
											</div>
										</div>						
										<div class="row">
											<label for="notesid" class="col-sm-2 control-label">Note</label>
											<div class="col-sm-10">
												<asp:TextBox TextMode="MultiLine" Rows="3" ID="notesid" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
											</div>
										</div>
										<div class="row">
											<div class="col-sm-10">
												<asp:TextBox ID="id" type="hidden" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
												<asp:TextBox ID="myStatus" type="hidden" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
											</div>
										</div>					
										<div class="modal-footer">
											<asp:Button ID="ButtonOk2" class="btn btn-default" runat="server" Text="Cancel" />
											<asp:Button ID="btnUpdate" OnClientClick="<% %>" class="btn btn-info" runat="server" Text="Save" CommandArgument='<%# Eval("Id") %>' OnCommand="btnUpdate_Click" />
										</div>							
									</div>
								</div>
							</div><!-- /.modal-body --> 
						</div><!-- /.modal-content -->
					</div><!-- /.modal-dialog -->
				</asp:Panel> 					
			</ContentTemplate>
		</asp:UpdatePanel>
		
		<asp:UpdateProgress runat="server" id="UpdateProgressProspects">
            <ProgressTemplate>
				<div align="center" style="left:900px; top:200px; position:fixed;
					opacity:.5;">
					<asp:Panel ID="Panel1" runat="server" Height="1px" Width="1px" BackColor="White" >
						<i class="fa fa-spinner fa-spin text-info" style="font-size:150px"></i>
					</asp:Panel>
				</div> 
            </ProgressTemplate>
        </asp:UpdateProgress>
				
		
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
				<asp:Label visible="false" id="HeadText" style="padding-left:15px" runat="server" />
			</div>
		</section>
	</div>
	<!-- content-wrapper -->
	<div id="alertModal" class="modal fade">
		<script type="text/javascript">
			function openAlert() {
				$('[id*=alertModal]').modal('show');
			} 
		</script>
		<div class="modal-dialog modal-md" role="document">
			<div class="modal-content">
				<div class="modal-header modal-header-danger">
					<button type="button" class="close" data-dismiss="modal">&times;</button>
					<h3 class="modal-title">Alert!</h3>
				</div>
				<div class="modal-body">
					<div class="row">
						<div class="col-md-12">
							<fieldset>
							<p>
								<asp:Label runat="server" id="AlertText" Text="" />									
							</p>
							</fieldset>
							<div class="modal-footer">
								<button type="submit" class="btn btn-default" data-dismiss="modal" aria-hidden="true">Close</button>
							</div>							
						</div>
					</div>
				</div><!-- /.modal-body -->
			</div><!-- /.modal-content -->
		</div><!-- /.modal-dialog -->
	</div><!-- /.modal -->
	<div id="archiveModal" class="modal fade" method="POST" role="dialog">
		<script type="text/javascript">
			function archiveWarning() {
				$('[id*=archiveModal]').modal('show');
			} 
		</script>
		<div class="modal-dialog">
			<div class="modal-content">
				<div class="modal-header modal-warning">
					<button type="button" class="close" data-dismiss="modal">&times;</button>
					<h4 class="modal-title">Warning!</h4>
				</div>
				<div class="modal-body">
					<p>If you continue, all "In The Shop" will be <strong>moved</strong> to "Archive"!</p>
				</div>
				<div class="modal-footer">
					<button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
					<asp:Button ID="btnArchive" OnClientClick="<% %>" class="btn btn-warning" runat="server" Text="Archive Week" CommandArgument='<%# Eval("Id") %>' OnCommand="Archive_Click" />
					
				</div>
			</div>
		</div>
	</div>
	<div id="delConfModal" class="modal fade">
		<script type="text/javascript">
			function ConfirmDeleteModal() {
				$('[id*=delConfModal]').modal('show');
			} 
		</script>
		<div class="modal-dialog modal-md" role="document">
			<div class="modal-content">
				<div class="modal-header modal-header-danger">
					<button type="button" class="close" data-dismiss="modal">&times;</button>
					<h3 class="modal-title">Warning!</h3>
				</div>
				<div class="modal-body">
					<div class="row">
						<div class="col-md-12">
							<fieldset>
								<p> Are you sure you want to Delete this record?</p>
							</fieldset>
							<div class="modal-footer">
								<button type="submit" class="btn btn-default" data-dismiss="modal" aria-hidden="true">Close</button>
								<asp:Button ID="btnDelete" OnClientClick="<% %>" class="btn btn-primary" runat="server" Text="Delete" CommandArgument='<%# Eval("Id") %>' OnCommand="btnDelete_Click" />
							</div>							
						</div>
					</div>
				</div><!-- /.modal-body -->
			</div><!-- /.modal-content -->
		</div><!-- /.modal-dialog -->
	</div><!-- /.modal -->

	<!-- REQUIRED SCRIPTS -->
	<script type="text/javascript" src="../js/jquery-1.12.4.js" charset="UTF-8"></script>
	<script type="text/javascript" src="../js/jquery-ui.js" charset="UTF-8"></script>
	<script type="text/javascript" src="../js/bootstrap.min.js"></script>
	<script src="../js/MaxLength.min.js" type="text/javascript" ></script>
	<script type="text/javascript">
		$(function () {
			//Specifying the Character Count control explicitly
			$("[id*=notesid]").MaxLength(
			{
				MaxLength: 250,
				DisplayCharacterCount: false
			});
		});
	</script>
	<script type="text/javascript">
        function pageLoad() 
        { 
            $(function () { 

				var dayOfWeek = 4;//thurs
				var nextThu = new Date();
				var lastThu = new Date();

				nextThu.setDate(nextThu.getDate() + (dayOfWeek + 7 - nextThu.getDay()) % 7);		
				lastThu.setDate(lastThu.getDate() + (dayOfWeek - 13 - lastThu.getDay()) % 7);		

				$('.date_thisweek').datepicker({
					dateFormat: "dd-M-yy",
					minDate: lastThu,
					maxDate: nextThu
				});	
				
				$( '.date_future' ).datepicker({
					dateFormat: "dd-M-yy",
					showOtherMonths: true,
					selectOtherMonths: true
				});			

            }); 
        } 
		
	</script>  	
	<script type="text/javascript">
		function OnlyThursdays(date) {
			var day = date.getDay();
			if (day == 4) {
				return [true] ; 
			} else { 
				return [false] ;
			}
		}
	  
		$(function() {
			$( '.date_we' ).datepicker({
				beforeShowDay: OnlyThursdays,
				dateFormat: "dd-M-yy",
				showOtherMonths: true,
				selectOtherMonths: true
			});
		});
	</script>	
	<script src="../js/bootstrap.bundle.min.js"></script>
	<script src="../js/adminlte.js"></script>
</form>	
</div>
</body>
</html>									
								