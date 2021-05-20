<!DOCTYPE html>
<%@ Page Language="C#" Trace="False" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="log.aspx.cs" Inherits="_Default" Debug="true" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"  
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<%@ Register Assembly="Shield.Web.UI" Namespace="Shield.Web.UI" TagPrefix="shield" %>	
	
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
	<meta charset="utf-8" />
	<meta name="viewport" content="width=device-width, initial-scale=1" />
	<meta http-equiv="x-ua-compatible" content="ie=edge" />
	<meta http-equiv="refresh" content="300">

	<title>
		WDAH Log
	</title>
		<link rel="stylesheet" href="../css/all.min.css" type="text/css"  />
		<link rel="stylesheet" href="../css/font-awesome.min.css">
		<link rel="stylesheet" href="../css/adminlte.css">
		<link rel="stylesheet" href="../css/jquery-ui.css">

	<script src="../js/jquery-3.3.1.min.js"></script>
	<script src="../js/shieldui-all.min.js" type="text/javascript">//</script>
		
	<link rel="icon" href="../img/favicon.png" />
	<style type="text/css">a {text-decoration: none}</style>	
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
		<a class="btn btn-default btn-block disabled" id="cmb" runat="server" >Combined</a>
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
				  <p>WDAH Log</p>
				</a>
				<hr>
				<li class="nav-item">
					<a href="setup_pc.aspx" class="nav-link">
						<i class="fa fa-cog nav-icon text-info"></i>
						<p>PC Setup</p>
					</a>
				</li>
				<li class="nav-item">
					<a href="setup_auditor.aspx" class="nav-link">
						<i class="fa fa-cog nav-icon text-info"></i>
						<p>Auditor Setup</p>
					</a>
				</li>
				<li class="nav-item">
					<a href="intensive_log.aspx" class="nav-link">
						<i class="fa fa-cog nav-icon text-info"></i>
						<p>Intensive Setup</p>
					</a>
				</li>
				
			</li>	
			<hr>    
        </ul>
      </nav>
      <!-- /.sidebar-menu -->
    </div>
    <!-- /.sidebar -->

	</aside>

  	<form id="form1" runat="server">
	<!-- Content Wrapper. Contains page content -->
	<div class="content-wrapper">
		<!-- Content Header (Page header) -->
		<div class="content-header">
		  <div class="container-fluid">
			<div class="row mb-2">
			  <div class="col-sm-9">
				<h1 class="m-0 text-dark">
					<asp:Label id="HeaderText" runat="server" />
				</h1>
				<asp:Label id="OrgText" runat="server" Text="Combined" />
			  </div><!-- /.col -->
			  <div class="col-sm-3">
				<div class="row">
					<div class="col-sm-2">
						<p class=text-right> W/E: </p>
					</div>
					<div class="col-sm-7">
						<asp:TextBox ID="lblWeekending" runat="server" Text="" CssClass="form-control date_we" ></asp:TextBox>
					</div>
					<div class="col-sm-3 text-center">
						archive
					</div>
				</div>
			  </div><!-- /.col -->
			  <!-- <div class="col-sm-6"> -->
				<!-- <ol class="breadcrumb float-sm-right"> -->
				  <!-- <li class="breadcrumb-item"><a href="../home.aspx">Home</a></li> -->
				  <!-- <li class="breadcrumb-item active">WDAH Log</li> -->
				<!-- </ol> -->
			  <!-- </div> -->
			</div><!-- /.row -->
		  </div><!-- /.container-fluid -->
		</div>
		<!-- /.content-header -->
	
		<!-- Main content -->
		<section class="content">
			<div class="container-fluid">
				<!-- TOTALS -->
				<div class="row">
				  <div class="col-3">
					<div class="small-box bg-info-gradient">
					<!-- <div class="info-box"> -->
					  <div class="info-box-content">
						<span class="info-box-number">WDAH TOT</span>
						<span class="info-box-text">
						<h3>
							<asp:Label id="lblHeaderTot" runat="server" />
						</h3>
						</span>
						<span class="progress-description">
							<asp:Label id="lblHeaderTotDay" runat="server" />
						</span>
						<span class="progress-description">
							<asp:Label id="lblHeaderTotFdn" runat="server" />
						</span>
					  </div>
					  <!-- /.info-box-content -->
					</div>
					<!-- /.info-box -->
				  </div>
				  <div class="col-3">
					<div class="small-box bg-info-gradient">
					<!-- <div class="info-box"> -->
					  <div class="info-box-content">
						<span class="info-box-number">WDAH W/O</span>
						<span class="info-box-text">
						<h3>
							<asp:Label id="lblHeaderWdahwo" runat="server" />
						</h3>
						</span>
						<span class="progress-description">
							<asp:Label id="lblHeaderWdahwoDay" runat="server" />
						</span>
						<span class="progress-description">
							<asp:Label id="lblHeaderWdahwoFdn" runat="server" />
						</span>
					  </div>
					  <!-- /.info-box-content -->
					</div>
					<!-- /.info-box -->
				  </div>				  
				  <!-- /.col -->
				  <div class="col-2">
					<div class="small-box bg-default-gradient">
					  <div class="info-box-content">
						<span class="info-box-number">CHAIR</span>
						<span class="info-box-number">
							<h3><asp:Label id="lblHeaderChair" runat="server" /></h3>
						</span>
						<span class="progress-description">
							<asp:Label id="CardConfirmedDay" runat="server" /> 
						</span>
						<span class="progress-description">
							<asp:Label id="CardConfirmedFdn" runat="server" />
						</span>
					  </div>
					  <!-- /.info-box-content -->
					</div>
					<!-- /.info-box -->
				  </div>
				  <!-- /.col -->
				  <div class="col-2">
					<div class="small-box bg-default-gradient">
					  <div class="info-box-content">
						<span class="info-box-number">ADMIN</span>
						<span class="info-box-number">
							<h3>
							<asp:Label id="lblHeaderAdmin" runat="server" />
							</h3>
						</span>
						<span class="progress-description">
							<asp:Label id="CardInConfirmedDay" runat="server" /> 
						</span>
						<span class="progress-description">
							<asp:Label id="CardInConfirmedFdn" runat="server" /> 
						</span>
					  </div>
					  <!-- /.info-box-content -->
					</div>
					<!-- /.info-box -->
				  </div>
				  <!-- /.col -->
				  <div class="col-2">
					<div class="small-box bg-info-gradient">
					  <div class="info-box-content">
						<span class="info-box-number">PURIF</span>
						<span class="info-box-text">
							<h3><asp:Label id="lblHeaderPurif" runat="server" /></h3>
						</span>
						<span class="progress-description">
							<asp:Label id="CardInConfirmedDayA" runat="server" /> 
						</span>
						<span class="progress-description">
							<asp:Label id="CardInConfirmedFdnA" runat="server" /> 
						</span>
					  </div>
					  <!-- /.info-box-content -->
					</div>
					<!-- /.info-box -->
				  </div>
				  <!-- /.col -->
				</div>
				<!-- /.row -->	
				<!-- SEARCH BOX -->
				<div class="row">
					<div class="col-md-6 col-12">
						<div class="card">		
							<div class="card-body">
								<div class="row">
									<div class="col-6">
										<asp:DropDownList id="ddl_pc" name="ddl_pc" runat="server" AutoPostBack="True" OnSelectedIndexChanged="OnSelectedIndexChanged" CssClass="form-control"></asp:DropDownList>												
									</div>
									<div class="col-6">
										<button runat="server" id="btnLogSession" class="btn btn-block btn-outline-info btn-md" onserverclick="OpenLog" title="Log Session">
											Log Session
										</button>							
									</div>	
									
								</div>
							</div>
						</div>
					<!-- /.col -->		
					</div>
				<!-- /.col -->		
				</div>	
				<!-- GRID -->
				<div class="row">
				  <div class="col-md-6">
					<div class="card">
					  <div class="card-header">
						<h5 class="card-title">
						Auditor Weekly Totals
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
								<shield:ShieldChart ID="ShieldChartAuditor" Width="100%" Height="250px" runat="server"
									OnTakeDataSource="ShieldChartAuditor_TakeDataSource"
									CssClass="chart">
									<PrimaryHeader Text="HGC Hours"></PrimaryHeader>
									<ExportOptions AllowExportToImage="false" AllowPrint="false" />
									<Axes>
										<shield:ChartAxisX 
											CategoricalValuesField="name">
										</shield:ChartAxisX> 
										<shield:ChartAxisY>
											<Title Text="WDAH"></Title>
										</shield:ChartAxisY>
									</Axes>
									<DataSeries>
										<shield:ChartBarSeries DataFieldY="totMinutes" CollectionAlias="Hours">
										</shield:ChartBarSeries>
									</DataSeries>
								</shield:ShieldChart>
								</div>	
							</div>
						</div>
					  </div>
					</div>				
				  <div class="col-md-6">
					<div class="card">
					  <div class="card-header">
						<h5 class="card-title">
						Preclear Summary
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
											ID="GridViewSummary" 
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
												<asp:BoundField DataField="pcId" HeaderText="ID" >
													<HeaderStyle CssClass="no-display"></HeaderStyle>
													<ItemStyle CssClass="no-display"></ItemStyle>
												</asp:BoundField>
												<asp:BoundField DataField="org" visible="False" HeaderText="Org" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_med" Width="150" />
												</asp:BoundField>
												<asp:BoundField DataField="pc" visible="True" SortExpression="pc" HeaderText="Preclear" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
													<ControlStyle width="100%" />																
												</asp:BoundField>
												<asp:BoundField DataField="vsd" visible="True" SortExpression="vsd " HeaderText="VSD Value" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
													<ControlStyle width="100%" />																
												</asp:BoundField>
												<asp:BoundField DataField="intMinUsed" visible="True" SortExpression="intMinUsed" HeaderText="Int Time Used" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_xs_center" />
													<ControlStyle width="100%" />																
												</asp:BoundField>
												<asp:BoundField DataField="hrsToVSD" visible="True" SortExpression="hrsToVSD" HeaderText="Hours to VSD" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_xs_center" />
													<ControlStyle width="100%" />																
												</asp:BoundField>
												<asp:BoundField DataField="inco" visible="True" SortExpression="inco" HeaderText="This Week" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_xs_center" />
													<ControlStyle width="100%" />																
												</asp:BoundField>												
												<asp:BoundField DataField="hrsToInco" visible="True" SortExpression="hrsToInco" HeaderText="Hrs to INCO" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_xs_center" />
													<ControlStyle width="100%" />																
												</asp:BoundField>												
											</Columns>									
										</asp:GridView>	
									</div>
								</div>	
							</div>
						</div>
					  </div>
					</div>

				</div>					

				
				<div class="row">
				  <div class="col-md-12">
					<div class="card">
					  <div class="card-header">
						<h5 class="card-title">
						Thursday After 2pm
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
								<div class="col-lg-6 ">  
									<div class="table-responsive">
										<asp:GridView 
											ID="GridViewSessionThu" 
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
												<asp:BoundField DataField="sessionId" HeaderText="ID" >
													<HeaderStyle CssClass="no-display"></HeaderStyle>
													<ItemStyle CssClass="no-display"></ItemStyle>
												</asp:BoundField>
												<asp:BoundField DataField="org" visible="False" HeaderText="Org" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_med" Width="150" />
												</asp:BoundField>
												<asp:BoundField DataField="sessionDate" visible="True" HeaderText="Date" DataFormatString="{0:d}" SortExpression="sessionDate">
													<HeaderStyle HorizontalAlign="Left" CssClass="col_xs_center" />
												</asp:BoundField>
												<asp:BoundField DataField="pc" visible="True" SortExpression="pc" HeaderText="Preclear" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
													<ControlStyle width="100%" />																
												</asp:BoundField>
												<asp:BoundField DataField="auditor" visible="True" SortExpression="auditor" HeaderText="Auditor" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
													<ControlStyle width="100%" />																
												</asp:BoundField>
												<asp:BoundField DataField="sessionTime" visible="True" SortExpression="sessionTime" HeaderText="Session" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_xs_center" />
													<ControlStyle width="100%" />																
												</asp:BoundField>
												<asp:BoundField DataField="adminTime" visible="True" SortExpression="adminTime" HeaderText="Admin" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_xs_center" />
													<ControlStyle width="100%" />																
												</asp:BoundField>
												<asp:BoundField DataField="type" visible="True" SortExpression="type" HeaderText="Type" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_xs_center" />
													<ControlStyle width="100%" />																
												</asp:BoundField>
												<asp:TemplateField>
													<ItemTemplate>
														<asp:LinkButton ID="lnkBtnEdit" class="btn btn-outline-info btn-sm col-12" runat="server" Text="Edit" 
														OnClick="Display"></asp:LinkButton>
													</ItemTemplate>						
												</asp:TemplateField>
												<asp:TemplateField>
													<ItemTemplate>
														<asp:LinkButton ID="lnkBtnDeleteEvent" class="btn btn-outline-danger btn-sm col-12" runat="server" Text="Delete" 
														OnClick="DeleteRow"></asp:LinkButton>
													</ItemTemplate>						
												</asp:TemplateField>
											</Columns>									
										</asp:GridView>	
									</div>
								</div>	
								<div class="col-md-6">
									<div class="row">
										<div class="col-lg-4 ">  
											<div class="row">
												</BR>
											</div>
											<b>
											<div class="row">
												<div class="col-lg-1"><h4></h4></div>
												<div class="col-lg-6">TOTAL:</div>
												<div class="col-lg-4"><asp:Label class="text-right" id="lblTot" runat="server" /></div>
											</div>
											<div class="row">
												<div class="col-lg-1"><h4></h4></div>
												<div class="col-lg-6">CHAIR:</div>
												<div class="col-lg-4"><asp:Label class="text-right" id="lblChair" runat="server" /></div>
											</div>
											<div class="row">
												<div class="col-lg-1"><h4></h4></div>
												<div class="col-lg-6">ADMIN:</div>
												<div class="col-lg-4"><asp:Label class="text-right" id="lblAdmin" runat="server" /></div>
											</div>
											<div class="row">
												<div class="col-lg-1"><h4></h4></div>
												<div class="col-lg-6">PURIF:</div>
												<div class="col-lg-4"><asp:Label class="text-right" id="lblPurif" runat="server" /></div>
											</div>
											</b> 
										</div>
										<div class="col-lg-8 ">  
											<shield:ShieldChart ID="ShieldChartThu" Width="100%" Height="250px" runat="server"
												OnTakeDataSource="ShieldChartThu_TakeDataSource"
												CssClass="chart">
												<PrimaryHeader Text="HGC Hours"></PrimaryHeader>
												<ExportOptions AllowExportToImage="false" AllowPrint="false" />
												<Axes>
													<shield:ChartAxisX 
														CategoricalValuesField="name">
													</shield:ChartAxisX> 
													<shield:ChartAxisY>
														<Title Text="WDAH"></Title>
													</shield:ChartAxisY>
												</Axes>
												<DataSeries>
													<shield:ChartBarSeries DataFieldY="totMinutes" CollectionAlias="Hours">
													</shield:ChartBarSeries>
												</DataSeries>
											</shield:ShieldChart>
										</div>	
									</div>
								</div>									
							</div>
						</div>
					  </div>
					</div>
				</div>
				<div class="row">
				  <div class="col-md-12">
					<div class="card">
					  <div class="card-header">
						<h5 class="card-title">
						Friday
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
								<div class="col-lg-6 ">  
									<div class="table-responsive">
										<asp:GridView 
											ID="GridViewSessionFri" 
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
												<asp:BoundField DataField="sessionId" HeaderText="ID" >
													<HeaderStyle CssClass="no-display"></HeaderStyle>
													<ItemStyle CssClass="no-display"></ItemStyle>
												</asp:BoundField>
												<asp:BoundField DataField="org" visible="False" HeaderText="Org" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_med" Width="150" />
												</asp:BoundField>
												<asp:BoundField DataField="sessionDate" visible="True" HeaderText="Date" DataFormatString="{0:d}" SortExpression="sessionDate">
													<HeaderStyle HorizontalAlign="Left" CssClass="col_xs_center" />
												</asp:BoundField>
												<asp:BoundField DataField="pc" visible="True" SortExpression="pc" HeaderText="Preclear" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
													<ControlStyle width="100%" />																
												</asp:BoundField>
												<asp:BoundField DataField="auditor" visible="True" SortExpression="auditor" HeaderText="Auditor" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
													<ControlStyle width="100%" />																
												</asp:BoundField>
												<asp:BoundField DataField="sessionTime" visible="True" SortExpression="sessionTime" HeaderText="Session" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_xs_center" />
													<ControlStyle width="100%" />																
												</asp:BoundField>
												<asp:BoundField DataField="adminTime" visible="True" SortExpression="adminTime" HeaderText="Admin" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_xs_center" />
													<ControlStyle width="100%" />																
												</asp:BoundField>
												<asp:BoundField DataField="type" visible="True" SortExpression="type" HeaderText="Type" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_xs_center" />
													<ControlStyle width="100%" />																
												</asp:BoundField>
												<asp:TemplateField>
													<ItemTemplate>
														<asp:LinkButton ID="lnkBtnEdit" class="btn btn-outline-info btn-sm col-12" runat="server" Text="Edit" 
														OnClick="Display"></asp:LinkButton>
													</ItemTemplate>						
												</asp:TemplateField>
												<asp:TemplateField>
													<ItemTemplate>
														<asp:LinkButton ID="lnkBtnDeleteEvent" class="btn btn-outline-danger btn-sm col-12" runat="server" Text="Delete" 
														OnClick="DeleteRow"></asp:LinkButton>
													</ItemTemplate>						
												</asp:TemplateField>
											</Columns>									
										</asp:GridView>	
									</div>
								</div>	
								<div class="col-md-6">
									<div class="row">
										<div class="col-lg-4 ">  
											<div class="row">
												</BR>
											</div>
											<b>
											<div class="row">
												<div class="col-lg-1"><h4></h4></div>
												<div class="col-lg-6">TOTAL:</div>
												<div class="col-lg-4"><asp:Label class="text-right" id="lblTotFri" runat="server" /></div>
											</div>
											<div class="row">
												<div class="col-lg-1"><h4></h4></div>
												<div class="col-lg-6">CHAIR:</div>
												<div class="col-lg-4"><asp:Label class="text-right" id="lblChairFri" runat="server" /></div>
											</div>
											<div class="row">
												<div class="col-lg-1"><h4></h4></div>
												<div class="col-lg-6">ADMIN:</div>
												<div class="col-lg-4"><asp:Label class="text-right" id="lblAdminFri" runat="server" /></div>
											</div>
											<div class="row">
												<div class="col-lg-1"><h4></h4></div>
												<div class="col-lg-6">PURIF:</div>
												<div class="col-lg-4"><asp:Label class="text-right" id="lblPurifFri" runat="server" /></div>
											</div>
											</b> 
										</div>
										<div class="col-lg-8 ">  
											<shield:ShieldChart ID="ShieldChartFri" Width="100%" Height="250px" runat="server"
												OnTakeDataSource="ShieldChartFri_TakeDataSource"
												CssClass="chart">
												<PrimaryHeader Text="HGC Hours"></PrimaryHeader>
												<ExportOptions AllowExportToImage="false" AllowPrint="false" />
												<Axes>
													<shield:ChartAxisX 
														CategoricalValuesField="name">
													</shield:ChartAxisX> 
													<shield:ChartAxisY>
														<Title Text="WDAH"></Title>
													</shield:ChartAxisY>
												</Axes>
												<DataSeries>
													<shield:ChartBarSeries DataFieldY="totMinutes" CollectionAlias="Hours">
													</shield:ChartBarSeries>
												</DataSeries>
											</shield:ShieldChart>
										</div>	
									</div>
								</div>									
							</div>
						</div>
					  </div>
					</div>
				</div>				
				<div class="row">
				  <div class="col-md-12">
					<div class="card">
					  <div class="card-header">
						<h5 class="card-title">
						Saturday
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
								<div class="col-lg-6 ">  
									<div class="table-responsive">
										<asp:GridView 
											ID="GridViewSessionSat" 
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
												<asp:BoundField DataField="sessionId" HeaderText="ID" >
													<HeaderStyle CssClass="no-display"></HeaderStyle>
													<ItemStyle CssClass="no-display"></ItemStyle>
												</asp:BoundField>
												<asp:BoundField DataField="org" visible="False" HeaderText="Org" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_med" Width="150" />
												</asp:BoundField>
												<asp:BoundField DataField="sessionDate" visible="True" HeaderText="Date" DataFormatString="{0:d}" SortExpression="sessionDate">
													<HeaderStyle HorizontalAlign="Left" CssClass="col_xs_center" />
												</asp:BoundField>
												<asp:BoundField DataField="pc" visible="True" SortExpression="pc" HeaderText="Preclear" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
													<ControlStyle width="100%" />																
												</asp:BoundField>
												<asp:BoundField DataField="auditor" visible="True" SortExpression="auditor" HeaderText="Auditor" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
													<ControlStyle width="100%" />																
												</asp:BoundField>
												<asp:BoundField DataField="sessionTime" visible="True" SortExpression="sessionTime" HeaderText="Session" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_xs_center" />
													<ControlStyle width="100%" />																
												</asp:BoundField>
												<asp:BoundField DataField="adminTime" visible="True" SortExpression="adminTime" HeaderText="Admin" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_xs_center" />
													<ControlStyle width="100%" />																
												</asp:BoundField>
												<asp:BoundField DataField="type" visible="True" SortExpression="type" HeaderText="Type" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_xs_center" />
													<ControlStyle width="100%" />																
												</asp:BoundField>
												<asp:TemplateField>
													<ItemTemplate>
														<asp:LinkButton ID="lnkBtnEdit" class="btn btn-outline-info btn-sm col-12" runat="server" Text="Edit" 
														OnClick="Display"></asp:LinkButton>
													</ItemTemplate>						
												</asp:TemplateField>
												<asp:TemplateField>
													<ItemTemplate>
														<asp:LinkButton ID="lnkBtnDeleteEvent" class="btn btn-outline-danger btn-sm col-12" runat="server" Text="Delete" 
														OnClick="DeleteRow"></asp:LinkButton>
													</ItemTemplate>						
												</asp:TemplateField>
											</Columns>									
										</asp:GridView>	
									</div>
								</div>	
								<div class="col-md-6">
									<div class="row">
										<div class="col-lg-4 ">  
											<div class="row">
												</BR>
											</div>
											<b>
											<div class="row">
												<div class="col-lg-1"><h4></h4></div>
												<div class="col-lg-6">TOTAL:</div>
												<div class="col-lg-4"><asp:Label class="text-right" id="lblTotSat" runat="server" /></div>
											</div>
											<div class="row">
												<div class="col-lg-1"><h4></h4></div>
												<div class="col-lg-6">CHAIR:</div>
												<div class="col-lg-4"><asp:Label class="text-right" id="lblChairSat" runat="server" /></div>
											</div>
											<div class="row">
												<div class="col-lg-1"><h4></h4></div>
												<div class="col-lg-6">ADMIN:</div>
												<div class="col-lg-4"><asp:Label class="text-right" id="lblAdminSat" runat="server" /></div>
											</div>
											<div class="row">
												<div class="col-lg-1"><h4></h4></div>
												<div class="col-lg-6">PURIF:</div>
												<div class="col-lg-4"><asp:Label class="text-right" id="lblPurifSat" runat="server" /></div>
											</div>
											</b> 
										</div>
										<div class="col-lg-8 ">  
											<shield:ShieldChart ID="ShieldChartSat" Width="100%" Height="250px" runat="server"
												OnTakeDataSource="ShieldChartSat_TakeDataSource"
												CssClass="chart">
												<PrimaryHeader Text="HGC Hours"></PrimaryHeader>
												<ExportOptions AllowExportToImage="false" AllowPrint="false" />
												<Axes>
													<shield:ChartAxisX 
														CategoricalValuesField="name">
													</shield:ChartAxisX> 
													<shield:ChartAxisY>
														<Title Text="WDAH"></Title>
													</shield:ChartAxisY>
												</Axes>
												<DataSeries>
													<shield:ChartBarSeries DataFieldY="totMinutes" CollectionAlias="Hours">
													</shield:ChartBarSeries>
												</DataSeries>
											</shield:ShieldChart>
										</div>	
									</div>
								</div>									
							</div>
						</div>
					  </div>
					</div>
				</div>
				<div class="row">
				  <div class="col-md-12">
					<div class="card">
					  <div class="card-header">
						<h5 class="card-title">
						Sunday
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
								<div class="col-lg-6 ">  
									<div class="table-responsive">
										<asp:GridView 
											ID="GridViewSessionSun" 
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
												<asp:BoundField DataField="sessionId" HeaderText="ID" >
													<HeaderStyle CssClass="no-display"></HeaderStyle>
													<ItemStyle CssClass="no-display"></ItemStyle>
												</asp:BoundField>
												<asp:BoundField DataField="org" visible="False" HeaderText="Org" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_med" Width="150" />
												</asp:BoundField>
												<asp:BoundField DataField="sessionDate" visible="True" HeaderText="Date" DataFormatString="{0:d}" SortExpression="sessionDate">
													<HeaderStyle HorizontalAlign="Left" CssClass="col_xs_center" />
												</asp:BoundField>
												<asp:BoundField DataField="pc" visible="True" SortExpression="pc" HeaderText="Preclear" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
													<ControlStyle width="100%" />																
												</asp:BoundField>
												<asp:BoundField DataField="auditor" visible="True" SortExpression="auditor" HeaderText="Auditor" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
													<ControlStyle width="100%" />																
												</asp:BoundField>
												<asp:BoundField DataField="sessionTime" visible="True" SortExpression="sessionTime" HeaderText="Session" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_xs_center" />
													<ControlStyle width="100%" />																
												</asp:BoundField>
												<asp:BoundField DataField="adminTime" visible="True" SortExpression="adminTime" HeaderText="Admin" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_xs_center" />
													<ControlStyle width="100%" />																
												</asp:BoundField>
												<asp:BoundField DataField="type" visible="True" SortExpression="type" HeaderText="Type" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_xs_center" />
													<ControlStyle width="100%" />																
												</asp:BoundField>
												<asp:TemplateField>
													<ItemTemplate>
														<asp:LinkButton ID="lnkBtnEdit" class="btn btn-outline-info btn-sm col-12" runat="server" Text="Edit" 
														OnClick="Display"></asp:LinkButton>
													</ItemTemplate>						
												</asp:TemplateField>
												<asp:TemplateField>
													<ItemTemplate>
														<asp:LinkButton ID="lnkBtnDeleteEvent" class="btn btn-outline-danger btn-sm col-12" runat="server" Text="Delete" 
														OnClick="DeleteRow"></asp:LinkButton>
													</ItemTemplate>						
												</asp:TemplateField>
											</Columns>									
										</asp:GridView>	
									</div>
								</div>	
								<div class="col-md-6">
									<div class="row">
										<div class="col-lg-4 ">  
											<div class="row">
												</BR>
											</div>
											<b>
											<div class="row">
												<div class="col-lg-1"><h4></h4></div>
												<div class="col-lg-6">TOTAL:</div>
												<div class="col-lg-4"><asp:Label class="text-right" id="lblTotSun" runat="server" /></div>
											</div>
											<div class="row">
												<div class="col-lg-1"><h4></h4></div>
												<div class="col-lg-6">CHAIR:</div>
												<div class="col-lg-4"><asp:Label class="text-right" id="lblChairSun" runat="server" /></div>
											</div>
											<div class="row">
												<div class="col-lg-1"><h4></h4></div>
												<div class="col-lg-6">ADMIN:</div>
												<div class="col-lg-4"><asp:Label class="text-right" id="lblAdminSun" runat="server" /></div>
											</div>
											<div class="row">
												<div class="col-lg-1"><h4></h4></div>
												<div class="col-lg-6">PURIF:</div>
												<div class="col-lg-4"><asp:Label class="text-right" id="lblPurifSun" runat="server" /></div>
											</div>
											</b> 
										</div>
										<div class="col-lg-8 ">  
											<shield:ShieldChart ID="ShieldChartSun" Width="100%" Height="250px" runat="server"
												OnTakeDataSource="ShieldChartSun_TakeDataSource"
												CssClass="chart">
												<PrimaryHeader Text="HGC Hours"></PrimaryHeader>
												<ExportOptions AllowExportToImage="false" AllowPrint="false" />
												<Axes>
													<shield:ChartAxisX 
														CategoricalValuesField="name">
													</shield:ChartAxisX> 
													<shield:ChartAxisY>
														<Title Text="WDAH"></Title>
													</shield:ChartAxisY>
												</Axes>
												<DataSeries>
													<shield:ChartBarSeries DataFieldY="totMinutes" CollectionAlias="Hours">
													</shield:ChartBarSeries>
												</DataSeries>
											</shield:ShieldChart>
										</div>	
									</div>
								</div>									
							</div>
						</div>
					  </div>
					</div>
				</div>
				<div class="row">
				  <div class="col-md-12">
					<div class="card">
					  <div class="card-header">
						<h5 class="card-title">
						Monday
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
								<div class="col-lg-6 ">  
									<div class="table-responsive">
										<asp:GridView 
											ID="GridViewSessionMon" 
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
												<asp:BoundField DataField="sessionId" HeaderText="ID" >
													<HeaderStyle CssClass="no-display"></HeaderStyle>
													<ItemStyle CssClass="no-display"></ItemStyle>
												</asp:BoundField>
												<asp:BoundField DataField="org" visible="False" HeaderText="Org" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_med" Width="150" />
												</asp:BoundField>
												<asp:BoundField DataField="sessionDate" visible="True" HeaderText="Date" DataFormatString="{0:d}" SortExpression="sessionDate">
													<HeaderStyle HorizontalAlign="Left" CssClass="col_xs_center" />
												</asp:BoundField>
												<asp:BoundField DataField="pc" visible="True" SortExpression="pc" HeaderText="Preclear" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
													<ControlStyle width="100%" />																
												</asp:BoundField>
												<asp:BoundField DataField="auditor" visible="True" SortExpression="auditor" HeaderText="Auditor" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
													<ControlStyle width="100%" />																
												</asp:BoundField>
												<asp:BoundField DataField="sessionTime" visible="True" SortExpression="sessionTime" HeaderText="Session" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_xs_center" />
													<ControlStyle width="100%" />																
												</asp:BoundField>
												<asp:BoundField DataField="adminTime" visible="True" SortExpression="adminTime" HeaderText="Admin" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_xs_center" />
													<ControlStyle width="100%" />																
												</asp:BoundField>
												<asp:BoundField DataField="type" visible="True" SortExpression="type" HeaderText="Type" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_xs_center" />
													<ControlStyle width="100%" />																
												</asp:BoundField>
												<asp:TemplateField>
													<ItemTemplate>
														<asp:LinkButton ID="lnkBtnEdit" class="btn btn-outline-info btn-sm col-12" runat="server" Text="Edit" 
														OnClick="Display"></asp:LinkButton>
													</ItemTemplate>						
												</asp:TemplateField>
												<asp:TemplateField>
													<ItemTemplate>
														<asp:LinkButton ID="lnkBtnDeleteEvent" class="btn btn-outline-danger btn-sm col-12" runat="server" Text="Delete" 
														OnClick="DeleteRow"></asp:LinkButton>
													</ItemTemplate>						
												</asp:TemplateField>
											</Columns>									
										</asp:GridView>	
									</div>
								</div>	
								<div class="col-md-6">
									<div class="row">
										<div class="col-lg-4 ">  
											<div class="row">
												</BR>
											</div>
											<b>
											<div class="row">
												<div class="col-lg-1"><h4></h4></div>
												<div class="col-lg-6">TOTAL:</div>
												<div class="col-lg-4"><asp:Label class="text-right" id="lblTotMon" runat="server" /></div>
											</div>
											<div class="row">
												<div class="col-lg-1"><h4></h4></div>
												<div class="col-lg-6">CHAIR:</div>
												<div class="col-lg-4"><asp:Label class="text-right" id="lblChairMon" runat="server" /></div>
											</div>
											<div class="row">
												<div class="col-lg-1"><h4></h4></div>
												<div class="col-lg-6">ADMIN:</div>
												<div class="col-lg-4"><asp:Label class="text-right" id="lblAdminMon" runat="server" /></div>
											</div>
											<div class="row">
												<div class="col-lg-1"><h4></h4></div>
												<div class="col-lg-6">PURIF:</div>
												<div class="col-lg-4"><asp:Label class="text-right" id="lblPurifMon" runat="server" /></div>
											</div>
											</b> 
										</div>
										<div class="col-lg-8 ">  
											<shield:ShieldChart ID="ShieldChartMon" Width="100%" Height="250px" runat="server"
												OnTakeDataSource="ShieldChartMon_TakeDataSource"
												CssClass="chart">
												<PrimaryHeader Text="HGC Hours"></PrimaryHeader>
												<ExportOptions AllowExportToImage="false" AllowPrint="false" />
												<Axes>
													<shield:ChartAxisX 
														CategoricalValuesField="name">
													</shield:ChartAxisX> 
													<shield:ChartAxisY>
														<Title Text="WDAH"></Title>
													</shield:ChartAxisY>
												</Axes>
												<DataSeries>
													<shield:ChartBarSeries DataFieldY="totMinutes" CollectionAlias="Hours">
													</shield:ChartBarSeries>
												</DataSeries>
											</shield:ShieldChart>
										</div>	
									</div>
								</div>									
							</div>
						</div>
					  </div>
					</div>
				</div>
				<div class="row">
				  <div class="col-md-12">
					<div class="card">
					  <div class="card-header">
						<h5 class="card-title">
						Tuesday
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
								<div class="col-lg-6 ">  
									<div class="table-responsive">
										<asp:GridView 
											ID="GridViewSessionTue" 
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
												<asp:BoundField DataField="sessionId" HeaderText="ID" >
													<HeaderStyle CssClass="no-display"></HeaderStyle>
													<ItemStyle CssClass="no-display"></ItemStyle>
												</asp:BoundField>
												<asp:BoundField DataField="org" visible="False" HeaderText="Org" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_med" Width="150" />
												</asp:BoundField>
												<asp:BoundField DataField="sessionDate" visible="True" HeaderText="Date" DataFormatString="{0:d}" SortExpression="sessionDate">
													<HeaderStyle HorizontalAlign="Left" CssClass="col_xs_center" />
												</asp:BoundField>
												<asp:BoundField DataField="pc" visible="True" SortExpression="pc" HeaderText="Preclear" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
													<ControlStyle width="100%" />																
												</asp:BoundField>
												<asp:BoundField DataField="auditor" visible="True" SortExpression="auditor" HeaderText="Auditor" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
													<ControlStyle width="100%" />																
												</asp:BoundField>
												<asp:BoundField DataField="sessionTime" visible="True" SortExpression="sessionTime" HeaderText="Session" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_xs_center" />
													<ControlStyle width="100%" />																
												</asp:BoundField>
												<asp:BoundField DataField="adminTime" visible="True" SortExpression="adminTime" HeaderText="Admin" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_xs_center" />
													<ControlStyle width="100%" />																
												</asp:BoundField>
												<asp:BoundField DataField="type" visible="True" SortExpression="type" HeaderText="Type" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_xs_center" />
													<ControlStyle width="100%" />																
												</asp:BoundField>
												<asp:TemplateField>
													<ItemTemplate>
														<asp:LinkButton ID="lnkBtnEdit" class="btn btn-outline-info btn-sm col-12" runat="server" Text="Edit" 
														OnClick="Display"></asp:LinkButton>
													</ItemTemplate>						
												</asp:TemplateField>
												<asp:TemplateField>
													<ItemTemplate>
														<asp:LinkButton ID="lnkBtnDeleteEvent" class="btn btn-outline-danger btn-sm col-12" runat="server" Text="Delete" 
														OnClick="DeleteRow"></asp:LinkButton>
													</ItemTemplate>						
												</asp:TemplateField>
											</Columns>									
										</asp:GridView>	
									</div>
								</div>	
								<div class="col-md-6">
									<div class="row">
										<div class="col-lg-4 ">  
											<div class="row">
												</BR>
											</div>
											<b>
											<div class="row">
												<div class="col-lg-1"><h4></h4></div>
												<div class="col-lg-6">TOTAL:</div>
												<div class="col-lg-4"><asp:Label class="text-right" id="lblTotTue" runat="server" /></div>
											</div>
											<div class="row">
												<div class="col-lg-1"><h4></h4></div>
												<div class="col-lg-6">CHAIR:</div>
												<div class="col-lg-4"><asp:Label class="text-right" id="lblChairTue" runat="server" /></div>
											</div>
											<div class="row">
												<div class="col-lg-1"><h4></h4></div>
												<div class="col-lg-6">ADMIN:</div>
												<div class="col-lg-4"><asp:Label class="text-right" id="lblAdminTue" runat="server" /></div>
											</div>
											<div class="row">
												<div class="col-lg-1"><h4></h4></div>
												<div class="col-lg-6">PURIF:</div>
												<div class="col-lg-4"><asp:Label class="text-right" id="lblPurifTue" runat="server" /></div>
											</div>
											</b> 
										</div>
										<div class="col-lg-8 ">  
											<shield:ShieldChart ID="ShieldChartTue" Width="100%" Height="250px" runat="server"
												OnTakeDataSource="ShieldChartTue_TakeDataSource"
												CssClass="chart">
												<PrimaryHeader Text="HGC Hours"></PrimaryHeader>
												<ExportOptions AllowExportToImage="false" AllowPrint="false" />
												<Axes>
													<shield:ChartAxisX 
														CategoricalValuesField="name">
													</shield:ChartAxisX> 
													<shield:ChartAxisY>
														<Title Text="WDAH"></Title>
													</shield:ChartAxisY>
												</Axes>
												<DataSeries>
													<shield:ChartBarSeries DataFieldY="totMinutes" CollectionAlias="Hours">
													</shield:ChartBarSeries>
												</DataSeries>
											</shield:ShieldChart>
										</div>	
									</div>
								</div>									
							</div>
						</div>
					  </div>
					</div>
				</div>
				<div class="row">
				  <div class="col-md-12">
					<div class="card">
					  <div class="card-header">
						<h5 class="card-title">
						Wednesday
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
								<div class="col-lg-6 ">  
									<div class="table-responsive">
										<asp:GridView 
											ID="GridViewSessionWed" 
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
												<asp:BoundField DataField="sessionId" HeaderText="ID" >
													<HeaderStyle CssClass="no-display"></HeaderStyle>
													<ItemStyle CssClass="no-display"></ItemStyle>
												</asp:BoundField>
												<asp:BoundField DataField="org" visible="False" HeaderText="Org" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_med" Width="150" />
												</asp:BoundField>
												<asp:BoundField DataField="sessionDate" visible="True" HeaderText="Date" DataFormatString="{0:d}" SortExpression="sessionDate">
													<HeaderStyle HorizontalAlign="Left" CssClass="col_xs_center" />
												</asp:BoundField>
												<asp:BoundField DataField="pc" visible="True" SortExpression="pc" HeaderText="Preclear" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
													<ControlStyle width="100%" />																
												</asp:BoundField>
												<asp:BoundField DataField="auditor" visible="True" SortExpression="auditor" HeaderText="Auditor" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
													<ControlStyle width="100%" />																
												</asp:BoundField>
												<asp:BoundField DataField="sessionTime" visible="True" SortExpression="sessionTime" HeaderText="Session" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_xs_center" />
													<ControlStyle width="100%" />																
												</asp:BoundField>
												<asp:BoundField DataField="adminTime" visible="True" SortExpression="adminTime" HeaderText="Admin" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_xs_center" />
													<ControlStyle width="100%" />																
												</asp:BoundField>
												<asp:BoundField DataField="type" visible="True" SortExpression="type" HeaderText="Type" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_xs_center" />
													<ControlStyle width="100%" />																
												</asp:BoundField>
												<asp:TemplateField>
													<ItemTemplate>
														<asp:LinkButton ID="lnkBtnEdit" class="btn btn-outline-info btn-sm col-12" runat="server" Text="Edit" 
														OnClick="Display"></asp:LinkButton>
													</ItemTemplate>						
												</asp:TemplateField>
												<asp:TemplateField>
													<ItemTemplate>
														<asp:LinkButton ID="lnkBtnDeleteEvent" class="btn btn-outline-danger btn-sm col-12" runat="server" Text="Delete" 
														OnClick="DeleteRow"></asp:LinkButton>
													</ItemTemplate>						
												</asp:TemplateField>
											</Columns>									
										</asp:GridView>	
									</div>
								</div>	
								<div class="col-md-6">
									<div class="row">
										<div class="col-lg-4 ">  
											<div class="row">
												</BR>
											</div>
											<b>
											<div class="row">
												<div class="col-lg-1"><h4></h4></div>
												<div class="col-lg-6">TOTAL:</div>
												<div class="col-lg-4"><asp:Label class="text-right" id="lblTotWed" runat="server" /></div>
											</div>
											<div class="row">
												<div class="col-lg-1"><h4></h4></div>
												<div class="col-lg-6">CHAIR:</div>
												<div class="col-lg-4"><asp:Label class="text-right" id="lblChairWed" runat="server" /></div>
											</div>
											<div class="row">
												<div class="col-lg-1"><h4></h4></div>
												<div class="col-lg-6">ADMIN:</div>
												<div class="col-lg-4"><asp:Label class="text-right" id="lblAdminWed" runat="server" /></div>
											</div>
											<div class="row">
												<div class="col-lg-1"><h4></h4></div>
												<div class="col-lg-6">PURIF:</div>
												<div class="col-lg-4"><asp:Label class="text-right" id="lblPurifWed" runat="server" /></div>
											</div>
											</b> 
										</div>
										<div class="col-lg-8 ">  
											<shield:ShieldChart ID="ShieldChartWed" Width="100%" Height="250px" runat="server"
												OnTakeDataSource="ShieldChartWed_TakeDataSource"
												CssClass="chart">
												<PrimaryHeader Text="HGC Hours"></PrimaryHeader>
												<ExportOptions AllowExportToImage="false" AllowPrint="false" />
												<Axes>
													<shield:ChartAxisX 
														CategoricalValuesField="name">
													</shield:ChartAxisX> 
													<shield:ChartAxisY>
														<Title Text="WDAH"></Title>
													</shield:ChartAxisY>
												</Axes>
												<DataSeries>
													<shield:ChartBarSeries DataFieldY="totMinutes" CollectionAlias="Hours">
													</shield:ChartBarSeries>
												</DataSeries>
											</shield:ShieldChart>
										</div>	
									</div>
								</div>									
							</div>
						</div>
					  </div>
					</div>
				</div>
				<div class="row">
				  <div class="col-md-12">
					<div class="card">
					  <div class="card-header">
						<h5 class="card-title">
						Thursday Before 2pm
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
								<div class="col-lg-6 ">  
									<div class="table-responsive">
										<asp:GridView 
											ID="GridViewSessionThu2" 
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
												<asp:BoundField DataField="sessionId" HeaderText="ID" >
													<HeaderStyle CssClass="no-display"></HeaderStyle>
													<ItemStyle CssClass="no-display"></ItemStyle>
												</asp:BoundField>
												<asp:BoundField DataField="org" visible="False" HeaderText="Org" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_med" Width="150" />
												</asp:BoundField>
												<asp:BoundField DataField="sessionDate" visible="True" HeaderText="Date" DataFormatString="{0:d}" SortExpression="sessionDate">
													<HeaderStyle HorizontalAlign="Left" CssClass="col_xs_center" />
												</asp:BoundField>
												<asp:BoundField DataField="pc" visible="True" SortExpression="pc" HeaderText="Preclear" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
													<ControlStyle width="100%" />																
												</asp:BoundField>
												<asp:BoundField DataField="auditor" visible="True" SortExpression="auditor" HeaderText="Auditor" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
													<ControlStyle width="100%" />																
												</asp:BoundField>
												<asp:BoundField DataField="sessionTime" visible="True" SortExpression="sessionTime" HeaderText="Session" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_xs_center" />
													<ControlStyle width="100%" />																
												</asp:BoundField>
												<asp:BoundField DataField="adminTime" visible="True" SortExpression="adminTime" HeaderText="Admin" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_xs_center" />
													<ControlStyle width="100%" />																
												</asp:BoundField>
												<asp:TemplateField>
													<ItemTemplate>
														<asp:LinkButton ID="lnkBtnEdit" class="btn btn-outline-info btn-sm col-12" runat="server" Text="Edit" 
														OnClick="Display"></asp:LinkButton>
													</ItemTemplate>						
												</asp:TemplateField>
												<asp:TemplateField>
													<ItemTemplate>
														<asp:LinkButton ID="lnkBtnDeleteEvent" class="btn btn-outline-danger btn-sm col-12" runat="server" Text="Delete" 
														OnClick="DeleteRow"></asp:LinkButton>
													</ItemTemplate>						
												</asp:TemplateField>
											</Columns>									
										</asp:GridView>	
									</div>
								</div>	
								<div class="col-md-6">
									<div class="row">
										<div class="col-lg-4 ">  
											<div class="row">
												</BR>
											</div>
											<b>
											<div class="row">
												<div class="col-lg-1"><h4></h4></div>
												<div class="col-lg-6">TOTAL:</div>
												<div class="col-lg-4"><asp:Label class="text-right" id="lblTotThu2" runat="server" /></div>
											</div>
											<div class="row">
												<div class="col-lg-1"><h4></h4></div>
												<div class="col-lg-6">CHAIR:</div>
												<div class="col-lg-4"><asp:Label class="text-right" id="lblChairThu2" runat="server" /></div>
											</div>
											<div class="row">
												<div class="col-lg-1"><h4></h4></div>
												<div class="col-lg-6">ADMIN:</div>
												<div class="col-lg-4"><asp:Label class="text-right" id="lblAdminThu2" runat="server" /></div>
											</div>
											<div class="row">
												<div class="col-lg-1"><h4></h4></div>
												<div class="col-lg-6">PURIF:</div>
												<div class="col-lg-4"><asp:Label class="text-right" id="lblPurifThu2" runat="server" /></div>
											</div>
											</b> 
										</div>
										<div class="col-lg-8 ">  
											<shield:ShieldChart ID="ShieldChartThu2" Width="100%" Height="250px" runat="server"
												OnTakeDataSource="ShieldChartThu2_TakeDataSource"
												CssClass="chart">
												<PrimaryHeader Text="HGC Hours"></PrimaryHeader>
												<ExportOptions AllowExportToImage="false" AllowPrint="false" />
												<Axes>
													<shield:ChartAxisX 
														CategoricalValuesField="name">
													</shield:ChartAxisX> 
													<shield:ChartAxisY>
														<Title Text="WDAH"></Title>
													</shield:ChartAxisY>
												</Axes>
												<DataSeries>
													<shield:ChartBarSeries DataFieldY="totMinutes" CollectionAlias="Hours">
													</shield:ChartBarSeries>
												</DataSeries>
											</shield:ShieldChart>
										</div>	
									</div>
								</div>									
							</div>
						</div>
					  </div>
					</div>
				</div>
				
				<div class="row">
					<div class="col-sm-12">
						<p></p>
					</div>
				</div>
				<div class="row">
					<!-- ERROR -->				
					<div class="col-12">
						<div class="card">		
							<div class="card-body">
								<div class="row">
									<div class="col-9">	
										<asp:Label runat="server" id="ErrorText" Text="" />									
									</div>			
								</div>
							</div>
						</div>
					</div>
				</div>									
				<asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
				<asp:Label id="HeadText" visible="false" runat="server" />
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
	<div id="sessionLog" class="modal fade">
		<script type="text/javascript">
			function openLog() {
				$('[id*=sessionLog]').modal('show');
			} 
		</script>
		<div class="modal-dialog modal-lg" role="document">
			<div class="modal-content">
				<div class="modal-header">
					<button type="button" class="close" data-dismiss="modal">&times;</button>
					<h3 class="modal-title">Log Session</h3>
				</div>
				<div class="modal-body">
					<div class="row">
						<div class="col-md-12">
							<div class="row">
								<label for="ddl_auditor" class="col-sm-2 control-label">Auditor</label>
								<div class="col-sm-6">
									<asp:DropDownList id="ddl_auditor" name="ddl_auditor" runat="server" CssClass="form-control"></asp:DropDownList>												
								</div>
								<label for="session_date_ID" class="col-sm-1 control-label">Date</label>
								<div class="col-sm-3">
									<asp:TextBox ID="session_date_ID" runat="server" Text="" CssClass="form-control date_future" ></asp:TextBox>
								</div>
								
							</div>
							<div class="row">
								<label class="col-sm-2 control-label"></label>
								<div class="col-sm-2">
								</div>
							</div>					
							<div class="row">
								<label for="ddl_int" class="col-sm-2 control-label">Intensive</label>
								<div class="col-sm-6">
									<asp:DropDownList id="ddl_int" name="ddl_int" runat="server" CssClass="form-control"></asp:DropDownList>												
								</div>
								<label for="typeID" class="col-sm-1 control-label">Type</label>
								<div class="col-sm-3">
									<asp:DropDownList id="typeID" runat="server" CssClass="form-control">
										<asp:ListItem Value="HGC"> HGC </asp:ListItem>
										<asp:ListItem Value="Purif"> PURIF </asp:ListItem>
									</asp:DropDownList>
								</div>								
							</div>				
							<div class="row">
								<label class="col-sm-2 control-label"></label>
								<div class="col-sm-2">
								</div>
							</div>					
							<div class="row">
								<label for="session_hr_ID" class="col-sm-2 control-label">Session Hours</label>
								<div class="col-sm-2">
									<asp:DropDownList id="session_hr_ID" runat="server" CssClass="form-control">
										<asp:ListItem Value="0"> 0 </asp:ListItem>
										<asp:ListItem Value="1"> 1 </asp:ListItem>
										<asp:ListItem Value="2"> 2 </asp:ListItem>
										<asp:ListItem Value="3"> 3 </asp:ListItem>
										<asp:ListItem Value="4"> 4 </asp:ListItem>
										<asp:ListItem Value="5"> 5 </asp:ListItem>
										<asp:ListItem Value="6"> 6 </asp:ListItem>
										<asp:ListItem Value="7"> 7 </asp:ListItem>
										<asp:ListItem Value="8"> 8 </asp:ListItem>
										<asp:ListItem Value="9"> 9 </asp:ListItem>
									</asp:DropDownList>
								</div>
								<label for="session_min_ID" class="col-sm-2 control-label">Session Mins</label>
								<div class="col-sm-2">
									<asp:DropDownList id="session_min_ID" runat="server" CssClass="form-control">
										<asp:ListItem Value="0"> 00 </asp:ListItem>
										<asp:ListItem Value="1"> 01 </asp:ListItem>
										<asp:ListItem Value="2"> 02 </asp:ListItem>
										<asp:ListItem Value="3"> 03 </asp:ListItem>
										<asp:ListItem Value="4"> 04 </asp:ListItem>
										<asp:ListItem Value="5"> 05 </asp:ListItem>
										<asp:ListItem Value="6"> 06 </asp:ListItem>
										<asp:ListItem Value="7"> 07 </asp:ListItem>
										<asp:ListItem Value="8"> 08 </asp:ListItem>
										<asp:ListItem Value="9"> 09 </asp:ListItem>
										<asp:ListItem Value="10"> 10 </asp:ListItem>
										<asp:ListItem Value="11"> 11 </asp:ListItem>
										<asp:ListItem Value="12"> 12 </asp:ListItem>
										<asp:ListItem Value="13"> 13 </asp:ListItem>
										<asp:ListItem Value="14"> 14 </asp:ListItem>
										<asp:ListItem Value="15"> 15 </asp:ListItem>
										<asp:ListItem Value="16"> 16 </asp:ListItem>
										<asp:ListItem Value="17"> 17 </asp:ListItem>
										<asp:ListItem Value="18"> 18 </asp:ListItem>
										<asp:ListItem Value="19"> 19 </asp:ListItem>
										<asp:ListItem Value="20"> 20 </asp:ListItem>
										<asp:ListItem Value="21"> 21 </asp:ListItem>
										<asp:ListItem Value="22"> 22 </asp:ListItem>
										<asp:ListItem Value="23"> 23 </asp:ListItem>
										<asp:ListItem Value="24"> 24 </asp:ListItem>
										<asp:ListItem Value="25"> 25 </asp:ListItem>
										<asp:ListItem Value="26"> 26 </asp:ListItem>
										<asp:ListItem Value="27"> 27 </asp:ListItem>
										<asp:ListItem Value="28"> 28 </asp:ListItem>
										<asp:ListItem Value="29"> 29 </asp:ListItem>
										<asp:ListItem Value="30"> 30 </asp:ListItem>
										<asp:ListItem Value="31"> 31 </asp:ListItem>
										<asp:ListItem Value="32"> 32 </asp:ListItem>
										<asp:ListItem Value="33"> 33 </asp:ListItem>
										<asp:ListItem Value="34"> 34 </asp:ListItem>
										<asp:ListItem Value="35"> 35 </asp:ListItem>
										<asp:ListItem Value="36"> 36 </asp:ListItem>
										<asp:ListItem Value="37"> 37 </asp:ListItem>
										<asp:ListItem Value="38"> 38 </asp:ListItem>
										<asp:ListItem Value="39"> 39 </asp:ListItem>
										<asp:ListItem Value="40"> 40 </asp:ListItem>
										<asp:ListItem Value="41"> 41 </asp:ListItem>
										<asp:ListItem Value="42"> 42 </asp:ListItem>
										<asp:ListItem Value="43"> 43 </asp:ListItem>
										<asp:ListItem Value="44"> 44 </asp:ListItem>
										<asp:ListItem Value="45"> 45 </asp:ListItem>
										<asp:ListItem Value="46"> 46 </asp:ListItem>
										<asp:ListItem Value="47"> 47 </asp:ListItem>
										<asp:ListItem Value="48"> 48 </asp:ListItem>
										<asp:ListItem Value="49"> 49 </asp:ListItem>
										<asp:ListItem Value="50"> 50 </asp:ListItem>
										<asp:ListItem Value="51"> 51 </asp:ListItem>
										<asp:ListItem Value="52"> 52 </asp:ListItem>
										<asp:ListItem Value="53"> 53 </asp:ListItem>
										<asp:ListItem Value="54"> 54 </asp:ListItem>
										<asp:ListItem Value="55"> 55 </asp:ListItem>
										<asp:ListItem Value="56"> 56 </asp:ListItem>
										<asp:ListItem Value="57"> 57 </asp:ListItem>
										<asp:ListItem Value="58"> 58 </asp:ListItem>
										<asp:ListItem Value="59"> 59 </asp:ListItem>
									</asp:DropDownList>
								</div>
							</div>
							<div class="row">
								<label class="col-sm-2 control-label"></label>
								<div class="col-sm-2">
								</div>
							</div>					
							<div class="row">
								<label for="admin_hr_ID" class="col-sm-2 control-label">Admin Hours</label>
								<div class="col-sm-2">
									<asp:DropDownList id="admin_hr_ID" runat="server" CssClass="form-control">
										<asp:ListItem Value="0"> 0 </asp:ListItem>
										<asp:ListItem Value="1"> 1 </asp:ListItem>
										<asp:ListItem Value="2"> 2 </asp:ListItem>
										<asp:ListItem Value="3"> 3 </asp:ListItem>
										<asp:ListItem Value="4"> 4 </asp:ListItem>
										<asp:ListItem Value="5"> 5 </asp:ListItem>
										<asp:ListItem Value="6"> 6 </asp:ListItem>
										<asp:ListItem Value="7"> 7 </asp:ListItem>
										<asp:ListItem Value="8"> 8 </asp:ListItem>
										<asp:ListItem Value="9"> 9 </asp:ListItem>
									</asp:DropDownList>
								</div>
								<label for="admin_min_ID" class="col-sm-2 control-label">Admin Mins</label>
								<div class="col-sm-2">
									<asp:DropDownList id="admin_min_ID" runat="server" CssClass="form-control">
										<asp:ListItem Value="0"> 00 </asp:ListItem>
										<asp:ListItem Value="1"> 01 </asp:ListItem>
										<asp:ListItem Value="2"> 02 </asp:ListItem>
										<asp:ListItem Value="3"> 03 </asp:ListItem>
										<asp:ListItem Value="4"> 04 </asp:ListItem>
										<asp:ListItem Value="5"> 05 </asp:ListItem>
										<asp:ListItem Value="6"> 06 </asp:ListItem>
										<asp:ListItem Value="7"> 07 </asp:ListItem>
										<asp:ListItem Value="8"> 08 </asp:ListItem>
										<asp:ListItem Value="9"> 09 </asp:ListItem>
										<asp:ListItem Value="10"> 10 </asp:ListItem>
										<asp:ListItem Value="11"> 11 </asp:ListItem>
										<asp:ListItem Value="12"> 12 </asp:ListItem>
										<asp:ListItem Value="13"> 13 </asp:ListItem>
										<asp:ListItem Value="14"> 14 </asp:ListItem>
										<asp:ListItem Value="15"> 15 </asp:ListItem>
										<asp:ListItem Value="16"> 16 </asp:ListItem>
										<asp:ListItem Value="17"> 17 </asp:ListItem>
										<asp:ListItem Value="18"> 18 </asp:ListItem>
										<asp:ListItem Value="19"> 19 </asp:ListItem>
										<asp:ListItem Value="20"> 20 </asp:ListItem>
										<asp:ListItem Value="21"> 21 </asp:ListItem>
										<asp:ListItem Value="22"> 22 </asp:ListItem>
										<asp:ListItem Value="23"> 23 </asp:ListItem>
										<asp:ListItem Value="24"> 24 </asp:ListItem>
										<asp:ListItem Value="25"> 25 </asp:ListItem>
										<asp:ListItem Value="26"> 26 </asp:ListItem>
										<asp:ListItem Value="27"> 27 </asp:ListItem>
										<asp:ListItem Value="28"> 28 </asp:ListItem>
										<asp:ListItem Value="29"> 29 </asp:ListItem>
										<asp:ListItem Value="30"> 30 </asp:ListItem>
										<asp:ListItem Value="31"> 31 </asp:ListItem>
										<asp:ListItem Value="32"> 32 </asp:ListItem>
										<asp:ListItem Value="33"> 33 </asp:ListItem>
										<asp:ListItem Value="34"> 34 </asp:ListItem>
										<asp:ListItem Value="35"> 35 </asp:ListItem>
										<asp:ListItem Value="36"> 36 </asp:ListItem>
										<asp:ListItem Value="37"> 37 </asp:ListItem>
										<asp:ListItem Value="38"> 38 </asp:ListItem>
										<asp:ListItem Value="39"> 39 </asp:ListItem>
										<asp:ListItem Value="40"> 40 </asp:ListItem>
										<asp:ListItem Value="41"> 41 </asp:ListItem>
										<asp:ListItem Value="42"> 42 </asp:ListItem>
										<asp:ListItem Value="43"> 43 </asp:ListItem>
										<asp:ListItem Value="44"> 44 </asp:ListItem>
										<asp:ListItem Value="45"> 45 </asp:ListItem>
										<asp:ListItem Value="46"> 46 </asp:ListItem>
										<asp:ListItem Value="47"> 47 </asp:ListItem>
										<asp:ListItem Value="48"> 48 </asp:ListItem>
										<asp:ListItem Value="49"> 49 </asp:ListItem>
										<asp:ListItem Value="50"> 50 </asp:ListItem>
										<asp:ListItem Value="51"> 51 </asp:ListItem>
										<asp:ListItem Value="52"> 52 </asp:ListItem>
										<asp:ListItem Value="53"> 53 </asp:ListItem>
										<asp:ListItem Value="54"> 54 </asp:ListItem>
										<asp:ListItem Value="55"> 55 </asp:ListItem>
										<asp:ListItem Value="56"> 56 </asp:ListItem>
										<asp:ListItem Value="57"> 57 </asp:ListItem>
										<asp:ListItem Value="58"> 58 </asp:ListItem>
										<asp:ListItem Value="59"> 59 </asp:ListItem>
									</asp:DropDownList>
								</div>
							</div>							
							<div class="row">
								<label class="col-sm-2 control-label"></label>
								<div class="col-sm-2">
								</div>
							</div>					
							<div class="row">
								<div class="col-sm-10">
									<asp:TextBox ID="id" type="hidden" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
								</div>
							</div>					
							<div class="modal-footer">
								<asp:Button ID="btnUpdate" OnClientClick="<% %>" class="btn btn-default" runat="server" Text="Save" CommandArgument='<%# Eval("Id") %>' OnCommand="btnLog_Click" />
							</div>							
						</div>
					</div>
				</div><!-- /.modal-body --> 
			</div><!-- /.modal-content -->
		</div><!-- /.modal-dialog -->
	</div><!-- /.modal -->
	<div id="archiveModal" class="modal fade">
		<script type="text/javascript">
			function openModal() {
				$('[id*=archiveModal]').modal('show');
			} 
		</script>
		<div class="modal-dialog modal-lg" role="document">
			<div class="modal-content">
				<div class="modal-header">
					<button type="button" class="close" data-dismiss="modal">&times;</button>
					<h3 class="modal-title">Archive Event</h3>
					<br>
					<!-- <label for="ddl_Event" class="col-sm-2 control-label">Event Name</label> -->
					<div class="col-12">
						<asp:DropDownList id="ddl_EventDetail" name="ddl_EventDetail" runat="server" OnSelectedIndexChanged="Event_Changed" CssClass="form-control"></asp:DropDownList>												
					</div>	
					
				</div>
				<div class="modal-body">
					<div class="row">
						<div class="col-md-12">
							<div class="row">
								<label for="lblEventName" class="col-sm-2 control-label">Event Name</label>
								<div class="col-sm-6">
									<asp:TextBox ID="lblEventName" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
								</div>
								<label for="lblEventDate" class="col-sm-2 control-label">Event Date</label>
								<div class="col-sm-2">
									<asp:TextBox ID="lblEventDate" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
								</div>
							</div>
							<div class="row">
								<label for="lblEventDetail" class="col-sm-2 control-label">Event Detail</label>
								<div class="col-sm-6">
									<asp:TextBox ID="lblEventDetail" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
								</div>
								<label for="lblEventCode" class="col-sm-2 control-label">Event Code</label>
								<div class="col-sm-2">
									<asp:TextBox ID="lblEventCode" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
								</div>
							</div>						
							<div class="row">
							</div>
							<div class="row">
							</div>	
							<br>							
							<div class="modal-footer">
								<asp:Button ID="btnArchive" OnClientClick="<% %>" class="btn btn-warning" runat="server" Text="Archive" CommandArgument='<%# Eval("Id") %>' OnCommand="btnArchive_Click" />
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

		var dayOfWeek = 4;//thurs
		var nextThu = new Date();
		var lastThu = new Date();

		nextThu.setDate(nextThu.getDate() + (dayOfWeek + 7 - nextThu.getDay()) % 7);		
		lastThu.setDate(lastThu.getDate() + (dayOfWeek - 7 - lastThu.getDay()) % 7);		

		$('.date_thisweek').datepicker({
			dateFormat: "dd-M-yy",
			minDate: lastThu,
			maxDate: nextThu
		});	

		$(function () {
			$( '.date_future' ).datepicker({
				dateFormat: "dd-M-yy",
				showOtherMonths: true,
				selectOtherMonths: true
			});			
		});
		
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
	<script type="text/javascript">
		$(function () {
			//Specifying the Character Count control explicitly
			$("[id*=notesid]").MaxLength(
			{
				MaxLength: 250,
				DisplayCharacterCount: false
			});
		});
		
		$('body').on('shown.bs.modal', function (e) {
			var ele = $(e.target).find('input[type=text],textarea,select').filter(':visible:first'); // find the first input on the bs modal
			if (ele) {ele.focus();} // if we found one then set focus.
		})		
	</script>
	<script src="../js/shieldui-all.min.js" type="text/javascript">//</script>
	
</form>	
	<!-- ./form  -->
</div>
<!-- wrapper -->
</body>
</html>									