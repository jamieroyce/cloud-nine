<!DOCTYPE html>
<%@ Page Language="C#" Trace="False" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="expectancy.aspx.cs" Inherits="_Default" Debug="true" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"  
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
	<meta charset="utf-8">
	<meta name="viewport" content="width=device-width, initial-scale=1">
	<meta http-equiv="x-ua-compatible" content="ie=edge">

	<title>Set Expectancy Quotas</title>

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
	  <li class="nav-item d-none d-sm-inline-block">
		<a href="../delivery.aspx" class="nav-link">Delivery View</a>
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
				<a href="../delivery.aspx" class="nav-link" >
				  <i class="nav-icon fa fa-address-card text-info"></i>
				  <p>
					Delivery View
				  </p>
				</a>
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
              <li class="breadcrumb-item active">Expectancy by Delivery Area</li>
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
				<div class="col-md-6 col-sm-6 col-12">
					<div class="card">		
						<div class="card-body">
							<div class="row">
								<div class="col-10">
									<b>Info:</b> Set the Expectancy Quotas by Area. This will be used in the Delivery View. 
								</div>
								<div class="col-2">
									<button type="button" id="btnAddReg" runat="server" class="btn btn-outline-info btn-block" data-toggle="modal" data-target="#addRegModal">Add</button>												
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>				
			<div class="row">
				<div class="col-md-6 col-sm-12 col-6">
					<div class="card">		
						<div class="card-body">
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
													<asp:DropDownList id="desc1" AutoPostBack="True" OnSelectedIndexChanged="Selection_Change_Desc1" runat="server" 
														CssClass="col_xs" SelectedValue='<%# Eval("desc1") %>' TabIndex='<%# TabIndex %>'>
														<asp:ListItem Value="div6"> DIV6 </asp:ListItem>
														<asp:ListItem Value="purif"> PURIF </asp:ListItem>
														<asp:ListItem Value="srd"> SRD </asp:ListItem>
														<asp:ListItem Value="hgc"> HGC </asp:ListItem>
														<asp:ListItem Value="acad"> ACAD </asp:ListItem>
														<asp:ListItem Value="gak"> GAK </asp:ListItem>
											</asp:DropDownList>
												</ItemTemplate>
												<ControlStyle Width="100%" />								
											</asp:TemplateField>																						
											<asp:TemplateField SortExpression="desc2" HeaderText="Quota">
												<ItemTemplate>
													<asp:TextBox ID="full_name" AutoPostBack="True" CssClass="col_med" runat="server" Text='<%# Eval("desc2") %>' Width=150 OnTextChanged="text_change_desc2" />
												</ItemTemplate>
												<ControlStyle Width="100%" />								
											</asp:TemplateField>
											<asp:TemplateField SortExpression="desc3" visible="True" HeaderText="Org">
												<ItemTemplate>
													<asp:DropDownList id="ddlOrg" AutoPostBack="True" OnSelectedIndexChanged="Selection_Change_Org" runat="server" 
													CssClass="col_xs" SelectedValue='<%# Eval("desc3") %>' TabIndex='<%# TabIndex %>'>
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
				<div class="col-md-6 col-sm-6 col-12">
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
		<div class="modal-dialog modal-lg" role="document">
			<div class="modal-content">
				<div class="modal-header modal-header-danger">
					<button type="button" class="close" data-dismiss="modal">&times;</button>
					<h3 class="modal-title">Warning!</h3>
				</div>
				<div class="modal-body">
					<div class="row">
						<div class="col-md-12">
								<p> Are you <b>sure</b> you want to delete this Quota from the  list?</p>
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
					$('#shortnameID').val('');
					$('#postID').val('');
				});
			});
		</script>
		<div class="modal-dialog modal-sm" role="document">
			<div class="modal-content">
				<div class="modal-header">
					<button type="button" class="close" data-dismiss="modal">&times;</button>
					<h3 class="modal-title" id="addModalTitle">Add Reg</h3>
				</div>
				<div class="modal-body">
					<div class="row">
						<div class="col-md-12">

								<div class="row" >
									<label class="col-sm-4 control-label" for="textinput">Area</label>
									<div class="col-sm-8">
										<asp:DropDownList id="shortnameID" runat="server" CssClass="form-control"
											SelectedValue='<%# Eval("desc1") %>'>
												<asp:ListItem Value="div6"> DIV6 </asp:ListItem>
												<asp:ListItem Value="purif"> PURIF </asp:ListItem>
												<asp:ListItem Value="srd"> SRD </asp:ListItem>
												<asp:ListItem Value="hgc"> HGC </asp:ListItem>
												<asp:ListItem Value="acad"> ACAD </asp:ListItem>
												<asp:ListItem Value="gak"> GAK </asp:ListItem>
										</asp:DropDownList>
									</div>
								</div>
								<div class="row" >
									<label class="col-sm-4 control-label" for="textinput">Quota</label>
									<div class="col-sm-8">
										<asp:TextBox ID="quotaID" name="quotaID" runat="server" Text="" CssClass="form-control" style="width=100%" ></asp:TextBox>
									</div>
								</div>
								<div class="row" >
									<label for="postID" class="col-sm-4 control-label">Org</label>
									<div class="col-sm-8">
										<asp:DropDownList id="postID" runat="server" CssClass="form-control" 
										SelectedValue='<%# Eval("org") %>' TabIndex='<%# TabIndex %>'>
											<asp:ListItem Value="Day"> Day </asp:ListItem>
											<asp:ListItem Value="Fdn"> Fdn </asp:ListItem>							
										</asp:DropDownList>	
									</div>
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
								<asp:Button ID="btnSubmitReg" OnClientClick="<% %>" class="btn btn-info" runat="server" Text="Save" CommandArgument='<%# Eval("Id") %>' OnCommand="btnAddReg_Click" />
							</div>
						</div>
					</div>
				</div>	
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