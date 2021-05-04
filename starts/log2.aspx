<!DOCTYPE html>
<%@ Page Language="C#" Trace="False" enableEventValidation="false" MaintainScrollPositionOnPostback="false" AutoEventWireup="true" CodeFile="log2.aspx.cs" Inherits="_Default" Debug="true" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"  
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
	
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
	<meta charset="utf-8">
	<meta name="viewport" content="width=device-width, initial-scale=1">
	<meta http-equiv="x-ua-compatible" content="ie=edge">
	<meta http-equiv="refresh" content="1000">
	
	<title>Starts</title>

	<link rel="stylesheet" href="../css/font-awesome.min.css" />
	<link rel="stylesheet" href="../css/adminlte.css" />
	<link rel="stylesheet" href="../css/jquery-ui.css">

	<link rel="icon" href="../img/favicon.png" />
    <style> 
	
	.ModalPopupBG
    { 
		background-color: #ffffff;
		filter: alpha(opacity=50);
		opacity: 0.7;
    } 

	.modalBackground
    { 
		background-color: #ffffff;
		filter: alpha(opacity=50);
		opacity: 0.7;
    } 
	.progress
	{
		z-index: 100002 !important;
	}
	
    </style> 

	
	<script>
		.highlight[
			background: blue;
		]
	</script>  	
	<script>
		$("div").click(function()[
			$(this).toggleClass("highlight");
		]);
	</script>  	
	
	
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
	  <li class="nav-item d-none d-sm-inline-block">
		<a href="../delivery.aspx" class="nav-link">Delivery View</a>
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
	  <li class="nav-item">
		<a href="../settings/registrars.aspx" class="nav-link">	  
		  <i class="fa fa-cog fa-lg text-info" title="Setup Registrars"></i>
		</a>
	  </li>	  
	  </ul>
	
  </nav>
  <!-- /.navbar -->

	<!-- Main Sidebar Container -->
	<aside class="main-sidebar sidebar-light-primary elevation-4">
		<!-- Brand Logo -->
		<a href="../home.aspx" class="brand-link">
		  <img src="../img/scn_sm.png" alt="AdminLTE Logo" class="brand-image img-circle elevation-3"
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
			  <!-- Add icons to the links using the .nav-icon class
				   with font-awesome or any other icon font library -->

			<li class="nav-item">
				<a href="log.aspx" class="nav-link" >
				  <i class="nav-icon fa fa-play-circle-o text-info"></i>
				  <p>
					Starts Log
				  </p>
				</a>
				<hr>    
				<li class="nav-item">
					<a href="../gi/fppp.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-secondary"></i>
						<p>Fully Partially Paid</p>
					</a>
				</li>
				<hr>
				<li class="nav-item">
					<a href="archive.aspx" class="nav-link">
						<i class="fa fa-archive nav-icon text-secondary"></i>
						<p>Archive</p>
					</a>
				</li>	  
				<hr>    
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
			  <div class="col-sm-6">
				<h1 class="m-0 text-dark">
				<i class="nav-icon fa fa-play-circle-o text-info"></i>
				<asp:Label id="HeaderText" runat="server" />								
				</h1>
				<asp:Label visible="false" id="OrgText" runat="server" Text="Combined" />				
			  </div><!-- /.col -->
			  <div class="col-sm-6">
				<ol class="breadcrumb float-sm-right">
				  <li class="breadcrumb-item"><a href="../home.aspx">Home</a></li>
				  <li class="breadcrumb-item active">GI Log</li>
				</ol>
			  </div><!-- /.col -->
			</div><!-- /.row -->
		  </div><!-- /.container-fluid -->
		</div>
		<!-- /.content-header -->
		<ajaxToolKit:ToolkitScriptManager EnablePartialRendering="true" runat="server" />

		<!-- Main content -->
		<section class="content">
			<div class="container-fluid">
				<!-- TOTALS -->
				<asp:UpdatePanel ID="UpdatePanelTotal" runat="server" UpdateMode="Conditional">
				<ContentTemplate>
				<!-- TOTALS -->
				<div class="row">
				  <div class="col-3">
					<div class="small-box bg-info-gradient">
					<!-- <div class="info-box"> -->
					  <div class="info-box-content">
						<span class="info-box-number">PAID STARTS</span>
						<span class="info-box-text">
						<h3>
							<asp:Label id="CardStarted" runat="server" /> 
						</h3>
						</span>
						<span class="progress-description">
							<asp:Label id="CardStartedDay" runat="server" /> 
						</span>
						<span class="progress-description">
							<asp:Label id="CardStartedFdn" runat="server" /> 
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
						<a href="#scheduled"><font color="white">
						<span class="info-box-number">SCHEDULED</span>
						<span class="info-box-text">
							<h3> 
							<asp:Label id="CardScheduled" runat="server" /> 
							</h3>
						</span>
						<span class="progress-description">
							<asp:Label id="CardScheduledDay" runat="server" /> 
						</span>
						<span class="progress-description">
							<asp:Label id="CardScheduledFdn" runat="server" /> 
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
					<div class="small-box bg-info-gradient">
					  <div class="info-box-content">

						<a href="#named"><font color="white">
						<span class="info-box-number">NAMED</span>
						<span class="info-box-text">
							<h3> 
							<asp:Label id="CardNamed" runat="server" /> 
							</h3>
						</span>
						<span class="progress-description">
							<asp:Label id="CardNamedDay" runat="server" /> 
						</span>
						<span class="progress-description">
							<asp:Label id="CardNamedFdn" runat="server" /> 
						</span>
						</font>
						</a>

					</div>
					  <!-- /.info-box-content -->
					</div>
					<!-- /.info-box -->
				  </div>
				  <div class="col-3">
					<div class="small-box bg-secondary-gradient">
					  <div class="info-box-content">

						<a href="#future"><font color="white">
						<span class="info-box-number">FUTURE</span>
						<span class="info-box-text">
							<h3> 
							<asp:Label id="CardFuture" runat="server" /> 
							</h3>
						</span>
						<span class="progress-description">
							<asp:Label id="CardFutureDay" runat="server" /> 
						</span>
						<span class="progress-description">
							<asp:Label id="CardFutureFdn" runat="server" /> 
						</span>
						</font>
						</a>

					</div>
					  <!-- /.info-box-content -->
					</div>
					<!-- /.info-box -->
				  </div>				  
				</div>
				<div class="row">
				  <div class="col-3">
					<div class="small-box bg-warning-gradient">
					<!-- <div class="info-box"> --><font color="teal">
					  <div class="info-box-content">
						<span class="info-box-number">PUBLIC REG PAID STARTS</span>
						<span class="info-box-text">
						<h3>
							<asp:Label id="CardStarted6" runat="server" /> 
						</h3>
						</span>
						<span class="progress-description">
							<asp:Label id="CardStartedDay6" runat="server" /> 
						</span>
						<span class="progress-description">
							<asp:Label id="CardStartedFdn6" runat="server" /> 
						</span>
						</font>
					  </div>
					  <!-- /.info-box-content -->
					</div>
					<!-- /.info-box -->
				  </div>
				  <!-- /.col -->				  	
				  <div class="col-3">
					<div class="small-box bg-warning-gradient">
					  <div class="info-box-content">
						<a href="#scheduled"><font color="teal">
						<span class="info-box-number">SCHEDULED</span>
						<span class="info-box-text">
							<h3> 
							<asp:Label id="CardScheduled6" runat="server" /> 
							</h3>
						</span>
						<span class="progress-description">
							<asp:Label id="CardScheduledDay6" runat="server" /> 
						</span>
						<span class="progress-description">
							<asp:Label id="CardScheduledFdn6" runat="server" /> 
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

						<a href="#named"><font color="teal">
						<span class="info-box-number">NAMED</span>
						<span class="info-box-text">
							<h3> 
							<asp:Label id="CardNamed6" runat="server" /> 
							</h3>
						</span>
						<span class="progress-description">
							<asp:Label id="CardNamedDay6" runat="server" /> 
						</span>
						<span class="progress-description">
							<asp:Label id="CardNamedFdn6" runat="server" /> 
						</span>
						</font>
						</a>

					</div>
					  <!-- /.info-box-content -->
					</div>
					<!-- /.info-box -->
				  </div>
				  <div class="col-3">
					<div class="small-box bg-secondary-gradient">
					  <div class="info-box-content">

						<a href="#future"><font color="white">
						<span class="info-box-number">FUTURE</span>
						<span class="info-box-text">
							<h3> 
							<asp:Label id="CardFuture6" runat="server" /> 
							</h3>
						</span>
						<span class="progress-description">
							<asp:Label id="CardFutureDay6" runat="server" /> 
						</span>
						<span class="progress-description">
							<asp:Label id="CardFutureFdn6" runat="server" /> 
						</span>
						</font>
						</a>

					</div>
					  <!-- /.info-box-content -->
					</div>
					<!-- /.info-box -->
				  </div>				  
				</div>
				<div class="row no-print">						
				  <div class="col-md-2">
						<asp:LinkButton runat="server" CommandArgument="Resign" OnClick="LinkButton_Command">					
						<div class="small-box bg-default-gradient">
						  <div id="boxResign" runat="server" class="info-box-content">
							<span class="info-box-number">RESIGN</span>
							<span class="info-box-text">
							<span class="progress-description">
								<asp:Label id="resignDay" runat="server" /> 
							</span>
							<span class="progress-description">
								<asp:Label id="resignFdn" runat="server" /> 
							</span>
							</div>
						</div>
						</asp:LinkButton>							
				  </div>
				  <div class="col-md-2">
					<asp:LinkButton runat="server" CommandArgument="Arrival" OnClick="LinkButton_Command">					
					<div class="small-box bg-default-gradient">
						<div id="boxArrival" runat="server" class="info-box-content">					
						<span class="info-box-number">ARRIVAL</span>
						<span class="info-box-text">
						</span>
						<span class="progress-description">
							<asp:Label id="arrivalDay" runat="server" /> 
						</span>
						<span class="progress-description">
							<asp:Label id="arrivalFdn" runat="server" /> 
						</span>
					  </div>
					  <!-- /.info-box-content -->
					</div>
					</asp:LinkButton>							
					<!-- /.info-box -->
				  </div>
				  <div class="col-md-2">
					<asp:LinkButton runat="server" CommandArgument="CF" OnClick="LinkButton_Command">					
					<div class="small-box bg-default-gradient">
					  <div id="boxCF" runat="server" class="info-box-content">
						<span class="info-box-number">CF</span>
						<span class="info-box-text">
						</span>
						<span class="progress-description">
							<asp:Label id="cfDay" runat="server" /> 
						</span>
						<span class="progress-description">
							<asp:Label id="cfFdn" runat="server" /> 
						</span>
					  </div>
					  <!-- /.info-box-content -->
					</div>
					</asp:LinkButton>							
					<!-- /.info-box -->
				  </div>
				  <div class="col-md-2">
					<asp:LinkButton runat="server" CommandArgument="FSM" OnClick="LinkButton_Command">					
					<div class="small-box bg-default-gradient">
					  <div id="boxFSM" runat="server" class="info-box-content">
						<span class="info-box-number">FSM</span>
						<span class="info-box-text">
						</span>
						<span class="progress-description">
							<asp:Label id="fsmDay" runat="server" /> 
						</span>
						<span class="progress-description">
							<asp:Label id="fsmFdn" runat="server" /> 
						</span>
					  </div>
					  <!-- /.info-box-content -->
					</div>
					</asp:LinkButton>							
					<!-- /.info-box -->
				  </div>
				  <div class="col-md-2">
					<asp:LinkButton runat="server" CommandArgument="Prospecting" OnClick="LinkButton_Command">					
					<div class="small-box bg-default-gradient">
					  <div id="boxProspecting" runat="server" class="info-box-content">
						<span class="info-box-number">PROSPECTING</span>
						<span class="info-box-text">
						</span>
						<span class="progress-description">
							<asp:Label id="prospectingDay" runat="server" /> 
						</span>
						<span class="progress-description">
							<asp:Label id="prospectingFdn" runat="server" /> 
						</span>
					  </div>
					  <!-- /.info-box-content -->
					</div>
					</asp:LinkButton>							
					<!-- /.info-box -->
				  </div>
				  <div class="col-md-2">
					<asp:LinkButton runat="server" CommandArgument="Other" OnClick="LinkButton_Command">					
					<div class="small-box bg-default-gradient">
					  <div id="boxOther" runat="server" class="info-box-content">
						<span class="info-box-number">OTHER</span>
						<span class="info-box-text">
						</span>
						<span class="progress-description">
							<asp:Label id="otherDay" runat="server" /> 
						</span>
						<span class="progress-description">
							<asp:Label id="otherFdn" runat="server" /> 
						</span>
					  </div>
					  <!-- /.info-box-content -->
					</div>
					</asp:LinkButton>							
					<!-- /.info-box -->
				  </div>				  
				</div>	
				<div class="row no-print">						
				  <div class="col-md-2">
					<asp:LinkButton runat="server" CommandArgument="DIV6" OnClick="LinkButtonArea_Command">					
					<div class="small-box bg-default-gradient">
					  <div id="boxDiv6" runat="server" class="info-box-content">
						<span class="info-box-number">DIV 6</span>
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
					  <div id="boxDiv6Training" runat="server" class="info-box-content">
						<span class="info-box-number">GAK</span>
						<span class="info-box-text">
						</span>
						<span class="progress-description">
							<asp:Label id="div6trainingDay" runat="server" /> 
						</span>
						<span class="progress-description">
							<asp:Label id="div6trainingFdn" runat="server" /> 
						</span>
					  </div>
					  <!-- /.info-box-content -->
					</div>
					</asp:LinkButton>							
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
										<asp:DropDownList id="ddlSearchGI" CssClass="form-control form-control-md" runat="server">
										<asp:ListItem Value="Name"> Name </asp:ListItem>
										<asp:ListItem Value="Reg"> Terminal </asp:ListItem>
										</asp:DropDownList>  
									</div>				
									<div class="col-5">
										<asp:TextBox ID="txtGI" CssClass="form-control form-control-md" runat="server"></asp:TextBox>
									</div>				
									<div class="col-2">
										<button runat="server" id="Button5" class="btn btn-block btn-outline-info btn-md" onserverclick="BtnSearch_Click" title="Search">
										Search
										</button>							
									</div>	
									<div class="col-2">
										<button runat="server" id="btnOpenModal" class="btn btn-block btn-info btn-md" onserverclick="OpenModal" title="Add Name">
											Add Name
										</button>							
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
										<button runat="server" id="btnOpenArchiveModal" class="form-control btn btn-outline-danger" onserverclick="OpenArchiveModal" title="Archive">
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
				<div class="row">
				  <div class="col-md-12">
					<div class="card">
					  <div class="card-header bg-info-gradient"">
						<h5 class="card-title">
							Paid Starts
							<asp:Label runat="server" id="lblStart" Text="" /> 
							/ Public Reg Paid Starts 
							<asp:Label runat="server" id="lblPRPS" Text="" /> </br>
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
											ID="GridViewStarted" 
											runat="server" 
											OnSorting="TaskGridView_Sorting" 
											AllowSorting="True" 
											AutoGenerateColumns="False" 
											BorderWidth="1px" 
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
															<asp:HyperLink runat="server" CssClass="col_med" Text='<%# Eval("Addo_ID") %>' Target="_blank" NavigateUrl='<%# "http://webapp2a.scientology.net/portal/cars/cosCarsPersonReport.jsp?hideLinks=true&addoId="+ Eval("Addo_ID") +"&orgId=" + System.Configuration.ConfigurationManager.AppSettings["orgid"] + "&browser=&userId=allhandsharlem&password=harlembas1"%>' />
														</ItemTemplate>
													</asp:TemplateField> 
													<asp:TemplateField SortExpression="rank" Visible="true" HeaderText="Type" >
														<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
														<ItemTemplate>
														<asp:DropDownList id="ddlRank" AutoPostBack="True" OnSelectedIndexChanged="Rank_Change" runat="server" CssClass="col_med" 
														SelectedValue='<%# Eval("rank") %>' TabIndex='<%# TabIndex %>'>
														<asp:ListItem Value="PS"> Paid Start </asp:ListItem>
														<asp:ListItem Value="PRPS"> PRPS </asp:ListItem>
														</asp:DropDownList>
														</ItemTemplate>
													</asp:TemplateField>													
													<asp:TemplateField SortExpression="name" HeaderText="Name">
														<HeaderStyle HorizontalAlign="Left" CssClass="col_name" />
														<ItemTemplate>
															<asp:TextBox ID="name" AutoPostBack="True" CssClass="col_name" runat="server" Text='<%# Eval("name") %>' Width=180 OnTextChanged="text_change" />
														</ItemTemplate>					
													</asp:TemplateField>
													<asp:TemplateField SortExpression="area" Visible="true" HeaderText="Area" >
														<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
														<ItemTemplate>
														<asp:DropDownList id="ddlArea" AutoPostBack="True" OnSelectedIndexChanged="Area_Change" runat="server" CssClass="col_med" 
														SelectedValue='<%# Eval("area") %>' TabIndex='<%# TabIndex %>'>
														<asp:ListItem Value="DIV6"> DIV6 </asp:ListItem>
														<asp:ListItem Value="Purif"> PURIF </asp:ListItem>
														<asp:ListItem Value="SRD"> SRD </asp:ListItem>
														<asp:ListItem Value="HGC"> HGC </asp:ListItem>
														<asp:ListItem Value="ACAD"> ACAD </asp:ListItem>
														<asp:ListItem Value="GAK"> GAK </asp:ListItem>
														</asp:DropDownList>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField SortExpression="service" Visible="true" HeaderText="Service">
														<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
														<ItemTemplate>
															<asp:TextBox ID="service" CssClass="col_med" AutoPostBack="True" runat="server" Text='<%# Eval("service") %>' Width=125 OnTextChanged="text_change" 
																TabIndex='<%# TabIndex %>' />
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField SortExpression="reg" HeaderText="Terminal">
														<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
														<ItemTemplate>
														<asp:TextBox ID="reg" AutoPostBack="True" CssClass="col_med" runat="server" Text='<%# Eval("reg") %>' Width=100 OnTextChanged="text_change" />
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField Visible="true" SortExpression="scheduled" HeaderText="Scheduled">												
														<HeaderStyle HorizontalAlign="Left" />												
														<ItemTemplate>
															<asp:TextBox ID="scheduled" AutoPostBack="True" runat="server" class="date_future col_med" Text='<%# Eval("scheduled", "{0:dd-MMM-yyyy}") %>' Width=100 OnTextChanged="text_change_date" ></asp:TextBox>
														</ItemTemplate>
													</asp:TemplateField>																
													<asp:TemplateField SortExpression="line" HeaderText="Line" >
														<ItemTemplate>
															<asp:DropDownList id="ddlLine" AutoPostBack="True" OnSelectedIndexChanged="Selection_Change_Line" runat="server" CssClass="col_xs"
																SelectedValue='<%# Eval("line") %>' TabIndex='<%# TabIndex %>'>
																<asp:ListItem Value="Other"> - </asp:ListItem>
																<asp:ListItem Value="Resign"> Resign </asp:ListItem>
																<asp:ListItem Value="FSM"> FSM </asp:ListItem>
																<asp:ListItem Value="Prospecting"> Prospecting </asp:ListItem>
																<asp:ListItem Value="Arrival"> Arrival </asp:ListItem>
																<asp:ListItem Value="CF"> CF </asp:ListItem>
															</asp:DropDownList>
														</ItemTemplate>
														<ControlStyle Width="100%" />								
													</asp:TemplateField>												
													<asp:TemplateField SortExpression="status" HeaderText="Status" >
														<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
														<ItemTemplate>
															<asp:DropDownList id="ddlStatus" AutoPostBack="True" OnSelectedIndexChanged="Selection_Change_Status" Width=90 runat="server" 
																CssClass="col_med" SelectedValue='<%# Eval("status") %>' TabIndex='<%# TabIndex %>'>
																<asp:ListItem Value="Started"> Started </asp:ListItem>
																<asp:ListItem Value="Scheduled"> Scheduled </asp:ListItem>
																<asp:ListItem Value="Named"> Named </asp:ListItem>
																<asp:ListItem Value="Future"> Future </asp:ListItem>
															</asp:DropDownList>
														</ItemTemplate>
														<ControlStyle Width="100%" />								
													</asp:TemplateField>
													<asp:TemplateField SortExpression="notes" HeaderText="Notes">
														<HeaderStyle HorizontalAlign="Left" CssClass="col_large" />
														<ItemTemplate>
															<asp:TextBox ID="notes" AutoPostBack="True" CssClass="col_large" runat="server" Text='<%# Eval("notes") %>' Width="400" OnTextChanged="text_change" />
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField Visible="true" >
														<ItemTemplate>
															<asp:LinkButton 
																ID="addToInStarted" Tooltip="In and Started" Class="fa fa-check-square-o text-info fa-lg" runat="server" OnClick="Click_AddToInAndStarted">
															</asp:LinkButton>
														</ItemTemplate>						
													</asp:TemplateField>
													<asp:TemplateField>
														<ItemTemplate>
															<asp:LinkButton 
																ID="lnkBtnDelete" Class="fa fa-times" style="color:red" runat="server" OnClick="DeleteRow">
															</asp:LinkButton>
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

		<asp:UpdatePanel ID="UpdatePanelScheduled" runat="server" UpdateMode="Conditional">
		    <ContentTemplate>
				<div class="row">
				  <div class="col-md-12">
					<div class="card">
					  <div class="card-header bg-light-gradient" >
					  <a name="scheduled">
						<h5 class="card-title">
						Scheduled for this Week
						<asp:Label runat="server" id="LblSched" />
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
											BorderWidth="1px" 
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
															<asp:HyperLink runat="server" CssClass="col_med" Text='<%# Eval("Addo_ID") %>' Target="_blank" NavigateUrl='<%# "http://webapp2a.scientology.net/portal/cars/cosCarsPersonReport.jsp?hideLinks=true&addoId="+ Eval("Addo_ID") +"&orgId=" + System.Configuration.ConfigurationManager.AppSettings["orgid"] + "&browser=&userId=allhandsharlem&password=harlembas1"%>' />
														</ItemTemplate>
													</asp:TemplateField> 
													<asp:TemplateField SortExpression="rank" Visible="true" HeaderText="Type" >
														<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
														<ItemTemplate>
														<asp:DropDownList id="ddlRank" AutoPostBack="True" OnSelectedIndexChanged="Rank_Change" runat="server" CssClass="col_med" 
														SelectedValue='<%# Eval("rank") %>' TabIndex='<%# TabIndex %>'>
														<asp:ListItem Value="PS"> Paid Start </asp:ListItem>
														<asp:ListItem Value="PRPS"> PRPS </asp:ListItem>
														</asp:DropDownList>
														</ItemTemplate>
													</asp:TemplateField>													
													
													<asp:TemplateField SortExpression="name" HeaderText="Name">
														<HeaderStyle HorizontalAlign="Left" CssClass="col_name" />
														<ItemTemplate>
															<asp:TextBox ID="name" AutoPostBack="True" CssClass="col_name" runat="server" Text='<%# Eval("name") %>' Width=180 OnTextChanged="text_change" />
														</ItemTemplate>					
													</asp:TemplateField>
													<asp:TemplateField SortExpression="area" Visible="true" HeaderText="Area" >
														<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
														<ItemTemplate>
														<asp:DropDownList id="ddlArea" AutoPostBack="True" OnSelectedIndexChanged="Area_Change" runat="server" CssClass="col_med" 
														SelectedValue='<%# Eval("area") %>' TabIndex='<%# TabIndex %>'>
														<asp:ListItem Value="DIV6"> DIV6 </asp:ListItem>
														<asp:ListItem Value="Purif"> PURIF </asp:ListItem>
														<asp:ListItem Value="SRD"> SRD </asp:ListItem>
														<asp:ListItem Value="HGC"> HGC </asp:ListItem>
														<asp:ListItem Value="ACAD"> ACAD </asp:ListItem>
														<asp:ListItem Value="GAK"> GAK </asp:ListItem>
														</asp:DropDownList>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField SortExpression="service" Visible="true" HeaderText="Service">
														<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
														<ItemTemplate>
															<asp:TextBox ID="service" CssClass="col_med" AutoPostBack="True" runat="server" Text='<%# Eval("service") %>' Width=125 OnTextChanged="text_change" 
																TabIndex='<%# TabIndex %>' />
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField SortExpression="reg" HeaderText="Terminal">
														<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
														<ItemTemplate>
														<asp:TextBox ID="reg" AutoPostBack="True" CssClass="col_med" runat="server" Text='<%# Eval("reg") %>' Width=100 OnTextChanged="text_change" />
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField Visible="true" SortExpression="scheduled" HeaderText="Scheduled">												
														<HeaderStyle HorizontalAlign="Left" />												
														<ItemTemplate>
															<asp:TextBox ID="scheduled" AutoPostBack="True" runat="server" class="date_future col_med" Text='<%# Eval("scheduled", "{0:dd-MMM-yyyy}") %>' Width=100 OnTextChanged="text_change_date" ></asp:TextBox>
														</ItemTemplate>
													</asp:TemplateField>																<asp:TemplateField SortExpression="line" HeaderText="Line" >
														<ItemTemplate>
															<asp:DropDownList id="ddlLine" AutoPostBack="True" OnSelectedIndexChanged="Selection_Change_Line" runat="server" CssClass="col_xs"
																SelectedValue='<%# Eval("line") %>' TabIndex='<%# TabIndex %>'>
																<asp:ListItem Value="Other"> - </asp:ListItem>
																<asp:ListItem Value="Resign"> Resign </asp:ListItem>
																<asp:ListItem Value="FSM"> FSM </asp:ListItem>
																<asp:ListItem Value="Prospecting"> Prospecting </asp:ListItem>
																<asp:ListItem Value="Arrival"> Arrival </asp:ListItem>
																<asp:ListItem Value="CF"> CF </asp:ListItem>
															</asp:DropDownList>
														</ItemTemplate>
														<ControlStyle Width="100%" />								
													</asp:TemplateField>												
													<asp:TemplateField SortExpression="status" HeaderText="Status" >
														<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
														<ItemTemplate>
															<asp:DropDownList id="ddlStatus" AutoPostBack="True" OnSelectedIndexChanged="Selection_Change_Status" Width=90 runat="server" 
																CssClass="col_med" SelectedValue='<%# Eval("status") %>' TabIndex='<%# TabIndex %>'>
																<asp:ListItem Value="Started"> Started </asp:ListItem>
																<asp:ListItem Value="Scheduled"> Scheduled </asp:ListItem>
																<asp:ListItem Value="Named"> Named </asp:ListItem>
																<asp:ListItem Value="Future"> Future </asp:ListItem>
															</asp:DropDownList>
														</ItemTemplate>
														<ControlStyle Width="100%" />								
													</asp:TemplateField>
													<asp:TemplateField SortExpression="notes" HeaderText="Notes">
														<HeaderStyle HorizontalAlign="Left" CssClass="col_large" />
														<ItemTemplate>
															<asp:TextBox ID="notes" AutoPostBack="True" CssClass="col_large" runat="server" Text='<%# Eval("notes") %>' Width="400" OnTextChanged="text_change" />
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField Visible="true" >
														<ItemTemplate>
															<asp:LinkButton 
																ID="addToInStarted" Tooltip="In and Started" Class="fa fa-check-square-o text-info fa-lg" runat="server" OnClick="Click_AddToInAndStarted">
															</asp:LinkButton>
														</ItemTemplate>						
													</asp:TemplateField>
													<asp:TemplateField>
														<ItemTemplate>
															<asp:LinkButton 
																ID="lnkBtnDelete" Class="fa fa-times" style="color:red" runat="server" OnClick="DeleteRow">
															</asp:LinkButton>
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
					  <div class="card-header bg-light-gradient" >
						<a name="named">
						<h5 class="card-title">
						Named for this Week
						<asp:Label id="LblNamed" runat="server" /> 						
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
											BorderWidth="1px" 
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
															<asp:HyperLink runat="server" CssClass="col_med" Text='<%# Eval("Addo_ID") %>' Target="_blank" NavigateUrl='<%# "http://webapp2a.scientology.net/portal/cars/cosCarsPersonReport.jsp?hideLinks=true&addoId="+ Eval("Addo_ID") +"&orgId=" + System.Configuration.ConfigurationManager.AppSettings["orgid"] + "&browser=&userId=allhandsharlem&password=harlembas1"%>' />
														</ItemTemplate>
													</asp:TemplateField> 
													<asp:TemplateField SortExpression="rank" Visible="true" HeaderText="Type" >
														<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
														<ItemTemplate>
														<asp:DropDownList id="ddlRank" AutoPostBack="True" OnSelectedIndexChanged="Rank_Change" runat="server" CssClass="col_med" 
														SelectedValue='<%# Eval("rank") %>' TabIndex='<%# TabIndex %>'>
														<asp:ListItem Value="PS"> Paid Start </asp:ListItem>
														<asp:ListItem Value="PRPS"> PRPS </asp:ListItem>
														</asp:DropDownList>
														</ItemTemplate>
													</asp:TemplateField>													
													<asp:TemplateField SortExpression="name" HeaderText="Name">
														<HeaderStyle HorizontalAlign="Left" CssClass="col_name" />
														<ItemTemplate>
															<asp:TextBox ID="name" AutoPostBack="True" CssClass="col_name" runat="server" Text='<%# Eval("name") %>' Width=180 OnTextChanged="text_change" />
														</ItemTemplate>					
													</asp:TemplateField>
													<asp:TemplateField SortExpression="area" Visible="true" HeaderText="Area" >
														<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
														<ItemTemplate>
														<asp:DropDownList id="ddlArea" AutoPostBack="True" OnSelectedIndexChanged="Area_Change" runat="server" CssClass="col_med" 
														SelectedValue='<%# Eval("area") %>' TabIndex='<%# TabIndex %>'>
														<asp:ListItem Value="DIV6"> DIV6 </asp:ListItem>
														<asp:ListItem Value="Purif"> PURIF </asp:ListItem>
														<asp:ListItem Value="SRD"> SRD </asp:ListItem>
														<asp:ListItem Value="HGC"> HGC </asp:ListItem>
														<asp:ListItem Value="ACAD"> ACAD </asp:ListItem>
														<asp:ListItem Value="GAK"> GAK </asp:ListItem>
														</asp:DropDownList>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField SortExpression="service" Visible="true" HeaderText="Service">
														<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
														<ItemTemplate>
															<asp:TextBox ID="service" CssClass="col_med" AutoPostBack="True" runat="server" Text='<%# Eval("service") %>' Width=125 OnTextChanged="text_change" 
																TabIndex='<%# TabIndex %>' />
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField SortExpression="reg" HeaderText="Terminal">
														<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
														<ItemTemplate>
														<asp:TextBox ID="reg" AutoPostBack="True" CssClass="col_med" runat="server" Text='<%# Eval("reg") %>' Width=100 OnTextChanged="text_change" />
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField Visible="true" SortExpression="scheduled" HeaderText="Scheduled">												
														<HeaderStyle HorizontalAlign="Left" />												
														<ItemTemplate>
															<asp:TextBox ID="scheduled" AutoPostBack="True" runat="server" class="date_future col_med" Text='<%# Eval("scheduled", "{0:dd-MMM-yyyy}") %>' Width=100 OnTextChanged="text_change_date" ></asp:TextBox>
														</ItemTemplate>
													</asp:TemplateField>																<asp:TemplateField SortExpression="line" HeaderText="Line" >
														<ItemTemplate>
															<asp:DropDownList id="ddlLine" AutoPostBack="True" OnSelectedIndexChanged="Selection_Change_Line" runat="server" CssClass="col_xs"
																SelectedValue='<%# Eval("line") %>' TabIndex='<%# TabIndex %>'>
																<asp:ListItem Value="Other"> - </asp:ListItem>
																<asp:ListItem Value="Resign"> Resign </asp:ListItem>
																<asp:ListItem Value="FSM"> FSM </asp:ListItem>
																<asp:ListItem Value="Prospecting"> Prospecting </asp:ListItem>
																<asp:ListItem Value="Arrival"> Arrival </asp:ListItem>
																<asp:ListItem Value="CF"> CF </asp:ListItem>
															</asp:DropDownList>
														</ItemTemplate>
														<ControlStyle Width="100%" />								
													</asp:TemplateField>												
													<asp:TemplateField SortExpression="status" HeaderText="Status" >
														<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
														<ItemTemplate>
															<asp:DropDownList id="ddlStatus" AutoPostBack="True" OnSelectedIndexChanged="Selection_Change_Status" Width=90 runat="server" 
																CssClass="col_med" SelectedValue='<%# Eval("status") %>' TabIndex='<%# TabIndex %>'>
																<asp:ListItem Value="Started"> Started </asp:ListItem>
																<asp:ListItem Value="Scheduled"> Scheduled </asp:ListItem>
																<asp:ListItem Value="Named"> Named </asp:ListItem>
																<asp:ListItem Value="Future"> Future </asp:ListItem>
															</asp:DropDownList>
														</ItemTemplate>
														<ControlStyle Width="100%" />								
													</asp:TemplateField>
													<asp:TemplateField SortExpression="notes" HeaderText="Notes">
														<HeaderStyle HorizontalAlign="Left" CssClass="col_large" />
														<ItemTemplate>
															<asp:TextBox ID="notes" AutoPostBack="True" CssClass="col_large" runat="server" Text='<%# Eval("notes") %>' Width="400" OnTextChanged="text_change" />
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField Visible="true" >
														<ItemTemplate>
															<asp:LinkButton 
																ID="addToInStarted" Tooltip="In and Started" Class="fa fa-check-square-o text-info fa-lg" runat="server" OnClick="Click_AddToInAndStarted">
															</asp:LinkButton>
														</ItemTemplate>						
													</asp:TemplateField>
													<asp:TemplateField>
														<ItemTemplate>
															<asp:LinkButton 
																ID="lnkBtnDelete" Class="fa fa-times" style="color:red" runat="server" OnClick="DeleteRow">
															</asp:LinkButton>
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
						
		<asp:UpdatePanel ID="UpdatePanelFuture" runat="server" UpdateMode="Conditional">
		    <ContentTemplate>
				<div class="row">
					<div class="col-md-12">
						<div class="card">
							<div class="card-header bg-light-gradient">
								<a name="future"><font color="black">						
								<h5 class="card-title">
								Future
								<asp:Label id="LblFuture" runat="server" /> 						
								</h5>
								</font>
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
											ID="GridViewFuture" 
											runat="server" 
											OnSorting="TaskGridView_Sorting" 
											AllowSorting="True" 
											AutoGenerateColumns="False" 
											BorderWidth="1px" 
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
															<asp:HyperLink runat="server" CssClass="col_med" Text='<%# Eval("Addo_ID") %>' Target="_blank" NavigateUrl='<%# "http://webapp2a.scientology.net/portal/cars/cosCarsPersonReport.jsp?hideLinks=true&addoId="+ Eval("Addo_ID") +"&orgId=" + System.Configuration.ConfigurationManager.AppSettings["orgid"] + "&browser=&userId=allhandsharlem&password=harlembas1"%>' />
														</ItemTemplate>
													</asp:TemplateField> 
													<asp:TemplateField SortExpression="rank" Visible="true" HeaderText="Type" >
														<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
														<ItemTemplate>
														<asp:DropDownList id="ddlRank" AutoPostBack="True" OnSelectedIndexChanged="Rank_Change" runat="server" CssClass="col_med" 
														SelectedValue='<%# Eval("rank") %>' TabIndex='<%# TabIndex %>'>
														<asp:ListItem Value="PS"> Paid Start </asp:ListItem>
														<asp:ListItem Value="PRPS"> PRPS </asp:ListItem>
														</asp:DropDownList>
														</ItemTemplate>
													</asp:TemplateField>													
													<asp:TemplateField SortExpression="name" HeaderText="Name">
														<HeaderStyle HorizontalAlign="Left" CssClass="col_name" />
														<ItemTemplate>
															<asp:TextBox ID="name" AutoPostBack="True" CssClass="col_name" runat="server" Text='<%# Eval("name") %>' Width=180 OnTextChanged="text_change" />
														</ItemTemplate>					
													</asp:TemplateField>
													<asp:TemplateField SortExpression="area" Visible="true" HeaderText="Area" >
														<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
														<ItemTemplate>
														<asp:DropDownList id="ddlArea" AutoPostBack="True" OnSelectedIndexChanged="Area_Change" runat="server" CssClass="col_med" 
														SelectedValue='<%# Eval("area") %>' TabIndex='<%# TabIndex %>'>
														<asp:ListItem Value="DIV6"> DIV6 </asp:ListItem>
														<asp:ListItem Value="Purif"> PURIF </asp:ListItem>
														<asp:ListItem Value="SRD"> SRD </asp:ListItem>
														<asp:ListItem Value="HGC"> HGC </asp:ListItem>
														<asp:ListItem Value="ACAD"> ACAD </asp:ListItem>
														<asp:ListItem Value="GAK"> GAK </asp:ListItem>
														</asp:DropDownList>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField SortExpression="service" Visible="true" HeaderText="Service">
														<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
														<ItemTemplate>
															<asp:TextBox ID="service" CssClass="col_med" AutoPostBack="True" runat="server" Text='<%# Eval("service") %>' Width=125 OnTextChanged="text_change" 
																TabIndex='<%# TabIndex %>' />
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField SortExpression="reg" HeaderText="Terminal">
														<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
														<ItemTemplate>
														<asp:TextBox ID="reg" AutoPostBack="True" CssClass="col_med" runat="server" Text='<%# Eval("reg") %>' Width=100 OnTextChanged="text_change" />
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField Visible="true" SortExpression="scheduled" HeaderText="Scheduled">												
														<HeaderStyle HorizontalAlign="Left" />												
														<ItemTemplate>
															<asp:TextBox ID="scheduled" AutoPostBack="True" runat="server" class="date_future col_med" Text='<%# Eval("scheduled", "{0:dd-MMM-yyyy}") %>' Width=100 OnTextChanged="text_change_date" ></asp:TextBox>
														</ItemTemplate>
													</asp:TemplateField>																<asp:TemplateField SortExpression="line" HeaderText="Line" >
														<ItemTemplate>
															<asp:DropDownList id="ddlLine" AutoPostBack="True" OnSelectedIndexChanged="Selection_Change_Line" runat="server" CssClass="col_xs"
																SelectedValue='<%# Eval("line") %>' TabIndex='<%# TabIndex %>'>
																<asp:ListItem Value="Other"> - </asp:ListItem>
																<asp:ListItem Value="Resign"> Resign </asp:ListItem>
																<asp:ListItem Value="FSM"> FSM </asp:ListItem>
																<asp:ListItem Value="Prospecting"> Prospecting </asp:ListItem>
																<asp:ListItem Value="Arrival"> Arrival </asp:ListItem>
																<asp:ListItem Value="CF"> CF </asp:ListItem>
															</asp:DropDownList>
														</ItemTemplate>
														<ControlStyle Width="100%" />								
													</asp:TemplateField>												
													<asp:TemplateField SortExpression="status" HeaderText="Status" >
														<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
														<ItemTemplate>
															<asp:DropDownList id="ddlStatus" AutoPostBack="True" OnSelectedIndexChanged="Selection_Change_Status" Width=90 runat="server" 
																CssClass="col_med" SelectedValue='<%# Eval("status") %>' TabIndex='<%# TabIndex %>'>
																<asp:ListItem Value="Started"> Started </asp:ListItem>
																<asp:ListItem Value="Scheduled"> Scheduled </asp:ListItem>
																<asp:ListItem Value="Named"> Named </asp:ListItem>
																<asp:ListItem Value="Future"> Future </asp:ListItem>
															</asp:DropDownList>
														</ItemTemplate>
														<ControlStyle Width="100%" />								
													</asp:TemplateField>
													<asp:TemplateField SortExpression="notes" HeaderText="Notes">
														<HeaderStyle HorizontalAlign="Left" CssClass="col_large" />
														<ItemTemplate>
															<asp:TextBox ID="notes" AutoPostBack="True" CssClass="col_large" runat="server" Text='<%# Eval("notes") %>' Width="400" OnTextChanged="text_change" />
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField Visible="true" >
														<ItemTemplate>
															<asp:LinkButton 
																ID="addToInStarted" Tooltip="In and Started" Class="fa fa-check-square-o text-info fa-lg" runat="server" OnClick="Click_AddToInAndStarted">
															</asp:LinkButton>
														</ItemTemplate>						
													</asp:TemplateField>
													<asp:TemplateField>
														<ItemTemplate>
															<asp:LinkButton 
																ID="lnkBtnDelete" Class="fa fa-times" style="color:red" runat="server" OnClick="DeleteRow">
															</asp:LinkButton>
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
						
		<asp:UpdatePanel ID="UpdatePanelFullyPaid" runat="server" UpdateMode="Conditional">
		    <ContentTemplate>
				<div class="row">
				    <div class="col-md-12">
						<div class="card">
							<div class="card-header bg-warning-gradient">
								<h5 class="card-title">
								Fully Paids
								<asp:Label runat="server" id="BothBoardLbla" Text="" CssClass="CombGITable"/>		
								
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
											ID="GridView3" 
											runat="server" 
											borderwidth="0" 
											CssClass="mGridAppt"
											GridLines="None"
											Visible="true"
											AllowPaging="True" 
											OnPageIndexChanging="grdData_PageIndexChanging" 
											OnSorting="TaskGridViewFPPP_Sorting" 
											AlternatingRowStyle-CssClass="alt"
											AllowSorting="True" 
											PageSize="<%# Convert.ToInt32(ddlPage.Text) %>"  
											AutoGenerateColumns="False" 
											EmptyDataText="  (No records were found...)"
											Width="100%">
											<PagerStyle HorizontalAlign = "Center" CssClass = "pgr" />											
											<Columns>
												<asp:BoundField DataField="addo_ID">
													<HeaderStyle CssClass="no-display"></HeaderStyle>
													<ItemStyle CssClass="no-display"></ItemStyle>
												</asp:BoundField>  
												<asp:TemplateField HeaderText="Addo ID">
													<ItemTemplate>
														<asp:HyperLink runat="server" class="no-print" Text='<%# Eval("Addo_ID") %>' Target="_blank" NavigateUrl='<%# "http://webapp2a.scientology.net/portal/cars/cosCarsPersonReport.jsp?hideLinks=true&addoId="+ Eval("Addo_ID") +"&orgId=" + System.Configuration.ConfigurationManager.AppSettings["orgid"] + "&browser=&userId=allhandsharlem&password=harlembas1"%>' />
													</ItemTemplate>
												</asp:TemplateField> 
												<asp:BoundField DataField="name" HeaderText="Name" SortExpression="name" />
												<asp:BoundField DataField="item" HeaderText="Service" SortExpression="item" />
												<asp:BoundField DataField="amt_paid" HeaderText="Amount Paid" SortExpression="amt_paid" />
												<asp:BoundField DataField="phone" HeaderText="Phone" SortExpression="phone" />
												<asp:TemplateField>
													<ItemTemplate>
														<asp:LinkButton ID="addToInStarted1" Tooltip="Add to Future" Class="fa fa-play-circle-o text-secondary fa-lg" runat="server" OnClick="Click_AddToFuture"></asp:LinkButton>
													</ItemTemplate>						
												</asp:TemplateField>
												<asp:TemplateField>
													<ItemTemplate>
														<asp:LinkButton ID="addToInStarted11" Tooltip="Add to Named" Class="fa fa-play-circle-o text-success fa-lg" runat="server" OnClick="Click_AddToNamed"></asp:LinkButton>
													</ItemTemplate>						
												</asp:TemplateField>
												<asp:TemplateField>
													<ItemTemplate>
														<asp:LinkButton ID="addToInStarted12" Tooltip="Add to Scheduled" Class="fa fa-play-circle-o text-info fa-lg" runat="server" OnClick="Click_AddToScheduled"></asp:LinkButton>
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
							<div class="row">
								<div class="col-1"> 							
									<div id="Six" runat="server">
										<asp:DropDownList id="ddlPage" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" OnSelectedIndexChanged="Selection_Change_Page" >
											<asp:ListItem Selected="True" Value="10"> 10 </asp:ListItem>
											<asp:ListItem Value="20"> 20 </asp:ListItem>
											<asp:ListItem Value="50"> 50 </asp:ListItem>
											<asp:ListItem Value="100"> 100 </asp:ListItem>
											<asp:ListItem Value="250"> 250 </asp:ListItem>
											<asp:ListItem Value="500"> 500 </asp:ListItem>
										</asp:DropDownList>        
										<asp:Label runat="server" CssClass="rows" text="(Rows)" ID="RowLable" />
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

                <ajaxToolkit:ConfirmButtonExtender ID="cbe" runat="server" DisplayModalPopupID="ModalPopupExtender2" TargetControlID="hiddenButton2">
                </ajaxToolkit:ConfirmButtonExtender>

                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="PanelAlert" X="770" Y="100" TargetControlID="hiddenButton2" OkControlID = "btnYes"
                    CancelControlID="btnNo" BackgroundCssClass="modalBackground">
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
											<asp:Button ID="btnYes" class="btn btn-default" runat="server" Text="Yes" OnClick="btnConfirmed_Click" />
											<asp:Button ID="btnNo" class="btn btn-default" runat="server" Text="No" />
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
						
		
		<asp:UpdatePanel ID="UpdatePanelError" runat="server" UpdateMode="Always">
		    <ContentTemplate>
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
			</ContentTemplate>
		</asp:UpdatePanel>
				
				<asp:Label visible="false" id="HeadText" style="padding-left:15px" runat="server" />
			</div>
		</section>
	</div>
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
					<p>If you continue, all records will be <strong>moved</strong> to "Archive"!</p>
				</div>
				<div class="modal-footer">
					<button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
					<asp:Button ID="btnArchive" OnClientClick="<% %>" class="btn btn-warning" runat="server" Text="Archive Week" CommandArgument='<%# Eval("Id") %>' OnCommand="Archive_Click" />
					
				</div>
			</div>
		</div>
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
	<!-- REQUIRED SCRIPTS -->

	<script type="text/javascript" src="../js/jquery-1.12.4.js" charset="UTF-8"></script>
	<script type="text/javascript" src="../js/jquery-ui.js" charset="UTF-8"></script>
	<script type="text/javascript" src="../js/bootstrap.min.js"></script>
	<script src="../js/MaxLength.min.js" type="text/javascript" ></script>
		
	<script type="text/javascript">
		$('.form_date').datetimepicker({
			daysOfWeekDisabled: [0,1,2,3,5,6],
			daysOfWeekHighlighted: "4",
			format: 'dd-M-yyyy',
			autoclose: 1,
			todayHighlight: 1,
			startView: 2,
			weekStart: 0,
			minView: 2,
			orientation: "bottom"
		});

		$('.form_day').datetimepicker({
			format: 'dd-M-yyyy',
			autoclose: 1,
			todayHighlight: 1,
			startView: 2,
			startDate: '-2d',
			endDate: '+7d',
			weekStart: 5,
			minView: 2,
			orientation: "bottom"
		});

		$('.form_date_inv').datetimepicker({
			autoclose: 1,
			format: 'dd-M-yyyy',
			weekStart: 0,
			minView: 2,
			startView: 2,
			orientation: "bottom"
		});
		
		$('.appt').datetimepicker({
			//language:  'fr',
			weekStart: 1,
			todayBtn:  1,
			autoclose: 1,
			todayHighlight: 1,
			startView: 2,
			forceParse: 0,
			showMeridian: 1
		});
		$('.form_datetime').datetimepicker({
			//language:  'fr',
			weekStart: 1,
			todayBtn:  1,
			autoclose: 1,
			todayHighlight: 1,
			startView: 2,
			forceParse: 0,
			showMeridian: 1
		});
		
		$('.form_date2').datetimepicker({
			language:  'fr',
			weekStart: 1,
			todayBtn:  1,
			autoclose: 1,
			todayHighlight: 1,
			startView: 2,
			minView: 2,
			forceParse: 0
		});
		$('.form_time').datetimepicker({
			language:  'fr',
			weekStart: 1,
			todayBtn:  1,
			autoclose: 1,
			todayHighlight: 1,
			startView: 1,
			minView: 0,
			maxView: 1,
			forceParse: 0
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
	
	<script src="../js/bootstrap.bundle.min.js"></script>
	<script src="../js/adminlte.js"></script>
	</form>	
	<!-- ./form  -->
</div>
<!-- wrapper -->
</body>
</html>