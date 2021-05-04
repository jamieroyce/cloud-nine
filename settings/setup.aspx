<!DOCTYPE html>
<%@ Page Language="C#" Trace="False" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="setup.aspx.cs" Inherits="_Default" Debug="true" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"  
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
	<meta charset="utf-8">
	<meta name="viewport" content="width=device-width, initial-scale=1">
	<meta http-equiv="x-ua-compatible" content="ie=edge">

	<title>Settings</title>

	<link rel="stylesheet" href="../css/font-awesome.min.css">
	<link rel="stylesheet" href="../css/adminlte.css">
	<link rel="stylesheet" href="../css/jquery-ui.css">
	<link rel="stylesheet" href="../css/jquery.timepicker.css">

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
		<a href="registrars.aspx" class="nav-link">	  
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
				<asp:Label id="HeadText" runat="server" />			
			</h1>
				<asp:Label id="OrgText" runat="server"/>												
          </div><!-- /.col -->
          <div class="col-sm-6">
            <ol class="breadcrumb float-sm-right">
              <li class="breadcrumb-item"><a href="../home.aspx">Home</a></li>
              <li class="breadcrumb-item active">Settings</li>
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
			<!-- MAIN -->
			<div class="row">
				<div class="col-md-6 col-sm-6">
					<div class="card">		
						<div class="card-header">
							<h3 class="card-title">
								<i class="fa fa-cog text-info"></i>
								Setup Registrars
							</h3>
						</div>					
						<div class="card-body">
							<div class="row">
								<div class="col-10">
									<b>Info:</b> Setup the default Registrars. The first six will be used in the "Registrar Report".
								</div>
								<div class="col-2">
									<button type="button" id="btnAddReg" runat="server" class="btn btn-outline-info btn-block" data-toggle="modal" data-target="#addRegModal">Add</button>												
									<br/>
								</div>
							</div>
							<div id="gridview" 	runat="server">
								<div class="row">  
									<div class="col-lg-12 ">  
										<div class="table-responsive">  
										<asp:GridView 
											ID="GridViewReg" 
											runat="server" 
											AutoGenerateColumns="false" 
											DataKeyNames="id" 
											OnSorting="TaskGridView_Sorting" 
											BorderWidth="0" 
											CellPadding="0" 
											CssClass="mGridAppt"
											GridLines="None" 
											Width="100%"											
											EmptyDataText="No records has been added.">
											<Columns>					
												<asp:BoundField DataField="ID" HeaderText="ID" >
													<HeaderStyle CssClass="no-display"></HeaderStyle>
												<ItemStyle CssClass="no-display"></ItemStyle>
												</asp:BoundField>
												<asp:TemplateField visible="True" HeaderText="RegNo">
													<ItemTemplate>
														<asp:DropDownList id="short_name" AutoPostBack="True" OnSelectedIndexChanged="Selection_Change_Regno" runat="server" 
															CssClass="col_xs" SelectedValue='<%# Eval("short_name") %>' TabIndex='<%# TabIndex %>'>
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
													</ItemTemplate>
													<ControlStyle Width="100%" />								
												</asp:TemplateField>																						
												<asp:TemplateField SortExpression="full_name" HeaderText="First Name">
													<ItemTemplate>
														<asp:TextBox ID="full_name" AutoPostBack="True" CssClass="col_med" runat="server" Text='<%# Eval("full_name") %>' Width=150 OnTextChanged="text_change_reg" />
													</ItemTemplate>
													<ControlStyle Width="100%" />								
												</asp:TemplateField>
												<asp:TemplateField SortExpression="post" visible="True" HeaderText="Org">
													<ItemTemplate>
														<asp:DropDownList id="ddlOrg" AutoPostBack="True" OnSelectedIndexChanged="Selection_Change_Org" runat="server" 
														CssClass="col_xs" SelectedValue='<%# Eval("post") %>' TabIndex='<%# TabIndex %>'>
															<asp:ListItem Value=""> </asp:ListItem>
															<asp:ListItem Value="Day"> Day </asp:ListItem>
															<asp:ListItem Value="Fdn"> Fdn </asp:ListItem>
														</asp:DropDownList>
													</ItemTemplate>
													<ControlStyle Width="100%" />								
												</asp:TemplateField>											
												<asp:TemplateField>
													<ItemTemplate>
														<asp:LinkButton ID="lnkBtnDelete" class="btn btn-outline-danger btn-sm col-12" runat="server" Text="Delete" 
														OnClick="DeleteRow"></asp:LinkButton>
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
			</div>				
			<div class="row">
				<div class="col-10">
					<div class="card">		
						<div class="card-header">
							<h3 class="card-title">
								<i class="fa fa-cog text-info"></i>
								Setup Events
							</h3>
						</div>					
						<div class="card-body">
							<div class="row">
								<div class="col-10">
									<b>Info:</b> Setup the Events here".
								</div>
								<div class="col-2">
									<button type="button" id="btnAddEvent" runat="server" class="btn btn-outline-info btn-block" data-toggle="modal" data-target="#addEventModal">Add</button>												
									<br/>
								</div>
							</div>
							<div runat="server">
								<div class="row">  
									<div class="col-lg-12 ">  
										<div class="table-responsive">  
										<asp:GridView 
											ID="GridViewEvent" 
											runat="server" 
											AutoGenerateColumns="false" 
											OnSorting="TaskGridView_Sorting" 
											BorderWidth="0" 
											CellPadding="0" 
											CssClass="mGridAppt"
											GridLines="None" 
											Width="100%"											
											EmptyDataText="No records.">
											<Columns>					
												<asp:BoundField DataField="ID" HeaderText="ID" >
													<HeaderStyle CssClass="no-display"></HeaderStyle>
												<ItemStyle CssClass="no-display"></ItemStyle>
												</asp:BoundField>
												<asp:TemplateField SortExpression="name" HeaderText="Event Name">
													<ItemTemplate>
														<asp:TextBox ID="name" AutoPostBack="True" CssClass="col_med" runat="server" Text='<%# Eval("name") %>' Width=200 OnTextChanged="text_change" />
													</ItemTemplate>
												</asp:TemplateField>
												<asp:TemplateField SortExpression="event_date" HeaderText="Event Date">
													<ItemTemplate>
														<asp:TextBox ID="event_date" AutoPostBack="True" CssClass="col_xs_center date_future" runat="server" Text='<%# Eval("event_date", "{0:dd-MMM-yyyy}") %>' Width=150 OnTextChanged="text_change" />
													</ItemTemplate>
													<ControlStyle Width="100%" />								
												</asp:TemplateField>
												<asp:TemplateField SortExpression="event_start" HeaderText="Start">
													<ItemTemplate>
														<asp:TextBox ID="event_start" AutoPostBack="True" CssClass="col_xs_center time" runat="server" Text='<%# Eval("event_start") %>' Width=100 OnTextChanged="text_change" />
													</ItemTemplate>
												</asp:TemplateField>
												<asp:TemplateField SortExpression="event_end" HeaderText="End">
													<ItemTemplate>
														<asp:TextBox ID="event_end" AutoPostBack="True" CssClass="col_xs_center time" runat="server" Text='<%# Eval("event_end") %>' Width=100 OnTextChanged="text_change" />
													</ItemTemplate>
												</asp:TemplateField>
												<asp:TemplateField SortExpression="event_location" HeaderText="Location">
													<ItemTemplate>
														<asp:TextBox ID="event_location" AutoPostBack="True" CssClass="col_med" runat="server" Text='<%# Eval("event_location") %>' Width=150 OnTextChanged="text_change" />
													</ItemTemplate>
													<ControlStyle Width="100%" />								
												</asp:TemplateField>
												<asp:TemplateField SortExpression="attendance_quota" HeaderText="Quota">
													<ItemTemplate>
														<asp:TextBox ID="attendance_quota" AutoPostBack="True" CssClass="col_xs_center" runat="server" Text='<%# Eval("attendance_quota") %>' Width=25 OnTextChanged="text_change" />
													</ItemTemplate>
												</asp:TemplateField>
												<asp:TemplateField SortExpression="event_desc" HeaderText="Description">
													<ItemTemplate>
														<asp:TextBox ID="event_desc" AutoPostBack="True" CssClass="col_med" runat="server" Text='<%# Eval("event_desc") %>' Width=300 OnTextChanged="text_change" />
													</ItemTemplate>
												</asp:TemplateField>
												<asp:TemplateField>
													<ItemTemplate>
														<asp:LinkButton ID="lnkBtnDeleteEvent" class="btn btn-outline-danger btn-sm col-12" runat="server" Text="Delete" 
														OnClick="DeleteEvent"></asp:LinkButton>
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
			</div>
			<div class="row">
				<div class="col-md-12">
					<div class="card">		
						<div class="card-body">
							<div class="row">
								<asp:Label runat="server" id="ErrorText" Text="" />
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
		<asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
	</div>
