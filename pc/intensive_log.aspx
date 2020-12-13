<!DOCTYPE html>
<%@ Page Language="C#" Trace="False" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="intensive_log.aspx.cs" Inherits="_Default" Debug="true" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"  
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
	<meta charset="utf-8">
	<meta name="viewport" content="width=device-width, initial-scale=1">
	<meta http-equiv="x-ua-compatible" content="ie=edge">

	<title>Log Intensive</title>

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
		<a href="intensive_log.aspx" class="nav-link">	  
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
				<li class="nav-item">
					<a href="intensive_log.aspx" class="nav-link">
						<i class="fa fa-cog nav-icon text-info"></i>
						<p>Log Intensive</p>
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
          <div class="col-sm-8">
            <h1 class="m-0 text-dark">
				<asp:Label id="HeaderText" runat="server" />
			</h1>
				<asp:Label id="OrgText" runat="server"/>												
          </div><!-- /.col -->
          <div class="col-sm-4">
            <ol class="breadcrumb float-sm-right">
              <li class="breadcrumb-item"><a href="../home.aspx">Home</a></li>
              <li class="breadcrumb-item active">Log Intensive</li>
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
			<div class="row">
				<div class="col-md-4 col-12">
					<div class="card">		
						<div class="card-body">
							<div class="row">
								<div class="col-12">
									<asp:DropDownList id="ddl_pc" name="ddl_pc" runat="server" AutoPostBack="True" OnSelectedIndexChanged="OnSelectedIndexChanged" CssClass="form-control"></asp:DropDownList>												
								</div>	
							</div>
						</div>
					</div>
				<!-- /.col -->		
				</div>
			<!-- /.col -->		
			</div>	
			<div id="pcdata" runat="server">
				<div class="row">
					<div class="col-12">
						<div class="card">		
							<div class="card-header">
								<h3 class="card-title">
									<i class="fa fa-cog text-info"></i>
									PC Intensives
								</h3>
							</div>	
							<div class="card-body">
								<div class="row">
									<div class="col-2">
										<button runat="server" id="btnAddIntensive" class="btn btn-block btn-outline-info btn-md" onserverclick="OpenAddIntensive" title="Log Intensive">
											Log Intensive
										</button>	
										</br>									
									</div>
									<div class="col-10">
									</div>
								</div>
								<div runat="server">
									<div class="row">  
										<div class="col-lg-12 ">  
											<div class="table-responsive">  
											<asp:GridView 
												ID="GridViewIntensive" 
												runat="server" 
												AutoGenerateColumns="false" 
												OnSorting="TaskGridView_Sorting" 
												BorderWidth="0" 
												CellPadding="0" 
												AllowSorting="True" 
												CssClass="mGridAppt"
												GridLines="None" 
												Width="100%"											
												EmptyDataText="No records.">
												<Columns>					
													<asp:BoundField DataField="ID" HeaderText="ID" >
														<HeaderStyle CssClass="no-display"></HeaderStyle>
														<ItemStyle CssClass="no-display"></ItemStyle>
													</asp:BoundField>
													<asp:BoundField DataField="pc_id" visible="False" HeaderText="PC_ID" >
														<HeaderStyle HorizontalAlign="Left" CssClass="col_med" Width="150" />
													</asp:BoundField>
													<asp:BoundField DataField="org" visible="False" HeaderText="Org" >
														<HeaderStyle HorizontalAlign="Left" CssClass="col_med" Width="150" />
													</asp:BoundField>
													<asp:TemplateField SortExpression="inv_nbr" HeaderText="Invoice Nbr">
														<ItemTemplate>
															<asp:TextBox ID="inv_nbr" AutoPostBack="True" CssClass="col_med" runat="server" Text='<%# Eval("inv_nbr") %>' Width=100 OnTextChanged="text_change_auditor" />
														</ItemTemplate>
														<ControlStyle width="100%" />																
													</asp:TemplateField>
													<asp:TemplateField SortExpression="vsd_value" HeaderText="VSD Value">
														<ItemTemplate>
															<asp:TextBox ID="vsd_value" AutoPostBack="True" CssClass="col_med" runat="server" Text='<%# Eval("vsd_value") %>' Width=100 OnTextChanged="text_change_auditor" />
														</ItemTemplate>
														<ControlStyle width="100%" />																
													</asp:TemplateField>
													<asp:TemplateField SortExpression="vsd_counted" HeaderText="VSD Counted">
														<ItemTemplate>
															<asp:TextBox ID="vsd_counted" AutoPostBack="True" CssClass="col_med" runat="server" Text='<%# Eval("vsd_counted") %>' Width=100 OnTextChanged="text_change_auditor" />
														</ItemTemplate>
														<ControlStyle width="100%" />																
													</asp:TemplateField>
													<asp:TemplateField Visible="true" SortExpression="date_started" HeaderText="Date Started">												
														<HeaderStyle HorizontalAlign="Left" />												
														<ItemTemplate>
															<asp:TextBox ID="date_started" AutoPostBack="True" runat="server" class="date_future col_med" Text='<%# Eval("date_started", "{0:dd-MMM-yyyy}") %>' Width=100 OnTextChanged="text_change_auditor" ></asp:TextBox>
														</ItemTemplate>
														<ControlStyle width="100%" />																
													</asp:TemplateField>			
													<asp:TemplateField Visible="true" SortExpression="we_started" HeaderText="WE Started">												
														<HeaderStyle HorizontalAlign="Left" />												
														<ItemTemplate>
															<asp:TextBox ID="we_started" AutoPostBack="True" runat="server" class="date_future col_med" Text='<%# Eval("we_started", "{0:dd-MMM-yyyy}") %>' Width=100 OnTextChanged="text_change_auditor" ></asp:TextBox>
														</ItemTemplate>
														<ControlStyle width="100%" />																
													</asp:TemplateField>			
													<asp:TemplateField Visible="true" SortExpression="we_completed" HeaderText="WE Completed">												
														<HeaderStyle HorizontalAlign="Left" />												
														<ItemTemplate>
															<asp:TextBox ID="we_completed" AutoPostBack="True" runat="server" class="date_future col_med" Text='<%# Eval("we_completed", "{0:dd-MMM-yyyy}") %>' Width=100 OnTextChanged="text_change_auditor" ></asp:TextBox>
														</ItemTemplate>
														<ControlStyle width="100%" />																
													</asp:TemplateField>	
													<asp:TemplateField SortExpression="intensive_minutes" HeaderText="Int Minutes">
														<ItemTemplate>
															<asp:TextBox ID="intensive_minutes" AutoPostBack="True" CssClass="col_med" runat="server" Text='<%# Eval("intensive_minutes") %>' Width=75 OnTextChanged="text_change_auditor" />
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField SortExpression="status" HeaderText="Status" >
														<HeaderStyle HorizontalAlign="Left" CssClass="col_med no-print" />
														<ItemTemplate>
														<asp:DropDownList id="ddlStatus" AutoPostBack="True" OnSelectedIndexChanged="Selection_Change_Status" Width=150 runat="server" 
														CssClass="col_med no-print" SelectedValue='<%# Eval("status") %>' TabIndex='<%# TabIndex %>'>
															<asp:ListItem Value="USED"> USED </asp:ListItem>
															<asp:ListItem Value="IP"> IP </asp:ListItem>
															<asp:ListItem Value="UNUSED"> UNUSED </asp:ListItem>
														</asp:DropDownList>
														</ItemTemplate>
													</asp:TemplateField>												
													<asp:TemplateField SortExpression="note" HeaderText="Note">
														<ItemTemplate>
															<asp:TextBox ID="note" AutoPostBack="True" CssClass="col_med" runat="server" Text='<%# Eval("note") %>' Width=350 OnTextChanged="text_change_auditor" />
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
					<div class="col-6">
						<div class="card">		
							<div class="card-header">
								<h3 class="card-title">
									<i class="fa fa-cog text-info"></i>
									Tech Estimates
								</h3>
							</div>	
							<div class="card-body">
								<div class="row">
									<div class="col-4">
										<button runat="server" id="btnAddTechEstimate" class="btn btn-block btn-outline-info btn-md" onserverclick="OpenLogTechEstimate" title="Log Tech Estimate">
											Log TE
										</button>	
										</br>									
									</div>
									<div class="col-10">
									</div>
								</div>
								<div runat="server">
									<div class="row">  
										<div class="col-lg-12 ">  
											<div class="table-responsive">  
											<asp:GridView 
												ID="GridViewTE" 
												runat="server" 
												AutoGenerateColumns="false" 
												OnSorting="TaskGridView_Sorting" 
												BorderWidth="0" 
												CellPadding="0" 
												AllowSorting="True" 
												CssClass="mGridAppt"
												GridLines="None" 
												Width="100%"											
												EmptyDataText="No records.">
												<Columns>					
													<asp:BoundField DataField="ID" HeaderText="ID" >
														<HeaderStyle CssClass="no-display"></HeaderStyle>
														<ItemStyle CssClass="no-display"></ItemStyle>
													</asp:BoundField>
													<asp:BoundField DataField="pc_id" visible="False" HeaderText="PC_ID" >
														<HeaderStyle HorizontalAlign="Left" CssClass="col_med" Width="150" />
													</asp:BoundField>
													<asp:BoundField DataField="org" visible="False" HeaderText="Org" >
														<HeaderStyle HorizontalAlign="Left" CssClass="col_med" Width="150" />
													</asp:BoundField>
													<asp:TemplateField Visible="true" SortExpression="te_date" HeaderText="Date">												
														<HeaderStyle HorizontalAlign="Left" />												
														<ItemTemplate>
															<asp:TextBox ID="te_date" AutoPostBack="True" runat="server" class="date_future col_med" Text='<%# Eval("te_date", "{0:d}") %>' Width=100 OnTextChanged="text_change_auditor" ></asp:TextBox>
														</ItemTemplate>
													</asp:TemplateField>			
													<asp:TemplateField SortExpression="ints_to_clear" HeaderText="Ints">
														<ItemTemplate>
															<asp:TextBox ID="ints_to_clear" AutoPostBack="True" CssClass="col_xs" runat="server" Text='<%# Eval("ints_to_clear") %>' Width=50 OnTextChanged="text_change_auditor" />
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField SortExpression="te_by" HeaderText="TE By">
														<ItemTemplate>
															<asp:TextBox ID="te_by" AutoPostBack="True" CssClass="col_med" runat="server" Text='<%# Eval("te_by") %>' Width=100 OnTextChanged="text_change_auditor" />
														</ItemTemplate>
														<ControlStyle width="100%" />																
													</asp:TemplateField>
													<asp:TemplateField SortExpression="note" HeaderText="Note">
														<ItemTemplate>
															<asp:TextBox ID="note" AutoPostBack="True" CssClass="col_med" runat="server" Text='<%# Eval("note") %>' Width=350 OnTextChanged="text_change_auditor" />
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField>
														<ItemTemplate>
															<asp:LinkButton ID="lnkBtnDeleteEvent" class="btn btn-outline-danger btn-sm col-12" runat="server" Text="Delete" 
															OnClick="DeleteTE"></asp:LinkButton>
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
					<div class="col-6">
						<div class="card">		
							<div class="card-header">
								<h3 class="card-title">
									<i class="fa fa-cog text-info"></i>
									Account Data
								</h3>
							</div>
							<div class="card-body">
								<div runat="server">
									<div class="row">  
										<div class="col-lg-12 ">  
											<div class="table-responsive">  
												<asp:GridView 
													ID="GridViewAccount" 
													runat="server" 
													AutoGenerateColumns="false" 
													OnSorting="TaskGridView_Sorting" 
													BorderWidth="0" 
													CellPadding="0" 
													AllowSorting="True" 
													CssClass="mGridAppt"
													GridLines="None" 
													Width="100%"											
													EmptyDataText="No records.">
													<Columns>					
														<asp:BoundField DataField="addo_id" visible="False" HeaderText="ADDO ID" >
															<HeaderStyle HorizontalAlign="Left" CssClass="col_med" Width="150" />
														</asp:BoundField>
														<asp:BoundField DataField="org_id" visible="False" HeaderText="Org" >
															<HeaderStyle HorizontalAlign="Left" CssClass="col_med" Width="150" />
														</asp:BoundField>
														<asp:BoundField DataField="item" visible="True" HeaderText="Item" SortExpression="item">
															<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
														</asp:BoundField>
														<asp:BoundField DataField="quantity" HeaderText="Quantity" SortExpression="quantity" >
															<HeaderStyle HorizontalAlign="Left" CssClass="col_xs" />
														</asp:BoundField>
														<asp:BoundField DataField="amt_paid" HeaderText="Amount Paid" SortExpression="amt_paid" >
															<HeaderStyle HorizontalAlign="Left" CssClass="col_xs" />
														</asp:BoundField>
														<asp:BoundField DataField="amt_to_fp" HeaderText="Left to Pay" SortExpression="amt_to_fp" >
															<HeaderStyle HorizontalAlign="Left" CssClass="col_xs" />
														</asp:BoundField>
														<asp:BoundField DataField="fp" HeaderText="Fully Paid?" SortExpression="fp" >
															<HeaderStyle HorizontalAlign="Left" CssClass="col_xs" />
														</asp:BoundField>
														
													</Columns>
												</asp:GridView>
											</div> 
										</div>  
									</div>  
								</div>								
							</div>							
						</div>
					</div>	
					<div class="col-6">
						<div class="card">		
							<div class="card-header">
								<h3 class="card-title">
									<i class="fa fa-cog text-info"></i>
									PC Invoices
								</h3>
							</div>	
							<div class="card-body">
								<div runat="server">
									<div class="row">  
										<div class="col-lg-12 ">  
											<div class="table-responsive">  
													<asp:GridView 
														ID="GridViewInvoice" 
														runat="server" 
														AutoGenerateColumns="false" 
														OnSorting="TaskGridView_Sorting" 
														BorderWidth="0" 
														CellPadding="0" 
														AllowSorting="True" 
														CssClass="mGridAppt"
														GridLines="None" 
														Width="100%"											
														EmptyDataText="No records.">
														<Columns>					
															<asp:BoundField DataField="addo_id" visible="False" HeaderText="ADDO ID" >
																<HeaderStyle HorizontalAlign="Left" CssClass="col_med" Width="150" />
															</asp:BoundField>
															<asp:BoundField DataField="org_id" visible="False" HeaderText="Org" >
																<HeaderStyle HorizontalAlign="Left" CssClass="col_med" Width="150" />
															</asp:BoundField>
															<asp:BoundField DataField="inv_nbr" visible="True" HeaderText="Invoice Number" SortExpression="inv_nbr">
																<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
															</asp:BoundField>
															<asp:BoundField DataField="inv_date" visible="True" HeaderText="Date" DataFormatString="{0:d}" SortExpression="inv_date">
																<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
															</asp:BoundField>
															<asp:BoundField DataField="item" visible="True" HeaderText="Item" SortExpression="item">
																<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
															</asp:BoundField>
															<asp:BoundField DataField="inv_total" HeaderText="Amount Paid" SortExpression="inv_total" >
																<HeaderStyle HorizontalAlign="Left" CssClass="col_xs" />
															</asp:BoundField>
															<asp:BoundField DataField="qty" HeaderText="Quantity" SortExpression="qty" >
																<HeaderStyle HorizontalAlign="Left" CssClass="col_xs" />
															</asp:BoundField>
															<asp:BoundField DataField="unit_price" visible="True" HeaderText="Unit Price" SortExpression="unit_price">
																<HeaderStyle HorizontalAlign="Left" CssClass="col_med" />
															</asp:BoundField>
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
			</div>
			</div>
			<div class="row">

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
		<asp:Label id="HeadText" visible="false" runat="server" />
	</div>
