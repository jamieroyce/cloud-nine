<!DOCTYPE html>
<%@ Page Language="C#" Trace="False" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="add.aspx.cs" Inherits="_Default" Debug="true" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"  
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
	<meta charset="utf-8">
	<meta name="viewport" content="width=device-width, initial-scale=1">
	<meta http-equiv="x-ua-compatible" content="ie=edge">

	<title>Add Name</title>

	<link rel="stylesheet" href="../css/font-awesome.min.css" />
	<link rel="stylesheet" href="../css/adminlte.css" />
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
		<a href="add.aspx" class="nav-link">	  
		  <i class="fa fa-refresh fa-lg text-info" title="Refresh"></i>
		</a>
	  </li>	  
    </ul>
  </nav>

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
					Log
				  </p>
				</a>
				<li class="nav-item">
					<a href="intheshop.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-info"></i>
						<p>In The Shop</p>
					</a>
				</li>
				<li class="nav-item">
					<a href="scheduled.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-secondary"></i>
						<p>Scheduled</p>
					</a>
				</li>
				<li class="nav-item">
					<a href="lastweek.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-warning"></i>
						<p>Last Week BIS</p>
					</a>
				</li>			
				<li class="nav-item">
					<a href="named.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-danger"></i>
						<p>Named/Fallen Off BIS</p>
					</a>
				</li>			
				<li class="nav-item">
					<a href="archive.aspx" class="nav-link">
						<i class="fa fa-archive nav-icon text-secondary"></i>
						<p>Archive</p>
					</a>
				</li>	  
				<li class="nav-item">
					<a href="graph.aspx" class="nav-link">
						<i class="fa fa-pie-chart nav-icon text-info"></i>
						<p>BIS Report</p>
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
            <h1 class="m-0 text-dark">Add Name</h1>
			<asp:Label visible="false" id="OrgText" runat="server"/>												
          </div><!-- /.col -->
          <div class="col-sm-6">
            <ol class="breadcrumb float-sm-right">
              <li class="breadcrumb-item"><a href="../home.aspx">Home</a></li>
              <li class="breadcrumb-item"><a href="log.aspx">BIS Log</a></li>
              <li class="breadcrumb-item active">Add Name</li>
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
			<!-- ADD NAME -->
			<div class="row">
				<div class="col-md-8 col-sm-6 col-12">
					<div class="card">		
						<div class="card-body">
							<div class="row">
								<div class="col-6">
									<asp:TextBox ID="txtAddoSearch" CssClass="form-control form-control-md" runat="server"></asp:TextBox>
								</div>				
								<div class="col-3">
									<button runat="server" id="ButtonAddoSearch" class="btn btn-block btn-outline-info btn-md" onserverclick="BtnSearch_Click_Addo" title="Search">
										Search
									</button>							
								</div>	
								<div class="col-3">
									<button runat="server" id="btnAddNew" class="btn btn-block btn-outline-warning btn-md" onserverclick="OpenAddNew" title="Not in Addo">
										Not in Addo
									</button>							
								</div>	
							</div>
						</div>
					</div>

					<div class="card">		
						<div class="card-body">
						<div id="addo" 	runat="server">
							<div class="row">  
								<div class="col-lg-12 ">  
									<div class="table-responsive">  
										<asp:GridView ID="GridViewAddo" 
											runat="server" 
											Width="100%" 
											borderwidth="0" 
											GridLines="None"
											OnPageIndexChanging="grdData_PageIndexChanging_addo" 
											PageSize="<%# Convert.ToInt32(ddlPage.Text) %>" 
											CssClass="mGridAppt"
											AllowPaging="True" 
											AllowSorting="True" 
											OnSorting="TaskGridView_Sorting" 
											PagerStyle-CssClass="pgr"
											AlternatingRowStyle-CssClass="alt"
											EmptyDataText="  (No records were found...)"
											AutoGenerateColumns="False"> 
											<EmptyDataRowStyle Font-Bold="True"/>											
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
												<asp:BoundField DataField="city" HeaderText="City" SortExpression="city" />					
												<asp:BoundField DataField="phone" HeaderText="Phone" SortExpression="phone" />					
												<asp:TemplateField>
													<ItemTemplate>
														<asp:LinkButton ID="lnkBtnAdd" class="btn btn-outline-info btn-sm col-12" runat="server" Text="Add" OnClick="ViewAddo"></asp:LinkButton>
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
		</div>
    </section>


  <!-- Control Sidebar -->
  <aside class="control-sidebar control-sidebar-dark">
    <!-- Control sidebar content goes here -->
  </aside>
  <!-- /.control-sidebar -->
