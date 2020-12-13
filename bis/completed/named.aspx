<!DOCTYPE html>
<%@ Page Language="C#" Trace="False" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="named.aspx.cs" Inherits="_Default" Debug="true" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"  
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
	<meta charset="utf-8">
	<meta name="viewport" content="width=device-width, initial-scale=1">
	<meta http-equiv="x-ua-compatible" content="ie=edge">

	
	<title>Named</title>

	<link rel="stylesheet" href="../../css/font-awesome.min.css">
	<link rel="stylesheet" href="../../css/adminlte.css">
    <link rel="stylesheet" href="../../css/bootstrap.css" media="screen">
    <link rel="stylesheet" href="../../css/bootstrap-datetimepicker.min.css" media="screen">

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
        <a href="../../bis.aspx" class="nav-link">Back</a>
      </li>
    </ul>
  </nav>
  <!-- /.navbar -->

  <!-- Main Sidebar Container -->
  <aside class="main-sidebar sidebar-light-primary elevation-4">
    <!-- Brand Logo -->
    <a href="index.aspx" class="brand-link">
      <img src="../../img/scn_sm.png" alt="Scn Logo" class="brand-image img-circle elevation-3"
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
            <a href="../../index.aspx" class="nav-link" >
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
				<a href="invoiced.aspx" class="nav-link">
					<i class="fa fa-money nav-icon text-info"></i>
					<p>GI Invoiced</p>
				</a>
			</li>
			<li class="nav-item">
				<a href="confirmed.aspx" class="nav-link">
					<i class="fa fa-money nav-icon nav-icon text-secondary"></i>
					<p>GI Confirmed</p>
				</a>
			</li>
			<li class="nav-item">
				<a href="opencycle.aspx" class="nav-link">
					<i class="fa fa-user-circle nav-icon text-info"></i>
					<p>Open Cycle</p>
				</a>
			</li>			
			<li class="nav-item">
				<a href="now_prospects.aspx" class="nav-link">
					<i class="fa fa-user-circle nav-icon text-info"></i>
					<p>Now Prospects</p>
				</a>
			</li>			
			<li class="nav-item">
				<a href="future_prospects.aspx" class="nav-link">
					<i class="fa fa-user-circle nav-icon text-info"></i>
					<p>Future Prospects</p>
				</a>
			</li>			
            </ul>
          </li>
			<li class="nav-item">
				<a href="archive.aspx" class="nav-link">
					<i class="fa fa-id-card nav-icon text-secondary"></i>
					<p>Archive</p>
				</a>
			</li>	  
          <li class="nav-item has-treeview">
            <a href="#" class="nav-link">
              <i class="nav-icon fa fa-superpowers"></i>
              <p>
                Change Org
                <i class="right fa fa-angle-left"></i>
              </p>
            </a>
            <ul class="nav nav-treeview">
				 <li class="nav-item">
					<button runat="server" id="nday" class="btn btn-block btn-outline-info btn-md" onserverclick="Day_Click" title="Day">
						Day
					</button>							
				</li>
				 <li class="nav-item">
					<button runat="server" id="nfdn" class="btn btn-block btn-outline-info btn-md" onserverclick="Fdn_Click" title="Fdn">
						Fdn
					</button>							
				</li>
				 <li class="nav-item">
					<button runat="server" id="bothOrgs" class="btn btn-block btn-outline-info btn-md" onserverclick="Combined_Click" title="Fdn">
						Combined
					</button>							
				</li>
            </ul>
          </li>

			  <li class="nav-item">
				<a href="../../bis.aspx" class="nav-link">
				  <i class="fa fa-address-card-o nav-icon text-info"></i>
				  <p>BIS Tracking System</p>
				</a>
			  </li>
			  <li class="nav-item has-treeview">
				<a href="#" class="nav-link">
				  <i class="nav-icon fa fa-user-circle text-info"></i>
				  <p>
					Details
					<i class="right fa fa-angle-left"></i>
				  </p>
				</a>
				<ul class="nav nav-treeview">
				<li class="nav-item">
					<a href="intheshop.aspx" class="nav-link">
						<i class="fa fa-user-circle nav-icon text-secondary"></i>
						<p>In The Shop</p>
					</a>
				</li>
				<li class="nav-item">
					<a href="scheduled.aspx" class="nav-link">
						<i class="fa fa-user-circle nav-icon text-secondary"></i>
						<p>Scheduled</p>
					</a>
				</li>
				<li class="nav-item">
					<a href="named.aspx" class="nav-link">
						<i class="fa fa-user-circle nav-icon text-secondary"></i>
						<p>Fallen Off BIS</p>
					</a>
				</li>		
				</ul>
			  </li>				  
          <li class="nav-header"></li>
		  
          <li class="nav-item" >
            <a href="pastInvoices.aspx" class="nav-link" >
              <i class="nav-icon fa fa-money text-info"></i>
              <p>
                Past Invoices 
                <span class="right badge badge-info">new</span>
              </p>
            </a>
          </li>
          <li class="nav-item" >
            <a href="fppp.aspx" class="nav-link" >
              <i class="nav-icon fa fa-user-circle-o text-info"></i>
              <p>
                Fully Partially Paid
                <span class="right badge badge-info">new</span>
              </p>
            </a>
          </li>
          <li class="nav-header">Coming Soon...</li>
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
			<asp:Label id="OrgText" runat="server"/>												
			<asp:Label id="AmountText" runat="server" />
			</h1>
          </div><!-- /.col -->
          <div class="col-sm-6">
            <ol class="breadcrumb float-sm-right">
              <li class="breadcrumb-item"><a href="../../bis.aspx">Home</a></li>
              <li class="breadcrumb-item active">Named</li>
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
			<!-- SEARCH BAR -->
			<div class="row">
				<div class="col-md-6 col-sm-3 col-6">
					<div class="card">		
						<div class="card-body">
							<div class="row">
								<div class="col-3">
									<asp:DropDownList id="ddlSearch" runat="server" CssClass="form-control form-control-md">
										<asp:ListItem Value="name"> Name </asp:ListItem>
										<asp:ListItem Value="area"> Area </asp:ListItem>
										<asp:ListItem Value="service"> Service </asp:ListItem>
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
									<a runat="server" href="./addBISName.aspx" id="btnAddName" class="btn btn-block btn-info btn-md">Add Name</a>
								</div>	
							</div>
						</div>
					</div>
				</div>
				<!-- ARCHIVE FEATURE -->				
			</div>	
			<!-- Named -->
			<div class="row">
				<div class="col-md-12 col-sm-12 col-12">
					<div class="card">		
						<div class="card-body">
							<div id="InvoiceTable" runat="server">
								<div class="row">  
									<div class="col-lg-12 ">  
										<div class="table-responsive">  
											<asp:GridView 
												ID="GridViewNamed" 
												runat="server" 
												OnSorting="TaskGridView_Sorting" 
												AllowPaging="True" 
												OnPageIndexChanging="grdData_PageIndexChanging" 
												PagerStyle-CssClass="pgr"
												PageSize="<%# Convert.ToInt32(ddlPage.Text) %>" 
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
															<asp:TextBox ID="name" AutoPostBack="True" CssClass="col_name" runat="server" Text='<%# Eval("name") %>' Width=150 OnTextChanged="text_change" />
														</ItemTemplate>
														<ControlStyle width="100%" />																
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
													<asp:TemplateField SortExpression="reg" HeaderText="Terminal">
														<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
														<ItemTemplate>
														<asp:TextBox ID="reg" AutoPostBack="True" CssClass="col_med" runat="server" Text='<%# Eval("reg") %>' Width=75 OnTextChanged="text_change" />
														</ItemTemplate>
														<ControlStyle width="100%" />																
													</asp:TemplateField>
													<asp:TemplateField SortExpression="fsm" HeaderText="FSM">
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
													<asp:TemplateField Visible="true" HeaderText="Scheduled">												
														<ItemTemplate>
														<div id="scheduled" style="position: relative;">
															<asp:TextBox ID="scheduled" AutoPostBack="True" CssClass="col_small" runat="server" Text='<%# Eval("scheduled", "{0:M-dd-yyyy h:mm tt}") %>' 
																OnTextChanged="text_change" />
														</div>
														</ItemTemplate>
														<ControlStyle width="100%" />																
													</asp:TemplateField>			
													<asp:TemplateField SortExpression="status" HeaderText="Status" >
														<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
														<ItemTemplate>
														<asp:DropDownList id="ddlStatus" AutoPostBack="True" OnSelectedIndexChanged="Selection_Change_Status" Width=100 runat="server" 
														CssClass="col_med" SelectedValue='<%# Eval("status") %>' TabIndex='<%# TabIndex %>'>
															<asp:ListItem Value="In and Started"> In and Started </asp:ListItem>
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
			<div class="row">
			<asp:Label runat="server" id="ErrorText" Text="" />
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
										<asp:ListItem Value="In and Started"> In and Started </asp:ListItem>
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
							</div>						
							<div class="row">
								<label for="fsmid" class="col-sm-2 control-label">FSM</label>
								<div class="col-sm-6">
									<asp:TextBox ID="fsmid" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
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
</form>

