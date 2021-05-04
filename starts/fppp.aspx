<!DOCTYPE html>
<%@ Page Language="C#" Trace="False" enableEventValidation="false" MaintainScrollPositionOnPostback="false" AutoEventWireup="true" CodeFile="fppp.aspx.cs" Inherits="_Default" Debug="true" %>
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
				  <i class="nav-icon fa fa-address-card text-info"></i>
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
						<span class="info-box-number">STARTED</span>
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
					<div class="small-box bg-dark-gradient">
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
				</div>	
				<div class="row no-print">						
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
					<asp:LinkButton runat="server" CommandArgument="DIV 6" OnClick="LinkButtonArea_Command">					
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
					<asp:LinkButton runat="server" CommandArgument="OTHER" OnClick="LinkButtonArea_Command">					
					<div class="small-box bg-default-gradient">
					  <div id="boxDiv6Training" runat="server" class="info-box-content">
						<span class="info-box-number">OTHER</span>
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
				</div>

		<asp:LinkButton ID="LinkButton1" runat="server"><%=DateTime.Now.ToString() %></asp:LinkButton>
        <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" Height="200px" Width="300px"
            Style="display: none">
            123123312<asp:LinkButton ID="LinkButton2" runat="server">LinkButton</asp:LinkButton>
            <asp:Button ID="Button1" runat="server" Text="Close" />
            <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" Height="200px" Width="300px"
                Style="display: none">
                333333
                <asp:Button ID="Button2" runat="server" Text="Close" />
            </asp:Panel>
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="LinkButton2"
                PopupControlID="Panel2" CancelControlID="Button2" BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>
        </asp:Panel>
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender" runat="server" TargetControlID="LinkButton1"
            PopupControlID="Panel1" CancelControlID="Button1" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>

		
		<asp:UpdatePanel ID="UpdatePanelStarted" runat="server" UpdateMode="Conditional">
		    <ContentTemplate>
				<div class="row">
				  <div class="col-md-12">
					<div class="card">
					  <div class="card-header">
						<h5 class="card-title">
							Starts for this Week
							<asp:Label runat="server" id="lblStart" Text="" /> </br>
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
													<asp:BoundField DataField="org" HeaderText="Org" SortExpression="org" >
														<HeaderStyle HorizontalAlign="Left" CssClass="col_xs" />
													</asp:BoundField>
													<asp:TemplateField HeaderText="ID">
														<ItemTemplate>
															<asp:HyperLink runat="server" CssClass="col_med" Text='<%# Eval("Addo_ID") %>' Target="_blank" NavigateUrl='<%# "http://webapp2a.scientology.net/portal/cars/cosCarsPersonReport.jsp?hideLinks=true&addoId="+ Eval("Addo_ID") +"&orgId=" + System.Configuration.ConfigurationManager.AppSettings["orgid"] + "&browser=&userId=allhandsharlem&password=harlembas1"%>' />
														</ItemTemplate>
													</asp:TemplateField>                    										
													<asp:TemplateField SortExpression="name" HeaderText="Name">
														<HeaderStyle HorizontalAlign="Left" CssClass="col_name" />
														<ItemTemplate>
															<asp:TextBox ID="name" AutoPostBack="True" CssClass="col_name" runat="server" Text='<%# Eval("name") %>' Width=180 OnTextChanged="text_change" />
														</ItemTemplate>					
													</asp:TemplateField>
													<asp:TemplateField SortExpression="service" Visible="true" HeaderText="Service">
														<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
														<ItemTemplate>
															<asp:TextBox ID="service" CssClass="col_med" AutoPostBack="True" runat="server" Text='<%# Eval("service") %>' Width=125 OnTextChanged="text_change" 
																TabIndex='<%# TabIndex %>' />
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField SortExpression="area" Visible="true" HeaderText="Area" >
														<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
														<ItemTemplate>
														<asp:DropDownList id="ddlRank" AutoPostBack="True" OnSelectedIndexChanged="Rank_Change" runat="server" CssClass="col_med" 
														SelectedValue='<%# Eval("area") %>' TabIndex='<%# TabIndex %>'>
														<asp:ListItem Value=""> </asp:ListItem>
														<asp:ListItem Value="DIV6"> DIV6 </asp:ListItem>
														<asp:ListItem Value="Purif"> PURIF </asp:ListItem>
														<asp:ListItem Value="SRD"> SRD </asp:ListItem>
														<asp:ListItem Value="HGC"> HGC </asp:ListItem>
														<asp:ListItem Value="ACAD"> ACAD </asp:ListItem>
														<asp:ListItem Value="GAK"> GAK </asp:ListItem>
														</asp:DropDownList>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField SortExpression="reg" HeaderText="Reg">
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
																<asp:ListItem Value=""></asp:ListItem>
																<asp:ListItem Value="Resign"> Resign </asp:ListItem>
																<asp:ListItem Value="FSM"> FSM </asp:ListItem>
																<asp:ListItem Value="Prospecting"> Prospecting </asp:ListItem>
																<asp:ListItem Value="Arrival"> Arrival </asp:ListItem>
																<asp:ListItem Value="CF"> CF </asp:ListItem>
																<asp:ListItem Value="Other"> Other </asp:ListItem>
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
													<asp:TemplateField Visible="false" >
														<ItemTemplate>
															<asp:LinkButton 
																ID="lnkBtnEdit" Class="fa fa-pencil-square-o" runat="server" style="color:grey" OnClick="Display">
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
					  <div class="card-header">
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
													<asp:BoundField DataField="org" HeaderText="Org" SortExpression="org" >
														<HeaderStyle HorizontalAlign="Left" CssClass="col_xs" />
													</asp:BoundField>
													<asp:TemplateField HeaderText="ID">
														<ItemTemplate>
															<asp:HyperLink runat="server" CssClass="col_med" Text='<%# Eval("Addo_ID") %>' Target="_blank" NavigateUrl='<%# "http://webapp2a.scientology.net/portal/cars/cosCarsPersonReport.jsp?hideLinks=true&addoId="+ Eval("Addo_ID") +"&orgId=" + System.Configuration.ConfigurationManager.AppSettings["orgid"] + "&browser=&userId=allhandsharlem&password=harlembas1"%>' />
														</ItemTemplate>
													</asp:TemplateField>                    										
													<asp:TemplateField SortExpression="name" HeaderText="Name">
														<HeaderStyle HorizontalAlign="Left" CssClass="col_name" />
														<ItemTemplate>
															<asp:TextBox ID="name" AutoPostBack="True" CssClass="col_name" runat="server" Text='<%# Eval("name") %>' Width=180 OnTextChanged="text_change" />
														</ItemTemplate>					
													</asp:TemplateField>
													<asp:TemplateField SortExpression="service" Visible="true" HeaderText="Service">
														<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
														<ItemTemplate>
															<asp:TextBox ID="service" CssClass="col_med" AutoPostBack="True" runat="server" Text='<%# Eval("service") %>' Width=125 OnTextChanged="text_change" 
																TabIndex='<%# TabIndex %>' />
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField SortExpression="area" Visible="true" HeaderText="Area" >
														<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
														<ItemTemplate>
														<asp:DropDownList id="ddlRank" AutoPostBack="True" OnSelectedIndexChanged="Rank_Change" runat="server" CssClass="col_med" 
														SelectedValue='<%# Eval("area") %>' TabIndex='<%# TabIndex %>'>
														<asp:ListItem Value=""> </asp:ListItem>
														<asp:ListItem Value="DIV6"> DIV6 </asp:ListItem>
														<asp:ListItem Value="Purif"> PURIF </asp:ListItem>
														<asp:ListItem Value="SRD"> SRD </asp:ListItem>
														<asp:ListItem Value="HGC"> HGC </asp:ListItem>
														<asp:ListItem Value="ACAD"> ACAD </asp:ListItem>
														<asp:ListItem Value="GAK"> GAK </asp:ListItem>
														</asp:DropDownList>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField SortExpression="reg" HeaderText="Reg">
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
																<asp:ListItem Value=""></asp:ListItem>
																<asp:ListItem Value="Resign"> Resign </asp:ListItem>
																<asp:ListItem Value="FSM"> FSM </asp:ListItem>
																<asp:ListItem Value="Prospecting"> Prospecting </asp:ListItem>
																<asp:ListItem Value="Arrival"> Arrival </asp:ListItem>
																<asp:ListItem Value="CF"> CF </asp:ListItem>
																<asp:ListItem Value="Other"> Other </asp:ListItem>
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
													<asp:TemplateField Visible="false" >
														<ItemTemplate>
															<asp:LinkButton 
																ID="lnkBtnEdit" Class="fa fa-pencil-square-o" runat="server" style="color:grey" OnClick="Display">
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
					  <div class="card-header">
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
													<asp:BoundField DataField="org" HeaderText="Org" SortExpression="org" >
														<HeaderStyle HorizontalAlign="Left" CssClass="col_xs" />
													</asp:BoundField>
													<asp:TemplateField HeaderText="ID">
														<ItemTemplate>
															<asp:HyperLink runat="server" CssClass="col_med" Text='<%# Eval("Addo_ID") %>' Target="_blank" NavigateUrl='<%# "http://webapp2a.scientology.net/portal/cars/cosCarsPersonReport.jsp?hideLinks=true&addoId="+ Eval("Addo_ID") +"&orgId=" + System.Configuration.ConfigurationManager.AppSettings["orgid"] + "&browser=&userId=allhandsharlem&password=harlembas1"%>' />
														</ItemTemplate>
													</asp:TemplateField>                    										
													<asp:TemplateField SortExpression="name" HeaderText="Name">
														<HeaderStyle HorizontalAlign="Left" CssClass="col_name" />
														<ItemTemplate>
															<asp:TextBox ID="name" AutoPostBack="True" CssClass="col_name" runat="server" Text='<%# Eval("name") %>' Width=180 OnTextChanged="text_change" />
														</ItemTemplate>					
													</asp:TemplateField>
													<asp:TemplateField SortExpression="service" Visible="true" HeaderText="Service">
														<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
														<ItemTemplate>
															<asp:TextBox ID="service" CssClass="col_med" AutoPostBack="True" runat="server" Text='<%# Eval("service") %>' Width=125 OnTextChanged="text_change" 
																TabIndex='<%# TabIndex %>' />
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField SortExpression="area" Visible="true" HeaderText="Area" >
														<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
														<ItemTemplate>
														<asp:DropDownList id="ddlRank" AutoPostBack="True" OnSelectedIndexChanged="Rank_Change" runat="server" CssClass="col_med" 
														SelectedValue='<%# Eval("area") %>' TabIndex='<%# TabIndex %>'>
														<asp:ListItem Value=""> </asp:ListItem>
														<asp:ListItem Value="DIV6"> DIV6 </asp:ListItem>
														<asp:ListItem Value="Purif"> PURIF </asp:ListItem>
														<asp:ListItem Value="SRD"> SRD </asp:ListItem>
														<asp:ListItem Value="HGC"> HGC </asp:ListItem>
														<asp:ListItem Value="ACAD"> ACAD </asp:ListItem>
														<asp:ListItem Value="GAK"> GAK </asp:ListItem>
														</asp:DropDownList>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField SortExpression="reg" HeaderText="Reg">
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
																<asp:ListItem Value=""></asp:ListItem>
																<asp:ListItem Value="Resign"> Resign </asp:ListItem>
																<asp:ListItem Value="FSM"> FSM </asp:ListItem>
																<asp:ListItem Value="Prospecting"> Prospecting </asp:ListItem>
																<asp:ListItem Value="Arrival"> Arrival </asp:ListItem>
																<asp:ListItem Value="CF"> CF </asp:ListItem>
																<asp:ListItem Value="Other"> Other </asp:ListItem>
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
													<asp:TemplateField Visible="false" >
														<ItemTemplate>
															<asp:LinkButton 
																ID="lnkBtnEdit" Class="fa fa-pencil-square-o" runat="server" style="color:grey" OnClick="Display">
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
							<div class="card-header">
								<a name="future">
								<h5 class="card-title">
								Future
								<asp:Label id="LblFuture" runat="server" /> 						
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
													<asp:BoundField DataField="org" HeaderText="Org" SortExpression="org" >
														<HeaderStyle HorizontalAlign="Left" CssClass="col_xs" />
													</asp:BoundField>
													<asp:TemplateField HeaderText="ID">
														<ItemTemplate>
															<asp:HyperLink runat="server" CssClass="col_med" Text='<%# Eval("Addo_ID") %>' Target="_blank" NavigateUrl='<%# "http://webapp2a.scientology.net/portal/cars/cosCarsPersonReport.jsp?hideLinks=true&addoId="+ Eval("Addo_ID") +"&orgId=" + System.Configuration.ConfigurationManager.AppSettings["orgid"] + "&browser=&userId=allhandsharlem&password=harlembas1"%>' />
														</ItemTemplate>
													</asp:TemplateField>                    										
													<asp:TemplateField SortExpression="name" HeaderText="Name">
														<HeaderStyle HorizontalAlign="Left" CssClass="col_name" />
														<ItemTemplate>
															<asp:TextBox ID="name" AutoPostBack="True" CssClass="col_name" runat="server" Text='<%# Eval("name") %>' Width=180 OnTextChanged="text_change" />
														</ItemTemplate>					
													</asp:TemplateField>
													<asp:TemplateField SortExpression="service" Visible="true" HeaderText="Service">
														<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
														<ItemTemplate>
															<asp:TextBox ID="service" CssClass="col_med" AutoPostBack="True" runat="server" Text='<%# Eval("service") %>' Width=125 OnTextChanged="text_change" 
																TabIndex='<%# TabIndex %>' />
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField SortExpression="area" Visible="true" HeaderText="Area" >
														<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
														<ItemTemplate>
														<asp:DropDownList id="ddlRank" AutoPostBack="True" OnSelectedIndexChanged="Rank_Change" runat="server" CssClass="col_med" 
														SelectedValue='<%# Eval("area") %>' TabIndex='<%# TabIndex %>'>
														<asp:ListItem Value=""> </asp:ListItem>
														<asp:ListItem Value="DIV6"> DIV6 </asp:ListItem>
														<asp:ListItem Value="Purif"> PURIF </asp:ListItem>
														<asp:ListItem Value="SRD"> SRD </asp:ListItem>
														<asp:ListItem Value="HGC"> HGC </asp:ListItem>
														<asp:ListItem Value="ACAD"> ACAD </asp:ListItem>
														<asp:ListItem Value="GAK"> GAK </asp:ListItem>
														</asp:DropDownList>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField SortExpression="reg" HeaderText="Reg">
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
																<asp:ListItem Value=""></asp:ListItem>
																<asp:ListItem Value="Resign"> Resign </asp:ListItem>
																<asp:ListItem Value="FSM"> FSM </asp:ListItem>
																<asp:ListItem Value="Prospecting"> Prospecting </asp:ListItem>
																<asp:ListItem Value="Arrival"> Arrival </asp:ListItem>
																<asp:ListItem Value="CF"> CF </asp:ListItem>
																<asp:ListItem Value="Other"> Other </asp:ListItem>
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
													<asp:TemplateField Visible="false" >
														<ItemTemplate>
															<asp:LinkButton 
																ID="lnkBtnEdit" Class="fa fa-pencil-square-o" runat="server" style="color:grey" OnClick="Display">
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
							
			<div class="row">
				<div class="col-md-12 col-sm-12 col-12">
					<div class="card">		
						<div class="card-body">
						<div id="pastInvoices" 	runat="server">
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
											OnSorting="TaskGridView_Sorting" 
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
												<asp:BoundField DataField="amt_to_fp" HeaderText="Amount Left" SortExpression="amt_to_fp" />
												<asp:BoundField DataField="phone" HeaderText="Phone" SortExpression="phone" />
												<asp:BoundField DataField="fp" HeaderText="Fully Paid" SortExpression="fp" />
												<asp:BoundField DataField="create_time" HeaderText="Date" SortExpression="create_time" />					
												<asp:TemplateField>
													<ItemTemplate>
														<asp:LinkButton ID="lnkBtnAdd" class="btn btn-outline-info btn-sm col-12 no-print" runat="server" Text="Add To Lineup" OnClick="ViewAddo"></asp:LinkButton>
													</ItemTemplate>						
												</asp:TemplateField>
											</Columns>			
										</asp:GridView>	
									</div>  
								</div>  
							</div>  
						</div>	
						<div class="row">
							<div class="col-1"> 							
								<div id="Six" runat="server">
									<asp:DropDownList id="ddlPage" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" OnSelectedIndexChanged="Selection_Change_Page" >
										<asp:ListItem Value="10"> 10 </asp:ListItem>
										<asp:ListItem Value="20"> 20 </asp:ListItem>
										<asp:ListItem Value="50"> 50 </asp:ListItem>
										<asp:ListItem Value="100"> 100 </asp:ListItem>
										<asp:ListItem Value="250"> 250 </asp:ListItem>
										<asp:ListItem Selected="True" Value="500"> 500 </asp:ListItem>
									</asp:DropDownList>        
									<asp:Label runat="server" CssClass="rows" text="(Rows)" ID="RowLable" />
								</div>
							</div>
						</div>						
						</div>
					</div>
				</div>
			</div>
			<!-- row  -->

			<div class="row">
				<div class="col-12 ">
					<div class="card">		
						<div class="card-body">
							<div class="row">
								<asp:Label runat="server" id="ErrorText" Text="" />
								<asp:Label visible="false" id="HeadText" style="padding-left:15px" runat="server" />
							</div>
						</div>
					</div>
				</div>
			</div>				

		</div>
    </section>
  <!-- Control Sidebar -->
  <aside class="control-sidebar control-sidebar-dark">
    <!-- Control sidebar content goes here -->
  </aside>
  <!-- /.control-sidebar -->