</div>

		<div id="addModal" class="modal fade">
			<script type="text/javascript">
				function openIntensive() {
					$('[id*=addModal]').modal('show');
				} 
			</script>
			<div class="modal-dialog modal-lg" role="document">
				<div class="modal-content">
					<div class="modal-header">
						<button type="button" class="close" data-dismiss="modal">&times;</button>
						<h3 class="modal-title">Log Intensive</h3>
					</div>
					<div class="modal-body">
						<div class="row">
							<div class="col-md-12">
								<fieldset>
									<div class="row">
										<label for="inv_nbr_ID" class="col-sm-2 control-label">Invoice Nbr</label>
										<div class="col-sm-4">
											<asp:TextBox ID="inv_nbr_ID"  runat="server" Text="" CssClass="form-control" ></asp:TextBox>
										</div>
										<label class="col-sm-2 control-label" for="status_ID">Status</label>
										<div class="col-sm-4">
											<select class="form-control" name="status_ID" id="status_ID">
												<option value="UNUSED"> UNUSED </option>
												<option value="IP"> IP </option>
												<option value="USED"> USED </option>
											</select>
										</div>
									</div>
									<div class="row">
										<label for="vsd_value_ID" class="col-sm-2 control-label">VSD Value</label>
										<div class="col-sm-4">
											<asp:TextBox ID="vsd_value_ID" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
										</div>
										<label for="date_started_ID" class="col-sm-2 control-label">Date Started</label>
										<div class="col-sm-4">
											<asp:TextBox ID="date_started_ID" runat="server" Text="" CssClass="form-control date_future" ></asp:TextBox>
										</div>
									</div>		
									<div class="row">
										<label for="vsd_counted_ID" class="col-sm-2 control-label">VSD Counted</label>
										<div class="col-sm-4">
											<asp:TextBox ID="vsd_counted_ID" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
										</div>
										<label for="we_started_ID" class="col-sm-2 control-label">WE Started</label>
										<div class="col-sm-4">
											<asp:TextBox ID="we_started_ID" runat="server" Text="" CssClass="form-control date_future" ></asp:TextBox>
										</div>
									</div>

									<div class="row">
										<label class="col-sm-2 control-label" for="intensive_minutes_ID">Int Hours</label>
										<div class="col-sm-4">
											<select class="form-control" name="intensive_minutes_ID" id="intensive_minutes_ID">
												<option value="750"> 12 1/2 </option>
												<option value=""> Other </option>
											</select>
										</div>
										<label for="we_completed_ID" class="col-sm-2 control-label">WE Completed</label>
										<div class="col-sm-4">
											<asp:TextBox ID="we_completed_ID" runat="server" Text="" CssClass="form-control date_future" ></asp:TextBox>
										</div>
									</div>
									<div class="row">
										<label for="note_ID" class="col-sm-2 control-label">Note</label>
										<div class="col-sm-10">
											<asp:TextBox TextMode="MultiLine" Rows="2" ID="note_ID" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
										</div>
									</div>
									<div class="row">
										<div class="col-sm-5">
											<asp:TextBox ID="id2" type="hidden" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
										</div>
										<div class="col-sm-5">
											<asp:TextBox ID="addo_id_ID" type="hidden" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
										</div>
									</div>		
								</fieldset>
								<div class="modal-footer">
									<asp:Button ID="btnAddInt" OnClientClick="<% %>" class="btn btn-outline-info btn-md col-3" runat="server" Text="Save" CommandArgument='<%# Eval("Id") %>' OnCommand="btnAddIntensive_Click" />
								</div>							
							</div>
						</div>
					</div><!-- /.modal-body --> 
				</div><!-- /.modal-content -->
			</div><!-- /.modal-dialog -->
		</div><!-- /.modal -->	
		<div id="addTE" class="modal fade">
			<script type="text/javascript">
				function openTechEstimate() {
					$('[id*=addTE]').modal('show');
				} 
			</script>
			<div class="modal-dialog modal-lg" role="document">
				<div class="modal-content">
					<div class="modal-header">
						<button type="button" class="close" data-dismiss="modal">&times;</button>
						<h3 class="modal-title">Log Tech Estimate</h3>
					</div>
					<div class="modal-body">
						<div class="row">
							<div class="col-md-12">
								<fieldset>
									<div class="row">
										<label for="ints_to_clear_ID" class="col-sm-2 control-label">Intensives</label>
										<div class="col-sm-2">
											<asp:TextBox ID="ints_to_clear_ID"  runat="server" Text="" CssClass="form-control" ></asp:TextBox>
										</div>
										<label for="te_date_ID" class="col-sm-1 control-label">Date</label>
										<div class="col-sm-3">
											<asp:TextBox ID="te_date_ID" runat="server" Text="" CssClass="form-control date_future" ></asp:TextBox>
										</div>
										<label for="te_by_ID" class="col-sm-1 control-label">TE By</label>
										<div class="col-sm-3">
											<asp:TextBox ID="te_by_ID"  runat="server" Text="" CssClass="form-control" ></asp:TextBox>
										</div>
									</div>
									<div class="row">
										<label for="te_note_ID" class="col-sm-2 control-label">Note</label>
										<div class="col-sm-10">
											<asp:TextBox TextMode="MultiLine" Rows="2" ID="te_note_ID" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
										</div>
									</div>
									<div class="row">
										<div class="col-sm-5">
											<asp:TextBox ID="id3" type="hidden" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
										</div>
										<div class="col-sm-5">
											<asp:TextBox ID="addo_id_ID2" type="hidden" runat="server" Text="" CssClass="form-control" ></asp:TextBox>
										</div>
									</div>		
								</fieldset>
								<div class="modal-footer">
									<asp:Button ID="btnAddTE" OnClientClick="<% %>" class="btn btn-outline-info btn-md col-3" runat="server" Text="Save" CommandArgument='<%# Eval("Id3") %>' OnCommand="btnLogTE_Click" />
								</div>							
							</div>
						</div>
					</div><!-- /.modal-body --> 
				</div><!-- /.modal-content -->
			</div><!-- /.modal-dialog -->
		</div><!-- /.modal -->			
	<div id="deleteTE" class="modal fade">
		<script type="text/javascript">
			function ConfirmDeleteTE() {
				$('[id*=deleteTE]').modal('show');
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
								<p> Are you <b>sure</b> you want to delete this TE from the list? <br/> </p>
								<asp:TextBox ID="teID" type="hidden" runat="server" Text="" ></asp:TextBox>
							<div class="modal-footer">
								<button type="submit" class="btn btn-default" data-dismiss="modal" aria-hidden="true">Close</button>
								<asp:Button ID="btnDeleteTE" OnClientClick="<% %>" class="btn btn-primary" runat="server" Text="Delete" CommandArgument='<%# Eval("Id") %>' OnCommand="btnDeleteTE_Click" />
							</div>							
						</div>
					</div>
				</div><!-- /.modal-body -->
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
								<asp:TextBox ID="deleteID" type="hidden" runat="server" Text="" ></asp:TextBox>
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
								<asp:TextBox ID="archiveID" type="hidden" runat="server" Text="" ></asp:TextBox>
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