<!-- TEST SCRIPTS	 -->

<script type="text/javascript" src="../../js/jquery-1.8.3.min.js" charset="UTF-8"></script>
<script type="text/javascript" src="../../js/bootstrap.min.js"></script>
<script type="text/javascript" src="../../js/bootstrap-datetimepicker.js" charset="UTF-8"></script>
<script type="text/javascript">
	function openModal() {
		$('[id*=myModal]').modal('show');
	} 
</script>
<script type="text/javascript">
	$('.form_date').datetimepicker({
		daysOfWeekDisabled: [0,1,2,3,5,6],
		daysOfWeekHighlighted: "4",
		format: 'dd-M-yyyy',
		autoclose: 1,
		todayHighlight: 1,
		startView: 2,
        weekStart: 0,
		minView: 2,
		orientation: "bottom"
	});

	$('.form_date_inv').datetimepicker({
		autoclose: 1,
		format: 'dd-M-yyyy',
        weekStart: 0,
		minView: 2,
		startView: 2,
		orientation: "bottom"
	});
	
    $('.appt').datetimepicker({
        //language:  'fr',
        weekStart: 1,
        todayBtn:  1,
		autoclose: 1,
		todayHighlight: 1,
		startView: 2,
		forceParse: 0,
        showMeridian: 1
    });
    $('.form_datetime').datetimepicker({
        //language:  'fr',
        weekStart: 1,
        todayBtn:  1,
		autoclose: 1,
		todayHighlight: 1,
		startView: 2,
		forceParse: 0,
        showMeridian: 1
    });
	
	$('.form_date2').datetimepicker({
        language:  'fr',
        weekStart: 1,
        todayBtn:  1,
		autoclose: 1,
		todayHighlight: 1,
		startView: 2,
		minView: 2,
		forceParse: 0
    });
	$('.form_time').datetimepicker({
        language:  'fr',
        weekStart: 1,
        todayBtn:  1,
		autoclose: 1,
		todayHighlight: 1,
		startView: 1,
		minView: 0,
		maxView: 1,
		forceParse: 0
    });
</script>
	
<!-- REQUIRED SCRIPTS -->
<script src="../../js/jquery.min.js"></script>
<script src="../../js/bootstrap.bundle.min.js"></script>
<script src="../../js/adminlte.js"></script>

<script type="text/javascript">
	$(document).ready(function() {
		$('.alert').delay(200).addClass("in").fadeOut(8000);
	})	
</script>	
</body>
</html>