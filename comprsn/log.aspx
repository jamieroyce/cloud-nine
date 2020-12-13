<!DOCTYPE html>
<%@ Page Language="C#" Trace="False" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="log.aspx.cs" Inherits="_Default" Debug="true" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"  
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<%@ Register Assembly="Shield.Web.UI" Namespace="Shield.Web.UI" TagPrefix="shield" %>
	
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
	<meta charset="utf-8" />
	<meta name="viewport" content="width=device-width, initial-scale=1" />
	<meta http-equiv="x-ua-compatible" content="ie=edge" />
		<meta http-equiv="refresh" content="300" />
	<title>
		Comp Resign Log
	</title>
	<link rel="stylesheet" href="../css/font-awesome.min.css" />
	<link rel="stylesheet" href="../css/adminlte.css" />
	<link rel="stylesheet" href="../css/jquery-ui.css">
	<!-- <link rel="stylesheet" href="../css/bootstrap.css" media="screen" /> -->
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
    </ul>	  
	</nav>
	<!-- /.navbar -->

	<!-- Main Sidebar Container -->
	<aside class="main-sidebar sidebar-light-primary elevation-4">
		<!-- Brand Logo -->
		<a href="../home.aspx" class="brand-link">
		  <img src="../img/scn_sm.png" alt="AdminLTE Logo" class="brand-image img-circle elevation-3" style="opacity: .8">
		  <span class="brand-text font-weight-light">PUBLIC TRACKING</span>
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
					Comp Resign Log
				  </p>
				</a>
				<li class="nav-item">
					<a href="compresign.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-info"></i>
						<p>Comp Resign</p>
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
					<a href="failedresign.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-danger"></i>
						<p>Failed to Resign</p>
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
						<p>Comp Resign Report</p>
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
			  <div class="col-sm-6">
				<h1 class="m-0 text-dark">
					<asp:Label id="HeaderText" runat="server" />								
				</h1>
				<asp:Label visible="false" id="OrgText" runat="server" Text="Combined" />												
			  </div><!-- /.col -->
			  <div class="col-sm-6">
				<ol class="breadcrumb float-sm-right">
				  <li class="breadcrumb-item"><a href="../home.aspx">Home</a></li>
				  <li class="breadcrumb-item active">Comp Resign Log</li>
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
				  <div class="col-md-3 col-sm-6 col-12">
					<div class="small-box bg-info-gradient">
					<!-- <div class="info-box"> -->
					  <div class="info-box-content">
						<span class="info-box-number">
						COMP RESIGN
						</span>
						<span class="info-box-text">
						<h3>
							<asp:Label id="Both" runat="server" /> 
						</h3>
						</span>
						<span class="progress-description">
							<asp:Label id="lblDay" runat="server" /> 
						</span>
						<span class="progress-description">
							<asp:Label id="lblFdn" runat="server" /> 
						</span>
					  </div>
					  <!-- /.info-box-content -->
					</div>
					<!-- /.info-box -->
				  </div>
				  <!-- /.col -->				  	
				  <div class="col-md-3 col-sm-6 col-12">
					<div class="small-box bg-info-gradient">
					  <div class="info-box-content">
						<a href="#named"><font color="white">
						<span class="info-box-number">SCHEDULED</span>
						<span class="info-box-text">
							<h3> 
							<asp:Label id="Sched" runat="server" /> 
							</h3>
						</span>
						<span class="progress-description">
							<asp:Label id="DaySched" runat="server" /> 
						</span>
						<span class="progress-description">
							<asp:Label id="FdnSched" runat="server" /> 
						</span>
						</font>
						</a>
					  </div>
					  <!-- /.info-box-content -->
					</div>
					<!-- /.info-box -->
				  </div>
				  <!-- /.col -->
				  <div class="col-3 col-sm-6 col-md-3">
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
				  <div class="col-3 col-sm-6 col-md-3">
					<div class="small-box bg-danger-gradient">
					  <div class="info-box-content">

						<a href="#fail"><font color="white">
						<span class="info-box-number">FAILED TO RESIGN</span>
						<span class="info-box-text">
							<h3> 
							<asp:Label id="Failed" runat="server" /> 
							</h3>
						</span>
						<span class="progress-description">
							<asp:Label id="DayFailed" runat="server" /> 
						</span>
						<span class="progress-description">
							<asp:Label id="FdnFailed" runat="server" /> 
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
											<asp:ListItem Value="reg"> Terminal </asp:ListItem>
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
				</div>
				<!-- GRID -->
				<div class="row">
				  <div class="col-md-12">
					<div class="card">
					  <div class="card-header">
						<h5 class="card-title">
						Comp Resign
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
										ID="GridViewCompResign" 
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
													<asp:TextBox ID="name" AutoPostBack="False" CssClass="col_name" runat="server" Text='<%# Eval("name") %>' Width=175 OnTextChanged="text_change" />
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
											<asp:TemplateField SortExpression="email" Visible="True" HeaderText="Resign To">
												<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
												<ItemTemplate>
												<asp:TextBox ID="email" AutoPostBack="True" CssClass="col_small" runat="server" Text='<%# Eval("email") %>' Width=100 OnTextChanged="text_change" />
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
											<asp:TemplateField SortExpression="fsm" Visible="True" HeaderText="FSM">
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
											<asp:TemplateField Visible="true" SortExpression="scheduled" HeaderText="Resign Date">												
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
													<asp:ListItem Value="Comp Resign"> Comp Resign </asp:ListItem>
													<asp:ListItem Value="Scheduled"> Scheduled </asp:ListItem>
													<asp:ListItem Value="Named"> Named </asp:ListItem>
													<asp:ListItem Value="Failed Resign"> Failed to Resign </asp:ListItem>
												</asp:DropDownList>
												</ItemTemplate>
												<ControlStyle width="100%" />																
											</asp:TemplateField>
											<asp:TemplateField SortExpression="notes" HeaderText="Notes">
												<HeaderStyle HorizontalAlign="Left" CssClass="col_large" />
												<ItemTemplate>
												<asp:TextBox ID="notes" AutoPostBack="True" CssClass="col_large" runat="server" Text='<%# Eval("notes") %>' Width="300" OnTextChanged="text_change" />
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
													<asp:TextBox ID="name" AutoPostBack="False" CssClass="col_name" runat="server" Text='<%# Eval("name") %>' Width=175 OnTextChanged="text_change" />
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
											<asp:TemplateField SortExpression="email" Visible="True" HeaderText="Resign To">
												<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
												<ItemTemplate>
												<asp:TextBox ID="email" AutoPostBack="True" CssClass="col_small" runat="server" Text='<%# Eval("email") %>' Width=100 OnTextChanged="text_change" />
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
											<asp:TemplateField SortExpression="fsm" Visible="True" HeaderText="FSM">
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
													<asp:TextBox ID="scheduled" AutoPostBack="True" runat="server" class="date_thisweek col_xs_center" Text='<%# Eval("scheduled", "{0:dd-MMM-yyyy}") %>' Width=100 OnTextChanged="text_change_date" ></asp:TextBox>
												</ItemTemplate>														
											</asp:TemplateField>
											<asp:TemplateField SortExpression="status" HeaderText="Status" >
												<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
												<ItemTemplate>
												<asp:DropDownList id="ddlStatus" AutoPostBack="True" OnSelectedIndexChanged="Selection_Change_Status" Width=100 runat="server" 
												CssClass="col_med" SelectedValue='<%# Eval("status") %>' TabIndex='<%# TabIndex %>'>
													<asp:ListItem Value="Comp Resign"> Comp Resign </asp:ListItem>
													<asp:ListItem Value="Scheduled"> Scheduled </asp:ListItem>
													<asp:ListItem Value="Named"> Named </asp:ListItem>
													<asp:ListItem Value="Failed Resign"> Failed to Resign </asp:ListItem>
												</asp:DropDownList>
												</ItemTemplate>
												<ControlStyle width="100%" />																
											</asp:TemplateField>
											<asp:TemplateField SortExpression="notes" HeaderText="Notes">
												<HeaderStyle HorizontalAlign="Left" CssClass="col_large" />
												<ItemTemplate>
												<asp:TextBox ID="notes" AutoPostBack="True" CssClass="col_large" runat="server" Text='<%# Eval("notes") %>' Width="300" OnTextChanged="text_change" />
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
													<asp:TextBox ID="name" AutoPostBack="False" CssClass="col_name" runat="server" Text='<%# Eval("name") %>' Width=175 OnTextChanged="text_change" />
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
											<asp:TemplateField SortExpression="email" Visible="True" HeaderText="Resign To">
												<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
												<ItemTemplate>
												<asp:TextBox ID="email" AutoPostBack="True" CssClass="col_small" runat="server" Text='<%# Eval("email") %>' Width=100 OnTextChanged="text_change" />
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
											<asp:TemplateField SortExpression="fsm" Visible="True" HeaderText="FSM">
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
													<asp:TextBox ID="scheduled" AutoPostBack="True" runat="server" class="date_thisweek col_xs_center" Text='<%# Eval("scheduled", "{0:dd-MMM-yyyy}") %>' Width=100 OnTextChanged="text_change_date" ></asp:TextBox>
												</ItemTemplate>														
											</asp:TemplateField>
											<asp:TemplateField SortExpression="status" HeaderText="Status" >
												<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
												<ItemTemplate>
												<asp:DropDownList id="ddlStatus" AutoPostBack="True" OnSelectedIndexChanged="Selection_Change_Status" Width=100 runat="server" 
												CssClass="col_med" SelectedValue='<%# Eval("status") %>' TabIndex='<%# TabIndex %>'>
													<asp:ListItem Value="Comp Resign"> Comp Resign </asp:ListItem>
													<asp:ListItem Value="Scheduled"> Scheduled </asp:ListItem>
													<asp:ListItem Value="Named"> Named </asp:ListItem>
													<asp:ListItem Value="Failed Resign"> Failed to Resign </asp:ListItem>
												</asp:DropDownList>
												</ItemTemplate>
												<ControlStyle width="100%" />																
											</asp:TemplateField>
											<asp:TemplateField SortExpression="notes" HeaderText="Notes">
												<HeaderStyle HorizontalAlign="Left" CssClass="col_large" />
												<ItemTemplate>
												<asp:TextBox ID="notes" AutoPostBack="True" CssClass="col_large" runat="server" Text='<%# Eval("notes") %>' Width="300" OnTextChanged="text_change" />
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
					<a name="fail">
					  <div class="card-header bg-danger">
						<h5 class="card-title">
						Failed to Resign
						<asp:Label id="LblFailedResign" runat="server" /> 						
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
										ID="GridViewFailedResign" 
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
													<asp:TextBox ID="name" AutoPostBack="False" CssClass="col_name" runat="server" Text='<%# Eval("name") %>' Width=175 OnTextChanged="text_change" />
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
											<asp:TemplateField SortExpression="email" Visible="True" HeaderText="Resign To">
												<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
												<ItemTemplate>
												<asp:TextBox ID="email" AutoPostBack="True" CssClass="col_small" runat="server" Text='<%# Eval("email") %>' Width=100 OnTextChanged="text_change" />
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
											<asp:TemplateField SortExpression="fsm" Visible="True" HeaderText="FSM">
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
													<asp:TextBox ID="scheduled" AutoPostBack="True" runat="server" class="date_thisweek col_xs_center" Text='<%# Eval("scheduled", "{0:dd-MMM-yyyy}") %>' Width=100 OnTextChanged="text_change_date" ></asp:TextBox>
												</ItemTemplate>														
											</asp:TemplateField>
											<asp:TemplateField SortExpression="status" HeaderText="Status" >
												<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
												<ItemTemplate>
												<asp:DropDownList id="ddlStatus" AutoPostBack="True" OnSelectedIndexChanged="Selection_Change_Status" Width=100 runat="server" 
												CssClass="col_med" SelectedValue='<%# Eval("status") %>' TabIndex='<%# TabIndex %>'>
													<asp:ListItem Value="Comp Resign"> Comp Resign </asp:ListItem>
													<asp:ListItem Value="Scheduled"> Scheduled </asp:ListItem>
													<asp:ListItem Value="Named"> Named </asp:ListItem>
													<asp:ListItem Value="Failed Resign"> Failed to Resign </asp:ListItem>
												</asp:DropDownList>
												</ItemTemplate>
												<ControlStyle width="100%" />																
											</asp:TemplateField>
											<asp:TemplateField SortExpression="notes" HeaderText="Notes">
												<HeaderStyle HorizontalAlign="Left" CssClass="col_large" />
												<ItemTemplate>
												<asp:TextBox ID="notes" AutoPostBack="True" CssClass="col_large" runat="server" Text='<%# Eval("notes") %>' Width="300" OnTextChanged="text_change" />
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
				<asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
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
										<asp:ListItem Value="Comp Resign"> Comp Resign </asp:ListItem>
										<asp:ListItem Value="Scheduled"> Scheduled </asp:ListItem>
										<asp:ListItem Value="Named"> Named </asp:ListItem>
										<asp:ListItem Value="Failed Resign"> Failed to Resign </asp:ListItem>
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
								<label for="emailid" class="col-sm-2 control-label">Resigned To</label>
								<div class="col-sm-6">
									<asp:TextBox ID="emailid" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
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
								<label for="regid" class="col-sm-2 control-label">Terminal</label>
								<div class="col-sm-6">
									<asp:TextBox ID="regid" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
								</div>
								<label for="fsmid" class="col-sm-1 control-label">FSM</label>
								<div class="col-sm-3">
									<asp:TextBox ID="fsmid" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
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

	<!-- REQUIRED SCRIPTS -->
	
	<script type="text/javascript" src="../js/jquery-1.12.4.js" charset="UTF-8"></script>
	<script type="text/javascript" src="../js/jquery-ui.js" charset="UTF-8"></script>
	
	<!-- <script type="text/javascript" src="../js/jquery-1.8.3.min.js" charset="UTF-8"></script> -->
	<script type="text/javascript" src="../js/bootstrap.min.js"></script>
	<!-- <script type="text/javascript" src="../js/bootstrap-datetimepicker.js" charset="UTF-8"></script> -->
	<!-- <script src="../js/jquery.min.js"></script> -->
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
									

	