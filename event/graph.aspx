<!DOCTYPE html>
<%@ Page Language="C#" Trace="False" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="graph.aspx.cs" Inherits="_Default" Debug="true" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"  
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<%@ Register Assembly="Shield.Web.UI" Namespace="Shield.Web.UI" TagPrefix="shield" %>	
	
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
	<meta charset="utf-8">
	<meta name="viewport" content="width=device-width, initial-scale=1">
	<meta http-equiv="x-ua-compatible" content="ie=edge">

	<title>First Service Report</title>

		<!-- charts -->
		<link rel="stylesheet" href="../css/all.min.css" type="text/css"  />
		<link rel="stylesheet" href="../css/font-awesome.min.css">
		<link rel="stylesheet" href="../css/adminlte.css">
		<link rel="stylesheet" href="../css/jquery-ui.css">

		<script src="../js/jquery-3.3.1.min.js"></script>

		<!-- <script src="../js/bootstrap.js"></script> -->
		<!-- <script src="../js/moment.js" type="text/javascript" ></script> -->
		<!-- <script src="../js/bootstrap-select.js" type="text/javascript"></script> -->
		<!-- <script src="../js/bootstrap-datetimepicker.min.js" type="text/javascript"></script>		 -->
		<!-- <script src="../js/bootstrap-datepicker.js" type="text/javascript" ></script> -->
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
        <a href="../home.aspx" class="nav-link">Home</a>
      </li>
    </ul>
    <!-- Right navbar links -->
	<ul class="navbar-nav ml-auto">
	  <li class="nav-item">
		<a href="graph.aspx" class="nav-link">	  
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
      <img src="../img/scn_sm.png" alt="Scn Logo" class="brand-image img-circle elevation-3"
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
				  <i class="nav-icon fa fa-address-card text-info"></i>
				  <p>
					Event Confirms Log
				  </p>
				</a>
			<hr>          	  
				<li class="nav-item">
					<a href="graph.aspx" class="nav-link">
						<i class="fa fa-pie-chart nav-icon text-info"></i>
						<p>Event Report</p>
					</a>
				</li>	
			</li>
			</ul>
      </nav>
      <!-- /.sidebar-menu -->
    </div>
    <!-- /.sidebar -->
  </aside>
  <!-- Content Wrapper. Contains page content -->
  <div class="content-wrapper">
    <!-- Content Header (Page header) -->
    <div class="content-header">
      <div class="container-fluid">
        <div class="row mb-2">
          <div class="col-sm-6">
            <h1 class="m-0 text-dark">Event Report</h1>
			<asp:Label id="OrgText" runat="server"/>												
			
          </div><!-- /.col -->
          <div class="col-sm-6">
            <ol class="breadcrumb float-sm-right">
              <li class="breadcrumb-item"><a href="../home.aspx">Home</a></li>
              <li class="breadcrumb-item active">Event Report</li>
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
		<!-- SEARCH -->
        <div class="row">
          <div class="col-md-6">
            <div class="card card-info">
              <div class="card-header">
                <h3 class="card-title">Event Confirms</h3>
                <div class="card-tools">
                  <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                  </button>
                  <button type="button" class="btn btn-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                </div>
              </div>
              <div class="card-body">
				
				<shield:ShieldChart ID="ShieldChartThisWeek" Width="100%" Height="400px" runat="server"
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
              <!-- /.card-body -->
            </div>
        </div>	
	  </div>
		<div class="row" >
			<!-- ERROR SECTION-->				
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
            <!-- /.card -->
	  </div>		  		  
     </div>
        <!-- /.row -->
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