<!DOCTYPE html>
<%@ Page Language="C#" Trace="False" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="graphbyreg.aspx.cs" Inherits="_Default" Debug="true" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"  
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<%@ Register Assembly="Shield.Web.UI" Namespace="Shield.Web.UI" TagPrefix="shield" %>	
	
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
	<meta charset="utf-8">
	<meta name="viewport" content="width=device-width, initial-scale=1">
	<meta http-equiv="x-ua-compatible" content="ie=edge">

	<title>Registrar Report</title>

		<!-- charts -->
		<link rel="stylesheet" href="../shieldui/css/light/all.min.css" type="text/css"  />
		<link rel="stylesheet" href="../css/font-awesome.min.css">
		<link rel="stylesheet" href="../css/adminlte.css">
		<link rel="stylesheet" href="../css/jquery-ui.css">
		<!-- <link rel="stylesheet" href="../css/bootstrap.css"> -->
		<!-- <link rel="stylesheet" href="../css/bootstrap-select.css"> -->
		<!-- <link rel="stylesheet" href="../css/bootstrap-datepicker.css"> -->
		<!-- <link rel="stylesheet" type="text/css" href="css/style.css" /> -->
		<!-- <link rel="stylesheet" href="/css/bootstrap_min.css"> -->

		<script src="../js/jquery-3.3.1.min.js"></script>
		<script src="../shieldui/js/shieldui-all.min.js" type="text/javascript">//</script>

		<script src="..chart.js"></script>
		<script src="../js/bootstrap.js"></script>
		<!-- <script type="text/javascript" src="../js/moment.js"></script> -->
		<!-- <script type="text/javascript" src="../js/bootstrap-select.js"></script> -->
		<!-- <script type="text/javascript" src="../js/bootstrap-datetimepicker.min.js"></script>		 -->
		<!-- <script type="text/javascript" src="../js/bootstrap-datepicker.js"></script> -->
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
		<a href="graphbyreg.aspx" class="nav-link">	  
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
      <img src="../img/scn_sm.png" alt="Scn Logo" class="brand-image img-circle elevation-3"
           style="opacity: .8">
      <span class="brand-text font-weight-light">PUBLIC TRACKING</span>
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
				  <p>
					GI Log
				  </p>
				</a>
				<li class="nav-item">
					<a href="invoiced.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-info"></i>
						<p>Invoiced</p>
					</a>
				</li>
				<li class="nav-item">
					<a href="confirmed.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-secondary"></i>
						<p>Confirmed</p>
					</a>
				</li>
				<li class="nav-item">
					<a href="definite.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-secondary"></i>
						<p>Definite</p>
					</a>
				</li>
				<li class="nav-item">
					<a href="possible.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-secondary"></i>
						<p>Possible</p>
					</a>
				</li>
				<li class="nav-item">
					<a href="opencycle.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-secondary"></i>
						<p>Open Cycle</p>
					</a>
				</li>			
				<li class="nav-item">
					<a href="now_prospects.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-secondary"></i>
						<p>Prospects</p>
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
					<a href="account.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-secondary"></i>
						<p>Account Total</p>
					</a>
				</li>
				<li class="nav-item">
					<a href="fppp.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-secondary"></i>
						<p>Fully Partially Paid</p>
					</a>
				</li>
				<li class="nav-item">
					<a href="pastInvoices.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-secondary"></i>
						<p>Past Invoices</p>
					</a>
				</li>
				<hr>
				<li class="nav-item">
					<a href="graph.aspx" class="nav-link">
						<i class="fa fa-pie-chart nav-icon text-info"></i>
						<p>Income Report</p>
					</a>
				</li>
				<li class="nav-item">
					<a href="graphbyreg.aspx" class="nav-link">
						<i class="fa fa-pie-chart nav-icon text-info"></i>
						<p>Registrar Report</p>
					</a>
				</li>	
				<hr>    
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
            <h1 class="m-0 text-dark">Registrar Report</h1>
			<asp:Label id="OrgText" runat="server"/>												
          </div><!-- /.col -->
          <div class="col-sm-6">
            <ol class="breadcrumb float-sm-right">
              <li class="breadcrumb-item"><a href="../home.aspx">Home</a></li>
              <li class="breadcrumb-item active">Registrar Report</li>
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
		<div class="col-md-4">
            <div class="card card-info">
              <div class="card-header">
                <h3 class="card-title">GI by Reg ( This Week )</h3>

                <div class="card-tools">
                  <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                  </button>
                  <button type="button" class="btn btn-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                </div>
              </div>
              <div class="card-body">
				<shield:ShieldChart 
					ID="ShieldChartbyRegThisWeek" 
					runat="server" AutoPostBack="true" Width="100%" Height="350px" 
					OnTakeDataSource="ShieldChartbyRegThisWeek_TakeDataSource"> 
					<PrimaryHeader Text="Gross Income"> 
					</PrimaryHeader> 
					<ExportOptions AllowExportToImage="false" AllowPrint="false" /> 
					<TooltipSettings CustomPointText="{point.dataSeries.collectionAlias}: <b>{point.y}</b>"></TooltipSettings> 
					<Axes> 
						<shield:ChartAxisX CategoricalValuesField="full_name"> 
							<Title Text="Reg"></Title> 
						</shield:ChartAxisX> 
						<shield:ChartAxisY min=0> 
							<Title Text="Gross Income"></Title> 
						</shield:ChartAxisY> 
					</Axes> 
					<DataSeries> 
						<shield:ChartBarSeries DataFieldY="tot"  CollectionAlias="In/Confirmed GI"> 
							<Settings EnablePointSelection="true" color="#FFB30F" EnableAnimation="false"> 
								<DataPointText BorderWidth=""> 
								</DataPointText> 
							</Settings> 
						</shield:ChartBarSeries> 
						<shield:ChartBarSeries DataFieldY="inv"  CollectionAlias="Invoiced GI"> 
							<Settings EnablePointSelection="true" color="#3DA5D9" EnableAnimation="false"> 
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
                <h3 class="card-title">GI by Reg ( Last Week )</h3>

                <div class="card-tools">
                  <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                  </button>
                  <button type="button" class="btn btn-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                </div>
              </div>
              <div class="card-body">
				<shield:ShieldChart 
					ID="ShieldChartbyRegLastWeek" runat="server" AutoPostBack="true" Width="100%" Height="350px" 
					OnTakeDataSource="ShieldChartbyRegLastWeek_TakeDataSource"> 
					<PrimaryHeader Text="Gross Income"> 
					</PrimaryHeader> 
					<ExportOptions AllowExportToImage="false" AllowPrint="false" /> 
					<TooltipSettings CustomPointText="{point.dataSeries.collectionAlias}: <b>{point.y}</b>"></TooltipSettings> 
					<Axes> 
						<shield:ChartAxisX CategoricalValuesField="full_name"> 
							<Title Text="Reg"></Title> 
						</shield:ChartAxisX> 
						<shield:ChartAxisY min=0> 
							<Title Text="Gross Income"></Title> 
						</shield:ChartAxisY> 
					</Axes> 
					<DataSeries> 
						<shield:ChartBarSeries DataFieldY="tot" CollectionAlias="Last Week GI"> 
							<Settings EnablePointSelection="true" color="#3DA5D9" EnableAnimation="false"> 
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
                <h3 class="card-title">GI by Reg ( All Time )</h3>

                <div class="card-tools">
                  <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                  </button>
                  <button type="button" class="btn btn-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                </div>
              </div>
              <div class="card-body">
				<shield:ShieldChart 
					ID="ShieldChartbyReg" runat="server" AutoPostBack="true" Width="100%" Height="350px" 
					OnTakeDataSource="ShieldChartbyReg_TakeDataSource"> 
					<PrimaryHeader Text="Gross Income"> 
					</PrimaryHeader> 
					<ExportOptions AllowExportToImage="false" AllowPrint="false" /> 
					<TooltipSettings CustomPointText="{point.dataSeries.collectionAlias}: <b>{point.y}</b>"></TooltipSettings> 
					<Axes> 
						<shield:ChartAxisX CategoricalValuesField="full_name"> 
							<Title Text="Reg"></Title> 
						</shield:ChartAxisX> 
						<shield:ChartAxisY min=0> 
							<Title Text="Gross Income"></Title> 
						</shield:ChartAxisY> 
					</Axes> 
					<DataSeries> 
						<shield:ChartBarSeries DataFieldY="tot" CollectionAlias="Gross Income"> 
							<Settings EnablePointSelection="true" color="#3DA5D9" EnableAnimation="false"> 
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
			<!-- /.card -->
			<div class="card card-info">
			  <div class="card-header">
				<h3 class="card-title">Procurement Line - All Time (
				<asp:Label id="pie_regid1" runat="server" /> 				
				)</h3>

				<div class="card-tools">
				  <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i>
				  </button>
				  <button type="button" class="btn btn-tool" data-widget="remove"><i class="fa fa-times"></i></button>
				</div>
			  </div>
			  <div class="card-body">

				<shield:ShieldChart 
					ID="ShieldChartPie1" runat="server" AutoPostBack="true" Width="100%" Height="350px" OnTakeDataSource="ShieldChartPie1_TakeDataSource"> 
					<PrimaryHeader 
						Text="Procurement Lines"> 
					</PrimaryHeader> 
					<ExportOptions AllowExportToImage="false" AllowPrint="false" /> 
					<TooltipSettings customHeaderText=" " CustomPointText="Income: <b>${point.y}</b>"></TooltipSettings> 
					<Axes> 
						<shield:ChartAxisX CategoricalValuesField="Line"> 
						</shield:ChartAxisX> 
						<shield:ChartAxisY min=0> 
							<Title Text="Procurement Breakdown"></Title> 
						</shield:ChartAxisY> 
					</Axes> 
					<DataSeries> 
						<shield:ChartPieSeries DataFieldY="Sales"> 
							<Settings EnablePointSelection="true" EnableAnimation="false"> 
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
            <div class="card card-info">
              <div class="card-header">
                <h3 class="card-title">
					Daily GI - 
					<asp:Label id="reg7R1" runat="server" /> 
				</h3>
                <div class="card-tools">
                  <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                  </button>
                  <button type="button" class="btn btn-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                </div>
              </div>
              <div class="card-body">
				<shield:ShieldChart 
					ID="ShieldChart7R1" 
					runat="server" AutoPostBack="true" Width="100%" Height="350px" 
					OnTakeDataSource="ShieldChart7R1_TakeDataSource"> 
					<PrimaryHeader Text="Gross Income Daily Graph"> 
					</PrimaryHeader> 
					<ExportOptions AllowExportToImage="false" AllowPrint="false" /> 
					<TooltipSettings CustomPointText="{point.dataSeries.collectionAlias}: <b>{point.y}</b>"></TooltipSettings> 
					<Axes> 
						<shield:ChartAxisX 
							CategoricalValuesField="dayofweek"> 
						</shield:ChartAxisX> 
						<shield:ChartAxisY min=0> 
							<Title Text="Gross Income"></Title> 
						</shield:ChartAxisY> 
					</Axes> 
					<DataSeries> 
						<shield:ChartLineSeries DataFieldY="thisweekinv" CollectionAlias="Invoiced (Daily)"> 
							<Settings seriesDashStyle="Dash" color="#3DA5D9" EnablePointSelection="true" EnableAnimation="false"> 
								<DataPointText BorderWidth=""> 
								</DataPointText> 
								<PointMark Enabled="false"></PointMark>
							</Settings> 
						</shield:ChartLineSeries> 
						<shield:ChartLineSeries DataFieldY="thisweekinvcum" CollectionAlias="Invoiced (Cum.)"> 
							<Settings EnablePointSelection="true" color="#3DA5D9" EnableAnimation="false" > 
								<DataPointText BorderWidth=""> 
								</DataPointText> 
								<PointMark Enabled="false"></PointMark>
							</Settings> 
						</shield:ChartLineSeries> 
						<shield:ChartLineSeries DataFieldY="thisweekgi" CollectionAlias="In/Conf (Daily)"> 
							<Settings seriesDashStyle="Dot" color="#FFB30F" EnablePointSelection="true" EnableAnimation="false"> 
								<DataPointText BorderWidth=""> 
								</DataPointText> 
								<PointMark Enabled="false"></PointMark>
							</Settings> 
						</shield:ChartLineSeries> 
						<shield:ChartLineSeries DataFieldY="thisweekcum" CollectionAlias="In/Conf (Cum.)"> 
							<Settings EnablePointSelection="true" color="#FFB30F" EnableAnimation="false" > 
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
		<div class="col-md-4">
            <div class="card card-info">
              <div class="card-header">
                <h3 class="card-title">
					Weekly GI - 
					<asp:Label id="regid1" runat="server" /> 
				</h3>

                <div class="card-tools">
                  <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                  </button>
                  <button type="button" class="btn btn-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                </div>
              </div>
              <div class="card-body">
				<shield:ShieldChart 
					ID="ShieldChartReg1" runat="server" AutoPostBack="true" Width="100%" Height="350px" 
					OnTakeDataSource="ShieldChartReg1_TakeDataSource"> 
					<PrimaryHeader Text="Gross Income"> 
					</PrimaryHeader> 
					<ExportOptions AllowExportToImage="false" AllowPrint="false" /> 
					<TooltipSettings CustomPointText="{point.dataSeries.collectionAlias}: <b>{point.y}</b>"></TooltipSettings> 
					<Axes> 
						<shield:ChartAxisX CategoricalValuesField="weekend"> 
							<Title Text="Weekending"></Title> 
						</shield:ChartAxisX> 
						<shield:ChartAxisY min=0> 
							<Title Text="Gross Income"></Title> 
						</shield:ChartAxisY> 
					</Axes> 
					<DataSeries> 
						<shield:ChartLineSeries DataFieldY="gi" CollectionAlias="Gross Income"> 
							<Settings EnablePointSelection="true" EnableAnimation="false"> 
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
		<div class="col-md-4">
            <!-- /.card -->
            <div class="card card-info">
              <div class="card-header">
                <h3 class="card-title">Procurement Line - All Time (
				<asp:Label id="pie_regid2" runat="server" /> 				
				)</h3>
                <div class="card-tools">
                  <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                  </button>
                  <button type="button" class="btn btn-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                </div>
              </div>
              <div class="card-body">
				<shield:ShieldChart 
					ID="ShieldChartPie2" runat="server" AutoPostBack="true" Width="100%" Height="350px" OnTakeDataSource="ShieldChartPie2_TakeDataSource"> 
					<PrimaryHeader 
						Text="Procurement Lines"> 
					</PrimaryHeader> 
					<ExportOptions AllowExportToImage="false" AllowPrint="false" /> 
					<TooltipSettings customHeaderText=" " CustomPointText="Income: <b>${point.y}</b>"></TooltipSettings> 
					<Axes> 
						<shield:ChartAxisX CategoricalValuesField="Line"> 
						</shield:ChartAxisX> 
						<shield:ChartAxisY min=0> 
							<Title Text="Procurement Breakdown"></Title> 
						</shield:ChartAxisY> 
					</Axes> 
					<DataSeries> 
						<shield:ChartPieSeries DataFieldY="Sales"> 
							<Settings EnablePointSelection="true" EnableAnimation="false"> 
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
            <div class="card card-info">
              <div class="card-header">
                <h3 class="card-title">
					Daily GI - 
					<asp:Label id="reg7R2" runat="server" /> 
				</h3>
                <div class="card-tools">
                  <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                  </button>
                  <button type="button" class="btn btn-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                </div>
              </div>
              <div class="card-body">
				<shield:ShieldChart 
					ID="ShieldChart7R2" 
					runat="server" AutoPostBack="true" Width="100%" Height="350px" 
					OnTakeDataSource="ShieldChart7R2_TakeDataSource"> 
					<PrimaryHeader Text="Gross Income Daily Graph"> 
					</PrimaryHeader> 
					<ExportOptions AllowExportToImage="false" AllowPrint="false" /> 
					<TooltipSettings CustomPointText="{point.dataSeries.collectionAlias}: <b>{point.y}</b>"></TooltipSettings> 
					<Axes> 
						<shield:ChartAxisX 
							CategoricalValuesField="dayofweek"> 
						</shield:ChartAxisX> 
						<shield:ChartAxisY min=0> 
							<Title Text="Gross Income"></Title> 
						</shield:ChartAxisY> 
					</Axes> 
					<DataSeries> 
						<shield:ChartLineSeries DataFieldY="thisweekinv" CollectionAlias="Invoiced (Daily)"> 
							<Settings seriesDashStyle="Dash" color="#3DA5D9" EnablePointSelection="true" EnableAnimation="false"> 
								<DataPointText BorderWidth=""> 
								</DataPointText> 
								<PointMark Enabled="false"></PointMark>
							</Settings> 
						</shield:ChartLineSeries> 
						<shield:ChartLineSeries DataFieldY="thisweekinvcum" CollectionAlias="Invoiced (Cum.)"> 
							<Settings EnablePointSelection="true" color="#3DA5D9" EnableAnimation="false" > 
								<DataPointText BorderWidth=""> 
								</DataPointText> 
								<PointMark Enabled="false"></PointMark>
							</Settings> 
						</shield:ChartLineSeries> 
						<shield:ChartLineSeries DataFieldY="thisweekgi" CollectionAlias="In/Conf (Daily)"> 
							<Settings seriesDashStyle="Dot" color="#FFB30F" EnablePointSelection="true" EnableAnimation="false"> 
								<DataPointText BorderWidth=""> 
								</DataPointText> 
								<PointMark Enabled="false"></PointMark>
							</Settings> 
						</shield:ChartLineSeries> 
						<shield:ChartLineSeries DataFieldY="thisweekcum" CollectionAlias="In/Conf (Cum.)"> 
							<Settings EnablePointSelection="true" color="#FFB30F" EnableAnimation="false" > 
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
		<div class="col-md-4">
            <div class="card card-info">
              <div class="card-header">
                <h3 class="card-title">
					Weekly GI - 
					<asp:Label id="regid2" runat="server" /> 
				</h3>

                <div class="card-tools">
                  <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                  </button>
                  <button type="button" class="btn btn-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                </div>
              </div>
              <div class="card-body">
				<shield:ShieldChart 
					ID="ShieldChartReg2" runat="server" AutoPostBack="true" Width="100%" Height="350px" 
					OnTakeDataSource="ShieldChartReg2_TakeDataSource"> 
					<PrimaryHeader Text="Gross Income"> 
					</PrimaryHeader> 
					<ExportOptions AllowExportToImage="false" AllowPrint="false" /> 
					<TooltipSettings CustomPointText="{point.dataSeries.collectionAlias}: <b>{point.y}</b>"></TooltipSettings> 
					<Axes> 
						<shield:ChartAxisX CategoricalValuesField="weekend"> 
							<Title Text="Weekending"></Title> 
						</shield:ChartAxisX> 
						<shield:ChartAxisY min=0> 
							<Title Text="Gross Income"></Title> 
						</shield:ChartAxisY> 
					</Axes> 
					<DataSeries> 
						<shield:ChartLineSeries DataFieldY="gi" CollectionAlias="Gross Income"> 
							<Settings EnablePointSelection="true" EnableAnimation="false"> 
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
        <div class="col-md-4">
            <!-- /.card -->
            <div class="card card-info">
              <div class="card-header">
                <h3 class="card-title">Procurement Line - All Time (
				<asp:Label id="pie_regid3" runat="server" /> 				
				)</h3>

                <div class="card-tools">
                  <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                  </button>
                  <button type="button" class="btn btn-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                </div>
              </div>
              <div class="card-body">
				<shield:ShieldChart 
					ID="ShieldChartPie3" runat="server" AutoPostBack="true" Width="100%" Height="350px" OnTakeDataSource="ShieldChartPie3_TakeDataSource"> 
					<PrimaryHeader 
						Text="Procurement Lines"> 
					</PrimaryHeader> 
					<ExportOptions AllowExportToImage="false" AllowPrint="false" /> 
					<TooltipSettings customHeaderText=" " CustomPointText="Income: <b>${point.y}</b>"></TooltipSettings> 
					<Axes> 
						<shield:ChartAxisX CategoricalValuesField="Line"> 
						</shield:ChartAxisX> 
						<shield:ChartAxisY min=0> 
							<Title Text="Procurement Breakdown"></Title> 
						</shield:ChartAxisY> 
					</Axes> 
					<DataSeries> 
						<shield:ChartPieSeries DataFieldY="Sales"> 
							<Settings EnablePointSelection="true" EnableAnimation="false"> 
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
            <div class="card card-info">
              <div class="card-header">
                <h3 class="card-title">
					Daily GI - 
					<asp:Label id="reg7R3" runat="server" /> 
				</h3>
                <div class="card-tools">
                  <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                  </button>
                  <button type="button" class="btn btn-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                </div>
              </div>
              <div class="card-body">
				<shield:ShieldChart 
					ID="ShieldChart7R3" 
					runat="server" AutoPostBack="true" Width="100%" Height="350px" 
					OnTakeDataSource="ShieldChart7R3_TakeDataSource"> 
					<PrimaryHeader Text="Gross Income Daily Graph"> 
					</PrimaryHeader> 
					<ExportOptions AllowExportToImage="false" AllowPrint="false" /> 
					<TooltipSettings CustomPointText="{point.dataSeries.collectionAlias}: <b>{point.y}</b>"></TooltipSettings> 
					<Axes> 
						<shield:ChartAxisX 
							CategoricalValuesField="dayofweek"> 
						</shield:ChartAxisX> 
						<shield:ChartAxisY min=0> 
							<Title Text="Gross Income"></Title> 
						</shield:ChartAxisY> 
					</Axes> 
					<DataSeries> 
						<shield:ChartLineSeries DataFieldY="thisweekinv" CollectionAlias="Invoiced (Daily)"> 
							<Settings seriesDashStyle="Dash" color="#3DA5D9" EnablePointSelection="true" EnableAnimation="false"> 
								<DataPointText BorderWidth=""> 
								</DataPointText> 
								<PointMark Enabled="false"></PointMark>
							</Settings> 
						</shield:ChartLineSeries> 
						<shield:ChartLineSeries DataFieldY="thisweekinvcum" CollectionAlias="Invoiced (Cum.)"> 
							<Settings EnablePointSelection="true" color="#3DA5D9" EnableAnimation="false" > 
								<DataPointText BorderWidth=""> 
								</DataPointText> 
								<PointMark Enabled="false"></PointMark>
							</Settings> 
						</shield:ChartLineSeries> 
						<shield:ChartLineSeries DataFieldY="thisweekgi" CollectionAlias="In/Conf (Daily)"> 
							<Settings seriesDashStyle="Dot" color="#FFB30F" EnablePointSelection="true" EnableAnimation="false"> 
								<DataPointText BorderWidth=""> 
								</DataPointText> 
								<PointMark Enabled="false"></PointMark>
							</Settings> 
						</shield:ChartLineSeries> 
						<shield:ChartLineSeries DataFieldY="thisweekcum" CollectionAlias="In/Conf (Cum.)"> 
							<Settings EnablePointSelection="true" color="#FFB30F" EnableAnimation="false" > 
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
		<div class="col-md-4">
            <div class="card card-info">
              <div class="card-header">
                <h3 class="card-title">
					Weekly GI - 
					<asp:Label id="regid3" runat="server" /> 
				</h3>

                <div class="card-tools">
                  <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                  </button>
                  <button type="button" class="btn btn-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                </div>
              </div>
              <div class="card-body">
				<shield:ShieldChart 
					ID="ShieldChartReg3" runat="server" AutoPostBack="true" Width="100%" Height="350px" 
					OnTakeDataSource="ShieldChartReg3_TakeDataSource"> 
					<PrimaryHeader Text="Gross Income"> 
					</PrimaryHeader> 
					<ExportOptions AllowExportToImage="false" AllowPrint="false" /> 
					<TooltipSettings CustomPointText="{point.dataSeries.collectionAlias}: <b>{point.y}</b>"></TooltipSettings> 
					<Axes> 
						<shield:ChartAxisX CategoricalValuesField="weekend"> 
							<Title Text="Weekending"></Title> 
						</shield:ChartAxisX> 
						<shield:ChartAxisY min=0> 
							<Title Text="Gross Income"></Title> 
						</shield:ChartAxisY> 
					</Axes> 
					<DataSeries> 
						<shield:ChartLineSeries DataFieldY="gi" CollectionAlias="Gross Income"> 
							<Settings EnablePointSelection="true" EnableAnimation="false"> 
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
        <div class="col-md-4">
            <!-- /.card -->
            <div class="card card-info">
              <div class="card-header">
                <h3 class="card-title">Procurement Line - All Time (
				<asp:Label id="pie_regid4" runat="server" /> 				
				)</h3>

                <div class="card-tools">
                  <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                  </button>
                  <button type="button" class="btn btn-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                </div>
              </div>
              <div class="card-body">
				<shield:ShieldChart 
					ID="ShieldChartPie4" runat="server" AutoPostBack="true" Width="100%" Height="350px" OnTakeDataSource="ShieldChartPie4_TakeDataSource"> 
					<PrimaryHeader 
						Text="Procurement Lines"> 
					</PrimaryHeader> 
					<ExportOptions AllowExportToImage="false" AllowPrint="false" /> 
					<TooltipSettings customHeaderText=" " CustomPointText="Income: <b>${point.y}</b>"></TooltipSettings> 
					<Axes> 
						<shield:ChartAxisX CategoricalValuesField="Line"> 
						</shield:ChartAxisX> 
						<shield:ChartAxisY min=0> 
							<Title Text="Procurement Breakdown"></Title> 
						</shield:ChartAxisY> 
					</Axes> 
					<DataSeries> 
						<shield:ChartPieSeries DataFieldY="Sales"> 
							<Settings EnablePointSelection="true" EnableAnimation="false"> 
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
            <div class="card card-info">
              <div class="card-header">
                <h3 class="card-title">
					Daily GI - 
					<asp:Label id="reg7R4" runat="server" /> 
				</h3>
                <div class="card-tools">
                  <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                  </button>
                  <button type="button" class="btn btn-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                </div>
              </div>
              <div class="card-body">
				<shield:ShieldChart 
					ID="ShieldChart7R4" 
					runat="server" AutoPostBack="true" Width="100%" Height="350px" 
					OnTakeDataSource="ShieldChart7R4_TakeDataSource"> 
					<PrimaryHeader Text="Gross Income Daily Graph"> 
					</PrimaryHeader> 
					<ExportOptions AllowExportToImage="false" AllowPrint="false" /> 
					<TooltipSettings CustomPointText="{point.dataSeries.collectionAlias}: <b>{point.y}</b>"></TooltipSettings> 
					<Axes> 
						<shield:ChartAxisX 
							CategoricalValuesField="dayofweek"> 
						</shield:ChartAxisX> 
						<shield:ChartAxisY min=0> 
							<Title Text="Gross Income"></Title> 
						</shield:ChartAxisY> 
					</Axes> 
					<DataSeries> 
						<shield:ChartLineSeries DataFieldY="thisweekinv" CollectionAlias="Invoiced (Daily)"> 
							<Settings seriesDashStyle="Dash" color="#3DA5D9" EnablePointSelection="true" EnableAnimation="false"> 
								<DataPointText BorderWidth=""> 
								</DataPointText> 
								<PointMark Enabled="false"></PointMark>
							</Settings> 
						</shield:ChartLineSeries> 
						<shield:ChartLineSeries DataFieldY="thisweekinvcum" CollectionAlias="Invoiced (Cum.)"> 
							<Settings EnablePointSelection="true" color="#3DA5D9" EnableAnimation="false" > 
								<DataPointText BorderWidth=""> 
								</DataPointText> 
								<PointMark Enabled="false"></PointMark>
							</Settings> 
						</shield:ChartLineSeries> 
						<shield:ChartLineSeries DataFieldY="thisweekgi" CollectionAlias="In/Conf (Daily)"> 
							<Settings seriesDashStyle="Dot" color="#FFB30F" EnablePointSelection="true" EnableAnimation="false"> 
								<DataPointText BorderWidth=""> 
								</DataPointText> 
								<PointMark Enabled="false"></PointMark>
							</Settings> 
						</shield:ChartLineSeries> 
						<shield:ChartLineSeries DataFieldY="thisweekcum" CollectionAlias="In/Conf (Cum.)"> 
							<Settings EnablePointSelection="true" color="#FFB30F" EnableAnimation="false" > 
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
		<div class="col-md-4">
            <div class="card card-info">
              <div class="card-header">
                <h3 class="card-title">
					Weekly GI - 
					<asp:Label id="regid4" runat="server" /> 
				</h3>
                <div class="card-tools">
                  <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                  </button>
                  <button type="button" class="btn btn-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                </div>
              </div>
              <div class="card-body">
				<shield:ShieldChart 
					ID="ShieldChartReg4" runat="server" AutoPostBack="true" Width="100%" Height="350px" 
					OnTakeDataSource="ShieldChartReg4_TakeDataSource"> 
					<PrimaryHeader Text="Gross Income"> 
					</PrimaryHeader> 
					<ExportOptions AllowExportToImage="false" AllowPrint="false" /> 
					<TooltipSettings CustomPointText="{point.dataSeries.collectionAlias}: <b>{point.y}</b>"></TooltipSettings> 
					<Axes> 
						<shield:ChartAxisX CategoricalValuesField="weekend"> 
							<Title Text="Weekending"></Title> 
						</shield:ChartAxisX> 
						<shield:ChartAxisY min=0> 
							<Title Text="Gross Income"></Title> 
						</shield:ChartAxisY> 
					</Axes> 
					<DataSeries> 
						<shield:ChartLineSeries DataFieldY="gi" CollectionAlias="Gross Income"> 
							<Settings EnablePointSelection="true" EnableAnimation="false"> 
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
        <div class="col-md-4">
            <!-- /.card -->
            <div class="card card-info">
              <div class="card-header">
                <h3 class="card-title">Procurement Line - All Time (
				<asp:Label id="pie_regid5" runat="server" /> 				
				)</h3>

                <div class="card-tools">
                  <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                  </button>
                  <button type="button" class="btn btn-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                </div>
              </div>
              <div class="card-body">

				<shield:ShieldChart 
					ID="ShieldChartPie5" runat="server" AutoPostBack="true" Width="100%" Height="350px" OnTakeDataSource="ShieldChartPie5_TakeDataSource"> 
					<PrimaryHeader 
						Text="Procurement Lines"> 
					</PrimaryHeader> 
					<ExportOptions AllowExportToImage="false" AllowPrint="false" /> 
					<TooltipSettings customHeaderText=" " CustomPointText="Income: <b>${point.y}</b>"></TooltipSettings> 
					<Axes> 
						<shield:ChartAxisX CategoricalValuesField="Line"> 
						</shield:ChartAxisX> 
						<shield:ChartAxisY min=0> 
							<Title Text="Procurement Breakdown"></Title> 
						</shield:ChartAxisY> 
					</Axes> 
					<DataSeries> 
						<shield:ChartPieSeries DataFieldY="Sales"> 
							<Settings EnablePointSelection="true" EnableAnimation="false"> 
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
            <div class="card card-info">
              <div class="card-header">
                <h3 class="card-title">
					Daily GI - 
					<asp:Label id="reg7R5" runat="server" /> 
				</h3>
                <div class="card-tools">
                  <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                  </button>
                  <button type="button" class="btn btn-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                </div>
              </div>
              <div class="card-body">
				<shield:ShieldChart 
					ID="ShieldChart7R5" 
					runat="server" AutoPostBack="true" Width="100%" Height="350px" 
					OnTakeDataSource="ShieldChart7R5_TakeDataSource"> 
					<PrimaryHeader Text="Gross Income Daily Graph"> 
					</PrimaryHeader> 
					<ExportOptions AllowExportToImage="false" AllowPrint="false" /> 
					<TooltipSettings CustomPointText="{point.dataSeries.collectionAlias}: <b>{point.y}</b>"></TooltipSettings> 
					<Axes> 
						<shield:ChartAxisX 
							CategoricalValuesField="dayofweek"> 
						</shield:ChartAxisX> 
						<shield:ChartAxisY min=0> 
							<Title Text="Gross Income"></Title> 
						</shield:ChartAxisY> 
					</Axes> 
					<DataSeries> 
						<shield:ChartLineSeries DataFieldY="thisweekinv" CollectionAlias="Invoiced (Daily)"> 
							<Settings seriesDashStyle="Dash" color="#3DA5D9" EnablePointSelection="true" EnableAnimation="false"> 
								<DataPointText BorderWidth=""> 
								</DataPointText> 
								<PointMark Enabled="false"></PointMark>
							</Settings> 
						</shield:ChartLineSeries> 
						<shield:ChartLineSeries DataFieldY="thisweekinvcum" CollectionAlias="Invoiced (Cum.)"> 
							<Settings EnablePointSelection="true" color="#3DA5D9" EnableAnimation="false" > 
								<DataPointText BorderWidth=""> 
								</DataPointText> 
								<PointMark Enabled="false"></PointMark>
							</Settings> 
						</shield:ChartLineSeries> 
						<shield:ChartLineSeries DataFieldY="thisweekgi" CollectionAlias="In/Conf (Daily)"> 
							<Settings seriesDashStyle="Dot" color="#FFB30F" EnablePointSelection="true" EnableAnimation="false"> 
								<DataPointText BorderWidth=""> 
								</DataPointText> 
								<PointMark Enabled="false"></PointMark>
							</Settings> 
						</shield:ChartLineSeries> 
						<shield:ChartLineSeries DataFieldY="thisweekcum" CollectionAlias="In/Conf (Cum.)"> 
							<Settings EnablePointSelection="true" color="#FFB30F" EnableAnimation="false" > 
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
		<div class="col-md-4">
            <div class="card card-info">
              <div class="card-header">
                <h3 class="card-title">
					Weekly GI - 
					<asp:Label id="regid5" runat="server" /> 
				</h3>

                <div class="card-tools">
                  <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                  </button>
                  <button type="button" class="btn btn-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                </div>
              </div>
              <div class="card-body">
				<shield:ShieldChart 
					ID="ShieldChartReg5" runat="server" AutoPostBack="true" Width="100%" Height="350px" 
					OnTakeDataSource="ShieldChartReg5_TakeDataSource"> 
					<PrimaryHeader Text="Gross Income"> 
					</PrimaryHeader> 
					<ExportOptions AllowExportToImage="false" AllowPrint="false" /> 
					<TooltipSettings CustomPointText="{point.dataSeries.collectionAlias}: <b>{point.y}</b>"></TooltipSettings> 
					<Axes> 
						<shield:ChartAxisX CategoricalValuesField="weekend"> 
							<Title Text="Weekending"></Title> 
						</shield:ChartAxisX> 
						<shield:ChartAxisY min=0> 
							<Title Text="Gross Income"></Title> 
						</shield:ChartAxisY> 
					</Axes> 
					<DataSeries> 
						<shield:ChartLineSeries DataFieldY="gi" CollectionAlias="Gross Income"> 
							<Settings EnablePointSelection="true" EnableAnimation="false"> 
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
        <div class="col-md-4">
            <div class="card card-info">
              <div class="card-header">
                <h3 class="card-title">Procurement Line - All Time (
				<asp:Label id="pie_regid6" runat="server" /> 				
				)</h3>

                <div class="card-tools">
                  <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                  </button>
                  <button type="button" class="btn btn-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                </div>
              </div>
              <div class="card-body">

				<shield:ShieldChart 
					ID="ShieldChartPie6" runat="server" AutoPostBack="true" Width="100%" Height="350px" OnTakeDataSource="ShieldChartPie6_TakeDataSource"> 
					<PrimaryHeader 
						Text="Procurement Lines"> 
					</PrimaryHeader> 
					<ExportOptions AllowExportToImage="false" AllowPrint="false" /> 
					<TooltipSettings customHeaderText=" " CustomPointText="Income: <b>${point.y}</b>"></TooltipSettings> 
					<Axes> 
						<shield:ChartAxisX CategoricalValuesField="Line"> 
						</shield:ChartAxisX> 
						<shield:ChartAxisY min=0> 
							<Title Text="Procurement Breakdown"></Title> 
						</shield:ChartAxisY> 
					</Axes> 
					<DataSeries> 
						<shield:ChartPieSeries DataFieldY="Sales"> 
							<Settings EnablePointSelection="true" EnableAnimation="false"> 
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
            <div class="card card-info">
              <div class="card-header">
                <h3 class="card-title">
					Daily GI - 
					<asp:Label id="reg7R6" runat="server" /> 
				</h3>
                <div class="card-tools">
                  <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                  </button>
                  <button type="button" class="btn btn-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                </div>
              </div>
              <div class="card-body">
				<shield:ShieldChart 
					ID="ShieldChart7R6" 
					runat="server" AutoPostBack="true" Width="100%" Height="350px" 
					OnTakeDataSource="ShieldChart7R6_TakeDataSource"> 
					<PrimaryHeader Text="Gross Income Daily Graph"> 
					</PrimaryHeader> 
					<ExportOptions AllowExportToImage="false" AllowPrint="false" /> 
					<TooltipSettings CustomPointText="{point.dataSeries.collectionAlias}: <b>{point.y}</b>"></TooltipSettings> 
					<Axes> 
						<shield:ChartAxisX 
							CategoricalValuesField="dayofweek"> 
						</shield:ChartAxisX> 
						<shield:ChartAxisY min=0> 
							<Title Text="Gross Income"></Title> 
						</shield:ChartAxisY> 
					</Axes> 
					<DataSeries> 
						<shield:ChartLineSeries DataFieldY="thisweekinv" CollectionAlias="Invoiced (Daily)"> 
							<Settings seriesDashStyle="Dash" color="#3DA5D9" EnablePointSelection="true" EnableAnimation="false"> 
								<DataPointText BorderWidth=""> 
								</DataPointText> 
								<PointMark Enabled="false"></PointMark>
							</Settings> 
						</shield:ChartLineSeries> 
						<shield:ChartLineSeries DataFieldY="thisweekinvcum" CollectionAlias="Invoiced (Cum.)"> 
							<Settings EnablePointSelection="true" color="#3DA5D9" EnableAnimation="false" > 
								<DataPointText BorderWidth=""> 
								</DataPointText> 
								<PointMark Enabled="false"></PointMark>
							</Settings> 
						</shield:ChartLineSeries> 
						<shield:ChartLineSeries DataFieldY="thisweekgi" CollectionAlias="In/Conf (Daily)"> 
							<Settings seriesDashStyle="Dot" color="#FFB30F" EnablePointSelection="true" EnableAnimation="false"> 
								<DataPointText BorderWidth=""> 
								</DataPointText> 
								<PointMark Enabled="false"></PointMark>
							</Settings> 
						</shield:ChartLineSeries> 
						<shield:ChartLineSeries DataFieldY="thisweekcum" CollectionAlias="In/Conf (Cum.)"> 
							<Settings EnablePointSelection="true" color="#FFB30F" EnableAnimation="false" > 
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
		<div class="col-md-4">
            <div class="card card-info">
              <div class="card-header">
                <h3 class="card-title">
					Weekly GI - 
					<asp:Label id="regid6" runat="server" /> 
				</h3>

                <div class="card-tools">
                  <button type="button" class="btn btn-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                  </button>
                  <button type="button" class="btn btn-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                </div>
              </div>
              <div class="card-body">
				<shield:ShieldChart 
					ID="ShieldChartReg6" runat="server" AutoPostBack="true" Width="100%" Height="350px" 
					OnTakeDataSource="ShieldChartReg6_TakeDataSource"> 
					<PrimaryHeader Text="Gross Income"> 
					</PrimaryHeader> 
					<ExportOptions AllowExportToImage="false" AllowPrint="false" /> 
					<TooltipSettings CustomPointText="{point.dataSeries.collectionAlias}: <b>{point.y}</b>"></TooltipSettings> 
					<Axes> 
						<shield:ChartAxisX CategoricalValuesField="weekend"> 
							<Title Text="Weekending"></Title> 
						</shield:ChartAxisX> 
						<shield:ChartAxisY min=0> 
							<Title Text="Gross Income"></Title> 
						</shield:ChartAxisY> 
					</Axes> 
					<DataSeries> 
						<shield:ChartLineSeries DataFieldY="gi" CollectionAlias="Gross Income"> 
							<Settings EnablePointSelection="true" EnableAnimation="false"> 
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