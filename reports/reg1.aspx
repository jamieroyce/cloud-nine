<!DOCTYPE html>
<%@ Page Language="C#" Trace="False" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="reg1.aspx.cs" Inherits="_Default" Debug="true" %>
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

	<title>Reg Reports</title>

		<!-- charts -->
		<link rel="stylesheet" href="../shieldui/css/light/all.min.css" type="text/css"  />
		<link rel="stylesheet" href="../css/font-awesome.min.css">
		<link rel="stylesheet" href="../css/adminlte.css">
		<link rel="stylesheet" href="../css/jquery-ui.css">
		<link rel="stylesheet" href="../css/bootstrap.css">
		<link rel="stylesheet" href="../css/bootstrap-select.css">
		<link rel="stylesheet" href="../css/bootstrap-datepicker.css">
		<link rel="stylesheet" type="text/css" media="print" href="../css/bootstrap.css">		
		<!-- <link rel="stylesheet" type="text/css" href="css/style.css" /> -->
		<!-- <link rel="stylesheet" href="/css/bootstrap_min.css"> -->

		<script src="../js/jquery-3.3.1.min.js"></script>
		<script src="../shieldui/js/shieldui-all.min.js" type="text/javascript">//</script>

		<script src="..chart.js"></script>
		<script src="../js/bootstrap.js"></script>
		<script type="text/javascript" src="../js/moment.js"></script>
		<script type="text/javascript" src="../js/bootstrap-select.js"></script>
		<script type="text/javascript" src="../js/bootstrap-datetimepicker.min.js"></script>		
		<script type="text/javascript" src="../js/bootstrap-datepicker.js"></script>
		<script src="../js/jquery.min.js"></script>
		<script src="../js/bootstrap.bundle.min.js"></script>
		<script src="../js/adminlte.js"></script>
		<script src="../js/shieldui-all.min.js" type="text/javascript">//</script>
	
	<link rel="icon" href="../img/favicon.png">
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
        <a href="../index.aspx" class="nav-link">Home</a>
      </li>
      <!-- <li class="nav-item d-none d-sm-inline-block"> -->
        <!-- <a href="#" class="nav-link">Contact</a> -->
      <!-- </li> -->
    </ul>

	
    <!-- Right navbar links -->
	<ul class="navbar-nav ml-auto">
	  <li class="nav-item">
		<a runat="server" id="pr" class="nav-icon fa fa-circle text-info" onserverclick="clickTest" title="Test">
		</a>							
	  </li>	  
	</ul>

	
  </nav>
  <!-- /.navbar -->

  <!-- Main Sidebar Container -->
  <aside class="main-sidebar sidebar-light-primary elevation-4">
    <!-- Brand Logo -->
    <a href="../index.aspx" class="brand-link">
      <img src="../img/scn_sm.png" alt="Scn Logo" class="brand-image img-circle elevation-3"
           style="opacity: .8">
      <span class="brand-text font-weight-light">PUBLIC TRACKING</span>
    </a>

    <!-- Sidebar -->
    <div class="sidebar">
		<!-- Sidebar Menu -->
		<nav class="mt-2">
			<ul class="nav nav-pills nav-sidebar flex-column" data-widget="treeview" role="menu" data-accordion="false">
			  <!-- Add icons to the links using the .nav-icon class
				   with font-awesome or any other icon font library -->

			  <li class="nav-item" >
				<a href="../index.aspx" class="nav-link" >
				  <i class="nav-icon fa fa-money text-info"></i>
				  <p>
					Reg Tracking System
					<span class="right badge badge-info"></span>
				  </p>
				</a>
			  </li>
				   
			  <li class="nav-item has-treeview">
				<a href="#" class="nav-link">
				  <i class="nav-icon fa fa-id-card-o text-info"></i>
				  <p>
					Details
					<i class="right fa fa-angle-left"></i>
				  </p>
				</a>
				<ul class="nav nav-treeview">
				<li class="nav-item">
					<a href="../pages/invoiced.aspx" class="nav-link">
						<i class="fa fa-money nav-icon text-info"></i>
						<p>GI Invoiced</p>
					</a>
				</li>
				<li class="nav-item">
					<a href="../pages/confirmed.aspx" class="nav-link">
						<i class="fa fa-money nav-icon nav-icon text-secondary"></i>
						<p>GI Confirmed</p>
					</a>
				</li>
				<li class="nav-item">
					<a href="../pages/opencycle.aspx" class="nav-link">
						<i class="fa fa-user-circle nav-icon text-info"></i>
						<p>Open Cycle</p>
					</a>
				</li>			
				<li class="nav-item">
					<a href="../pages/now_prospects.aspx" class="nav-link">
						<i class="fa fa-user-circle nav-icon text-info"></i>
						<p>Now Prospects</p>
					</a>
				</li>			
				<li class="nav-item">
					<a href="../pages/future_prospects.aspx" class="nav-link">
						<i class="fa fa-user-circle nav-icon text-info"></i>
						<p>Future Prospects</p>
					</a>
				</li>			
				</ul>
			  </li>
			  <li class="nav-item has-treeview">
				<a href="#" class="nav-link">
				  <i class="nav-icon fa fa-tachometer text-info"></i>
				  <p>
					Reports & Charts
					<i class="right fa fa-angle-left"></i>
				  </p>
				</a>
				<ul class="nav nav-treeview">
				<li class="nav-item">
					<a href="reg.aspx" class="nav-link">
						<i class="fa fa-pie-chart nav-icon text-info"></i>
						<p>Reg Stats</p>
					</a>
				</li>
				<li class="nav-item">
					<a href="procurement.aspx" class="nav-link">
						<i class="fa fa-pie-chart nav-icon text-info"></i>
						<p>Procurement Lines</p>
					</a>
				</li>
				</ul>
			  </li>		
				<li class="nav-item">
					<a href="../pages/archive.aspx" class="nav-link">
						<i class="fa fa-id-card nav-icon text-secondary"></i>
						<p>Archive</p>
					</a>
				</li>	  
			

			  <li class="nav-header">PROSPECTING:</li>
			  
			  <li class="nav-item" >
				<a href="../pages/pastInvoices.aspx" class="nav-link" >
				  <i class="nav-icon fa fa-money text-info"></i>
				  <p>
					Past Invoices 
					<span class="right badge badge-info">new</span>
				  </p>
				</a>
			  </li>
			  <li class="nav-item" >
				<a href="../pages/fppp.aspx" class="nav-link" >
				  <i class="nav-icon fa fa-user-circle-o text-info"></i>
				  <p>
					Fully Partially Paid
					<span class="right badge badge-info">new</span>
				  </p>
				</a>
			  </li>
			  <li class="nav-header">COMING SOON...</li>
			  <li class="nav-item">
				<a href="../bis.aspx" class="nav-link">
				  <i class="fa fa-address-card-o nav-icon"></i>
				  <p>BIS Tracking System</p>
				</a>
			  </li>
			  <li class="nav-item">
				<a href="" class="nav-link">
				  <i class="nav-icon fa fa-street-view text-info"></i>
				  <p>
					FSM Activity
					<!-- <span class="right badge badge-danger">New</span> -->
				  </p>
				</a>
			  </li>

			  <li class="nav-item">
				<a href="../pages/calendar.html" class="nav-link">
				  <i class="nav-icon fa fa-calendar"></i>
				  <p>
					Appointments
					<span class="badge badge-info right"></span>
				  </p>
				</a>
			  </li>
			</ul>
		</nav>
		<!-- /.sidebar-menu -->
    </div>
    <!-- Sidebar -->
  </aside>
  
  <!-- Content Wrapper. Contains page content -->
  <div class="content-wrapper">
    <!-- Content Header (Page header) -->
    <div class="content-header">
      <div class="container-fluid">
        <div class="row mb-2">
          <div class="col-sm-6">
            <h1 class="m-0 text-dark">Reg Stats</h1>
          </div><!-- /.col -->
          <div class="col-sm-6">
            <ol class="breadcrumb float-sm-right">
              <li class="breadcrumb-item"><a href="../index.aspx">Home</a></li>
              <li class="breadcrumb-item active">Reg Stats</li>
            </ol>
          </div><!-- /.col -->
        </div><!-- /.row -->
      </div><!-- /.container-fluid -->
    </div>
    <!-- /.content-header -->
	
	<form id="form1" runat="server">
	<asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
    <!-- Main content -->
    <section class="content">
      <div class="container-fluid">
									<div class="col-3">
										<div class="row">
											<div class="col-12">
												<h1 class="m-0 text-dark">In and Started</h1>
												<asp:Label id="OrgText" runat="server" Text="Combined" />												
											</div>
											<div class="col-6 text-info">
												<h5>In and Started</h5>
												<h5>Scheduled</h5>
												<h5>Last Week</h5>
											</div>
											<div class="col-6 text-info">
												<h5><asp:Label id="ThisWeekInStarted" runat="server" /></h5>
												<h5><asp:Label id="ThisWeekSchedInStarted" runat="server" /></h5>
												<h5><asp:Label id="LastWeekInStarted" runat="server" /></h5>
											</div>
										</div>
									</div>
		<div class="row">
		<div class="col-md-4">
            <div class="card card-info">
              <div class="card-header">
                <h3 class="card-title">TEST GRAPH</h3>
				

                <div class="card-tools">
                  <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                  </button>
                  <button type="button" class="btn btn-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                </div>
              </div>
              <div class="card-body">
												<shield:ShieldChart 
												ID="ShieldChartInStarted7R" 
												runat="server" AutoPostBack="true" Width="100%" Height="200px" 
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
              <!-- /.card-body -->
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

		</div><!-- /.container-fluid -->
	  
    </section>
    <!-- /.content -->	
  <!-- Control Sidebar -->
  <aside class="control-sidebar control-sidebar-dark">
    <!-- Control sidebar content goes here -->
  </aside>
  <!-- /.control-sidebar -->
</div>
<!-- ./wrapper -->
</div>

	</form>
</body>
</html>