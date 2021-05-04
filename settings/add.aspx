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

	<link rel="stylesheet" href="../css/font-awesome.min.css">
	<link rel="stylesheet" href="../css/adminlte.css">
	
	<!-- <link rel="stylesheet" href="/css/bootstrap-select.css"> -->
	<!-- <link rel="stylesheet" href="/css/bootstrap-datepicker.css"> -->
	<!-- <link rel="stylesheet" type="text/css" href="css/style.css" /> -->
	<!-- <link rel="stylesheet" href="/css/bootstrap_min.css"> -->
	
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
      <!-- <li class="nav-item d-none d-sm-inline-block"> -->
        <!-- <a href="#" class="nav-link">Contact</a> -->
      <!-- </li> -->
    </ul>

    <!-- SEARCH FORM -->
    <!-- <form class="form-inline ml-3"> -->
	
      <!-- <div class="input-group input-group-sm"> -->
        <!-- <input class="form-control form-control-navbar" type="search" placeholder="Search" aria-label="Search"> -->
        <!-- <div class="input-group-append"> -->
          <!-- <button class="btn btn-navbar" type="submit"> -->
            <!-- <i class="fa fa-search"></i> -->
          <!-- </button> -->
        <!-- </div> -->
      <!-- </div> -->
    <!-- </form> -->

				
	
	
    <!-- Right navbar links -->
   
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
					<a href="invoiced.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-info"></i>
						<p>GI Invoiced</p>
					</a>
				</li>
				<li class="nav-item">
					<a href="confirmed.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-secondary"></i>
						<p>GI Confirmed</p>
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
						<p>Now Prospects</p>
					</a>
				</li>			
				<li class="nav-item">
					<a href="future_prospects.aspx" class="nav-link">
						<i class="fa fa-address-book-o nav-icon text-secondary"></i>
						<p>Future Prospects</p>
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
				<li class="nav-item">
					<a href="archive.aspx" class="nav-link">
						<i class="fa fa-archive nav-icon text-secondary"></i>
						<p>Archive</p>
					</a>
				</li>	  
				<hr>
				<li class="nav-item">
					<a href="../reports/reg.aspx" class="nav-link">
						<i class="fa fa-pie-chart nav-icon text-info"></i>
						<p>Reg Stats</p>
					</a>
				</li>
				<li class="nav-item">
					<a href="../reports/procurement.aspx" class="nav-link">
						<i class="fa fa-pie-chart nav-icon text-info"></i>
						<p>Procurement Lines</p>
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
          </div><!-- /.col -->
          <div class="col-sm-6">
            <ol class="breadcrumb float-sm-right">
              <li class="breadcrumb-item"><a href="./index.aspx">Home</a></li>
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
				<div class="col-md-6 col-sm-6 col-12">
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

<!-- REQUIRED SCRIPTS -->

<!-- <script src="/js/jquery-3.3.1.min.js"></script> -->
<script src="../js/bootstrap.js"></script>
<script type="text/javascript" src="../js/moment.js"></script>
<script type="text/javascript" src="../js/bootstrap-select.js"></script>
<script type="text/javascript" src="../js/bootstrap-datetimepicker.min.js"></script>		
<script type="text/javascript" src="../js/bootstrap-datepicker.js"></script>
<script type="text/javascript">
	$(function () {
		$("[id$=appt]").children().datetimepicker();
	});		
	$(document).ready(function() {
		$('.alert').delay(200).addClass("in").fadeOut(8000);
	})	

</script>


<script src="../js/jquery.min.js"></script>
<script src="../js/bootstrap.bundle.min.js"></script>
<script src="../js/adminlte.js"></script>

	<!-- <br><br><br> -->
<div id="Four" 	runat="server">
</div>
	
		
<asp:Label class="navbar-text pull-left" style="padding-left:15px" Text="GI Grid" runat="server" />
<asp:Label class="navbar-text pull-left" id="HeadText" style="padding-left:15px" runat="server" />
<asp:Label class="navbar-text pull-left" id="OrgText" runat="server" Text="Day" />												
<asp:Label class="navbar-text pull-left" id="AmountText" runat="server" />

	<li>
		<a href="#" class="dropdown-toggle pull-left" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">
			Org <span class="caret"></span>
		</a>
		<ul class="dropdown-menu pull-left">
			<li>
				<asp:Button type="button" class="btn btn-default btn-block" id="nday" text="Day" OnClick="Org_Btn_Click" runat="server" />
				<asp:Button type="button" class="btn btn-default btn-block" id="nfdn" text="Fdn" OnClick="Org_Btn_Click" runat="server" />
			</li>
		</ul>				
	</li>
		
		<div>
			<asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
			<div id="Six" 	runat="server">
				<asp:DropDownList id="ddlPage" runat="server" AutoPostBack="True" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" OnSelectedIndexChanged="Selection_Change_Page" >
					<asp:ListItem Value="10"> 10 </asp:ListItem>
					<asp:ListItem Selected="True" Value="20"> 20 </asp:ListItem>
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
										<label class="col-sm-1 control-label" for="addo_statusid">Status</label>
										<div class="col-sm-3">
											<select class="form-control" name="addo_statusid" id="addo_statusid">
												<option value="Open Cycle"> Open Cycle </option>
												<option value="GI Confirmed"> GI Confirmed </option>
												<option value="GI Invoiced"> GI Invoiced </option>
												<option value="Now Prospect"> Now Prospect </option>
												<option value="Future Prospect"> Future Prospect </option>
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
										<label class="col-sm-1 control-label" for="addo_rankid">Rank</label>
										<div class="col-sm-3">
											<select class="form-control" name="addo_rankid" id="addo_rankid">
												<option value="a">A</option>
												<option value="b">B</option>
												<option value="c">C</option>
											</select>
										</div>											
									</div>		
									<div class="row">
										<label for="fsmid" class="col-sm-2 control-label">FSM</label>
										<div class="col-sm-6">
											<asp:TextBox ID="fsmid" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
										</div>
										<label for="lineid3" class="col-sm-1 control-label">Line</label>
										<div class="col-sm-3">
											<asp:DropDownList id="lineid3" runat="server" CssClass="form-control"
											SelectedValue='<%# Eval("line") %>' TabIndex='<%# TabIndex %>'>
												<asp:ListItem Value=""> - </asp:ListItem>
												<asp:ListItem Value="Arrival"> Arrival </asp:ListItem>
												<asp:ListItem Value="CF"> CF </asp:ListItem>
												<asp:ListItem Value="FSM"> FSM </asp:ListItem>
												<asp:ListItem Value="Prospecting"> Prospecting </asp:ListItem>
												<asp:ListItem Value="Resign"> Resign </asp:ListItem>
												<asp:ListItem Value="Other"> Other </asp:ListItem>
											</asp:DropDownList>
										</div>
									</div>
									<div class="row">
										<label for="addophoneid" class="col-sm-2 control-label">Phone</label>
										<div class="col-sm-6">
											<asp:TextBox ID="addophoneid" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
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
	</form>
</body>
</html>