</div>
	<div id="delConfReg" class="modal fade">
		<script type="text/javascript">
			function ConfirmDeleteModal() {
				$('[id*=delConfReg]').modal('show');
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
								<p> Are you <b>sure</b> you want to delete this item from the default list? <br/> It may affect other parts of the system.</p>
								<asp:TextBox ID="RegistrarID" type="hidden" runat="server" Text="" ></asp:TextBox>
							<div class="modal-footer">
								<button type="submit" class="btn btn-default" data-dismiss="modal" aria-hidden="true">Close</button>
								<asp:Button ID="btnDeleteReg" OnClientClick="<% %>" class="btn btn-primary" runat="server" Text="Delete" CommandArgument='<%# Eval("Id") %>' OnCommand="btnDeleteReg_Click" />
							</div>							
						</div>
					</div>
				</div><!-- /.modal-body -->
			</div><!-- /.modal-content -->
		</div><!-- /.modal-dialog -->
	</div><!-- /.modal -->		
	<div id="addRegModal" class="modal fade" tabindex="-1" method="POST" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
		<script type="text/javascript">
			$(document).ready(function () {
				$('#addRegModal').on('hidden.bs.modal', function (e) {
					$('#regnameID').val('');
					$('#shortnameID').val('');
					$('#postID').val('');
				});
			});
		</script>
		<div class="modal-dialog modal-lg" role="document">
			<div class="modal-content">
				<div class="modal-header">
					<button type="button" class="close" data-dismiss="modal">&times;</button>
					<h3 class="modal-title" id="addModalTitle">Add Reg</h3>
				</div>
				<div class="modal-body">
					<div class="row">
						<div class="col-md-12">

								<div class="row" >
									<label class="col-sm-1 control-label" for="textinput">Nbr</label>
									<div class="col-sm-2">
										<asp:DropDownList id="shortnameID" runat="server" CssClass="form-control"
											SelectedValue='<%# Eval("short_name") %>'>
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
									<label class="col-sm-2 control-label" for="textinput">First Name</label>
									<div class="col-sm-4">
										<asp:TextBox ID="regnameID" name="regnameID" runat="server" Text="" CssClass="form-control" style="width=100%" ></asp:TextBox>
									</div>
									<label for="postID" class="col-sm-1 control-label">Org</label>
									<div class="col-sm-2">
										<asp:DropDownList id="postID" runat="server" CssClass="form-control" 
										SelectedValue='<%# Eval("org") %>' TabIndex='<%# TabIndex %>'>
											<asp:ListItem Value="Day"> Day </asp:ListItem>
											<asp:ListItem Value="Fdn"> Fdn </asp:ListItem>							
										</asp:DropDownList>	
									</div>
								</div>

						</div>
					</div>
					<div class="row">
						<div class="col-md-12">
							<br />
						</div>
					</div>					
					<div class="modal-footer">
						<div class="row">
							<div class="col-md-12">
								<asp:Button ID="btnSubmitReg" OnClientClick="<% %>" class="btn btn-primary" runat="server" Text="Save" CommandArgument='<%# Eval("Id") %>' OnCommand="btnAddReg_Click" />
							</div>
						</div>
					</div>
				</div>	
			</div><!-- /.modal-content -->
		</div><!-- /.modal-dialog -->
	</div><!-- /.modal -->	
	<div id="deleteEvent" class="modal fade">
		<script type="text/javascript">
			function ConfirmDeleteEvent() {
				$('[id*=deleteEvent]').modal('show');
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
								<p> Are you <b>sure</b> you want to delete this Event from the list? <br/> It may affect existing confirms.</p>
								<asp:TextBox ID="eventID" type="hidden" runat="server" Text="" ></asp:TextBox>
							<div class="modal-footer">
								<button type="submit" class="btn btn-default" data-dismiss="modal" aria-hidden="true">Close</button>
								<asp:Button ID="btnDeleteEvent" OnClientClick="<% %>" class="btn btn-primary" runat="server" Text="Delete" CommandArgument='<%# Eval("Id") %>' OnCommand="btnDeleteEvent_Click" />
							</div>							
						</div>
					</div>
				</div><!-- /.modal-body -->
			</div><!-- /.modal-content -->
		</div><!-- /.modal-dialog -->
	</div><!-- /.modal -->			
	<div id="addEventModal" class="modal fade" tabindex="-1" method="POST" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
		<script type="text/javascript">
			$(document).ready(function () {
				$('#addEventModal').on('hidden.bs.modal', function (e) {
					$('#postID2').val('');
				});
			});
		</script>
		<div class="modal-dialog modal-lg" role="document">
			<div class="modal-content">
				<div class="modal-header">
					<button type="button" class="close" data-dismiss="modal">&times;</button>
					<h3 class="modal-title" id="addModalTitle">Add Event</h3>
				</div>
				<div class="modal-body">
					<div class="row">
						<div class="col-md-12">

								<div class="row" >
									<label class="col-sm-2 control-label" for="textinput">Event Name</label>
									<div class="col-sm-6">
										<asp:TextBox ID="eventname" name="eventname" runat="server" Text="" CssClass="form-control" style="width=100%" ></asp:TextBox>
									</div>
									<label class="col-sm-1 control-label" for="textinput">Date</label>
									<div class="col-sm-3">
										<asp:TextBox ID="eventdate" name="eventdate" runat="server" Text="" CssClass="form-control date_future" style="width=100%" ></asp:TextBox>
									</div>
								</div>
								<div class="row" >
									<label class="col-sm-2 control-label" for="textinput">Location</label>
									<div class="col-sm-6">
										<asp:TextBox ID="eventloc" name="eventloc" runat="server" Text="" CssClass="form-control" style="width=100%" ></asp:TextBox>
									</div>
									<label class="col-sm-1 control-label" for="textinput">Start</label>
									<div class="col-sm-3">
										<asp:TextBox ID="starttime" name="starttime" runat="server" Text="" CssClass="form-control time" style="width=100%" ></asp:TextBox>
									</div>
								</div>
								<div class="row" >
									<label class="col-sm-2 control-label" for="textinput">Description</label>
									<div class="col-sm-6">
										<asp:TextBox ID="eventdesc" name="eventdesc" runat="server" Text="" CssClass="form-control" style="width=100%" ></asp:TextBox>
									</div>
									<label class="col-sm-1 control-label" for="textinput">End</label>
									<div class="col-sm-3">
										<asp:TextBox ID="endtime" name="endtime" runat="server" Text="" CssClass="form-control time" style="width=100%" ></asp:TextBox>
									</div>
								</div>
								<div class="row" >
									<label for="note" class="col-sm-2 control-label">Note</label>
									<div class="col-sm-6">
										<asp:TextBox TextMode="MultiLine" Rows="2" ID="note" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
									</div>
									<label class="col-sm-1 control-label" for="textinput">Quota</label>
									<div class="col-sm-3">
										<asp:TextBox ID="quota" name="quota" runat="server" Text="" CssClass="form-control" style="width=100%" ></asp:TextBox>
									</div>
								</div>

						</div>
					</div>
					<div class="row">
						<div class="col-md-12">
							<br />
						</div>
					</div>					
					<div class="modal-footer">
						<div class="row">
							<div class="col-md-12">
								<asp:Button ID="btnSubmitEvent" OnClientClick="<% %>" class="btn btn-primary" runat="server" Text="Save" CommandArgument='<%# Eval("Id") %>' OnCommand="btnAddEvent_Click" />
							</div>
						</div>
					</div>
				</div>	
			</div><!-- /.modal-content -->
		</div><!-- /.modal-dialog -->
	</div><!-- /.modal -->		
	<script type="text/javascript" src="../js/jquery-1.12.4.js" charset="UTF-8"></script>
	<script type="text/javascript" src="../js/jquery-ui.js" charset="UTF-8"></script>
	<script type="text/javascript" src="../js/jquery.timepicker.js"></script>
	
	<script type="text/javascript" src="../js/bootstrap.min.js"></script>
	<script src="../js/MaxLength.min.js" type="text/javascript" ></script>
	<script>
	<!-- $(document).ready(function(){ -->
		<!-- $( '.time' ).timepicker({ -->
			<!-- 'minTime': '9:00am', -->
			<!-- 'maxTime': '11:30pm' -->
		<!-- });			 -->
	<!-- }); -->

	
	$('#starttime').timepicker({
		'minTime': '9:00am',
		'maxTime': '11:30pm'
	});

	$('#endtime').timepicker({
		'minTime': '9:00am',
		'maxTime': '11:30pm'
	});

	$(function () {
		$( '.time' ).timepicker({
			'minTime': '9:00am',
			'maxTime': '11:30pm'
		});			
	});
	
	</script>
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