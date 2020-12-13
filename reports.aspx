<!DOCTYPE html>
<%@ Page Language="C#" Trace="False" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="reports.aspx.cs" Inherits="_Default" Debug="true" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"  
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<%@ Register Assembly="Shield.Web.UI" Namespace="Shield.Web.UI" TagPrefix="shield" %>	
	
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
	<meta charset="utf-8">
	<meta name="viewport" content="width=device-width, initial-scale=1">
	<meta http-equiv="x-ua-compatible" content="ie=edge">

	<title>Charts</title>

		<!-- charts -->
		<link rel="stylesheet" type="text/css" href="/shieldui/css/light/all.min.css" />

		<link rel="stylesheet" href="css/font-awesome.min.css">
		<link rel="stylesheet" href="css/adminlte.css">
		<link rel="stylesheet" href="css/jquery-ui.css">
		
		<link rel="stylesheet" href="/css/bootstrap.css">
		<link rel="stylesheet" href="/css/bootstrap-select.css">
		<link rel="stylesheet" href="/css/bootstrap-datepicker.css">
		<!-- <link rel="stylesheet" type="text/css" href="css/style.css" /> -->
		
		<!-- <link rel="stylesheet" href="/css/bootstrap_min.css"> -->
		<script src="/js/jquery-3.3.1.min.js"></script>
		<script src="chart.js"></script>
		<script src="/js/bootstrap.js"></script>
		<script type="text/javascript" src="/js/moment.js"></script>
		<script type="text/javascript" src="/js/bootstrap-select.js"></script>
		<script type="text/javascript" src="/js/bootstrap-datetimepicker.min.js"></script>		
		<script type="text/javascript" src="/js/bootstrap-datepicker.js"></script>
		<script src="/shieldui/js/shieldui-all.min.js" type="text/javascript">//</script>
	
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
        <a href="index.aspx" class="nav-link">Home</a>
      </li>
      <!-- <li class="nav-item d-none d-sm-inline-block"> -->
        <!-- <a href="#" class="nav-link">Contact</a> -->
      <!-- </li> -->
    </ul>

	
    <!-- Right navbar links -->
	<ul class="navbar-nav ml-auto">
	  <li class="nav-item">
		<a runat="server" id="pr" class="nav-icon fa fa-print text-info" onserverclick="PrintPage" title="Print">
		</a>							
	  </li>	  
	</ul>

	
  </nav>
  <!-- /.navbar -->

  <!-- Main Sidebar Container -->
  <aside class="main-sidebar sidebar-light-primary elevation-4">
    <!-- Brand Logo -->
    <a href="index.aspx" class="brand-link">
      <img src="img/scn_sm.png" alt="Scn Logo" class="brand-image img-circle elevation-3"
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
				<a href="index.aspx" class="nav-link" >
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
					<a href="pages/invoiced.aspx" class="nav-link">
						<i class="fa fa-money nav-icon text-info"></i>
						<p>GI Invoiced</p>
					</a>
				</li>
				<li class="nav-item">
					<a href="pages/confirmed.aspx" class="nav-link">
						<i class="fa fa-money nav-icon nav-icon text-secondary"></i>
						<p>GI Confirmed</p>
					</a>
				</li>
				<li class="nav-item">
					<a href="pages/opencycle.aspx" class="nav-link">
						<i class="fa fa-user-circle nav-icon text-info"></i>
						<p>Open Cycle</p>
					</a>
				</li>			
				<li class="nav-item">
					<a href="pages/now_prospects.aspx" class="nav-link">
						<i class="fa fa-user-circle nav-icon text-info"></i>
						<p>Now Prospects</p>
					</a>
				</li>			
				<li class="nav-item">
					<a href="pages/future_prospects.aspx" class="nav-link">
						<i class="fa fa-user-circle nav-icon text-info"></i>
						<p>Future Prospects</p>
					</a>
				</li>			
				</ul>
			  </li>
				<li class="nav-item">
					<a href="pages/archive.aspx" class="nav-link">
						<i class="fa fa-id-card nav-icon text-secondary"></i>
						<p>Archive</p>
					</a>
				</li>	  
				<li class="nav-item">
					<a href="reports.aspx" class="nav-link">
						<i class="fa fa-pie-chart nav-icon text-info"></i>
						<p>Charts</p>
					</a>
				</li>	  

			  <li class="nav-header">PROSPECTING:</li>
			  
			  <li class="nav-item" >
				<a href="pages/pastInvoices.aspx" class="nav-link" >
				  <i class="nav-icon fa fa-money text-info"></i>
				  <p>
					Past Invoices 
					<span class="right badge badge-info">new</span>
				  </p>
				</a>
			  </li>
			  <li class="nav-item" >
				<a href="pages/fppp.aspx" class="nav-link" >
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
				<a href="pages/calendar.html" class="nav-link">
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
            <h1 class="m-0 text-dark">Reports</h1>
          </div><!-- /.col -->
          <div class="col-sm-6">
            <ol class="breadcrumb float-sm-right">
              <li class="breadcrumb-item"><a href="./index.aspx">Home</a></li>
              <li class="breadcrumb-item active">Reports</li>
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

		
		<div class="row" >
			<!-- SEARCH WEEKEND FEATURE -->				
			<!-- <div class="col-md-6 col-sm-3 col-6"> -->
				<!-- <div class="card">		 -->
					<!-- <div class="card-body"> -->
						<!-- <div class="row"> -->
							<!-- <div class="col-4"> -->
								<!-- <asp:TextBox ID="toText" CssClass="form-control form_date form-control-md" placeholder="To:"  runat="server"></asp:TextBox> -->
							<!-- </div>				 -->
							<!-- <div class="col-4"> -->
								<!-- <asp:TextBox ID="fromText" CssClass="form-control form_date form-control-md" placeholder="From:"  runat="server"></asp:TextBox> -->
							<!-- </div>				 -->
							<!-- <div class="col-md-4 col-sm-3 col-4"> -->
								<!-- <button runat="server" id="btnSearchWE" class="btn btn-block btn-outline-info btn-md" onserverclick="BtnRange_Click" title="Search"> -->
									<!-- Search -->
								<!-- </button>							 -->
							<!-- </div>	 -->
						<!-- </div> -->
					<!-- </div> -->
				<!-- </div> -->
			<!-- </div> -->
		</div>			

		<div class="row">

		<div class="col-md-4">
            <div class="card card-info">
              <div class="card-header">
                <h3 class="card-title">GI by Registrar This Week</h3>

                <div class="card-tools">
                  <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                  </button>
                  <button type="button" class="btn btn-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                </div>
              </div>
              <div class="card-body">
				<shield:ShieldChart 
					ID="ShieldChartbyRegThisWeek" 
					runat="server" AutoPostBack="true" Width="100%" Height="400px" 
					OnTakeDataSource="ShieldChartbyRegThisWeek_TakeDataSource"> 
					<PrimaryHeader Text="Gross Income"> 
					</PrimaryHeader> 
					<ExportOptions AllowExportToImage="false" AllowPrint="false" /> 
					<TooltipSettings CustomPointText="Sales Volume: <b>{point.y}</b>"> 
					</TooltipSettings> 
					<Axes> 
						<shield:ChartAxisX CategoricalValuesField="full_name"> 
							<Title Text="Reg"></Title> 
						</shield:ChartAxisX> 
						<shield:ChartAxisY> 
							<Title Text="Gross Income"></Title> 
						</shield:ChartAxisY> 
					</Axes> 
					<DataSeries> 
						<shield:ChartBarSeries DataFieldY="tot"> 
							<Settings EnablePointSelection="true" EnableAnimation="true"> 
								<DataPointText BorderWidth=""> 
								</DataPointText> 
							</Settings> 
						</shield:ChartBarSeries> 
					</DataSeries> 
					<Legend Align="Center" BorderWidth=""></Legend> 
				</shield:ShieldChart> 
              </div>
              <!-- /.card-body -->
            </div>		
		</div>			
		<div class="col-md-4">
            <div class="card card-info">
              <div class="card-header">
                <h3 class="card-title">GI by Registrar Last Week</h3>

                <div class="card-tools">
                  <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                  </button>
                  <button type="button" class="btn btn-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                </div>
              </div>
              <div class="card-body">
				<shield:ShieldChart 
					ID="ShieldChartbyRegLastWeek" runat="server" AutoPostBack="true" Width="100%" Height="400px" 
					OnTakeDataSource="ShieldChartbyRegLastWeek_TakeDataSource"> 
					<PrimaryHeader Text="Gross Income"> 
					</PrimaryHeader> 
					<ExportOptions AllowExportToImage="false" AllowPrint="false" /> 
					<TooltipSettings CustomPointText="Sales Volume: <b>{point.y}</b>"> 
					</TooltipSettings> 
					<Axes> 
						<shield:ChartAxisX CategoricalValuesField="full_name"> 
							<Title Text="Reg"></Title> 
						</shield:ChartAxisX> 
						<shield:ChartAxisY> 
							<Title Text="Gross Income"></Title> 
						</shield:ChartAxisY> 
					</Axes> 
					<DataSeries> 
						<shield:ChartBarSeries DataFieldY="tot"> 
							<Settings EnablePointSelection="true" EnableAnimation="true"> 
								<DataPointText BorderWidth=""> 
								</DataPointText> 
							</Settings> 
						</shield:ChartBarSeries> 
					</DataSeries> 
					<Legend Align="Center" BorderWidth=""></Legend> 
				</shield:ShieldChart> 
              </div>
              <!-- /.card-body -->
            </div>		
		</div>	
		
		<div class="col-md-4">
            <div class="card card-info">
              <div class="card-header">
                <h3 class="card-title">GI by Registrar All Time</h3>

                <div class="card-tools">
                  <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                  </button>
                  <button type="button" class="btn btn-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                </div>
              </div>
              <div class="card-body">
				<shield:ShieldChart 
					ID="ShieldChartbyReg" runat="server" AutoPostBack="true" Width="100%" Height="400px" 
					OnTakeDataSource="ShieldChartbyReg_TakeDataSource"> 
					<PrimaryHeader Text="Gross Income"> 
					</PrimaryHeader> 
					<ExportOptions AllowExportToImage="false" AllowPrint="false" /> 
					<TooltipSettings CustomPointText="Sales Volume: <b>{point.y}</b>"> 
					</TooltipSettings> 
					<Axes> 
						<shield:ChartAxisX CategoricalValuesField="full_name"> 
							<Title Text="Reg"></Title> 
						</shield:ChartAxisX> 
						<shield:ChartAxisY> 
							<Title Text="Gross Income"></Title> 
						</shield:ChartAxisY> 
					</Axes> 
					<DataSeries> 
						<shield:ChartBarSeries DataFieldY="tot"> 
							<Settings EnablePointSelection="true" EnableAnimation="true"> 
								<DataPointText BorderWidth=""> 
								</DataPointText> 
							</Settings> 
						</shield:ChartBarSeries> 
					</DataSeries> 
					<Legend Align="Center" BorderWidth=""></Legend> 
				</shield:ShieldChart> 
              </div>
              <!-- /.card-body -->
            </div>		
		</div>		

		<div class="col-md-12">
            <div class="card card-info">
              <div class="card-header">
                <h3 class="card-title">Gross Income</h3>

                <div class="card-tools">
                  <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                  </button>
                  <button type="button" class="btn btn-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                </div>
              </div>
              <div class="card-body">
				<shield:ShieldChart 
					ID="ShieldChart0" 
					runat="server" AutoPostBack="true" Width="100%" Height="400px" 
					OnTakeDataSource="ShieldChart0_TakeDataSource"> 
					<PrimaryHeader Text="Gross Income"> 
					</PrimaryHeader> 
					<ExportOptions AllowExportToImage="false" AllowPrint="false" /> 
					<TooltipSettings CustomPointText="Sales Volume: <b>{point.y}</b>"> 
					</TooltipSettings> 
					<Axes> 
						<shield:ChartAxisX CategoricalValuesField="weekend"> 
							<Title Text="Weekending"></Title> 
						</shield:ChartAxisX> 
						<shield:ChartAxisY> 
							<Title Text="Gross Income"></Title> 
						</shield:ChartAxisY> 
					</Axes> 
					<DataSeries> 
						<shield:ChartLineSeries DataFieldY="gi"> 
							<Settings EnablePointSelection="true" EnableAnimation="true"> 
								<DataPointText BorderWidth=""> 
								</DataPointText> 
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
          <div class="col-md-4">
            <div class="card card-info">
              <div class="card-header">
                <h3 class="card-title">Procurement Line - Current Week</h3>

                <div class="card-tools">
                  <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                  </button>
                  <button type="button" class="btn btn-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                </div>
              </div>
              <div class="card-body">

				<shield:ShieldChart 
					ID="ShieldChartThisWeek" runat="server" AutoPostBack="true" Width="100%" Height="400px" OnTakeDataSource="ShieldChartThisWeek_TakeDataSource"> 
					<PrimaryHeader 
						Text="Procurement Lines"> 
					</PrimaryHeader> 
					<ExportOptions AllowExportToImage="false" AllowPrint="false" /> 
					<TooltipSettings CustomPointText="Income: <b>{point.y}</b>"> 
					</TooltipSettings> 
					<Axes> 
						<shield:ChartAxisX CategoricalValuesField="Line"> 
						</shield:ChartAxisX> 
						<shield:ChartAxisY> 
							<Title Text="Procurement Breakdown"></Title> 
						</shield:ChartAxisY> 
					</Axes> 
					<DataSeries> 
						<shield:ChartPieSeries DataFieldY="Sales"> 
							<Settings EnablePointSelection="true" EnableAnimation="true"> 
								<DataPointText BorderWidth=""> 
								</DataPointText> 
							</Settings> 
						</shield:ChartPieSeries> 
					</DataSeries> 
					<Legend Align="Center" BorderWidth=""></Legend> 
				</shield:ShieldChart> 		
			  
              </div>
              <!-- /.card-body -->
            </div>
        </div>	
          <div class="col-md-4">
            <!-- /.card -->
            <div class="card card-info">
              <div class="card-header">
                <h3 class="card-title">Procurement Line - Last Week</h3>

                <div class="card-tools">
                  <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                  </button>
                  <button type="button" class="btn btn-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                </div>
              </div>
              <div class="card-body">

				<shield:ShieldChart 
					ID="ShieldChartLastWeek" runat="server" AutoPostBack="true" Width="100%" Height="400px" OnTakeDataSource="ShieldChartLastWeek_TakeDataSource"> 
					<PrimaryHeader 
						Text="Procurement Lines"> 
					</PrimaryHeader> 
					<ExportOptions AllowExportToImage="false" AllowPrint="false" /> 
					<TooltipSettings CustomPointText="Income: <b>{point.y}</b>"> 
					</TooltipSettings> 
					<Axes> 
						<shield:ChartAxisX CategoricalValuesField="Line"> 
						</shield:ChartAxisX> 
						<shield:ChartAxisY> 
							<Title Text="Procurement Breakdown"></Title> 
						</shield:ChartAxisY> 
					</Axes> 
					<DataSeries> 
						<shield:ChartPieSeries DataFieldY="Sales"> 
							<Settings EnablePointSelection="true" EnableAnimation="true"> 
								<DataPointText BorderWidth=""> 
								</DataPointText> 
							</Settings> 
						</shield:ChartPieSeries> 
					</DataSeries> 
					<Legend Align="Center" BorderWidth=""></Legend> 
				</shield:ShieldChart> 		
			  
              </div>
              <!-- /.card-body -->
            </div>		  
           <!-- /.card -->
          </div>
          <div class="col-md-4">
            <!-- /.card -->
            <div class="card card-info">
              <div class="card-header">
                <h3 class="card-title">Procurement Line - All Time</h3>

                <div class="card-tools">
                  <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                  </button>
                  <button type="button" class="btn btn-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                </div>
              </div>
              <div class="card-body">

				<shield:ShieldChart 
					ID="ShieldChartPieAll" runat="server" AutoPostBack="true" Width="100%" Height="400px" OnTakeDataSource="ShieldChartPieAll_TakeDataSource"> 
					<PrimaryHeader 
						Text="Procurement Lines"> 
					</PrimaryHeader> 
					<ExportOptions AllowExportToImage="false" AllowPrint="false" /> 
					<TooltipSettings CustomPointText="Income: <b>{point.y}</b>"> 
					</TooltipSettings> 
					<Axes> 
						<shield:ChartAxisX CategoricalValuesField="Line"> 
						</shield:ChartAxisX> 
						<shield:ChartAxisY> 
							<Title Text="Procurement Breakdown"></Title> 
						</shield:ChartAxisY> 
					</Axes> 
					<DataSeries> 
						<shield:ChartPieSeries DataFieldY="Sales"> 
							<Settings EnablePointSelection="true" EnableAnimation="true"> 
								<DataPointText BorderWidth=""> 
								</DataPointText> 
							</Settings> 
						</shield:ChartPieSeries> 
					</DataSeries> 
					<Legend Align="Center" BorderWidth=""></Legend> 
				</shield:ShieldChart> 		
			  
              </div>
              <!-- /.card-body -->
            </div>		  
           <!-- /.card -->
          </div>





          </div>		  
		<div class="row">
          <!-- /.col (LEFT) -->
          <div class="col-md-6">
            <!-- LINE CHART -->
            <div class="card card-info">
              <div class="card-header">
                <h3 class="card-title">Arrival GI</h3>
                <div class="card-tools">
                  <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                  </button>
                  <button type="button" class="btn btn-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                </div>
              </div>
              <div class="card-body">
                <div class="chart">
					<shield:ShieldChart 
						ID="ShieldChart2" 
						runat="server" AutoPostBack="true" Width="100%" Height="400px" 
						OnTakeDataSource="ShieldChart2_TakeDataSource"> 
						<PrimaryHeader Text="Arrival GI"> 
						</PrimaryHeader> 
						<ExportOptions AllowExportToImage="false" AllowPrint="false" /> 
						<TooltipSettings CustomPointText="Income: <b>{point.y}</b>"> 
						</TooltipSettings> 
						<Axes> 
							<shield:ChartAxisX CategoricalValuesField="weekend"> 
								<Title Text="Weekending"></Title> 
							</shield:ChartAxisX> 
							<shield:ChartAxisY> 
								<Title Text="Gross Income"></Title> 
							</shield:ChartAxisY> 
						</Axes> 
						<DataSeries> 
							<shield:ChartLineSeries DataFieldY="gi"> 
								<Settings EnablePointSelection="true" EnableAnimation="true"> 
									<DataPointText BorderWidth=""> 
									</DataPointText> 
								</Settings> 
							</shield:ChartLineSeries> 
						</DataSeries> 
						<Legend Align="Center" BorderWidth=""></Legend> 
					</shield:ShieldChart> 
                </div>
              </div>
              <!-- /.card-body -->
            </div>
            <!-- /.card -->
            <!-- BAR CHART -->
            <div class="card card-info">
              <div class="card-header">
                <h3 class="card-title">CF GI</h3>
                <div class="card-tools">
                  <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                  </button>
                  <button type="button" class="btn btn-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                </div>
              </div>
              <div class="card-body">
                
					<shield:ShieldChart 
						ID="ShieldChart3" 
						runat="server" AutoPostBack="true" Width="100%" Height="400px" 
						OnTakeDataSource="ShieldChart3_TakeDataSource"> 
						<PrimaryHeader Text="CF GI"> 
						</PrimaryHeader> 
						<ExportOptions AllowExportToImage="false" AllowPrint="false" /> 
						<TooltipSettings CustomPointText="Income: <b>{point.y}</b>"> 
						</TooltipSettings> 
						<Axes> 
							<shield:ChartAxisX CategoricalValuesField="weekend"> 
								<Title Text="Weekending"></Title> 
							</shield:ChartAxisX> 
							<shield:ChartAxisY> 
								<Title Text="Gross Income"></Title> 
							</shield:ChartAxisY> 
						</Axes> 
						<DataSeries> 
							<shield:ChartLineSeries DataFieldY="gi"> 
								<Settings EnablePointSelection="true" EnableAnimation="true"> 
									<DataPointText BorderWidth=""> 
									</DataPointText> 
								</Settings> 
							</shield:ChartLineSeries> 
						</DataSeries> 
						<Legend Align="Center" BorderWidth=""></Legend> 
					</shield:ShieldChart> 
              </div>
              <!-- /.card-body -->
            </div>
            <!-- /.card -->
			
			
			
            <div class="card card-info">
              <div class="card-header">
                <h3 class="card-title">Resign GI</h3>

                <div class="card-tools">
                  <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                  </button>
                  <button type="button" class="btn btn-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                </div>
              </div>
              <div class="card-body">
				<shield:ShieldChart 
					ID="ShieldChart6" 
					runat="server" AutoPostBack="true" Width="100%" Height="400px" 
					OnTakeDataSource="ShieldChart6_TakeDataSource"> 
					<PrimaryHeader Text="Resign GI"> 
					</PrimaryHeader> 
					<ExportOptions AllowExportToImage="false" AllowPrint="false" /> 
					<TooltipSettings CustomPointText="Income: <b>{point.y}</b>"> 
					</TooltipSettings> 
					<Axes> 
						<shield:ChartAxisX CategoricalValuesField="weekend"> 
							<Title Text="Weekending"></Title> 
						</shield:ChartAxisX> 
						<shield:ChartAxisY> 
							<Title Text="Gross Income"></Title> 
						</shield:ChartAxisY> 
					</Axes> 
					<DataSeries> 
						<shield:ChartLineSeries DataFieldY="gi"> 
							<Settings EnablePointSelection="true" EnableAnimation="true"> 
								<DataPointText BorderWidth=""> 
								</DataPointText> 
							</Settings> 
						</shield:ChartLineSeries> 
					</DataSeries> 

					<Legend Align="Center" BorderWidth=""></Legend> 
				</shield:ShieldChart> 		  
              </div>
              <!-- /.card-body -->
            </div>			
						
          </div>
          <!-- /.col (RIGHT) -->
          <div class="col-md-6">
            <!-- LINE CHART -->
            <div class="card card-info">
              <div class="card-header">
                <h3 class="card-title">FSM GI</h3>
                <div class="card-tools">
                  <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                  </button>
                  <button type="button" class="btn btn-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                </div>
              </div>
              <div class="card-body">
                <div class="chart">
					<shield:ShieldChart 
						ID="ShieldChart4" 
						runat="server" AutoPostBack="true" Width="100%" Height="400px" 
						OnTakeDataSource="ShieldChart4_TakeDataSource"> 
						<PrimaryHeader Text="FSM GI"> 
						</PrimaryHeader> 
						<ExportOptions AllowExportToImage="false" AllowPrint="false" /> 
						<TooltipSettings CustomPointText="Income: <b>{point.y}</b>"> 
						</TooltipSettings> 
						<Axes> 
							<shield:ChartAxisX CategoricalValuesField="weekend"> 
								<Title Text="Weekending"></Title> 
							</shield:ChartAxisX> 
							<shield:ChartAxisY> 
								<Title Text="Gross Income"></Title> 
							</shield:ChartAxisY> 
						</Axes> 
						<DataSeries> 
							<shield:ChartLineSeries DataFieldY="gi"> 
								<Settings EnablePointSelection="true" EnableAnimation="true"> 
									<DataPointText BorderWidth=""> 
									</DataPointText> 
								</Settings> 
							</shield:ChartLineSeries> 
						</DataSeries> 

						<Legend Align="Center" BorderWidth=""></Legend> 
					</shield:ShieldChart> 					
                </div>
              </div>
              <!-- /.card-body -->
            </div>
            <!-- /.card -->

            <!-- BAR CHART -->
            <div class="card card-info">
              <div class="card-header">
                <h3 class="card-title">Prospecting GI</h3>

                <div class="card-tools">
                  <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                  </button>
                  <button type="button" class="btn btn-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                </div>
              </div>
              <div class="card-body">

			  
				<shield:ShieldChart 
					ID="ShieldChart5" 
					runat="server" AutoPostBack="true" Width="100%" Height="400px" 
					OnTakeDataSource="ShieldChart5_TakeDataSource"> 
					<PrimaryHeader Text="Prospecting GI"> 
					</PrimaryHeader> 
					<ExportOptions AllowExportToImage="false" AllowPrint="false" /> 
					<TooltipSettings CustomPointText="Income: <b>{point.y}</b>"> 
					</TooltipSettings> 
					<Axes> 
						<shield:ChartAxisX CategoricalValuesField="weekend"> 
							<Title Text="Weekending"></Title> 
						</shield:ChartAxisX> 
						<shield:ChartAxisY> 
							<Title Text="Gross Income"></Title> 
						</shield:ChartAxisY> 
					</Axes> 
					<DataSeries> 
						<shield:ChartLineSeries DataFieldY="gi"> 
							<Settings EnablePointSelection="true" EnableAnimation="true"> 
								<DataPointText BorderWidth=""> 
								</DataPointText> 
							</Settings> 
						</shield:ChartLineSeries> 
					</DataSeries> 

					<Legend Align="Center" BorderWidth=""></Legend> 
				</shield:ShieldChart> 			  
			  
			  
			  
              </div>
              <!-- /.card-body -->
            </div>
            <!-- /.card -->
			

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

