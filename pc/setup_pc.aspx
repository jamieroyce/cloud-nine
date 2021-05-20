<!DOCTYPE html>
<%@ Page Language="C#" Trace="False" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="setup_pc.aspx.cs" Inherits="_Default" Debug="true" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"  
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
	<meta charset="utf-8">
	<meta name="viewport" content="width=device-width, initial-scale=1">
	<meta http-equiv="x-ua-compatible" content="ie=edge">

	<title>Setup Preclears</title>

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
		<a href="setup_pc.aspx" class="nav-link">	  
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
      <img src="../img/scn_sm.png" alt="Scn Logo" class="brand-image img-circle"
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
				  <p>WDAH Log</p>
				</a>
				<hr>
				<li class="nav-item">
					<a href="setup_pc.aspx" class="nav-link">
						<i class="fa fa-cog nav-icon text-info"></i>
						<p>Setup PC</p>
					</a>
				</li>
				<li class="nav-item">
					<a href="setup_auditor.aspx" class="nav-link">
						<i class="fa fa-cog nav-icon text-info"></i>
						<p>Setup Auditor</p>
					</a>
				</li>
			</li>	
			<hr>    
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
              <li class="breadcrumb-item active">Setup Preclear</li>
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
				<div class="col-12">
					<div class="card">		
						<div class="card-header">
							<h3 class="card-title">
								<i class="fa fa-cog text-info"></i>
								Add/Update PCs
							</h3>
						</div>					
						<div class="card-body">
							<div class="row">
								<div class="col-10">
								</div>
								<div class="col-2">
									<a runat="server" href="add_pc.aspx" id="btnAddPC" class="btn btn-block btn-info btn-md">Add PC</a>
									<br/>
								</div>
							</div>
							<div runat="server">
								<div class="row">  
									<div class="col-lg-12 ">  
										<div class="table-responsive">  
										<asp:GridView 
											ID="GridViewPC" 
											runat="server" 
											AutoGenerateColumns="false" 
											OnSorting="TaskGridView_Sorting" 
											AllowSorting="True" 
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
												<asp:BoundField DataField="org_id" visible="False" HeaderText="Org ID" >
													<HeaderStyle HorizontalAlign="Left" CssClass="col_med" Width="150" />
												</asp:BoundField>
												<asp:TemplateField HeaderText="ID">
													<ItemTemplate>
														<asp:HyperLink runat="server" CssClass="col_med" Text='<%# Eval("Addo_ID") %>' Target="_blank" NavigateUrl='<%# "http://webapp2a.scientology.net/portal/cars/cosCarsPersonReport.jsp?hideLinks=true&addoId="+ Eval("Addo_ID") +"&orgId=" + System.Configuration.ConfigurationManager.AppSettings["orgid"] + "&browser=&userId=allhandsharlem&password=harlembas1"%>' />
													</ItemTemplate>
												</asp:TemplateField>                    																						
												<asp:TemplateField visible="True" HeaderText="Org">
													<ItemTemplate>
														<asp:DropDownList id="ddlOrg" AutoPostBack="True" OnSelectedIndexChanged="Selection_Change_Org_PC" runat="server" 
														CssClass="col_med" SelectedValue='<%# Eval("org") %>' TabIndex='<%# TabIndex %>'>
															<asp:ListItem Value=""> </asp:ListItem>
															<asp:ListItem Value="Day"> Day </asp:ListItem>
															<asp:ListItem Value="Fdn"> Fdn </asp:ListItem>
														</asp:DropDownList>
													</ItemTemplate>
													<ControlStyle Width="100%" />								
												</asp:TemplateField>
												<asp:TemplateField SortExpression="name" HeaderText="Auditor Name">
													<ItemTemplate>
														<asp:TextBox ID="name" AutoPostBack="True" CssClass="col_med" runat="server" Text='<%# Eval("name") %>' Width=100 OnTextChanged="text_change_pc" />
													</ItemTemplate>
												</asp:TemplateField>
												<asp:TemplateField SortExpression="trng_lvl" HeaderText="Training Level">
													<ItemTemplate>
														<asp:TextBox ID="trng_lvl" AutoPostBack="True" CssClass="col_med" runat="server" Text='<%# Eval("trng_lvl") %>' Width=100 OnTextChanged="text_change_pc" />
													</ItemTemplate>
												</asp:TemplateField>
												<asp:TemplateField SortExpression="case_lvl" HeaderText="Case Level">
													<ItemTemplate>
														<asp:TextBox ID="case_lvl" AutoPostBack="True" CssClass="col_med" runat="server" Text='<%# Eval("case_lvl") %>' Width=100 OnTextChanged="text_change_pc" />
													</ItemTemplate>
												</asp:TemplateField>
												<asp:TemplateField SortExpression="languages" HeaderText="Language">
													<ItemTemplate>
														<asp:TextBox ID="languages" AutoPostBack="True" CssClass="col_med" runat="server" Text='<%# Eval("languages") %>' Width=100 OnTextChanged="text_change_pc" />
													</ItemTemplate>
												</asp:TemplateField>
												<asp:TemplateField SortExpression="gak_course_on" HeaderText="GAK Course On">
													<ItemTemplate>
														<asp:TextBox ID="gak_course_on" AutoPostBack="True" CssClass="col_med" runat="server" Text='<%# Eval("gak_course_on") %>' Width=100 OnTextChanged="text_change_pc" />
													</ItemTemplate>
												</asp:TemplateField>
												<asp:TemplateField SortExpression="has_meter" HeaderText="Owns a Meter?">
													<ItemTemplate>
														<asp:TextBox ID="has_meter" AutoPostBack="True" CssClass="col_med" runat="server" Text='<%# Eval("has_meter") %>' Width=100 OnTextChanged="text_change_pc" />
													</ItemTemplate>
												</asp:TemplateField>
												<asp:TemplateField SortExpression="note" HeaderText="Note">
													<ItemTemplate>
														<asp:TextBox ID="note" AutoPostBack="True" CssClass="col_med" runat="server" Text='<%# Eval("note") %>' Width=350 OnTextChanged="text_change_pc" />
													</ItemTemplate>
												</asp:TemplateField>
												<asp:TemplateField>
													<ItemTemplate>
														<asp:LinkButton ID="lnkBtnArchiveEvent" class="btn btn-outline-secondary btn-sm col-12" runat="server" Text="Archive" 
														OnClick="ArchiveEvent"></asp:LinkButton>
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
	<div id="archiveEvent" class="modal fade">
		<script type="text/javascript">
			function ConfirmArchiveEvent() {
				$('[id*=archiveEvent]').modal('show');
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
								<p> Are you <b>sure</b> you want to archive this Event? <br/> This event will no longer be visible in the Event Confirms Log and will only be available in the Archive.</p>
								<asp:TextBox ID="eventarchiveID" type="hidden" runat="server" Text="" ></asp:TextBox>
							<div class="modal-footer">
								<button type="submit" class="btn btn-default" data-dismiss="modal" aria-hidden="true">Close</button>
								<asp:Button ID="btnArchiveEvent" OnClientClick="<% %>" class="btn btn-primary" runat="server" Text="Archive" CommandArgument='<%# Eval("Id") %>' OnCommand="btnArchiveEvent_Click" />
							</div>							
						</div>
					</div>
				</div><!-- /.modal-body -->
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