</div>
<!-- ./wrapper -->

		<div>
			<asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
			<div id="Six" 	runat="server">
				<asp:DropDownList id="ddlPage" runat="server" AutoPostBack="True" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" OnSelectedIndexChanged="Selection_Change_Page" >
					<asp:ListItem Selected="True" Value="10"> 10 </asp:ListItem>
					<asp:ListItem Value="20"> 20 </asp:ListItem>
					<asp:ListItem Value="50"> 50 </asp:ListItem>
					<asp:ListItem Value="100"> 100 </asp:ListItem>
					<asp:ListItem Value="250"> 250 </asp:ListItem>
					<asp:ListItem Value="500"> 500 </asp:ListItem>
				</asp:DropDownList>        
				<asp:Label runat="server" text="Rows Per Page" ID="RowLable" />
				<br />
				<br />					
			</div>
		</div>
			<asp:Label runat="server" id="ErrorText" Text="" />
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
									<label for="addo_statusid" class="col-sm-1 control-label">Status</label>
									<div class="col-sm-3">
										<asp:DropDownList id="addo_statusid" runat="server" CssClass="form-control" 
										SelectedValue='<%# Eval("status") %>' TabIndex='<%# TabIndex %>'>
											<asp:ListItem Value="Named"> Named </asp:ListItem>
											<asp:ListItem Value="Scheduled"> Scheduled </asp:ListItem>
											<asp:ListItem Value="In The Shop"> In The Shop </asp:ListItem>
										</asp:DropDownList>
									</div>
								</div>
								<div class="row">
									<label for="addo_serviceid" class="col-sm-2 control-label">Service</label>
									<div class="col-sm-6">
										<asp:TextBox ID="addo_serviceid" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
									</div>
									<label class="col-sm-1 control-label" for="ddlReg">Area</label>
									<div class="col-sm-3">
										<asp:DropDownList id="ddlReg" name="ddlReg" runat="server" CssClass="form-control"></asp:DropDownList>												
									</div>
								</div>
								<div class="row">
									<label for="apptid3" class="col-sm-2 control-label">Scheduled</label>
									<div class="col-sm-6">
										<asp:TextBox ID="apptid3" runat="server" autocomplete="off" Text="" CssClass="form-control date_future" ></asp:TextBox>
									</div>
									<script type="text/javascript">
										$(function () {
											$('#apptid3').datetimepicker();
										});
									</script>
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
									<label for="addo_serviceid" class="col-sm-2 control-label">Phone</label>
									<div class="col-sm-6">
										<asp:TextBox ID="addo_phoneid" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
									</div>
									<label class="col-sm-1 control-label" for="ddlReg"></label>
									<div class="col-sm-3">
									</div>
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
								<asp:Button ID="btnAddo" OnClientClick="<% %>" class="btn btn-default" runat="server" Text="Save" CommandArgument='<%# Eval("Id") %>' OnCommand="btnAddAddo_Click" />
							</div>							
						</div>
					</div>
				</div><!-- /.modal-body --> 
			</div><!-- /.modal-content -->
		</div><!-- /.modal-dialog -->
	</div>
	<!-- /.modal -->			
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