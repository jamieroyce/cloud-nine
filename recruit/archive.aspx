<!DOCTYPE html>
<%@ Page Language="C#" Trace="False" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="archive.aspx.cs" Inherits="_Default" Debug="true" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"  
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
	<meta charset="utf-8">
	<meta name="viewport" content="width=device-width, initial-scale=1">
	<meta http-equiv="x-ua-compatible" content="ie=edge">

	<title>Archive</title>

	<link rel="stylesheet" href="../css/font-awesome.min.css">
	<link rel="stylesheet" href="../css/adminlte.css">
	<link rel="stylesheet" href="../css/jquery-ui.css">

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
	<ul class="navbar-nav ml-auto">
	  <li class="nav-item">
		<a href="archive.aspx" class="nav-link">	  
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
			<li class="nav-item">
				<a href="log.aspx" class="nav-link" >
				  <i class="nav-icon fa fa-address-card text-info"></i>
				  <p>
					Recruitment Log
				  </p>
				</a>
			</li>
			<li class="nav-item">
				<a href="arrived.aspx" class="nav-link">
					<i class="fa fa-address-book-o nav-icon text-info"></i>
					<p>Arrived</p>
				</a>
			</li>
			<li class="nav-item">
				<a href="signed.aspx" class="nav-link">
					<i class="fa fa-address-book-o nav-icon text-secondary"></i>
					<p>Signed</p>
				</a>
			</li>
			<li class="nav-item">
				<a href="prospect.aspx" class="nav-link">
					<i class="fa fa-address-book-o nav-icon text-info"></i>
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
				<a href="graph.aspx" class="nav-link">
					<i class="fa fa-pie-chart nav-icon text-info"></i>
					<p>Recruitment Report</p>
				</a>
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
            <h1 class="m-0 text-dark">
				<asp:Label id="HeaderText" runat="server" />			
				<asp:Label id="AmountText" runat="server" />
			</h1>
			<asp:Label id="OrgText" runat="server"/>												
          </div><!-- /.col -->
          <div class="col-sm-6">
            <ol class="breadcrumb float-sm-right">
              <li class="breadcrumb-item"><a href="../home.aspx">Home</a></li>
              <li class="breadcrumb-item"><a href="log.aspx">Recruitment Log</a></li>
              <li class="breadcrumb-item active">Archive</li>
            </ol>
          </div><!-- /.col -->
        </div><!-- /.row -->
      </div><!-- /.container-fluid -->
    </div>
    <!-- /.content-header -->
	
	<form id="form1" runat="server">
    <!-- Main content -->
    <section class="content">
		<div class="container-fluid">
			<!-- SEARCH -->
			<div class="row">
				<div class="col-8">
					<div class="card">		
						<div class="card-body">
							<div class="row">
								<div class="form-group-md col-3">
									<asp:DropDownList id="ddlSearchArchive" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server">
										<asp:ListItem Value="Name"> Name </asp:ListItem>
										<asp:ListItem Value="Area"> Area </asp:ListItem>
										<asp:ListItem Value="Service"> Service </asp:ListItem>
									</asp:DropDownList> 
								</div>
								<div class="col-3">
									<asp:TextBox ID="txtArchive" 	CssClass="form-control form-control-md" runat="server"></asp:TextBox>
								</div>				
								<div class="col-3">
									<asp:TextBox ID="weText" CssClass="form-control date_we form-control-md" placeholder="Weekending:"  runat="server"></asp:TextBox>
								</div>				
								<div class="col-3">
									<button runat="server" id="btnSearchArchive" class="btn btn-block btn-outline-info btn-md" onserverclick="BtnSearch_Click" title="Search">
										Search
									</button>							
								</div>	
							</div>
						</div>
					</div>
				</div>
			</div>		
			<!-- GRID -->
			<div class="row">
				<div class="col-md-12 col-sm-12 col-12">
					<div class="card">		
						<div class="card-body">
						<div id="addo" 	runat="server">
							<div class="row">  
								<div class="col-lg-12 ">  
									<div class="table-responsive">  
										<asp:GridView 
											ID="GridViewArchive" 
											runat="server" 
											Width="100%" 
											borderwidth="0" 
											GridLines="None"
											OnPageIndexChanging="grdArchiveData_PageIndexChanging" 
											PageSize="<%# Convert.ToInt32(ddlPage.Text) %>" 
											CssClass="mGridAppt"
											AllowPaging="True" 
											AllowSorting="True" 
											OnSorting="TaskGridView_Sorting" 
											PagerStyle-CssClass="pgr"
											AlternatingRowStyle-CssClass="alt"
											EmptyDataText="  (No records were found...)"
											AutoGenerateColumns="False"> 
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
															<asp:HyperLink runat="server" Text='<%# Eval("Addo_ID") %>' Target="_blank" NavigateUrl='<%# "http://webapp2a.scientology.net/portal/cars/cosCarsPersonReport.jsp?hideLinks=true&addoId="+ Eval("Addo_ID") +"&orgId=" + System.Configuration.ConfigurationManager.AppSettings["orgid"] + "&browser=&userId=allhandsharlem&password=harlembas1"%>' />
														</ItemTemplate>
													</asp:TemplateField>                    										
													<asp:TemplateField SortExpression="name" HeaderText="Name">
														<HeaderStyle HorizontalAlign="Left" CssClass="col_name" />
														<ItemTemplate>
															<asp:TextBox ID="name" AutoPostBack="True" CssClass="col_name" runat="server" Text='<%# Eval("name") %>' Width="200px" OnTextChanged="text_change" />
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField SortExpression="reg" HeaderText="Recruiter">
														<HeaderStyle HorizontalAlign="Left" CssClass="col_med no-print" />
														<ItemTemplate>
														<asp:TextBox ID="reg" AutoPostBack="True" CssClass="col_med no-print" runat="server" Text='<%# Eval("reg") %>' Width=75 OnTextChanged="text_change" />
														</ItemTemplate>
														<ControlStyle width="100%" />																
													</asp:TemplateField>
													<asp:TemplateField Visible="true" SortExpression="Scheduled" HeaderText="Scheduled">												
														<HeaderStyle HorizontalAlign="Left" />												
														<ItemTemplate>
															<asp:TextBox ID="Scheduled" AutoPostBack="True" runat="server" class="date_future col_med" Text='<%# Eval("Scheduled", "{0:dd-MMM-yyyy}") %>' Width=150 OnTextChanged="text_change" ></asp:TextBox>
														</ItemTemplate>													
														<ControlStyle width="100%" />																
													</asp:TemplateField>			
													<asp:TemplateField Visible="true" SortExpression="Weekend" HeaderText="Weekend">												
														<HeaderStyle HorizontalAlign="Left" />												
														<ItemTemplate>
															<asp:TextBox ID="Weekend" AutoPostBack="True" runat="server" class="date_we col_med" Text='<%# Eval("Weekend", "{0:dd-MMM-yyyy}") %>' Width=150 OnTextChanged="text_change" ></asp:TextBox>
														</ItemTemplate>													
														<ControlStyle width="100%" />																
													</asp:TemplateField>			
													<asp:TemplateField SortExpression="status" visible="false" HeaderText="Status" >
														<HeaderStyle HorizontalAlign="Left" CssClass="col_med no-print" />
														<ItemTemplate>
														<asp:DropDownList id="ddlStatus" AutoPostBack="True" OnSelectedIndexChanged="Selection_Change_Status" Width=100 runat="server" 
														CssClass="col_med no-print" SelectedValue='<%# Eval("status") %>' TabIndex='<%# TabIndex %>'>
															<asp:ListItem Value="Arrived"> Arrived </asp:ListItem>
															<asp:ListItem Value="Signed"> Signed </asp:ListItem>
															<asp:ListItem Value="Prospect"> Prospect </asp:ListItem>
														</asp:DropDownList>
														</ItemTemplate>
														<ControlStyle width="100%" />																
													</asp:TemplateField>
													<asp:TemplateField visible="True" HeaderText="Category">
														<ItemTemplate>
															<asp:DropDownList id="ddlRegCat" AutoPostBack="True" OnSelectedIndexChanged="Selection_Change" runat="server" 
															CssClass="col_med" SelectedValue='<%# Eval("Reg_Cat_ID") %>' TabIndex='<%# TabIndex %>'>
																<asp:ListItem Value="Recruit"> Recruit </asp:ListItem>
																<asp:ListItem Value="Archive"> Archive </asp:ListItem>
															</asp:DropDownList>
														</ItemTemplate>
														<ControlStyle Width="100%" />								
													</asp:TemplateField>													
													<asp:TemplateField SortExpression="notes" HeaderText="Notes">
														<HeaderStyle HorizontalAlign="Left" CssClass="col_large" />
														<ItemTemplate>
														<asp:TextBox ID="notes" AutoPostBack="True" CssClass="col_large" runat="server" Text='<%# Eval("notes") %>' Width="600" OnTextChanged="text_change" />
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
		</div>
    </section>
  <!-- Control Sidebar -->
  <!-- /.control-sidebar -->
  </div>
<!-- ./wrapper -->

<!-- SCRIPTS	 -->
		<div>
			<asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
		</div>
		<asp:Label runat="server" id="ErrorText" Text="" />
		<asp:Label id="HeadText" runat="server" />			
		
		</div>	
	</form>
	
	<script type="text/javascript" src="../js/jquery-1.12.4.js" charset="UTF-8"></script>
	<script type="text/javascript" src="../js/jquery-ui.js" charset="UTF-8"></script>
	<script type="text/javascript" src="../js/bootstrap.min.js"></script>
	<script src="../js/MaxLength.min.js" type="text/javascript" ></script>
	<script type="text/javascript">
		var dayOfWeek = 4;//thurs
		var date = new Date();
		date.setDate(date.getDate() + (dayOfWeek + 7 - date.getDay()) % 7);		

		var dayOfWeek = 4;//thursday
		var lastdate = new Date();
		lastdate.setDate(lastdate.getDate() - (dayOfWeek + 10 - lastdate.getDay()) % 7);		
		
		$('.date').datepicker({
			dateFormat: "dd-M-yy",
			minDate: lastdate,
			maxDate: date
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
	
</body>
</html>