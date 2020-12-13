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
              <li class="breadcrumb-item"><a href="log.aspx">First Service Log</a></li>
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
														<HeaderStyle HorizontalAlign="Left" CssClass="col_med no-print" />
														<ItemTemplate>
														<asp:TextBox ID="reg" AutoPostBack="True" CssClass="col_med no-print" runat="server" Text='<%# Eval("reg") %>' Width=75 OnTextChanged="text_change" />
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField SortExpression="fsm" HeaderText="FSM">
														<HeaderStyle HorizontalAlign="Left" CssClass="col_med no-print" />
														<ItemTemplate>
														<asp:TextBox ID="fsm" AutoPostBack="True" CssClass="col_med no-print" runat="server" Text='<%# Eval("fsm") %>' Width=75 OnTextChanged="text_change" />
														</ItemTemplate>
													</asp:TemplateField>			
												<asp:TemplateField SortExpression="scheduled" HeaderText="Date">												
													<ItemTemplate>
														<asp:TextBox ID="scheduled" AutoPostBack="True" CssClass="col_xs_center date_future" runat="server" Text='<%# Eval("scheduled", "{0:dd-MMM-yyyy}") %>' 
															OnTextChanged="text_change" />
													</ItemTemplate>
												</asp:TemplateField>

												<asp:TemplateField SortExpression="weekend" HeaderText="Weekend">												
													<ItemTemplate>
														<asp:TextBox ID="weekend" AutoPostBack="True" CssClass="col_xs_center date_we" runat="server" Text='<%# Eval("weekend", "{0:dd-MMM-yyyy}") %>' 
															OnTextChanged="text_change" />
													</ItemTemplate>
												</asp:TemplateField>
												<asp:TemplateField visible="False" SortExpression="status" HeaderText="Status" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_med no-print" />
													<ItemTemplate>
													<asp:DropDownList id="ddlStatus" AutoPostBack="True" OnSelectedIndexChanged="Selection_Change_Status" Width=100 runat="server" 
													CssClass="col_med no-print" SelectedValue='<%# Eval("status") %>' TabIndex='<%# TabIndex %>'>
														<asp:ListItem Value="First Service"> First Service </asp:ListItem>
														<asp:ListItem Value="Scheduled"> Scheduled </asp:ListItem>
														<asp:ListItem Value="Named"> Named </asp:ListItem>
													</asp:DropDownList>
													</ItemTemplate>
													<ControlStyle Width="100%" />								
												</asp:TemplateField>
												<asp:TemplateField SortExpression="notes" HeaderText="Notes">
													<HeaderStyle HorizontalAlign="Left" CssClass="col_large" />
													<ItemTemplate>
													<asp:TextBox ID="notes" AutoPostBack="True" CssClass="col_large" runat="server" Text='<%# Eval("notes") %>' Width="200" OnTextChanged="text_change" />
													</ItemTemplate>

												</asp:TemplateField>
												<asp:TemplateField visible="True" HeaderText="Category">
													<ItemTemplate>
														<asp:DropDownList id="ddlRegCat" AutoPostBack="True" OnSelectedIndexChanged="Selection_Change" runat="server" 
														CssClass="col_med" SelectedValue='<%# Eval("Reg_Cat_ID") %>' TabIndex='<%# TabIndex %>'>
															<asp:ListItem Value="First Service"> First Service </asp:ListItem>
															<asp:ListItem Value="Archive"> Archive </asp:ListItem>
														</asp:DropDownList>
													</ItemTemplate>
													<ControlStyle Width="100%" />								
												</asp:TemplateField>
												<asp:TemplateField visible="True" HeaderText="Org">
													<ItemTemplate>
														<asp:DropDownList id="ddlOrg" AutoPostBack="True" OnSelectedIndexChanged="Selection_Change_Org" runat="server" 
														CssClass="col_xs" SelectedValue='<%# Eval("org") %>' TabIndex='<%# TabIndex %>'>
															<asp:ListItem Value=""> </asp:ListItem>
															<asp:ListItem Value="Day"> Day </asp:ListItem>
															<asp:ListItem Value="Fdn"> Fdn </asp:ListItem>
														</asp:DropDownList>
													</ItemTemplate>
													<ControlStyle Width="100%" />								
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
	
<div>
	<asp:Label visible="false" id="HeadText" runat="server" />
	<asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
</div>
<asp:Label runat="server" id="ErrorText" Text="" />
</div>
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
							<div class="col-sm-10">
								<asp:TextBox ID="id" type="hidden" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
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
</body>
</html>