<!-- REQUIRED SCRIPTS -->

<!-- <script src="/js/jquery-3.3.1.min.js"></script> -->
<script src="chart.js"></script>
<script src="/js/bootstrap.js"></script>
<script type="text/javascript" src="/js/moment.js"></script>
<script type="text/javascript" src="/js/bootstrap-select.js"></script>
<script type="text/javascript" src="/js/bootstrap-datetimepicker.min.js"></script>		
<script type="text/javascript" src="/js/bootstrap-datepicker.js"></script>
<script type="text/javascript">
	$(function () {
		$("[id$=appt]").children().datetimepicker();
	});		
	$(document).ready(function() {
		$('.alert').delay(200).addClass("in").fadeOut(8000);
	})	

</script>


<script src="js/jquery.min.js"></script>
<script src="js/bootstrap.bundle.min.js"></script>
<script src="js/adminlte.js"></script>
<script src="js/shieldui-all.min.js" type="text/javascript">//</script>
	<!-- <br><br><br> -->
<div id="Four" 	runat="server">
</div>
	
		
<asp:Label class="navbar-text pull-left" style="padding-left:15px" Text="GI Grid" runat="server" />
<asp:Label class="navbar-text pull-left" id="HeadText" style="padding-left:15px" runat="server" />
<asp:Label class="navbar-text pull-left" id="OrgText" runat="server" Text="Day" />												
<asp:Label class="navbar-text pull-left" id="AmountText" runat="server" />

	<li>
		<a href="#" class="dropdown-toggle pull-left" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">
			Org <span class="caret"></span>
		</a>
		<ul class="dropdown-menu pull-left">
			<li>
				<asp:Button type="button" class="btn btn-default btn-block" id="nday" text="Day" OnClick="Org_Btn_Click" runat="server" />
				<asp:Button type="button" class="btn btn-default btn-block" id="nfdn" text="Fdn" OnClick="Org_Btn_Click" runat="server" />
			</li>
		</ul>				
	</li>
		
			<asp:Label runat="server" id="ErrorText" Text="" />
	</div>
	</form>
</body>
</html>