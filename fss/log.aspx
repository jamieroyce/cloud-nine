<!DOCTYPE html>
<%@ Page Language="C#" Trace="False" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="log.aspx.cs" Inherits="_Default" Debug="true" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"  
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Register Assembly="Shield.Web.UI" Namespace="Shield.Web.UI" TagPrefix="shield" %>
	
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
	<meta charset="utf-8" />
	<meta name="viewport" content="width=device-width, initial-scale=1" />
	<meta http-equiv="x-ua-compatible" content="ie=edge" />
		<meta http-equiv="refresh" content="300" />
	<title>
		First Service Starts Log
	</title>
	<link rel="stylesheet" href="../css/font-awesome.min.css">
	<link rel="stylesheet" href="../css/adminlte.css">
	<link rel="stylesheet" href="../css/jquery-ui.css">

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
		  <img src="../img/scn_sm.png" alt="AdminLTE Logo" class="brand-image img-circle elevation-3" style="opacity: .8">
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
					First Service Log
				  </p>
				</a>
				<li class="nav-item">
					<a href="firststarts.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-info"></i>
						<p>Started</p>
					</a>
				</li>
				<li class="nav-item">
					<a href="scheduled.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-secondary"></i>
						<p>Scheduled</p>
					</a>
				</li>
				<li class="nav-item">
					<a href="named.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-secondary"></i>
						<p>Named</p>
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
					<p>First Service Report</p>
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
	<ajaxToolKit:ToolkitScriptManager EnablePartialRendering="true" runat="server" />
	
	<!-- Content Wrapper. Contains page content -->
	<div class="content-wrapper">
		<!-- Content Header (Page header) -->
		<div class="content-header">
		  <div class="container-fluid">
			<div class="row mb-2">
			  <div class="col-sm-6">
				<h1 class="m-0 text-dark">First Service Starts Log</h1>
				<asp:Label visible="false" id="OrgText" runat="server" Text="Combined" />												
			  </div><!-- /.col -->
			  <div class="col-sm-6">
				<ol class="breadcrumb float-sm-right">
				  <li class="breadcrumb-item"><a href="../home.aspx">Home</a></li>
				  <li class="breadcrumb-item active">First Service Starts Log</li>
				</ol>
			  </div><!-- /.col -->
			</div><!-- /.row -->
		  </div><!-- /.container-fluid -->
		</div>
		<!-- /.content-header -->
	
		<!-- Main content -->
		<section class="content">
			<div class="container-fluid">
				<!-- TOTALS -->
				<div class="row">
				  <div class="col-md-4 col-sm-4 col-12">
					<div class="small-box bg-info-gradient">
					<!-- <div class="info-box"> -->
					  <div class="info-box-content">
						<span class="info-box-number">
						FIRST SERVICE STARTS
						</span>
						<span class="info-box-text">
						<h3>
							<asp:Label id="Both" runat="server" /> 
						</h3>
						</span>
						<span class="progress-description">
							<asp:Label id="lblDay" runat="server" /> 
							(Day) 
						</span>
						<span class="progress-description">
							<asp:Label id="lblFdn" runat="server" /> 
							(Fdn)
						</span>
					  </div>
					  <!-- /.info-box-content -->
					</div>
					<!-- /.info-box -->
				  </div>
				  <!-- /.col -->				  	
				  <div class="col-md-4 col-sm-6 col-12">
					<div class="small-box bg-info-gradient">
					  <div class="info-box-content">
						<a href="#scheduled"><font color="white">
						<span class="info-box-number">SCHEDULED</span>
						<span class="info-box-text">
							<h3> 
							<asp:Label id="Sched" runat="server" /> 
							</h3>
						</span>
						<span class="progress-description">
							<asp:Label id="DaySched" runat="server" /> 
							(Day)
						</span>
						<span class="progress-description">
							<asp:Label id="FdnSched" runat="server" /> 
							(Fdn)
						</span>
						</font>
						</a>
					  </div>
					  <!-- /.info-box-content -->
					</div>
					<!-- /.info-box -->
				  </div>
				  <!-- /.col -->
				  <div class="col-4 col-sm-2 col-md-4">
					<div class="small-box bg-info-gradient">
					  <div class="info-box-content">

						<a href="#named"><font color="white">
						<span class="info-box-number">NAMED</span>
						<span class="info-box-text">
							<h3> 
							<asp:Label id="Named" runat="server" /> 
							</h3>
						</span>
						<span class="progress-description">
							<asp:Label id="DayNamed" runat="server" /> 
							(Day)
						</span>
						<span class="progress-description">
							<asp:Label id="FdnNamed" runat="server" /> 
							(Fdn)
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

				<!-- AREA TOTALS-->				
				<div class="row">
				<div class="col-md-1 col-sm-2">
					<div class="small-box bg-default-gradient">
					  <div class="info-box-content">
						<span class="info-box-number">FSM</span>
						<span class="info-box-text">
						<span class="progress-description">
							<asp:Label id="fsmd" runat="server" /> 
							(Day) 
						</span>
						<span class="progress-description">
							<asp:Label id="fsmf" runat="server" /> 
							(Fdn)
						</span>
					</div>
					<!-- /.info-box-content -->
					</div>
					<!-- /.info-box -->
				  </div>
				  <div class="col-md-1 col-sm-2">
					<div class="small-box bg-default-gradient">
					  <div class="info-box-content">
						<span class="info-box-number">BB</span>
						<span class="info-box-text">
						</span>
						<span class="progress-description">
							<asp:Label id="bbd" runat="server" /> 
							(Day) 
						</span>
						<span class="progress-description">
							<asp:Label id="bbf" runat="server" /> 
							(Fdn)
						</span>
					  </div>
					  <!-- /.info-box-content -->
					</div>
					<!-- /.info-box -->
				  </div>
				  <div class="col-md-1 col-sm-2">
					<div class="small-box bg-default-gradient">
					  <div class="info-box-content">
						<span class="info-box-number">Walk In</span>
						<span class="info-box-text">
						</span>
						<span class="progress-description">
							<asp:Label id="wid" runat="server" /> 
							(Day) 
						</span>
						<span class="progress-description">
							<asp:Label id="wif" runat="server" /> 
							(Fdn)
						</span>
					  </div>
					  <!-- /.info-box-content -->
					</div>
					<!-- /.info-box -->
				  </div>
				  <div class="col-md-1 col-sm-2">
					<div class="small-box bg-default-gradient">
					  <div class="info-box-content">
						<span class="info-box-number">4D</span>
						<span class="info-box-text">
						</span>
						<span class="progress-description">
							<asp:Label id="campaignd" runat="server" /> 
							(Day) 
						</span>
						<span class="progress-description">
							<asp:Label id="campaignf" runat="server" /> 
							(Fdn)
						</span>
					  </div>
					  <!-- /.info-box-content -->
					</div>
					<!-- /.info-box -->
				  </div>
				  <div class="col-md-1 col-sm-2">
					<div class="small-box bg-default-gradient">
					  <div class="info-box-content">
						<span class="info-box-number">BR</span>
						<span class="info-box-text">
						</span>
						<span class="progress-description">
							<asp:Label id="brd" runat="server" /> 
							(Day) 
						</span>
						<span class="progress-description">
							<asp:Label id="brf" runat="server" /> 
							(Fdn)
						</span>
					  </div>
					  <!-- /.info-box-content -->
					</div>
					<!-- /.info-box -->
				  </div>
			      <div class="col-md-1 col-sm-2">
					<div class="small-box bg-default-gradient">
					  <div class="info-box-content">
						<span class="info-box-number">Prospecting</span>
						<span class="info-box-text">
						</span>
						<span class="progress-description">
							<asp:Label id="prospectingd" runat="server" /> 
							(Day) 
						</span>
						<span class="progress-description">
							<asp:Label id="prospectingf" runat="server" /> 
							(Fdn)
						</span>
					  </div>
					  <!-- /.info-box-content -->
					</div>
					<!-- /.info-box -->
				  </div>
				  <div class="col-md-1 col-sm-2">
					<div class="small-box bg-default-gradient">
					  <div class="info-box-content">
						<span class="info-box-number">Field Group</span>
						<span class="info-box-text">
						</span>
						<span class="progress-description">
							<asp:Label id="fieldgroupd" runat="server" /> 
							(Day) 
						</span>
						<span class="progress-description">
							<asp:Label id="fieldgroupf" runat="server" /> 
							(Fdn)
						</span>
					  </div>
					  <!-- /.info-box-content -->
					</div>
					<!-- /.info-box -->
				  </div>
				  <div class="col-md-1 col-sm-2">
					<div class="small-box bg-default-gradient">
					  <div class="info-box-content">
						<span class="info-box-number">OCA</span>
						<span class="info-box-text">
						</span>
						<span class="progress-description">
							<asp:Label id="ocad" runat="server" /> 
							(Day) 
						</span>
						<span class="progress-description">
							<asp:Label id="ocaf" runat="server" /> 
							(Fdn)
						</span>
					  </div>
					  <!-- /.info-box-content -->
					</div>
					<!-- /.info-box -->
				  </div>
				  <div class="col-md-1 col-sm-2">
					<div class="small-box bg-default-gradient">
					  <div class="info-box-content">
						<span class="info-box-number">SCN TV</span>
						<span class="info-box-text">
						</span>
						<span class="progress-description">
							<asp:Label id="scntvd" runat="server" /> 
							(Day) 
						</span>
						<span class="progress-description">
							<asp:Label id="scntvf" runat="server" /> 
							(Fdn)
						</span>
					  </div>
					  <!-- /.info-box-content -->
					</div>
					<!-- /.info-box -->
				  </div>
				  <div class="col-md-3 col-sm-2">
					<div class="small-box bg-default-gradient">
					  <div class="info-box-content">
						<span class="info-box-number">Other</span>
						<span class="info-box-text">
						</span>
						<span class="progress-description">
							<asp:Label id="otherd" runat="server" /> 
							(Day) 
						</span>
						<span class="progress-description">
							<asp:Label id="otherf" runat="server" /> 
							(Fdn)
						</span>
					  </div>
					  <!-- /.info-box-content -->
					</div>
					<!-- /.info-box -->
				  </div>				  
				</div>				
				<!-- SEARCH BOX -->
				<div class="row">
					<div class="col-md-6 col-sm-6 col-12">
						<div class="card">		
							<div class="card-body">
								<div class="row">
									<div class="col-3">
										<asp:DropDownList id="ddlSearchGI" runat="server" CssClass="form-control form-control-md">
											<asp:ListItem Value="name"> Name </asp:ListItem>
											<asp:ListItem Value="area"> Source </asp:ListItem>
											<asp:ListItem Value="service"> Service </asp:ListItem>
											<asp:ListItem Value="reg"> Terminal </asp:ListItem>
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
										<a runat="server" href="add.aspx" id="btnAddName" class="btn btn-block btn-info btn-md">Add Name</a>
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
				  <div class="col-md-12">
					<div class="card">
					  <div class="card-header">
						<h5 class="card-title">
						First Service Starts
						<asp:Label id="lblTot" runat="server" /> 						
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
										ID="GridViewInStarted" 
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
													<asp:TextBox ID="name" AutoPostBack="True" CssClass="col_name" runat="server" Text='<%# Eval("name") %>' Width=175 OnTextChanged="text_change" />
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField SortExpression="area" HeaderText="Area">
												<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
												<ItemTemplate>
													<asp:DropDownList id="ddlReg" name="ddlReg" AutoPostBack="True" runat="server" Width=125 OnSelectedIndexChanged="Selection_Change_Reg"
														CssClass="col_med" SelectedValue='<%# Eval("area") %>' >
														<asp:ListItem Value="FSM"> FSM </asp:ListItem>
														<asp:ListItem Value="BB"> Bookbuyer </asp:ListItem>
														<asp:ListItem Value="Walk In"> Walk In </asp:ListItem>
														<asp:ListItem Value="4D Campaign"> 4D Campaign </asp:ListItem>
														<asp:ListItem Value="Body Routing"> Body Routing </asp:ListItem>
														<asp:ListItem Value="Prospecting"> Prospecting </asp:ListItem>
														<asp:ListItem Value="Field Group"> Field Group </asp:ListItem>
														<asp:ListItem Value="OCA"> OCA </asp:ListItem>
														<asp:ListItem Value="SCN TV"> SCN TV </asp:ListItem>
														<asp:ListItem Value="- Other"> Other </asp:ListItem>
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
											<asp:TemplateField SortExpression="fsm" visible="true" HeaderText="FSM">
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
											<asp:TemplateField Visible="true" SortExpression="scheduled" HeaderText="Scheduled">												
												<HeaderStyle HorizontalAlign="Left" />												
												<ItemTemplate>
													<asp:TextBox ID="scheduled" AutoPostBack="True" runat="server" class="date_thisweek col_xs_center" Text='<%# Eval("scheduled", "{0:dd-MMM-yyyy}") %>' Width=100 OnTextChanged="text_change_date" ></asp:TextBox>
												</ItemTemplate>														
											</asp:TemplateField>
											<asp:TemplateField SortExpression="status" HeaderText="Status" >
												<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
												<ItemTemplate>
												<asp:DropDownList id="ddlStatus" AutoPostBack="True" OnSelectedIndexChanged="Selection_Change_Status" Width=100 runat="server" 
												CssClass="col_med" SelectedValue='<%# Eval("status") %>' TabIndex='<%# TabIndex %>'>
													<asp:ListItem Value="First Service"> First Service </asp:ListItem>
													<asp:ListItem Value="Scheduled"> Scheduled </asp:ListItem>
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
													<asp:LinkButton ID="lnkBtnEdit" Class="fa fa-pencil-square-o" runat="server" style="color:grey" OnClick="Display"></asp:LinkButton>
												</ItemTemplate>						
											</asp:TemplateField>
											<asp:TemplateField>
												<ItemTemplate>
													<asp:LinkButton ID="lnkBtnDelete" Class="fa fa-times" style="color:red" runat="server" OnClick="DeleteRow"></asp:LinkButton>
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

				<div class="row">
				  <div class="col-md-12">
					<div class="card">
					<a name="scheduled">					
					  <div class="card-header">
						<h5 class="card-title">
						Scheduled
						<asp:Label id="LblSched" runat="server" /> 						
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
													<asp:TextBox ID="name" AutoPostBack="True" CssClass="col_name" runat="server" Text='<%# Eval("name") %>' Width=175 OnTextChanged="text_change" />
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField SortExpression="area" HeaderText="Area">
												<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
												<ItemTemplate>
													<asp:DropDownList id="ddlReg" name="ddlReg" AutoPostBack="True" runat="server" Width=125 OnSelectedIndexChanged="Selection_Change_Reg"
														CssClass="col_med" SelectedValue='<%# Eval("area") %>' >
														<asp:ListItem Value="FSM"> FSM </asp:ListItem>
														<asp:ListItem Value="BB"> Bookbuyer </asp:ListItem>
														<asp:ListItem Value="Walk In"> Walk In </asp:ListItem>
														<asp:ListItem Value="4D Campaign"> 4D Campaign </asp:ListItem>
														<asp:ListItem Value="Body Routing"> Body Routing </asp:ListItem>
														<asp:ListItem Value="Prospecting"> Prospecting </asp:ListItem>
														<asp:ListItem Value="Field Group"> Field Group </asp:ListItem>
														<asp:ListItem Value="OCA"> OCA </asp:ListItem>
														<asp:ListItem Value="SCN TV"> SCN TV </asp:ListItem>
														<asp:ListItem Value="- Other"> Other </asp:ListItem>
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
											<asp:TemplateField SortExpression="fsm" Visible="true" HeaderText="FSM">
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
											<asp:TemplateField Visible="true" SortExpression="scheduled" HeaderText="Scheduled">												
												<HeaderStyle HorizontalAlign="Left" />												
												<ItemTemplate>
													<asp:TextBox ID="scheduled" AutoPostBack="True" runat="server" class="date_future col_xs_center" Text='<%# Eval("scheduled", "{0:dd-MMM-yyyy}") %>' Width=100 OnTextChanged="text_change_date" ></asp:TextBox>
												</ItemTemplate>														
											</asp:TemplateField>
											<asp:TemplateField SortExpression="status" HeaderText="Status" >
												<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
												<ItemTemplate>
												<asp:DropDownList id="ddlStatus" AutoPostBack="True" OnSelectedIndexChanged="Selection_Change_Status" Width=100 runat="server" 
												CssClass="col_med" SelectedValue='<%# Eval("status") %>' TabIndex='<%# TabIndex %>'>
													<asp:ListItem Value="First Service"> First Service </asp:ListItem>
													<asp:ListItem Value="Scheduled"> Scheduled </asp:ListItem>
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
													<asp:LinkButton ID="lnkBtnEdit" Class="fa fa-pencil-square-o" runat="server" style="color:grey" OnClick="Display"></asp:LinkButton>
												</ItemTemplate>						
											</asp:TemplateField>
											<asp:TemplateField>
												<ItemTemplate>
													<asp:LinkButton ID="lnkBtnDelete" Class="fa fa-times" style="color:red" runat="server" OnClick="DeleteRow"></asp:LinkButton>
												</ItemTemplate>						
											</asp:TemplateField>										
										</Columns>
									</asp:GridView>										
									</div>
								</div>	
							</div>
						</div>
						</a> 						
					  </div>
					</div>
				</div>
				<div class="row">
				  <div class="col-md-12">
					<div class="card">
					<a name="named">
					  <div class="card-header">
						<h5 class="card-title">
						Named
						<asp:Label id="LblNamed" runat="server" /> 						
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
													<asp:TextBox ID="name" AutoPostBack="True" CssClass="col_name" runat="server" Text='<%# Eval("name") %>' Width=175 OnTextChanged="text_change" />
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField SortExpression="area" HeaderText="Area">
												<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
												<ItemTemplate>
													<asp:DropDownList id="ddlReg" name="ddlReg" AutoPostBack="True" runat="server" Width=125 OnSelectedIndexChanged="Selection_Change_Reg"
														CssClass="col_med" SelectedValue='<%# Eval("area") %>' >
														<asp:ListItem Value="FSM"> FSM </asp:ListItem>
														<asp:ListItem Value="BB"> Bookbuyer </asp:ListItem>
														<asp:ListItem Value="Walk In"> Walk In </asp:ListItem>
														<asp:ListItem Value="4D Campaign"> 4D Campaign </asp:ListItem>
														<asp:ListItem Value="Body Routing"> Body Routing </asp:ListItem>
														<asp:ListItem Value="Prospecting"> Prospecting </asp:ListItem>
														<asp:ListItem Value="Field Group"> Field Group </asp:ListItem>
														<asp:ListItem Value="OCA"> OCA </asp:ListItem>
														<asp:ListItem Value="SCN TV"> SCN TV </asp:ListItem>
														<asp:ListItem Value="- Other"> Other </asp:ListItem>
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
												<asp:TextBox ID="reg" AutoPostBack="True" CssClass="col_small" runat="server" Text='<%# Eval("reg") %>' Width=100 OnTextChanged="text_change" />
												</ItemTemplate>
												<ControlStyle width="100%" />																
											</asp:TemplateField>
											<asp:TemplateField SortExpression="fsm" Visible="true" HeaderText="FSM">
												<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
												<ItemTemplate>
												<asp:TextBox ID="fsm" AutoPostBack="True" CssClass="col_small" runat="server" Text='<%# Eval("fsm") %>' Width=100 OnTextChanged="text_change" />
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
											<asp:TemplateField Visible="true" SortExpression="scheduled" HeaderText="Scheduled">												
												<HeaderStyle HorizontalAlign="Left" />												
												<ItemTemplate>
													<asp:TextBox ID="scheduled" AutoPostBack="True" runat="server" class="date_future col_xs_center" Text='<%# Eval("scheduled", "{0:dd-MMM-yyyy}") %>' Width=100 OnTextChanged="text_change_date" ></asp:TextBox>
												</ItemTemplate>														
											</asp:TemplateField>
											<asp:TemplateField SortExpression="status" HeaderText="Status" >
												<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
												<ItemTemplate>
												<asp:DropDownList id="ddlStatus" AutoPostBack="True" OnSelectedIndexChanged="Selection_Change_Status" Width=100 runat="server" 
												CssClass="col_med" SelectedValue='<%# Eval("status") %>' TabIndex='<%# TabIndex %>'>
													<asp:ListItem Value="First Service"> First Service </asp:ListItem>
													<asp:ListItem Value="Scheduled"> Scheduled </asp:ListItem>
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
													<asp:LinkButton ID="lnkBtnEdit" Class="fa fa-pencil-square-o" runat="server" style="color:grey" OnClick="Display"></asp:LinkButton>
												</ItemTemplate>						
											</asp:TemplateField>
											<asp:TemplateField>
												<ItemTemplate>
													<asp:LinkButton ID="lnkBtnDelete" Class="fa fa-times" style="color:red" runat="server" OnClick="DeleteRow"></asp:LinkButton>
												</ItemTemplate>						
											</asp:TemplateField>										
										</Columns>
									</asp:GridView>	
									</div>
								</div>	
							</div>
						</div>
						</a> 
					  </div>
					</div>
				</div>		
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
	<div id="myModal" class="modal fade">
		<script type="text/javascript">
			function openModal() {
				$('[id*=myModal]').modal('show');
			} 
		</script>
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
										<asp:ListItem Value="First Service"> First Service </asp:ListItem>
										<asp:ListItem Value="Scheduled"> Scheduled </asp:ListItem>
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
							<asp:TextBox ID="fsmid" runat="server" Text="" visible="false" CssClass="form-control" ></asp:TextBox>
							<div class="row">
								<label for="notesid" class="col-sm-2 control-label">Note</label>
								<div class="col-sm-10">
									<asp:TextBox TextMode="MultiLine" Rows="3" ID="notesid" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
								</div>
							</div>
							<div class="row">
								<div class="col-sm-10">
									<asp:TextBox ID="id" type="hidden" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
								</div>
							</div>					
							<div class="modal-footer">
								<asp:Button ID="btnUpdate" OnClientClick="<% %>" class="btn btn-default" runat="server" Text="Save" CommandArgument='<%# Eval("Id") %>' OnCommand="btnUpdate_Click" />
							</div>							
						</div>
					</div>
				</div><!-- /.modal-body --> 
			</div><!-- /.modal-content -->
		</div><!-- /.modal-dialog -->
	</div><!-- /.modal -->
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
</div>
<!-- wrapper -->
</body>
</html>									
									

	