</div>
<!-- ./wrapper -->
<!-- REQUIRED SCRIPTS -->
<script src="../js/jquery.min.js"></script>
<script src="../js/bootstrap.bundle.min.js"></script>
<script src="../js/adminlte.js"></script>
</div>
		<div id="addoModal" class="modal fade">
			<script type="text/javascript">
				function openAddoModal() {
					$('[id*=addoModal]').modal('show');
				} 
			</script>
			<div class="modal-dialog modal-lg" role="document">
				<div class="modal-content">
					<div class="modal-header">
						<button type="button" class="close" data-dismiss="modal">&times;</button>
						<h3 class="modal-title">Add Name</h3>
					</div>
					<div class="modal-body">
						<div class="row">
							<div class="col-md-12">
								<fieldset>
									<div class="row">
										<label for="addonameid" class="col-sm-2 control-label">Name</label>
										<div class="col-sm-6">
											<asp:TextBox ID="addonameid"  runat="server" Text="" CssClass="form-control" ></asp:TextBox>
										</div>
										<label class="col-sm-1 control-label" for="addo_statusid">Status</label>
										<div class="col-sm-3">
											<select class="form-control" name="addo_statusid" id="addo_statusid">
												<option value="This Week"> Definite </option>
												<option value="Possible"> Possible </option>
												<option value="Open Cycle"> Open Cycle </option>
												<option value="Now Prospect"> Prospect </option>
												<option value="GI Confirmed"> Confirmed </option>
												<option value="GI Invoiced"> Invoiced </option>
											</select>
										</div>
									</div>
									<div class="row">
										<label for="addo_serviceid" class="col-sm-2 control-label">Service</label>
										<div class="col-sm-6">
											<asp:TextBox ID="addo_serviceid" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
										</div>
										<label class="col-sm-1 control-label" for="add_amountid">Amount</label>
										<div class="col-sm-3">
											<asp:TextBox ID="add_amountid" name="add_amountid" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
										</div>
									</div>
									<div class="row">
										<label class="col-sm-2 control-label" for="ddlReg_Addo">Main Reg</label>
										<div class="col-sm-6">
											<asp:DropDownList id="ddlReg_Addo" name="ddlReg_Addo" runat="server" CssClass="form-control"></asp:DropDownList>												
										</div>
										<label for="lineid3" class="col-sm-1 control-label">Line</label>
										<div class="col-sm-3">
											<asp:DropDownList id="lineid3" runat="server" CssClass="form-control"
											SelectedValue='<%# Eval("line") %>' TabIndex='<%# TabIndex %>'>
												<asp:ListItem Value="Other"> - </asp:ListItem>
												<asp:ListItem Value="Arrival"> Arrival </asp:ListItem>
												<asp:ListItem Value="CF"> CF </asp:ListItem>
												<asp:ListItem Value="FSM"> FSM </asp:ListItem>
												<asp:ListItem Value="Prospecting"> Prospecting </asp:ListItem>
												<asp:ListItem Value="Resign"> Resign </asp:ListItem>
											</asp:DropDownList>
										</div>
									</div>		
									<div class="row">
										<label for="fsmid" class="col-sm-2 control-label">FSM</label>
										<div class="col-sm-6">
											<asp:TextBox ID="fsmid" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
										</div>
										<label for="addo_orgid" disabled="false" class="col-sm-1 control-label">Org</label>
										<div class="col-sm-3">
											<asp:DropDownList id="addo_orgid" runat="server" CssClass="form-control" 
											SelectedValue='<%# Eval("org") %>' TabIndex='<%# TabIndex %>'>
												<asp:ListItem Value="Day"> Day </asp:ListItem>
												<asp:ListItem Value="Fdn"> Fdn </asp:ListItem>							
											</asp:DropDownList>	
										</div>
										
									</div>
									<div class="row">
									</div>						
									<div class="row">
										<label for="addo_noteid" class="col-sm-2 control-label">Note</label>
										<div class="col-sm-10">
											<asp:TextBox TextMode="MultiLine" Rows="2" ID="addo_noteid" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
										</div>
									</div>
									<div class="row">
										<div class="col-sm-5">
											<asp:TextBox ID="id2" type="hidden" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
										</div>
										<div class="col-sm-5">
											<asp:TextBox ID="addo_addoid" type="hidden" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
										</div>
									</div>		
								</fieldset>
								<div class="modal-footer">
									<asp:Button ID="btnAddo" OnClientClick="<% %>" class="btn btn-outline-info btn-md col-3" runat="server" Text="Save" CommandArgument='<%# Eval("Id") %>' OnCommand="btnAddAddo_Click" />
								</div>							
							</div>
						</div>
					</div><!-- /.modal-body --> 
				</div><!-- /.modal-content -->
			</div><!-- /.modal-dialog -->
		</div><!-- /.modal -->		

			<div id="bisModal" class="modal fade">
				<script type="text/javascript">
					function openBISModal() {
						$('[id*=bisModal]').modal('show');
					} 
				</script>
				<div class="modal-dialog modal-lg" role="document">
					<div class="modal-content">
						<div class="modal-header">
							<button type="button" class="close" data-dismiss="modal">&times;</button>
							<h3 class="modal-title">Add Name</h3>
						</div>
						<div class="modal-body">
							<div class="row">
								<div class="col-md-12">
									<fieldset>
										<div class="row">
											<label for="bis_nameid" class="col-sm-2 control-label">Name</label>
											<div class="col-sm-6">
												<asp:TextBox ID="bis_nameid"  runat="server" Text="" CssClass="form-control" ></asp:TextBox>
											</div>
											<label for="bis_statusid" class="col-sm-1 control-label">Status</label>
											<div class="col-sm-3">
												<asp:DropDownList id="bis_statusid" runat="server" CssClass="form-control" 
												SelectedValue='<%# Eval("status") %>' TabIndex='<%# TabIndex %>'>
													<asp:ListItem Value="Named"> Named </asp:ListItem>
													<asp:ListItem Value="Scheduled"> Scheduled </asp:ListItem>
													<asp:ListItem Value="In The Shop"> In The Shop </asp:ListItem>
												</asp:DropDownList>
											</div>
										</div>
										<div class="row">
											<label for="bis_serviceid" class="col-sm-2 control-label">Service</label>
											<div class="col-sm-6">
												<asp:TextBox ID="bis_serviceid" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
											</div>
											<label class="col-sm-1 control-label" for="ddlBIS">Area</label>
											<div class="col-sm-3">
												<asp:DropDownList id="ddlBIS" name="ddlBIS" runat="server" CssClass="form-control"></asp:DropDownList>												
											</div>
										</div>
										<div class="row">
											<label for="bis_apptid" class="col-sm-2 control-label">Scheduled</label>
											<div class="col-sm-6">
												<asp:TextBox ID="bis_apptid" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
											</div>
											<script type="text/javascript">
												$(function () {
													$('#bis_apptid').datetimepicker();
												});
											</script>
											<label for="bis_lineid" class="col-sm-1 control-label">Line</label>
											<div class="col-sm-3">
												<asp:DropDownList id="bis_lineid" runat="server" CssClass="form-control"
												SelectedValue='<%# Eval("line") %>' TabIndex='<%# TabIndex %>'>
													<asp:ListItem Value="Resign"> Resign </asp:ListItem>
													<asp:ListItem Value="FSM"> FSM </asp:ListItem>
													<asp:ListItem Value="Prospecting"> Prospecting </asp:ListItem>
													<asp:ListItem Value="Arrival"> Arrival </asp:ListItem>
													<asp:ListItem Value="CF"> CF </asp:ListItem>
													<asp:ListItem Value="Other"> Other </asp:ListItem>
												</asp:DropDownList>
											</div>
										</div>						
										<div class="row">
											<label for="bis_regid" class="col-sm-2 control-label">Terminal</label>
											<div class="col-sm-6">
												<asp:TextBox ID="bis_regid" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
											</div>
											<label for="bis_fsmid" class="col-sm-1 control-label">FSM</label>
											<div class="col-sm-3">
												<asp:TextBox ID="bis_fsmid" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
											</div>
										</div>		
										<div class="row">
											<label for="bis_phoneid" class="col-sm-2 control-label">Phone</label>
											<div class="col-sm-6">
												<asp:TextBox ID="bis_phoneid" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
											</div>
											<label for="bis_orgid" disabled="false" class="col-sm-1 control-label">Org</label>
											<div class="col-sm-3">
												<asp:DropDownList id="bis_orgid" runat="server" CssClass="form-control" 
												SelectedValue='<%# Eval("org") %>' TabIndex='<%# TabIndex %>'>
													<asp:ListItem Value="Day"> Day </asp:ListItem>
													<asp:ListItem Value="Fdn"> Fdn </asp:ListItem>							
												</asp:DropDownList>	
											</div>
										</div>						
										<div class="row">
											<label for="bis_email" class="col-sm-2 control-label">Email</label>
											<div class="col-sm-6">
												<asp:TextBox ID="bis_email" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
											</div>
											<label class="col-sm-1 control-label" for="bis_rankid">Rank</label>
											<div class="col-sm-3">
												<select class="form-control" name="bis_rankid" id="bis_rankid">
													<option value=""></option>
													<option value="a">A</option>
													<option value="b">B</option>
													<option value="c">C</option>
												</select>
											</div>											
											
										</div>						
										<div class="row">
											<label for="bis_noteid" class="col-sm-2 control-label">Note</label>
											<div class="col-sm-10">
												<asp:TextBox TextMode="MultiLine" Rows="2" ID="bis_noteid" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
											</div>
										</div>
										<div class="row">
											<div class="col-sm-5">
												<asp:TextBox ID="id3" type="hidden" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
											</div>
											<div class="col-sm-5">
												<asp:TextBox ID="bis_addoid" type="hidden" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
											</div>
										</div>					
									</fieldset>
									<div class="modal-footer">
										<asp:Button ID="btnBIS" OnClientClick="<% %>" class="btn btn-default" runat="server" Text="Save" CommandArgument='<%# Eval("Id") %>' OnCommand="btnAddBIS_Click" />
									</div>							
								</div>
							</div>
						</div><!-- /.modal-body --> 
					</div><!-- /.modal-content -->
				</div><!-- /.modal-dialog -->
			</div><!-- /.modal -->		
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
							<fieldset>
								<div class="row">
									<label for="lblnameid" class="col-sm-2 control-label">Name</label>
									<div class="col-sm-6">
										<asp:TextBox ID="lblnameid" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
									</div>
									<label for="rankid" class="col-sm-1 control-label">Area</label>
									<div class="col-sm-3">
									<asp:DropDownList id="rankid" runat="server" CssClass="form-control"
										SelectedValue='<%# Eval("area") %>' TabIndex='<%# TabIndex %>'>
											<asp:ListItem Value="DIV6"> DIV6 </asp:ListItem>
											<asp:ListItem Value="Purif"> PURIF </asp:ListItem>
											<asp:ListItem Value="SRD"> SRD </asp:ListItem>
											<asp:ListItem Value="HGC"> HGC </asp:ListItem>
											<asp:ListItem Value="ACAD"> ACAD </asp:ListItem>
											<asp:ListItem Value="GAK"> GAK </asp:ListItem>
									</asp:DropDownList>
									</div>
								</div>
								<div class="row">
									<label for="serviceid" class="col-sm-2 control-label">Service</label>
									<div class="col-sm-6">
										<asp:TextBox ID="serviceid" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
									</div>
								</div>
								<div class="row">
									<label for="regid" class="col-sm-2 control-label">Reg</label>
									<div class="col-sm-6">
										<asp:TextBox ID="regid" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
									</div>
									<label for="lineid" class="col-sm-1 control-label">Line</label>
									<div class="col-sm-3">
										<asp:DropDownList id="lineid" runat="server" CssClass="form-control"
										SelectedValue='<%# Eval("line") %>' TabIndex='<%# TabIndex %>'>
											<asp:ListItem Value="Resign"> Resign </asp:ListItem>
											<asp:ListItem Value="FSM"> FSM </asp:ListItem>
											<asp:ListItem Value="Prospecting"> Prospecting </asp:ListItem>
											<asp:ListItem Value="Arrival"> Arrival </asp:ListItem>
											<asp:ListItem Value="CF"> CF </asp:ListItem>
											<asp:ListItem Value="Other"> Other </asp:ListItem>
										</asp:DropDownList>
									</div>
								</div>						
								<div class="row">
									<label for="apptid" class="col-sm-2 control-label">Appointed</label>
									<div class="col-sm-6">
										<asp:TextBox ID="apptid" runat="server" Text="" CssClass="form-control date_future" ></asp:TextBox>
									</div>
									<script type="text/javascript">
										$(function () {
											$('#apptid').datetimepicker();
										});
									</script>
								</div>						
								<div class="row">
									<label for="phoneid" class="col-sm-2 control-label">Phone</label>
									<div class="col-sm-6">
										<asp:TextBox ID="phoneid" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
									</div>
									<label for="statusid" class="col-sm-1 control-label">Status</label>
									<div class="col-sm-3">
										<asp:DropDownList id="statusid" runat="server" CssClass="form-control" 
										SelectedValue='<%# Eval("status") %>' TabIndex='<%# TabIndex %>'>
											<asp:ListItem Value="This Week"> Definite </asp:ListItem>
											<asp:ListItem Value="Possible"> Possible </asp:ListItem>
											<asp:ListItem Value="Open Cycle"> Open Cycle </asp:ListItem>
											<asp:ListItem Value="Now Prospect"> Prospect </asp:ListItem>
											<asp:ListItem Value="GI Confirmed"> GI Confirmed </asp:ListItem>
											<asp:ListItem Value="GI Invoiced"> GI Invoiced </asp:ListItem>
										</asp:DropDownList>
									</div>
								</div>						
								<div class="row">
									<div margin-top:15px>
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
			
							</fieldset>
							<div class="modal-footer">
							</div>							
						</div>
					</div>
				</div><!-- /.modal-body --> 
			</div><!-- /.modal-content -->
		</div><!-- /.modal-dialog -->
	</div><!-- /.modal -->
	
</form>
</body>
</html>