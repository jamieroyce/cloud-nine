<!DOCTYPE html>
<%@ Page Language="C#" Trace="False" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="appt.aspx.cs" Inherits="_Default" Debug="true" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"  
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<%@ Register Assembly="Shield.Web.UI" Namespace="Shield.Web.UI" TagPrefix="shield" %>
	
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
	<head runat="server">
		<title>Appointments</title>
  <link rel="stylesheet" href="../plugins/fullcalendar/fullcalendar.min.css">
  <link rel="stylesheet" href="../plugins/fullcalendar/fullcalendar.print.css" media="print">
  <!-- Theme style -->
  <link rel="stylesheet" href="../dist/css/adminlte.min.css">



  
		<!-- <link rel="stylesheet" href="/css/bootstrap.css"> -->
		<!-- <link rel="stylesheet" href="/css/bootstrap-select.css"> -->
		<!-- <link rel="stylesheet" href="/css/bootstrap-datepicker.css"> -->
		<!-- <link rel="stylesheet" type="text/css" href="css/bis.css" /> -->


		<!-- <link rel="stylesheet" href="/css/bootstrap_min.css"> -->
		<!-- <script src="/js/jquery-3.3.1.min.js"></script> -->
		<!-- <script src="chart.js"></script> -->
		<!-- <script src="/js/bootstrap.js"></script> -->
		<!-- <script type="text/javascript" src="/js/moment.js"></script> -->
		<!-- <script type="text/javascript" src="/js/bootstrap-select.js"></script> -->
		<!-- <script type="text/javascript" src="/js/bootstrap-datetimepicker.min.js"></script>		 -->
		<!-- <script type="text/javascript" src="/js/bootstrap-datepicker.js"></script> -->
				
		<link href='/css/fullcalendar.min.css' rel='stylesheet' />
		<link href='/css/fullcalendar.print.min.css' rel='stylesheet' media='print' />
		<script src='/js/moment.min.js'></script>
		<script src='/js/lib/jquery.min.js'></script>
		<script src='/js/fullcalendar.min.js'></script>
		
		
		<script type="text/javascript">
			$(function () {
				$("[id$=scheduled]").children().datetimepicker();
			});		
			$(document).ready(function() {
				$('.alert').delay(200).addClass("in").fadeOut(8000);
			})	

			$(function() {

			  $('#calendar').fullCalendar({
				defaultView: 'agendaFourDay',
				groupByDateAndResource: true,
				header: {
				  left: 'prev,next',
				  center: 'title',
				  right: 'agendaDay,agendaFourDay'
				},
				views: {
				  agendaFourDay: {
					type: 'agenda',
					duration: { days: 2 }
				  }
				},
				resources: [
				  { id: 'a', title: 'Reg - Linley' },
				  { id: 'b', title: 'Reg - Jayden' },
				  { id: 'c', title: 'Reg - Emily' }
				],
				events: 'https://fullcalendar.io/demo-events.json?with-resources=2'
			  });

			});			
			
			
		</script>
		<link rel="icon" href="/img/favicon.png">
	</head>
	<body>
		<!-- <br><br><br> -->
		<form id="form1" runat="server">
			<div class="navbar navbar-default navbar-fixed-top" style="background-color: #e0ebeb;">
				<div class="container-fluid">
					<div class="navbar-header">
						<button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1" aria-expanded="false">
							<span class="sr-only">Toggle navigation</span> 
							<span class="icon-bar"></span>
							<span class="icon-bar"></span>
							<span class="icon-bar"></span>
						</button>
						<a class="navbar-brand" href="#">
							<img src="/img/scn_sm.png" alt="BIS Tracking System">
						</a>
						<a class="navbar-brand nav-item">
							<!-- <asp:Label class="navbar-text pull-left" style="padding-left:15px" id="staticText" Text="BIS Tracking" runat="server" /> -->
							<!-- <asp:Label class="navbar-text pull-left" style="padding-left:15px" id="HeadText" runat="server" /> -->
							<!-- <asp:Label class="navbar-text pull-left" id="OrgText" runat="server" Text="Day" />												 -->
							<!-- <asp:Label class="navbar-text pull-left" runat="server" id="AmountText" /> -->
							<!-- <asp:LinkButton type="button" class="btn btn-primary btn-outline navbar-btn" id="nowprospect2" Text="Add Name" OnClick="Addresso_Click" runat="server"/>							 -->
							<!-- <button type="button" id="btnAddModal" runat="server" class="btn btn-primary btn-outline navbar-btn" data-toggle="modal" data-target="#addModal" >Add Record</button>								 -->
						</a>
						
					</div>
					<div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
						<!-- <ul class="nav navbar-nav">		 -->
							<!-- <li class="nav-item">					 -->
								<!-- <asp:LinkButton type="button" id="gi3" text="Combined BIS" OnClick="Combined_Btn_Click" runat="server" /> -->
							<!-- </li> -->
							<!-- <li class="nav-item"> -->
								<!-- <asp:LinkButton type="button" id="bis_bodies" Text="In The Shop" OnClick="Btn_Click" runat="server"/> -->
							<!-- </li> -->
							<!-- <li class="nav-item"> -->
								<!-- <asp:LinkButton type="button" id="giinv2" Text="Scheduled" OnClick="Btn_Click" runat="server"/> -->
							<!-- </li> -->
							<!-- <li class="nav-item"> -->
								<!-- <asp:LinkButton type="button" id="giconf2" Text="Named" OnClick="Btn_Click" runat="server"/> -->
							<!-- </li> -->
							<!-- <li class="nav-item"> -->
								<!-- <asp:LinkButton type="button" id="nFPPP" Text="FP/PP List" OnClick="FPPP_Click" runat="server"/> -->
							<!-- </li> -->
						<!-- </ul>		 -->
						
						<ul class="nav navbar-nav navbar-right">
							<li>
								<a href="#" class="dropdown-toggle pull-left" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">
									Org <span class="caret"></span>
								</a>
								<ul class="dropdown-menu pull-left">
									<li>
										<asp:Button type="button" class="btn btn-default btn-block" id="day" text="Day" OnClick="Org_Btn_Click" runat="server" />
										<asp:Button type="button" class="btn btn-default btn-block" id="fdn" text="Fdn" OnClick="Org_Btn_Click" runat="server" />
									</li>
								</ul>				
							</li>
							<li>
								<a href="#" class="dropdown-toggle pull-left" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">
									Reports <span class="caret"></span>
								</a>
										<!-- <asp:Button type="button" class="btn btn-default btn-block" id="ncalendar2" text="Current Week" OnClick="Calendar2_Btn_Click" runat="server" /> -->
										<!-- <asp:Button type="button" class="btn btn-default btn-block" id="ncalendar" text="Calendar" OnClick="Calendar_Btn_Click" runat="server" /> -->
										<!-- <asp:Button type="button" class="btn btn-default btn-block" id="ncalendar3" text="Completed Cycles" OnClick="Calendar3_Btn_Click" runat="server" /> -->
								<ul class="dropdown-menu pull-left">
									<li>
										<!-- <asp:Button type="button" class="btn btn-default btn-block" id="Archive3" Text="Archive" OnClick="BtnArchive_Click" runat="server"/> -->
										<!-- <asp:Button type="button" class="btn btn-default btn-block" id="nInvTable" Text="Invoice Table" OnClick="Inv_Click" runat="server"/> -->
										<!-- <asp:Button type="button" class="btn btn-default btn-block" id="nTTLPayList" Text="Total Payment List" OnClick="TTLPayList_Click" runat="server"/> -->
									</li>
								</ul>							
							</li>
							<li class="nav-item">
								<asp:LinkButton type="button" class="glyphicon glyphicon-cog" id="config" OnClick="Maintenance_BtnClick" runat="server"/>
							</li>
						</ul>							
					</div>
				</div>
			</div>	
			<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
			<div>
				<div class="no-print">
				</div>
				<br />			
				<div id='calendar'>this is the calendar</div>
				<br />		
				<br />		
				<div id="Appt" runat="server">
					<!-- <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#addModal" >Add Record</button> -->
					<div style="clear: both;"></div>				
					<asp:GridView 
						ID="GridViewAppt" 
						runat="server" 
						AllowPaging="True" 
						OnRowDeleting="OnRowDeleting"
						OnPageIndexChanging="grdData_PageIndexChanging" 
						borderwidth="0" 
						OnSorting="TaskGridView_Sorting" 
						GridLines="None"
						CssClass="mGrid"
						PagerStyle-CssClass="pgr"
						AlternatingRowStyle-CssClass="alt"
						AllowSorting="True" 
						PageSize="<%# Convert.ToInt32(ddlPage.Text) %>" 
						AutoGenerateColumns="False" 
						CellPadding="0">
						<Columns>
							<asp:TemplateField SortExpression="reg" HeaderText="Reg">                
								<ItemTemplate>
									<asp:TextBox ID="name" CssClass="col_med" AutoPostBack="True" runat="server" Text='<%# Eval("name") %>' OnTextChanged="text_change" />
								</ItemTemplate>
								<ControlStyle width="100%" />																
							</asp:TemplateField>
							<asp:TemplateField SortExpression="nine" HeaderText="9AM">                
								<ItemTemplate>
									<asp:TextBox ID="nine" CssClass="col_med" AutoPostBack="True" runat="server" Text='<%# Eval("am9") %>' OnTextChanged="text_change" />
								</ItemTemplate>
								<ControlStyle width="100%" />																
							</asp:TemplateField>
							<asp:TemplateField SortExpression="am10" HeaderText="10AM">                
								<ItemTemplate>
									<asp:TextBox ID="am10" CssClass="col_med" AutoPostBack="True" runat="server" Text='<%# Eval("am10") %>' OnTextChanged="text_change" />
								</ItemTemplate>
								<ControlStyle width="100%" />																
							</asp:TemplateField>
							<asp:TemplateField SortExpression="am11" HeaderText="11AM">                
								<ItemTemplate>
									<asp:TextBox ID="am11" CssClass="col_med" AutoPostBack="True" runat="server" Text='<%# Eval("am11") %>' OnTextChanged="text_change" />
								</ItemTemplate>
								<ControlStyle width="100%" />																
							</asp:TemplateField>
							<asp:TemplateField SortExpression="pm12" HeaderText="12PM">                
								<ItemTemplate>
									<asp:TextBox ID="pm12" CssClass="col_med" AutoPostBack="True" runat="server" Text='<%# Eval("pm12") %>' OnTextChanged="text_change" />
								</ItemTemplate>
								<ControlStyle width="100%" />																
							</asp:TemplateField>
							<asp:TemplateField>
								<ItemTemplate>
									<asp:LinkButton ID="lnkBtnEdit" class="btn btn-default btn-xs" runat="server" Text="Edit" 
									OnClick="ViewArchive"></asp:LinkButton>
								</ItemTemplate>						
							</asp:TemplateField>
							<asp:TemplateField>
								<ItemTemplate>
									<asp:LinkButton ID="lnkBtnDelete" class="btn btn-danger btn-outline btn-xs" runat="server" Text="Delete" 
									OnClick="DeleteRow"></asp:LinkButton>
								</ItemTemplate>						
							</asp:TemplateField>
						</Columns>					
					</asp:GridView>
				</div>				
				
				
				<div id="One" runat="server">
					<!-- <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#addModal" >Add Record</button> -->
					<div style="clear: both;"></div>
					<div id="search">	
						<asp:Panel runat="server" DefaultButton="btnSearch">
							<div class="form-group mb-2 mr-sm-2 mb-sm-0">
								<asp:DropDownList id="ddlSearch" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server">
									<asp:ListItem Value="Name"> Name </asp:ListItem>
									<asp:ListItem Value="Amount"> Amount </asp:ListItem>
									<asp:ListItem Value="Service"> Service </asp:ListItem>
									<asp:ListItem Value="Reg"> Reg </asp:ListItem>
									<asp:ListItem Value="bird_dog"> Bird Dog </asp:ListItem>
									<asp:ListItem Value="Line"> Line </asp:ListItem>
									<asp:ListItem Value="status"> Status </asp:ListItem>
								</asp:DropDownList>  
								<asp:TextBox ID="txt" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server"></asp:TextBox>
								<asp:Button ID="btnSearch" runat="server" Text="Search" class="btn btn-primary btn-outline" OnClick="BtnSearch_Click" />												
							</div>
						</asp:Panel>
					</div>
					<asp:GridView 
						ID="GridView1" 
						runat="server" 
						AllowPaging="True" 
						OnRowDeleting="OnRowDeleting"
						OnPageIndexChanging="grdData_PageIndexChanging" 
						borderwidth="0" 
						OnSorting="TaskGridView_Sorting" 
						GridLines="None"
						CssClass="mGrid"
						PagerStyle-CssClass="pgr"
						AlternatingRowStyle-CssClass="alt"
						AllowSorting="True" 
						PageSize="<%# Convert.ToInt32(ddlPage.Text) %>" 
						AutoGenerateColumns="False" 
						CellPadding="0">
						<Columns>
							<asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID">
								<HeaderStyle CssClass="no-display"></HeaderStyle>
								<ItemStyle CssClass="no-display"></ItemStyle>
							</asp:BoundField>  
							<asp:BoundField DataField="addo_ID">
								<HeaderStyle CssClass="no-display"></HeaderStyle>
								<ItemStyle CssClass="no-display"></ItemStyle>
							</asp:BoundField>  
							<asp:TemplateField HeaderText="Addo ID">
								<ItemTemplate>
									<asp:HyperLink runat="server" class="no-print" Text='<%# Eval("Addo_ID") != System.DBNull.Value ? Eval("Addo_ID") : "" %>' Target="_blank" 
									NavigateUrl='<%# Eval("Addo_ID") != System.DBNull.Value ? 
									"http://webapp2a.scientology.net/portal/cars/cosCarsPersonReport.jsp?hideLinks=true&addoId=" + Eval("Addo_ID") + 
									"&orgId=89&browser=&userId=allhandsharlem&password=harlembas1" : ""%>'
									style='<%# Eval("Addo_ID") == System.DBNull.Value ? "display: none;" : "" %>' />
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField SortExpression="name" HeaderText="Name">                
								<ItemTemplate>
									<asp:TextBox ID="name" CssClass="col_med" AutoPostBack="True" runat="server" Text='<%# Eval("name") %>' OnTextChanged="text_change" />
								</ItemTemplate>
								<ControlStyle width="100%" />																
							</asp:TemplateField>
							<asp:TemplateField SortExpression="area" HeaderText="Area">
								<ItemTemplate>
									<asp:DropDownList id="ddlReg" name="ddlReg" AutoPostBack="True" runat="server" CssClass="col_small" OnSelectedIndexChanged="Selection_Change_Reg"
										SelectedValue='<%# Eval("area") %>' TabIndex='<%# TabIndex %>'>
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
								<ItemTemplate>
									<asp:TextBox ID="service" CssClass="col_xs" AutoPostBack="True" runat="server" Text='<%# Eval("service") %>' OnTextChanged="text_change" 
										TabIndex='<%# TabIndex %>' />
								</ItemTemplate>
								<ControlStyle Width="100%" />								
							</asp:TemplateField>
							<asp:TemplateField SortExpression="reg" HeaderText="Reg">
								<ItemTemplate>
									<asp:TextBox ID="reg" CssClass="col_xs" AutoPostBack="True" runat="server" Text='<%# Eval("reg") %>' OnTextChanged="text_change" 
										TabIndex='<%# TabIndex %>' />
								</ItemTemplate>
								<ControlStyle Width="100%" />								
							</asp:TemplateField>			
							<asp:TemplateField SortExpression="fsm" HeaderText="FSM">
										<ItemTemplate>
									<asp:TextBox ID="fsm" CssClass="col_xs" AutoPostBack="True" runat="server" Text='<%# Eval("fsm") %>' 
										OnTextChanged="text_change" />
								</ItemTemplate>									        		
								<ControlStyle Width="100%" />								
							</asp:TemplateField>
							<asp:TemplateField SortExpression="phone" HeaderText="Phone">
										<ItemTemplate>
									<asp:TextBox ID="phone" AutoPostBack="True" CssClass="col_xs" runat="server" Text='<%# Eval("phone") %>' 
										OnTextChanged="text_change" />
								</ItemTemplate>
								<ControlStyle Width="100%" />								
							</asp:TemplateField>			
							<asp:TemplateField SortExpression="email" HeaderText="Email">
										<ItemTemplate>
									<asp:TextBox ID="email" AutoPostBack="True" CssClass="col_xs" runat="server" Text='<%# Eval("email") %>' 
										OnTextChanged="text_change" />
								</ItemTemplate>
								<ControlStyle Width="100%" />								
							</asp:TemplateField>			
							<asp:TemplateField visible="False" HeaderText="Org">
								<ItemTemplate>
									<asp:DropDownList id="ddlOrg" AutoPostBack="True" OnSelectedIndexChanged="Selection_Change_Org" runat="server" 
									CssClass="mlm2" SelectedValue='<%# Eval("org") %>' TabIndex='<%# TabIndex %>'>
										<asp:ListItem Value=""> </asp:ListItem>
										<asp:ListItem Value="Day"> Day </asp:ListItem>
										<asp:ListItem Value="Fdn"> Fdn </asp:ListItem>
									</asp:DropDownList>
								</ItemTemplate>
								<ControlStyle Width="100%" />								
							</asp:TemplateField>
							<asp:TemplateField SortExpression="status" HeaderText="Status" >
								<ItemTemplate>
									<asp:DropDownList id="ddlStatus" AutoPostBack="True" OnSelectedIndexChanged="Selection_Change_Status" runat="server"  
									CssClass="col_small" SelectedValue='<%# Eval("status") %>' TabIndex='<%# TabIndex %>'>
										<asp:ListItem Value="In The Shop"> In The Shop </asp:ListItem>
										<asp:ListItem Value="Scheduled"> Scheduled </asp:ListItem>
										<asp:ListItem Value="Named"> Named </asp:ListItem>
									</asp:DropDownList>
								</ItemTemplate>
								<ControlStyle Width="100%" />								
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
							<asp:TemplateField SortExpression="notes" HeaderText="Notes">
										<ItemTemplate>
									<asp:TextBox ID="notes" AutoPostBack="True" CssClass="col_large" runat="server" Text='<%# Eval("notes") %>' 
										OnTextChanged="text_change" />
								</ItemTemplate>
								<ControlStyle Width="100%" />								
							</asp:TemplateField>
							<asp:TemplateField SortExpression="rank" HeaderText="Rank" Visible="false">
								<ItemTemplate>
									<asp:DropDownList id="ddlRank" AutoPostBack="True" OnSelectedIndexChanged="Rank_Change" runat="server" CssClass="col_xxs" 
									SelectedValue='<%# Eval("rank") %>' TabIndex='<%# TabIndex %>'>
										<asp:ListItem Value=""></asp:ListItem>
										<asp:ListItem Value="a"> A </asp:ListItem>
										<asp:ListItem Value="b"> B </asp:ListItem>
										<asp:ListItem Value="c"> C </asp:ListItem>
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
							<asp:TemplateField visible="False" HeaderText="Category">
								<ItemTemplate>
									<asp:DropDownList id="ddlRegCat" AutoPostBack="True" OnSelectedIndexChanged="Selection_Change" runat="server" 
									CssClass="mlm2" SelectedValue='<%# Eval("Reg_Cat_ID") %>' TabIndex='<%# TabIndex %>'>
										<asp:ListItem Value="LineUp"> LineUp </asp:ListItem>
										<asp:ListItem Value="Archive"> Archive </asp:ListItem>
									</asp:DropDownList>
								</ItemTemplate>
								<ControlStyle Width="100%" />								
							</asp:TemplateField>
							<asp:TemplateField>
								<ItemTemplate>
									<asp:LinkButton ID="lnkBtnEdit" class="btn btn-default btn-xs" runat="server" Text="Edit" 
									OnClick="ViewArchive"></asp:LinkButton>
								</ItemTemplate>						
							</asp:TemplateField>
							<asp:TemplateField>
								<ItemTemplate>
									<asp:LinkButton ID="lnkBtnDelete" class="btn btn-danger btn-outline btn-xs" runat="server" Text="Delete" 
									OnClick="DeleteRow"></asp:LinkButton>
								</ItemTemplate>						
							</asp:TemplateField>
						</Columns>					
					</asp:GridView>
					<!-- <asp:Button id="Add2" Text="Add" OnClick="Add_Click" runat="server"/> -->
					<!-- <asp:Button id="Add" Text="Add Row" class="btn btn-info" data-toggle="modal" data-target="#myModal"/> -->
				</div>
				<div id="InvoiceTable" runat="server">
					<!-- <ItemTemplate> -->
						<!-- <asp:Button type="button" class="btn btn-primary" id="AddInv" text="Add" data-toggle="modal" data-target="#addModal" />Add Record</asp:Button> -->
					<!-- </ItemTemplate> -->
					<div style="clear: both;"></div>
					<asp:Panel runat="server" DefaultButton="btnSearchInv">
						<div id="search">	
							<div class="form-group mb-2 mr-sm-2 mb-sm-0">
								<asp:DropDownList id="ddlSearchInvoice" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server">
									<asp:ListItem Value="Name"> Name </asp:ListItem>
									<asp:ListItem Value="Amount"> Amount </asp:ListItem>
									<asp:ListItem Value="Service"> Service </asp:ListItem>
									<asp:ListItem Value="Reg"> Reg </asp:ListItem>
									<asp:ListItem Value="bird_dog"> Bird Dog </asp:ListItem>
									<asp:ListItem Value="Appt"> Appt </asp:ListItem>
									<asp:ListItem Value="Line"> Line </asp:ListItem>
									<asp:ListItem Value="status"> Status </asp:ListItem>
								</asp:DropDownList>  
								<asp:TextBox ID="txtInv" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server"></asp:TextBox>
								<asp:Button ID="btnSearchInv" runat="server" Text="Search" class="btn btn-primary btn-outline" OnClick="BtnSearchInvGrid_Click" />												
								<!-- <asp:Button id="ArchiveWeekInv" Text="Archive Week" class="btn btn-warning" data-toggle="modal" data-target="#archiveModal" runat="server"/> -->
							</div>
						</div>
					</asp:Panel>
					<asp:GridView 
						ID="GridViewInvoice" 
						runat="server" 
						CssClass="mGrid"
						AllowPaging="True" 
						OnRowDeleting="OnRowDeleting"
						OnPageIndexChanging="grdData_PageIndexChanging_bis" 
						borderwidth="0" 
						OnSorting="TaskGridView_Sorting" 
						AllowSorting="True" 
						PageSize="<%# Convert.ToInt32(ddlPage.Text) %>" 
						AutoGenerateColumns="False" 
						GridLines="None"
						PagerStyle-CssClass="pgr"
						AlternatingRowStyle-CssClass="alt"
						CellPadding="0">
						<Columns>
							<asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID">
								<HeaderStyle CssClass="no-display"></HeaderStyle>
								<ItemStyle CssClass="no-display"></ItemStyle>
							</asp:BoundField>  
							<asp:BoundField DataField="addo_ID">
								<HeaderStyle CssClass="no-display"></HeaderStyle>
								<ItemStyle CssClass="no-display"></ItemStyle>
							</asp:BoundField>  
							<asp:TemplateField HeaderText="Addo ID">
								<ItemTemplate>
									<asp:HyperLink runat="server" class="no-print" Text='<%# Eval("Addo_ID") != System.DBNull.Value ? Eval("Addo_ID") : "" %>' Target="_blank" 
									NavigateUrl='<%# Eval("Addo_ID") != System.DBNull.Value ? 
									"http://webapp2a.scientology.net/portal/cars/cosCarsPersonReport.jsp?hideLinks=true&addoId=" + Eval("Addo_ID") + 
									"&orgId=89&browser=&userId=allhandsharlem&password=harlembas1" : ""%>'
									style='<%# Eval("Addo_ID") == System.DBNull.Value ? "display: none;" : "" %>' />
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField SortExpression="name" HeaderText="Name">                
								<ItemTemplate>
									<asp:TextBox ID="name" CssClass="col_med" AutoPostBack="True" runat="server" Text='<%# Eval("name") %>' OnTextChanged="text_change" />
								</ItemTemplate>
								<ControlStyle width="100%" />																
							</asp:TemplateField>
							<asp:TemplateField SortExpression="area" HeaderText=" Service Area">
								<ItemStyle CssClass="mlm2"></ItemStyle>
								<ItemTemplate>
									<asp:DropDownList id="ddlReg" name="ddlReg" AutoPostBack="True" runat="server" Width=125 OnSelectedIndexChanged="Selection_Change_Reg"
										CssClass="mlm2" SelectedValue='<%# Eval("area") %>' >
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
								<ItemTemplate>
									<asp:TextBox ID="service" CssClass="col_xs" AutoPostBack="True" runat="server" Text='<%# Eval("service") %>' OnTextChanged="text_change" 
										TabIndex='<%# TabIndex %>' />
								</ItemTemplate>
								<ControlStyle Width="100%" />								
							</asp:TemplateField>
							<asp:TemplateField SortExpression="reg" HeaderText="Reg">
								<ItemTemplate>
									<asp:TextBox ID="reg" CssClass="col_xs" AutoPostBack="True" runat="server" Text='<%# Eval("reg") %>' OnTextChanged="text_change" 
										TabIndex='<%# TabIndex %>' />
								</ItemTemplate>
								<ControlStyle Width="100%" />								
							</asp:TemplateField>			
							<asp:TemplateField SortExpression="fsm" HeaderText="FSM">
										<ItemTemplate>
									<asp:TextBox ID="fsm" CssClass="col_xs" AutoPostBack="True" runat="server" Text='<%# Eval("fsm") %>' 
										OnTextChanged="text_change" />
								</ItemTemplate>									        		
								<ControlStyle Width="100%" />								
							</asp:TemplateField>
							<asp:TemplateField SortExpression="phone" HeaderText="Phone">
										<ItemTemplate>
									<asp:TextBox ID="phone" AutoPostBack="True" CssClass="col_xs" runat="server" Text='<%# Eval("phone") %>' 
										OnTextChanged="text_change" />
								</ItemTemplate>
								<ControlStyle Width="100%" />								
							</asp:TemplateField>			
							<asp:TemplateField SortExpression="email" HeaderText="Email">
										<ItemTemplate>
									<asp:TextBox ID="email" AutoPostBack="True" CssClass="col_xs" runat="server" Text='<%# Eval("email") %>' 
										OnTextChanged="text_change" />
								</ItemTemplate>
								<ControlStyle Width="100%" />								
							</asp:TemplateField>			
							<asp:TemplateField visible="False" HeaderText="Org">
								<ItemTemplate>
									<asp:DropDownList id="ddlOrg" AutoPostBack="True" OnSelectedIndexChanged="Selection_Change_Org" runat="server" 
									CssClass="mlm2" SelectedValue='<%# Eval("org") %>' TabIndex='<%# TabIndex %>'>
										<asp:ListItem Value=""> </asp:ListItem>
										<asp:ListItem Value="Day"> Day </asp:ListItem>
										<asp:ListItem Value="Fdn"> Fdn </asp:ListItem>
									</asp:DropDownList>
								</ItemTemplate>
								<ControlStyle Width="100%" />								
							</asp:TemplateField>
							<asp:TemplateField SortExpression="status" HeaderText="Status" >
								<ItemTemplate>
									<asp:DropDownList id="ddlStatus" AutoPostBack="True" OnSelectedIndexChanged="Selection_Change_Status" runat="server"  
									CssClass="col_small" SelectedValue='<%# Eval("status") %>' TabIndex='<%# TabIndex %>'>
										<asp:ListItem Value="In The Shop"> In The Shop </asp:ListItem>
										<asp:ListItem Value="Scheduled"> Scheduled </asp:ListItem>
										<asp:ListItem Value="Named"> Named </asp:ListItem>
									</asp:DropDownList>
								</ItemTemplate>
								<ControlStyle Width="100%" />								
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
							<asp:TemplateField SortExpression="notes" HeaderText="Notes">
										<ItemTemplate>
									<asp:TextBox ID="notes" AutoPostBack="True" CssClass="col_large" runat="server" Text='<%# Eval("notes") %>' 
										OnTextChanged="text_change" />
								</ItemTemplate>
								<ControlStyle Width="100%" />								
							</asp:TemplateField>
							<asp:TemplateField SortExpression="rank" HeaderText="Rank" Visible="false">
								<ItemTemplate>
									<asp:DropDownList id="ddlRank" AutoPostBack="True" OnSelectedIndexChanged="Rank_Change" runat="server" CssClass="col_xxs" 
									SelectedValue='<%# Eval("rank") %>' TabIndex='<%# TabIndex %>'>
										<asp:ListItem Value=""></asp:ListItem>
										<asp:ListItem Value="a"> A </asp:ListItem>
										<asp:ListItem Value="b"> B </asp:ListItem>
										<asp:ListItem Value="c"> C </asp:ListItem>
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
							<asp:TemplateField visible="False" HeaderText="Category">
								<ItemTemplate>
									<asp:DropDownList id="ddlRegCat" AutoPostBack="True" OnSelectedIndexChanged="Selection_Change" runat="server" 
									CssClass="mlm2" SelectedValue='<%# Eval("Reg_Cat_ID") %>' TabIndex='<%# TabIndex %>'>
										<asp:ListItem Value="LineUp"> LineUp </asp:ListItem>
										<asp:ListItem Value="Archive"> Archive </asp:ListItem>
									</asp:DropDownList>
								</ItemTemplate>
								<ControlStyle Width="100%" />								
							</asp:TemplateField>
							<asp:TemplateField>
								<ItemTemplate>
									<asp:LinkButton ID="lnkBtnEdit" class="btn btn-default btn-xs" runat="server" Text="Edit" 
									OnClick="Display"></asp:LinkButton>
								</ItemTemplate>						
							</asp:TemplateField>
							<asp:TemplateField>
								<ItemTemplate>
									<asp:LinkButton ID="lnkBtnDelete" class="btn btn-danger btn-outline btn-xs" runat="server" Text="Delete" 
									OnClick="DeleteRow"></asp:LinkButton>
								</ItemTemplate>						
							</asp:TemplateField>
						</Columns>					
					</asp:GridView>
										
						<div class="form-group mb-4 mr-sm-2 mb-sm-0">
							<div class='input-group date col-xs-2' style="padding-right: 5px;" id='datetimepicker11'>
								<input id="weekendingText" runat="server" type='text' placeholder="Weekending:" class="form-control" />
								<span class="input-group-addon">
									<span class="glyphicon glyphicon-calendar">
									</span>
								</span>
								<button type="button" id="btnArchiveModal" runat="server" class="form-control btn btn-primary btn-outline" data-toggle="modal" data-target="#archiveModal">Archive Week</button>								
							</div>  
						</div>
						<script type="text/javascript">
							$(function () {
								$('#datetimepicker11').datepicker({
									daysOfWeekDisabled: [0,1,2,3,5,6],
									daysOfWeekHighlighted: "4",
									format: 'dd-M-yyyy',
									autoclose: true,
									todayHighlight: true,
									orientation: "bottom left"
								});
							});
						</script>
										
				</div>			
				<div id="ArchiveTable" runat="server">
					<!-- <ItemTemplate> -->
						<!-- <asp:Button type="button" class="btn btn-primary" id="AddInv" text="Add" data-toggle="modal" data-target="#addModal" />Add Record</asp:Button> -->
					<!-- </ItemTemplate> -->
					<div style="clear: both;"></div>
					<div id="search">	
						<asp:Panel runat="server" DefaultButton="btnSearchArchive">
							<div class="form-group mb-2 mr-sm-2 mb-sm-0">
								<asp:DropDownList id="ddlSearchArchive" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server">
									<asp:ListItem Value="Name"> Name </asp:ListItem>
									<asp:ListItem Value="Area"> Area </asp:ListItem>
									<asp:ListItem Value="Service"> Service </asp:ListItem>
									<asp:ListItem Value="Reg"> Reg </asp:ListItem>
									<asp:ListItem Value="fsm"> FSM </asp:ListItem>
									<asp:ListItem Value="Line"> Line </asp:ListItem>
									<asp:ListItem Value="status"> Status </asp:ListItem>
								</asp:DropDownList>  
								<asp:TextBox ID="txtArchive" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server"></asp:TextBox>
								<asp:Button ID="btnSearchArchive" runat="server" Text="Search" class="btn btn-primary btn-outline" OnClick="BtnSearch_Click" />												
								<div class="form-group mb-4 mr-sm-2 mb-sm-0 pull-right">
									<div class='input-group date col-xs-2' style="padding-right: 5px;" id='datetimepicker12'>
										<input id="weText" runat="server" type='text' placeholder="Weekending:" class="form-control" />
										<span class="input-group-addon">
											<span class="glyphicon glyphicon-calendar">
											</span>
										</span>
										<button type="button" id="btnSearchWE" runat="server" class="form-control btn btn-primary btn-outline" OnServerClick="BtnWESearch_Click">Filter Weekend</button>								
									</div>
								</div>
								<script type="text/javascript">
									$(function () {
										$('#datetimepicker12').datepicker({
											daysOfWeekDisabled: [0,1,2,3,5,6],
											daysOfWeekHighlighted: "4",
											format: 'dd-M-yyyy',
											autoclose: true,
											todayHighlight: true,
											orientation: "bottom left"
										});
									});
								</script>
							</div>
						</asp:Panel>
					</div>				
					<asp:GridView 
						ID="GridViewArchive" 
						runat="server" 
						AllowPaging="True" 
						OnRowDeleting="OnRowDeleting"
						OnPageIndexChanging="grdArchiveData_PageIndexChanging" 
						borderwidth="0" 
						OnSorting="TaskGridView_Sorting" 
						GridLines="None"
						CssClass="mGrid"
						PagerStyle-CssClass="pgr"
						AlternatingRowStyle-CssClass="alt"
						AllowSorting="True" 
						PageSize="<%# Convert.ToInt32(ddlPage.Text) %>" 
						AutoGenerateColumns="False" 
						CellPadding="0">
						<Columns>
							<asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID">
								<HeaderStyle CssClass="no-display"></HeaderStyle>
								<ItemStyle CssClass="no-display"></ItemStyle>
							</asp:BoundField>  
							<asp:BoundField DataField="addo_ID">
								<HeaderStyle CssClass="no-display"></HeaderStyle>
								<ItemStyle CssClass="no-display"></ItemStyle>
							</asp:BoundField>  
							<asp:TemplateField HeaderText="Addo ID">
								<ItemTemplate>
									<asp:HyperLink runat="server" class="no-print" Text='<%# Eval("Addo_ID") != System.DBNull.Value ? Eval("Addo_ID") : "" %>' Target="_blank" 
									NavigateUrl='<%# Eval("Addo_ID") != System.DBNull.Value ? 
									"http://webapp2a.scientology.net/portal/cars/cosCarsPersonReport.jsp?hideLinks=true&addoId=" + Eval("Addo_ID") + 
									"&orgId=89&browser=&userId=allhandsharlem&password=harlembas1" : ""%>'
									style='<%# Eval("Addo_ID") == System.DBNull.Value ? "display: none;" : "" %>' />
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField SortExpression="name" HeaderText="Name">                
								<ItemTemplate>
									<asp:TextBox ID="name" CssClass="col_med" AutoPostBack="True" runat="server" Text='<%# Eval("name") %>' OnTextChanged="text_change" />
								</ItemTemplate>
								<ControlStyle width="100%" />																
							</asp:TemplateField>
							<asp:TemplateField SortExpression="area" HeaderText="Area">
								<ItemStyle CssClass="mlm2"></ItemStyle>
								<ItemTemplate>
									<asp:DropDownList id="area" name="area" AutoPostBack="True" runat="server" Width=125 OnSelectedIndexChanged="Selection_Change_Reg"
										CssClass="mlm2" SelectedValue='<%# Eval("area") %>' >
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
								<ItemTemplate>
									<asp:TextBox ID="service" CssClass="col_xs" AutoPostBack="True" runat="server" Text='<%# Eval("service") %>' OnTextChanged="text_change" 
										TabIndex='<%# TabIndex %>' />
								</ItemTemplate>
								<ControlStyle Width="100%" />								
							</asp:TemplateField>
							<asp:TemplateField SortExpression="reg" HeaderText="Reg">
								<ItemTemplate>
									<asp:TextBox ID="reg" CssClass="col_xs" AutoPostBack="True" runat="server" Text='<%# Eval("reg") %>' OnTextChanged="text_change" 
										TabIndex='<%# TabIndex %>' />
								</ItemTemplate>
								<ControlStyle Width="100%" />								
							</asp:TemplateField>			
							<asp:TemplateField SortExpression="scheduled" Visible="False" HeaderText="Scheduled">												
								<ItemTemplate>
								<div id="appt" style="position: relative;">
									<asp:TextBox ID="scheduled" AutoPostBack="True" CssClass="col_small_dt" runat="server" Text='<%# Eval("scheduled", "{0:M-dd-yyyy h:mm tt}") %>' 
										OnTextChanged="text_change" />
								</div>
								</ItemTemplate>
								<ControlStyle width="100%" />																
							</asp:TemplateField>

							<asp:TemplateField SortExpression="weekend" HeaderText="W/E">												
								<ItemTemplate>
								<div id="weekend" style="position: relative;">
									<asp:TextBox ID="tm" AutoPostBack="True" CssClass="col_xs_center" runat="server" Text='<%# Eval("weekend", "{0:dd-MMM-yyyy}") %>' 
										OnTextChanged="text_change" />
								</div>
								<script type="text/javascript">
									$(function () {
										$("[#weekend]").datepicker({
											daysOfWeekDisabled: [0,1,2,3,5,6],
											daysOfWeekHighlighted: "4",
											format: 'dd-M-yyyy',
											autoclose: true,
											todayHighlight: true,
											orientation: "bottom left"
										});
									});		
								</script>
								
								</ItemTemplate>
								<ControlStyle width="100%" />																
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
							<asp:TemplateField SortExpression="fsm" HeaderText="FSM">
										<ItemTemplate>
									<asp:TextBox ID="fsm" CssClass="col_xs" AutoPostBack="True" runat="server" Text='<%# Eval("fsm") %>' 
										OnTextChanged="text_change" />
								</ItemTemplate>									        		
								<ControlStyle Width="100%" />								
							</asp:TemplateField>
							<asp:TemplateField SortExpression="phone" HeaderText="Phone">
										<ItemTemplate>
									<asp:TextBox ID="phone" AutoPostBack="True" CssClass="col_xs" runat="server" Text='<%# Eval("phone") %>' 
										OnTextChanged="text_change" />
								</ItemTemplate>
								<ControlStyle Width="100%" />								
							</asp:TemplateField>			
							<asp:TemplateField SortExpression="email" HeaderText="Email">
										<ItemTemplate>
									<asp:TextBox ID="email" AutoPostBack="True" CssClass="col_xs" runat="server" Text='<%# Eval("email") %>' 
										OnTextChanged="text_change" />
								</ItemTemplate>
								<ControlStyle Width="100%" />								
							</asp:TemplateField>			
							<asp:TemplateField SortExpression="notes" HeaderText="Notes">
										<ItemTemplate>
									<asp:TextBox ID="notes" AutoPostBack="True" CssClass="col_large" runat="server" Text='<%# Eval("notes") %>' 
										OnTextChanged="text_change" />
								</ItemTemplate>
								<ControlStyle Width="100%" />								
							</asp:TemplateField>
							<asp:TemplateField visible="True" HeaderText="Category">
								<ItemTemplate>
									<asp:DropDownList id="ddlRegCat" AutoPostBack="True" OnSelectedIndexChanged="Selection_Change" runat="server" 
									CssClass="mlm2" SelectedValue='<%# Eval("Reg_Cat_ID") %>' TabIndex='<%# TabIndex %>'>
										<asp:ListItem Value="LineUp"> LineUp </asp:ListItem>
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
									<asp:LinkButton ID="lnkBtnAdd" class="btn btn-primary btn-outline btn-xs" runat="server" Text="Add to New Week" OnClick="ViewArchive"></asp:LinkButton>
								</ItemTemplate>						
							</asp:TemplateField>
						</Columns>					
					</asp:GridView>
				</div>								
				<div id="Two" 	runat="server">
					<asp:Panel runat="server" DefaultButton="Button2">
						<div id="search_Inv">
							<div class="form-group mb-2 mr-sm-2 mb-sm-0">
								<asp:DropDownList id="ddlSearchInv" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server">
										<asp:ListItem Value="Name"> Name </asp:ListItem>
										<asp:ListItem Value="Amount"> Amount </asp:ListItem>
										<asp:ListItem Value="Service"> Service </asp:ListItem>
								</asp:DropDownList>  
								<asp:TextBox ID="TextBox2" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server"></asp:TextBox>
								<asp:Button ID="Button2" runat="server" Text="Search" class="btn btn-primary btn-outline" OnClick="BtnSearch_Click_Inv" />												
							</div>
						</div>
					</asp:Panel>
					<asp:GridView ID="GridView2" runat="server" CssClass="grid" AllowPaging="True" OnRowDeleting="OnRowDeleting" OnPageIndexChanging="grdData_PageIndexChanging_Inv" 
					OnSorting="TaskGridView_Sorting" AllowSorting="True" PageSize="<%# Convert.ToInt32(ddlPage.Text) %>"  AutoGenerateColumns="False" Width="1250px" >
						<Columns>
							<asp:BoundField DataField="addo_ID">
								<HeaderStyle CssClass="no-display"></HeaderStyle>
								<ItemStyle CssClass="no-display"></ItemStyle>
							</asp:BoundField>  
							<asp:TemplateField HeaderText="Addo ID">
								<ItemTemplate>
									<asp:HyperLink runat="server" class="no-print" Text='<%# Eval("Addo_ID") %>' Target="_blank" NavigateUrl='<%# "http://webapp2a.scientology.net/portal/cars/cosCarsPersonReport.jsp?hideLinks=true&addoId="+ Eval("Addo_ID") +"&orgId=89&browser=&userId=allhandsharlem&password=harlembas1"%>' />
								</ItemTemplate>
							</asp:TemplateField> 
							<asp:BoundField DataField="name" HeaderText="Full Name" SortExpression="name" />
							<asp:BoundField DataField="item" HeaderText="Service" SortExpression="item" />
							<asp:BoundField DataField="amt_paid" HeaderText="Amount" SortExpression="amt_paid" />
							<asp:BoundField DataField="fsm_name" HeaderText="FSM" SortExpression="fsm_name" />
							<asp:BoundField DataField="phone" HeaderText="Phone" SortExpression="phone" />
							<asp:BoundField DataField="inv_date" HeaderText="Date" SortExpression="inv_date" />  
							<asp:TemplateField HeaderText="">
								<ItemTemplate>
									<asp:Button id="LineupAdd" text="Add to Lineup" OnClick="LineUp_Add_Click" runat="server" />
								</ItemTemplate>
							</asp:TemplateField>
						</Columns>			
					</asp:GridView>				
				</div>
				<div id="Three" runat="server">
					<asp:Panel runat="server" DefaultButton="Button1">
						<div id="search_FPPP">	
							<div class="form-group mb-2 mr-sm-2 mb-sm-0">
								<asp:DropDownList id="ddlSearchFPPP" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server">
									<asp:ListItem Value="Name"> Name </asp:ListItem>
									<asp:ListItem Value="Service"> Service </asp:ListItem>										
								</asp:DropDownList>  
								<asp:TextBox ID="TextBox1" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server"></asp:TextBox>
								<asp:Button ID="Button1" runat="server" Text="Search" class="btn btn-primary btn-outline" OnClick="BtnSearch_Click_FPPP" />												
							</div>
						</div>
					</asp:Panel>
					<asp:GridView ID="GridView3" 
						runat="server" 
						GridLines="None"
						CssClass="mGrid"
						PagerStyle-CssClass="pgr"
						AlternatingRowStyle-CssClass="alt"
						AllowPaging="True" 
						OnRowDeleting="OnRowDeleting" 
						OnPageIndexChanging="grdData_PageIndexChanging_FPPP" 
						OnSorting="TaskGridView_Sorting" 
						AllowSorting="True" 
						PageSize="<%# Convert.ToInt32(ddlPage.Text) %>" AutoGenerateColumns="False" Width="1250px" >
						<Columns>
							<asp:BoundField DataField="addo_ID">
								<HeaderStyle CssClass="no-display"></HeaderStyle>
								<ItemStyle CssClass="no-display"></ItemStyle>
							</asp:BoundField>  
							<asp:TemplateField HeaderText="Addo ID">
								<ItemTemplate>
									<asp:HyperLink runat="server" class="no-print" Text='<%# Eval("Addo_ID") %>' Target="_blank" NavigateUrl='<%# "http://webapp2a.scientology.net/portal/cars/cosCarsPersonReport.jsp?hideLinks=true&addoId="+ Eval("Addo_ID") +"&orgId=89&browser=&userId=allhandsharlem&password=harlembas1"%>' />
								</ItemTemplate>
							</asp:TemplateField> 
							<asp:BoundField DataField="name" HeaderText="Name" SortExpression="name" />
							<asp:BoundField DataField="item" HeaderText="Service" SortExpression="item" />
							<asp:BoundField DataField="amt_paid" HeaderText="Amount Paid" SortExpression="amt_paid" />
							<asp:BoundField DataField="amt_to_fp" HeaderText="Amount Left" SortExpression="amt_to_fp" />
							<asp:BoundField DataField="phone" HeaderText="Phone" SortExpression="phone" />
							<asp:BoundField DataField="fp" HeaderText="Fully Paid" SortExpression="fp" />
							<asp:BoundField DataField="create_time" HeaderText="Date" SortExpression="create_time" />					
							<asp:TemplateField HeaderText="">
								<ItemTemplate>
									<asp:Button id="LineupAdd" text="Add to Lineup" OnClick="LineUp_Add_Click" runat="server" />
								</ItemTemplate>
						</asp:TemplateField>
						</Columns> 
					</asp:GridView>				
				</div>
				<div id="Four" 	runat="server">
					<div style="clear: both;"></div>
					<asp:Table runat="server" CellSpacing="1" CssClass="head-text" Font-Names="Arial" Font-Size="Small" Width="875px" BackColor="Silver" Font-Italic="False" Height="100px" CaptionAlign="Left" HorizontalAlign="Left">
						<asp:TableRow runat="server" BackColor="Silver" BorderColor="White" BorderStyle="None">
							<asp:TableCell Text="Org" runat="server" BorderWidth="0px" BorderStyle="solid" BorderColor="black" />
							<asp:TableCell Text="In The Shop:" BorderWidth="0px" BorderStyle="solid" BorderColor="black" runat="server"/>
							<asp:TableCell Text="Scheduled:" BorderWidth="0px" BorderStyle="solid" BorderColor="black"  runat="server"/>
							<asp:TableCell Text="Named:" BorderWidth="0px" BorderStyle="solid" BorderColor="black"  runat="server"/>
							<asp:TableCell Text="In/Scheduled:" runat="server" BorderWidth="0px" BorderStyle="solid" BorderColor="black" />
							<asp:TableCell Text="In/Sched/Named:" BorderWidth="0px" BorderStyle="solid" BorderColor="black" runat="server"/>
						</asp:TableRow>
						<asp:TableRow>
							<asp:TableCell runat="server" Text="Day & Fdn" BorderWidth="0px" BorderStyle="solid" BorderColor="black" />
							<asp:TableCell runat="server" id="BothBIS" BorderWidth="0px" BorderStyle="solid" BorderColor="black" />
							<asp:TableCell runat="server" id="BothScheduled" BorderWidth="0px" BorderStyle="solid" BorderColor="black" />
							<asp:TableCell runat="server" id="BothNamed" BorderWidth="0px" BorderStyle="solid" BorderColor="black" />
							<asp:TableCell runat="server" id="BothInSched" BorderWidth="0px" BorderStyle="solid" BorderColor="black" />
							<asp:TableCell runat="server" id="BothInSchedNamed" BorderWidth="0px" BorderStyle="solid" BorderColor="black" />
						</asp:TableRow>
						<asp:TableRow>
							<asp:TableCell runat="server" Text="Day" BorderWidth="0px" BorderStyle="solid" BorderColor="black" />
							<asp:TableCell runat="server" id="DayInv" BorderWidth="0px" BorderStyle="solid" BorderColor="black" />
							<asp:TableCell runat="server" id="DayConf" BorderWidth="0px" BorderStyle="solid" BorderColor="black" />
							<asp:TableCell runat="server" id="DayNamed" BorderWidth="0px" BorderStyle="solid" BorderColor="black" />
							<asp:TableCell runat="server" id="DayIn" BorderWidth="0px" BorderStyle="solid" BorderColor="black" />
							<asp:TableCell runat="server" id="DayOn" BorderWidth="0px" BorderStyle="solid" BorderColor="black" />
						</asp:TableRow>
						<asp:TableRow>
							<asp:TableCell runat="server" Text="Fdn" BorderWidth="0px" BorderStyle="solid" BorderColor="black" />
							<asp:TableCell runat="server" id="FdnInv" BorderWidth="0px" BorderStyle="solid" BorderColor="black" />
							<asp:TableCell runat="server" id="FdnConf" BorderWidth="0px" BorderStyle="solid" BorderColor="black" />
							<asp:TableCell runat="server" id="FdnNamed" BorderWidth="0px" BorderStyle="solid" BorderColor="black" />
							<asp:TableCell runat="server" id="FdnIn" BorderWidth="0px" BorderStyle="solid" BorderColor="black" />
							<asp:TableCell runat="server" id="FdnOn" BorderWidth="0px" BorderStyle="solid" BorderColor="black" />
						</asp:TableRow>
					</asp:Table>			
					<br />
					<br />
					<br />
					<br />
					<br />
					<br />
					<div id="search_combo">	
						<asp:Panel runat="server" DefaultButton="Button5">
							<div class="form-group mb-2 mr-sm-2 mb-sm-0">
								<asp:DropDownList id="ddlSearchGI" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server">
									<asp:ListItem Value="Name"> Name </asp:ListItem>
									<asp:ListItem Value="area"> Area </asp:ListItem>
									<asp:ListItem Value="service"> Service </asp:ListItem>
									<asp:ListItem Value="fsm"> FSM </asp:ListItem>
								</asp:DropDownList>  
								<asp:TextBox ID="txtGI" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server"></asp:TextBox>
								<asp:Button ID="Button5" runat="server" Text="Search" class="btn btn-primary btn-outline" OnClick="BtnSearch_Click" />												
							</div>
						</asp:Panel>
					</div>
					<div class="panel-group">
						<div class="panel panel-default">
						<div class="panel-heading">
						<h4 class="panel-title">
							<a data-toggle="collapse" href="#collapse0">
								<asp:Label runat="server" Text="In The Shop" CssClass="CombGITable" />
								<asp:Label runat="server" id="BothInLbl" Text=""  CssClass="CombGITable" />
							</a>
						</h4>
						</div>
							<div id="collapse0" class="panel-collapse collapse in">
								<div class="panel-body">
									<asp:GridView 
										ID="GridView4" 
										runat="server" 
										OnSorting="TaskGridView_Sorting" 
										AllowSorting="True" 
										AutoGenerateColumns="False" 
										BorderWidth="1px" 
										CellPadding="2" 
										ForeColor="Black" 
										GridLines="None" 
										Width="835px" >
										<Columns>
											<asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID">
												<HeaderStyle CssClass="no-display"></HeaderStyle>
												<ItemStyle CssClass="no-display"></ItemStyle>
											</asp:BoundField>
											<asp:BoundField DataField="org" HeaderText="Org" SortExpression="org" >
												<HeaderStyle HorizontalAlign="Left" CssClass="mlm2" />
											</asp:BoundField>
											<asp:TemplateField HeaderText="ID">
												<ItemTemplate>
													<asp:HyperLink runat="server" class="no-print" Text='<%# Eval("Addo_ID") %>' Target="_blank" NavigateUrl='<%# "http://webapp2a.scientology.net/portal/cars/cosCarsPersonReport.jsp?hideLinks=true&addoId="+ Eval("Addo_ID") +"&orgId=89&browser=&userId=allhandsharlem&password=harlembas1"%>' />
												</ItemTemplate>
											</asp:TemplateField>                    										
											<asp:TemplateField SortExpression="name" HeaderText="Name">
												<HeaderStyle HorizontalAlign="Left" CssClass="mlm2" />
												<ItemTemplate>
													<asp:TextBox ID="name" AutoPostBack="True" CssClass="mlm2" runat="server" Text='<%# Eval("name") %>' Width=125 OnTextChanged="text_change" />
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField SortExpression="area" HeaderText="Area">
												<HeaderStyle HorizontalAlign="Left" CssClass="mlm2" />
												<ItemTemplate>
													<asp:DropDownList id="ddlReg" name="ddlReg" AutoPostBack="True" runat="server" Width=125 OnSelectedIndexChanged="Selection_Change_Reg"
														CssClass="mlm2" SelectedValue='<%# Eval("area") %>' >
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
												<HeaderStyle HorizontalAlign="Left" CssClass="mlm2" />
												<ItemTemplate>
												<asp:TextBox ID="service" AutoPostBack="True" CssClass="mlm2" runat="server" Text='<%# Eval("service") %>' Width=100 OnTextChanged="text_change" />
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField SortExpression="reg" HeaderText="Terminal">
												<HeaderStyle HorizontalAlign="Left" CssClass="mlm2" />
												<ItemTemplate>
												<asp:TextBox ID="reg" AutoPostBack="True" CssClass="mlm2" runat="server" Text='<%# Eval("reg") %>' Width=75 OnTextChanged="text_change" />
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField SortExpression="fsm" HeaderText="FSM">
												<HeaderStyle HorizontalAlign="Left" CssClass="mlm2" />
												<ItemTemplate>
												<asp:TextBox ID="fsm" AutoPostBack="True" CssClass="mlm2" runat="server" Text='<%# Eval("fsm") %>' Width=75 OnTextChanged="text_change" />
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField visible="False" HeaderText="Org">
												<ItemTemplate>
													<asp:DropDownList id="ddlOrg" AutoPostBack="True" OnSelectedIndexChanged="Selection_Change_Org" runat="server" 
													CssClass="mlm2" SelectedValue='<%# Eval("org") %>' TabIndex='<%# TabIndex %>'>
														<asp:ListItem Value=""> </asp:ListItem>
														<asp:ListItem Value="Day"> Day </asp:ListItem>
														<asp:ListItem Value="Fdn"> Fdn </asp:ListItem>
													</asp:DropDownList>
												</ItemTemplate>
												<ControlStyle Width="100%" />								
											</asp:TemplateField>
											<asp:TemplateField Visible="false" HeaderText="Scheduled">												
												<ItemTemplate>
												<div id="scheduled" style="position: relative;">
													<asp:TextBox ID="scheduled" AutoPostBack="True" CssClass="col_small" runat="server" Text='<%# Eval("scheduled", "{0:M-dd-yyyy h:mm tt}") %>' 
														OnTextChanged="text_change" />
												</div>
												</ItemTemplate>
												<ControlStyle width="100%" />																
											</asp:TemplateField>			
											<asp:TemplateField Visible="false" HeaderText="Phone">
														<ItemTemplate>
													<asp:TextBox ID="phone" AutoPostBack="True" CssClass="col_xs" runat="server" Text='<%# Eval("phone") %>' 
														OnTextChanged="text_change" />
												</ItemTemplate>
												<ControlStyle Width="100%" />								
											</asp:TemplateField>			
											<asp:TemplateField Visible="false" HeaderText="Email">
														<ItemTemplate>
													<asp:TextBox ID="email" AutoPostBack="True" CssClass="col_xs" runat="server" Text='<%# Eval("email") %>' 
														OnTextChanged="text_change" />
												</ItemTemplate>
												<ControlStyle Width="100%" />								
											</asp:TemplateField>			
											<asp:TemplateField SortExpression="status" HeaderText="Status" >
												<HeaderStyle HorizontalAlign="Left" CssClass="mlm2" />
												<ItemTemplate>
												<asp:DropDownList id="ddlStatus" AutoPostBack="True" OnSelectedIndexChanged="Selection_Change_Status" Width=100 runat="server" 
												CssClass="mlm2" SelectedValue='<%# Eval("status") %>' TabIndex='<%# TabIndex %>'>
													<asp:ListItem Value="In The Shop"> In The Shop </asp:ListItem>
													<asp:ListItem Value="Scheduled"> Scheduled </asp:ListItem>
													<asp:ListItem Value="Named"> Named </asp:ListItem>
												</asp:DropDownList>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField visible="False" SortExpression="rank" HeaderText="Rank">
												<ItemTemplate>
													<asp:DropDownList id="ddlRank" AutoPostBack="True" OnSelectedIndexChanged="Rank_Change" runat="server" CssClass="col_xxs" 
													SelectedValue='<%# Eval("rank") %>' TabIndex='<%# TabIndex %>'>
														<asp:ListItem Value=""></asp:ListItem>
														<asp:ListItem Value="a"> A </asp:ListItem>
														<asp:ListItem Value="b"> B </asp:ListItem>
														<asp:ListItem Value="c"> C </asp:ListItem>
													</asp:DropDownList>
												</ItemTemplate>
												<ControlStyle Width="100%" />								
											</asp:TemplateField>
											<asp:TemplateField SortExpression="notes" HeaderText="Notes">
												<HeaderStyle HorizontalAlign="Left" CssClass="mlm2" />
												<ItemTemplate>
												<asp:TextBox ID="notes" AutoPostBack="True" CssClass="mlm2" runat="server" Text='<%# Eval("notes") %>' Width="175" OnTextChanged="text_change" />
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField>
												<ItemTemplate>
													<asp:LinkButton ID="lnkBtnEdit" Class="glyphicon glyphicon-edit" runat="server" style="color:grey" OnClick="Display"></asp:LinkButton>
												</ItemTemplate>						
											</asp:TemplateField>
											<asp:TemplateField>
												<ItemTemplate>
													<asp:LinkButton ID="lnkBtnDelete" Class="glyphicon glyphicon-remove" style="color:red" runat="server" 
													OnClick="DeleteRow"></asp:LinkButton>
												</ItemTemplate>						
											</asp:TemplateField>										
										</Columns>
										<FooterStyle BackColor="Tan" />
										<HeaderStyle BackColor="Silver" Font-Bold="True" Font-Names="Arial Black" HorizontalAlign="Left" />
										<PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
										<RowStyle Height="22px" />
										<SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
										<SortedAscendingCellStyle BackColor="#FAFAE7" />
										<SortedAscendingHeaderStyle BackColor="#DAC09E" />
										<SortedDescendingCellStyle BackColor="#E1DB9C" />
										<SortedDescendingHeaderStyle BackColor="#C2A47B" />
									</asp:GridView>	
								</div>
							</div>
						</div>
					</div>					
					<div class="panel-group">
						<div class="panel panel-default">
							<div class="panel-heading">
								<h4 class="panel-title">
								<a data-toggle="collapse" href="#collapseA">
								<asp:Label runat="server" Text="Scheduled For The Week" CssClass="CombGITable" />
								<asp:Label runat="server" id="SchedLbl" Text="" CssClass="CombGITable" />
								</a>
								</h4>
							</div>
							<div id="collapseA" class="panel-collapse collapse in">
								<div class="panel-body">
									<asp:GridView 
										ID="GridView5" 
										runat="server" 
										OnSorting="TaskGridView_Sorting" 
										AllowSorting="True" 
										AutoGenerateColumns="False" 
										BorderWidth="1px" 
										CellPadding="2" 
										ForeColor="Black" 
										GridLines="None" 
										Width="835px" >
										<Columns>
											<asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID">
												<HeaderStyle CssClass="no-display"></HeaderStyle>
												<ItemStyle CssClass="no-display"></ItemStyle>
											</asp:BoundField>
											<asp:BoundField DataField="org" HeaderText="Org" SortExpression="org" >
												<HeaderStyle HorizontalAlign="Left" CssClass="mlm2" />
											</asp:BoundField>
											<asp:TemplateField HeaderText="ID">
												<ItemTemplate>
													<asp:HyperLink runat="server" class="no-print" Text='<%# Eval("Addo_ID") %>' Target="_blank" NavigateUrl='<%# "http://webapp2a.scientology.net/portal/cars/cosCarsPersonReport.jsp?hideLinks=true&addoId="+ Eval("Addo_ID") +"&orgId=89&browser=&userId=allhandsharlem&password=harlembas1"%>' />
												</ItemTemplate>
											</asp:TemplateField>                    										
											<asp:TemplateField SortExpression="name" HeaderText="Name">
												<HeaderStyle HorizontalAlign="Left" CssClass="mlm2" />
												<ItemTemplate>
													<asp:TextBox ID="name" AutoPostBack="True" CssClass="mlm2" runat="server" Text='<%# Eval("name") %>' Width=125 OnTextChanged="text_change" />
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField SortExpression="area" HeaderText="Area">
												<HeaderStyle HorizontalAlign="Left" CssClass="mlm2" />
												<ItemTemplate>
													<asp:DropDownList id="ddlReg" name="ddlReg" AutoPostBack="True" runat="server" Width=125 OnSelectedIndexChanged="Selection_Change_Reg"
														CssClass="mlm2" SelectedValue='<%# Eval("area") %>' >
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
												<HeaderStyle HorizontalAlign="Left" CssClass="mlm2" />
												<ItemTemplate>
												<asp:TextBox ID="service" AutoPostBack="True" CssClass="mlm2" runat="server" Text='<%# Eval("service") %>' Width=100 OnTextChanged="text_change" />
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField SortExpression="reg" HeaderText="Reg">
												<HeaderStyle HorizontalAlign="Left" CssClass="mlm2" />
												<ItemTemplate>
												<asp:TextBox ID="reg" AutoPostBack="True" CssClass="mlm2" runat="server" Text='<%# Eval("reg") %>' Width=75 OnTextChanged="text_change" />
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField SortExpression="fsm" HeaderText="FSM">
												<HeaderStyle HorizontalAlign="Left" CssClass="mlm2" />
												<ItemTemplate>
												<asp:TextBox ID="fsm" AutoPostBack="True" CssClass="mlm2" runat="server" Text='<%# Eval("fsm") %>' Width=75 OnTextChanged="text_change" />
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField visible="False" HeaderText="Org">
												<ItemTemplate>
													<asp:DropDownList id="ddlOrg" AutoPostBack="True" OnSelectedIndexChanged="Selection_Change_Org" runat="server" 
													CssClass="mlm2" SelectedValue='<%# Eval("org") %>' TabIndex='<%# TabIndex %>'>
														<asp:ListItem Value=""> </asp:ListItem>
														<asp:ListItem Value="Day"> Day </asp:ListItem>
														<asp:ListItem Value="Fdn"> Fdn </asp:ListItem>
													</asp:DropDownList>
												</ItemTemplate>
												<ControlStyle Width="100%" />								
											</asp:TemplateField>
											<asp:TemplateField Visible="false" HeaderText="Scheduled">												
												<ItemTemplate>
												<div id="scheduled" style="position: relative;">
													<asp:TextBox ID="scheduled" AutoPostBack="True" CssClass="col_small" runat="server" Text='<%# Eval("scheduled", "{0:M-dd-yyyy h:mm tt}") %>' 
														OnTextChanged="text_change" />
												</div>
												</ItemTemplate>
												<ControlStyle width="100%" />																
											</asp:TemplateField>			
											<asp:TemplateField Visible="false" HeaderText="Phone">
														<ItemTemplate>
													<asp:TextBox ID="phone" AutoPostBack="True" CssClass="col_xs" runat="server" Text='<%# Eval("phone") %>' 
														OnTextChanged="text_change" />
												</ItemTemplate>
												<ControlStyle Width="100%" />								
											</asp:TemplateField>			
											<asp:TemplateField Visible="false" HeaderText="Email">
														<ItemTemplate>
													<asp:TextBox ID="email" AutoPostBack="True" CssClass="col_xs" runat="server" Text='<%# Eval("email") %>' 
														OnTextChanged="text_change" />
												</ItemTemplate>
												<ControlStyle Width="100%" />								
											</asp:TemplateField>			
											<asp:TemplateField SortExpression="status" HeaderText="Status" >
												<HeaderStyle HorizontalAlign="Left" CssClass="mlm2" />
												<ItemTemplate>
												<asp:DropDownList id="ddlStatus" AutoPostBack="True" OnSelectedIndexChanged="Selection_Change_Status" Width=100 runat="server" 
												CssClass="mlm2" SelectedValue='<%# Eval("status") %>' TabIndex='<%# TabIndex %>'>
													<asp:ListItem Value="In The Shop"> In The Shop </asp:ListItem>
													<asp:ListItem Value="Scheduled"> Scheduled </asp:ListItem>
													<asp:ListItem Value="Named"> Named </asp:ListItem>
												</asp:DropDownList>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField visible="False" SortExpression="rank" HeaderText="Rank">
												<ItemTemplate>
													<asp:DropDownList id="ddlRank" AutoPostBack="True" OnSelectedIndexChanged="Rank_Change" runat="server" CssClass="col_xxs" 
													SelectedValue='<%# Eval("rank") %>' TabIndex='<%# TabIndex %>'>
														<asp:ListItem Value=""></asp:ListItem>
														<asp:ListItem Value="a"> A </asp:ListItem>
														<asp:ListItem Value="b"> B </asp:ListItem>
														<asp:ListItem Value="c"> C </asp:ListItem>
													</asp:DropDownList>
												</ItemTemplate>
												<ControlStyle Width="100%" />								
											</asp:TemplateField>
											<asp:TemplateField SortExpression="notes" HeaderText="Notes">
												<HeaderStyle HorizontalAlign="Left" CssClass="mlm2" />
												<ItemTemplate>
												<asp:TextBox ID="notes" AutoPostBack="True" CssClass="mlm2" runat="server" Text='<%# Eval("notes") %>' Width="175" OnTextChanged="text_change" />
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField>
												<ItemTemplate>
													<asp:LinkButton ID="lnkBtnEdit" Class="glyphicon glyphicon-edit" runat="server" style="color:grey" OnClick="Display"></asp:LinkButton>
												</ItemTemplate>						
											</asp:TemplateField>
											<asp:TemplateField>
												<ItemTemplate>
													<asp:LinkButton ID="lnkBtnDelete" Class="glyphicon glyphicon-remove" style="color:red" runat="server" 
													OnClick="DeleteRow"></asp:LinkButton>
												</ItemTemplate>						
											</asp:TemplateField>										
										</Columns>
										<HeaderStyle Font-Names="Arial Black" ForeColor="Black" BackColor="Gray" HorizontalAlign="Left" />
										<RowStyle Height="22px" />
									</asp:GridView>	
								</div>
							</div>
						</div>
					</div>											
					<div class="panel-group">
					<div class="panel panel-default">
					<div class="panel-heading">
					<h4 class="panel-title">
					<a data-toggle="collapse" href="#collapseB">
					<asp:Label runat="server" Text="Named For The Week" CssClass="CombGITable" />
					<asp:Label runat="server" id="NamedLbl" Text="" CssClass="CombGITable" />
					</a>
					</h4>
					</div>
					<div id="collapseB" class="panel-collapse collapse in">
					<div class="panel-body">
					<asp:GridView 
						ID="GridView6" 
						runat="server" 
						OnSorting="TaskGridView_Sorting" 
						AllowSorting="True" 
						AutoGenerateColumns="False" 
						Width="835px" 
						BorderWidth="1px" 
						CellPadding="2" 
						ForeColor="Black" 
						GridLines="None" >
										<Columns>
											<asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID">
												<HeaderStyle CssClass="no-display"></HeaderStyle>
												<ItemStyle CssClass="no-display"></ItemStyle>
											</asp:BoundField>
											<asp:BoundField DataField="org" HeaderText="Org" SortExpression="org" >
												<HeaderStyle HorizontalAlign="Left" CssClass="mlm2" />
											</asp:BoundField>
											<asp:TemplateField HeaderText="ID">
												<ItemTemplate>
													<asp:HyperLink runat="server" class="no-print" Text='<%# Eval("Addo_ID") %>' Target="_blank" NavigateUrl='<%# "http://webapp2a.scientology.net/portal/cars/cosCarsPersonReport.jsp?hideLinks=true&addoId="+ Eval("Addo_ID") +"&orgId=89&browser=&userId=allhandsharlem&password=harlembas1"%>' />
												</ItemTemplate>
											</asp:TemplateField>                    										
											<asp:TemplateField SortExpression="name" HeaderText="Name">
												<HeaderStyle HorizontalAlign="Left" CssClass="mlm2" />
												<ItemTemplate>
													<asp:TextBox ID="name" AutoPostBack="True" CssClass="mlm2" runat="server" Text='<%# Eval("name") %>' Width=125 OnTextChanged="text_change" />
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField SortExpression="area" HeaderText="Area">
												<HeaderStyle HorizontalAlign="Left" CssClass="mlm2" />
												<ItemTemplate>
													<asp:DropDownList id="ddlReg" name="ddlReg" AutoPostBack="True" runat="server" Width=125 OnSelectedIndexChanged="Selection_Change_Reg"
														CssClass="mlm2" SelectedValue='<%# Eval("area") %>' >
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
												<HeaderStyle HorizontalAlign="Left" CssClass="mlm2" />
												<ItemTemplate>
												<asp:TextBox ID="service" AutoPostBack="True" CssClass="mlm2" runat="server" Text='<%# Eval("service") %>' Width=100 OnTextChanged="text_change" />
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField SortExpression="reg" HeaderText="Reg">
												<HeaderStyle HorizontalAlign="Left" CssClass="mlm2" />
												<ItemTemplate>
												<asp:TextBox ID="reg" AutoPostBack="True" CssClass="mlm2" runat="server" Text='<%# Eval("reg") %>' Width=75 OnTextChanged="text_change" />
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField SortExpression="fsm" HeaderText="FSM">
												<HeaderStyle HorizontalAlign="Left" CssClass="mlm2" />
												<ItemTemplate>
												<asp:TextBox ID="fsm" AutoPostBack="True" CssClass="mlm2" runat="server" Text='<%# Eval("fsm") %>' Width=75 OnTextChanged="text_change" />
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField visible="False" HeaderText="Org">
												<ItemTemplate>
													<asp:DropDownList id="ddlOrg" AutoPostBack="True" OnSelectedIndexChanged="Selection_Change_Org" runat="server" 
													CssClass="mlm2" SelectedValue='<%# Eval("org") %>' TabIndex='<%# TabIndex %>'>
														<asp:ListItem Value=""> </asp:ListItem>
														<asp:ListItem Value="Day"> Day </asp:ListItem>
														<asp:ListItem Value="Fdn"> Fdn </asp:ListItem>
													</asp:DropDownList>
												</ItemTemplate>
												<ControlStyle Width="100%" />								
											</asp:TemplateField>
											<asp:TemplateField Visible="false" HeaderText="Scheduled">												
												<ItemTemplate>
												<div id="scheduled" style="position: relative;">
													<asp:TextBox ID="scheduled" AutoPostBack="True" CssClass="col_small" runat="server" Text='<%# Eval("scheduled", "{0:M-dd-yyyy h:mm tt}") %>' 
														OnTextChanged="text_change" />
												</div>
												</ItemTemplate>
												<ControlStyle width="100%" />																
											</asp:TemplateField>			
											<asp:TemplateField Visible="false" HeaderText="Phone">
														<ItemTemplate>
													<asp:TextBox ID="phone" AutoPostBack="True" CssClass="col_xs" runat="server" Text='<%# Eval("phone") %>' 
														OnTextChanged="text_change" />
												</ItemTemplate>
												<ControlStyle Width="100%" />								
											</asp:TemplateField>			
											<asp:TemplateField Visible="false" HeaderText="Email">
														<ItemTemplate>
													<asp:TextBox ID="email" AutoPostBack="True" CssClass="col_xs" runat="server" Text='<%# Eval("email") %>' 
														OnTextChanged="text_change" />
												</ItemTemplate>
												<ControlStyle Width="100%" />								
											</asp:TemplateField>			
											<asp:TemplateField SortExpression="status" HeaderText="Status" >
												<HeaderStyle HorizontalAlign="Left" CssClass="mlm2" />
												<ItemTemplate>
												<asp:DropDownList id="ddlStatus" AutoPostBack="True" OnSelectedIndexChanged="Selection_Change_Status" Width=100 runat="server" 
												CssClass="mlm2" SelectedValue='<%# Eval("status") %>' TabIndex='<%# TabIndex %>'>
													<asp:ListItem Value="In The Shop"> In The Shop </asp:ListItem>
													<asp:ListItem Value="Scheduled"> Scheduled </asp:ListItem>
													<asp:ListItem Value="Named"> Named </asp:ListItem>
												</asp:DropDownList>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField visible="False" SortExpression="rank" HeaderText="Rank">
												<ItemTemplate>
													<asp:DropDownList id="ddlRank" AutoPostBack="True" OnSelectedIndexChanged="Rank_Change" runat="server" CssClass="col_xxs" 
													SelectedValue='<%# Eval("rank") %>' TabIndex='<%# TabIndex %>'>
														<asp:ListItem Value=""></asp:ListItem>
														<asp:ListItem Value="a"> A </asp:ListItem>
														<asp:ListItem Value="b"> B </asp:ListItem>
														<asp:ListItem Value="c"> C </asp:ListItem>
													</asp:DropDownList>
												</ItemTemplate>
												<ControlStyle Width="100%" />								
											</asp:TemplateField>
											<asp:TemplateField SortExpression="notes" HeaderText="Notes">
												<HeaderStyle HorizontalAlign="Left" CssClass="mlm2" />
												<ItemTemplate>
												<asp:TextBox ID="notes" AutoPostBack="True" CssClass="mlm2" runat="server" Text='<%# Eval("notes") %>' Width="175" OnTextChanged="text_change" />
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField>
												<ItemTemplate>
													<asp:LinkButton ID="lnkBtnEdit" Class="glyphicon glyphicon-edit" runat="server" style="color:grey" OnClick="Display"></asp:LinkButton>
												</ItemTemplate>						
											</asp:TemplateField>
											<asp:TemplateField>
												<ItemTemplate>
													<asp:LinkButton ID="lnkBtnDelete" Class="glyphicon glyphicon-remove" style="color:red" runat="server" 
													OnClick="DeleteRow"></asp:LinkButton>
												</ItemTemplate>						
											</asp:TemplateField>										
										</Columns>
										<HeaderStyle Font-Names="Arial Black" ForeColor="Black" HorizontalAlign="Left" />
					<RowStyle Height="22px" />
					</asp:GridView>	
					</div>
					</div>
					</div>
					</div>											
				</br>		
				</div>
				<div id="Five" 	runat="server">
					<asp:Panel runat="server" DefaultButton="Button3">
						<div id="search_TTL">
							<div class="form-group mb-2 mr-sm-2 mb-sm-0">
								<asp:TextBox ID="TextBox3" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" runat="server"></asp:TextBox>
								<asp:Button ID="Button3" runat="server" Text="Search" class="btn btn-primary btn-outline" OnClick="BtnSearch_Click_TTL" />												
								<asp:LinkButton ID="btnAddNew" class="btn btn-warning btn-outline" runat="server" Text="Not in Addo" OnClick="OpenAddNew"></asp:LinkButton>
							</div>
						</div>
					</asp:Panel>
					<asp:GridView ID="GridView8" 
						runat="server" 
						borderwidth="0" 
						GridLines="None"
						CssClass="mGrid"
						PagerStyle-CssClass="pgr"
						AlternatingRowStyle-CssClass="alt"
						AllowPaging="True" 
						OnRowDeleting="OnRowDeleting" 
						OnPageIndexChanging="grdData_PageIndexChanging_total" 
						OnSorting="TaskGridView_Sorting" 
						AllowSorting="True" 
						PageSize="<%# Convert.ToInt32(ddlPage.Text) %>" AutoGenerateColumns="False" Width="500px" >
						<Columns>               
							<asp:BoundField DataField="addo_ID">
								<HeaderStyle CssClass="no-display"></HeaderStyle>
								<ItemStyle CssClass="no-display"></ItemStyle>
							</asp:BoundField>  
							<asp:TemplateField HeaderText="Addo ID">
								<ItemTemplate>
									<asp:HyperLink runat="server" class="no-print" Text='<%# Eval("Addo_ID") %>' Target="_blank" NavigateUrl='<%# "http://webapp2a.scientology.net/portal/cars/cosCarsPersonReport.jsp?hideLinks=true&addoId="+ Eval("Addo_ID") +"&orgId=89&browser=&userId=allhandsharlem&password=harlembas1"%>' />
								</ItemTemplate>
							</asp:TemplateField>                    
							<asp:BoundField DataField="name" HeaderText="Name" SortExpression="name" />				
							<asp:BoundField DataField="city" HeaderText="City" SortExpression="city" />					
							<asp:TemplateField>
								<ItemTemplate>
									<asp:LinkButton ID="lnkBtnAdd" class="btn btn-default btn-xs" runat="server" Text="Add" OnClick="ViewAddo"></asp:LinkButton>
								</ItemTemplate>						
							</asp:TemplateField>
						</Columns> 
					</asp:GridView>				
				</div>
				<div id="Six" 	runat="server">
					<asp:DropDownList id="ddlPage" runat="server" AutoPostBack="True" CssClass="form-control mb-2 mr-sm-2 mb-sm-0" OnSelectedIndexChanged="Selection_Change_Page" >
						<asp:ListItem Value="10"> 10 </asp:ListItem>
						<asp:ListItem Selected="True" Value="20"> 20 </asp:ListItem>
						<asp:ListItem Value="50"> 50 </asp:ListItem>
						<asp:ListItem Value="100"> 100 </asp:ListItem>
						<asp:ListItem Value="250"> 250 </asp:ListItem>
						<asp:ListItem Value="500"> 500 </asp:ListItem>
					</asp:DropDownList>        
					<asp:Label runat="server" text="Rows Per Page" ID="RowLabel" />
					<br />
					<br />					
				</div>
				<div id="Seven" runat="server" Visible="false">
					<div id="report_input" style="float: left;">
						<asp:Panel ID="Panel1" runat="server" DefaultButton="Report_Btn">					
								<ContentTemplate>
									<asp:TextBox ID="report_text" runat="server"></asp:TextBox>
									<asp:Button ID="Report_Btn" runat="server" Text="Submit" OnClick="BtnReport_Click" />
								</ContentTemplate>					
						</asp:Panel>
					</div>
					<br />
					<div runat="server" style="width: 400px;">
						<div>
							<asp:Label id="lblChart" runat="server">Lines Pie Chart</asp:Label>
							<canvas id="piechart" width="400" height="400">
							</canvas>
							<div id="lbltest" runat="server"></div>
						</div>
					</div>
				</div>
				<div id="Eight" runat="server">
					<div class="panel-group">
						<div class="panel panel-default">
						<div class="panel-heading">
							<div class="btn-group pull-right">
								<button type="button" id="btnAddReg" runat="server" class="btn btn-primary btn-outline navbar-btn pull-right" data-toggle="modal" data-target="#addRegModal">Add Item</button>												
							</div>
							<h4>Maintain Lookups</h4>			
						</div>
							<div id="collapse0" class="panel-collapse collapse in">
								<div class="panel-body">
									<asp:GridView 
										ID="GridViewLookup" 
										runat="server" 
										AutoGenerateColumns="false" 
										DataKeyNames="id" 
										BorderWidth="1px" 
										CellPadding="2" 
										ForeColor="Black" 
										GridLines="None" 
										EmptyDataText="No records has been added.">
										
										<Columns>					
											<asp:BoundField DataField="ID" HeaderText="ID" >
												<HeaderStyle CssClass="no-display"></HeaderStyle>
											<ItemStyle CssClass="no-display"></ItemStyle>
											</asp:BoundField>
											<asp:TemplateField HeaderText="Desc1">
												<HeaderStyle HorizontalAlign="Left" CssClass="mlm2" />
												<ItemTemplate>
													<asp:TextBox ID="Desc1" AutoPostBack="True" CssClass="mlm2" runat="server" Text='<%# Eval("Desc1") %>' Width=150 OnTextChanged="text_change_reg" />
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Desc2">
												<HeaderStyle HorizontalAlign="Left" CssClass="mlm2" />
												<ItemTemplate>
													<asp:TextBox ID="Desc2" AutoPostBack="True" CssClass="mlm2" runat="server" Text='<%# Eval("Desc2") %>' Width=150 OnTextChanged="text_change_reg" />
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Desc3">
												<HeaderStyle HorizontalAlign="Left" CssClass="mlm2" />
												<ItemTemplate>
													<asp:TextBox ID="Desc3" AutoPostBack="True" CssClass="mlm2" runat="server" Text='<%# Eval("Desc3") %>' Width=150 OnTextChanged="text_change_reg" />
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField>
												<ItemTemplate>
													<asp:LinkButton ID="lnkBtnDelete" class="btn btn-danger btn-outline btn-xs" runat="server" Text="Delete" 
													OnClick="DeleteReg"></asp:LinkButton>
												</ItemTemplate>						
											</asp:TemplateField>

										</Columns>
										<FooterStyle BackColor="Tan" />
										<HeaderStyle BackColor="Silver" Font-Bold="True" Font-Names="Arial Black" HorizontalAlign="Left" />
										<PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
										<RowStyle Height="22px" />
										<SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
									</asp:GridView>
								</div>
							</div>
						</div>
					</div>
				</div>
				<div id="Reports" runat="server">
				
					<div class="panel-group">
						<div class="panel panel-default">
						<div class="panel-heading">
						<h4 class="panel-title">
							<a data-toggle="collapse" href="#PaneBISAll">
								<asp:Label runat="server" Text="BIS Breakdown By Area" CssClass="CombGITable" />
							</a>
						</h4>
						</div>
							<div id="PaneBISAll" class="panel-collapse collapse in">
								<div class="panel-body">
									<asp:SqlDataSource runat="server" ID="SqlDataSource1" 
										ConnectionString="<%$ ConnectionStrings:reg %>" 
										SelectCommand="SELECT area, count(1) as total from bis WHERE reg_cat_id = 'Archive' and weekend > getdate() - 7 Group By area">
									</asp:SqlDataSource>
								
									<shield:ShieldChart 
										ID="ShieldChart1a" 
										runat="server" AutoPostBack="true" Width="820px" Height="400px" 
										OnTakeDataSource="ShieldChart1a_TakeDataSource"> 
										<PrimaryHeader Text="Bodies In The Shop By Area"> 
										</PrimaryHeader> 
										<ExportOptions AllowExportToImage="false" AllowPrint="false" /> 
										<TooltipSettings CustomPointText="BIS Amount: <b>{point.y}</b>"> 
										</TooltipSettings> 
										<Axes> 
											<shield:ChartAxisX CategoricalValuesField="area"> 
											</shield:ChartAxisX> 
											<shield:ChartAxisY> 
												<Title Text="BIS Breakdown"></Title> 
											</shield:ChartAxisY> 
										</Axes> 
										<DataSeries> 
											<shield:ChartPieSeries DataFieldY="total"> 
												<Settings EnablePointSelection="true" EnableAnimation="true"> 
													<DataPointText BorderWidth=""> 
													</DataPointText> 
												</Settings> 
											</shield:ChartPieSeries> 
										</DataSeries> 
										<Legend Align="Center" BorderWidth=""></Legend> 
									</shield:ShieldChart> 
								</div>
							</div>
						</div>	
					</div>					
				</div>
				<asp:Label runat="server" id="ErrorText" Text="" />
			</div>
			<div id="archiveModal" class="modal fade" method="POST" role="dialog">
				<div class="modal-dialog">
					<div class="modal-content">
						<div class="modal-header modal-warning">
							<button type="button" class="close" data-dismiss="modal">&times;</button>
							<h4 class="modal-title">Warning!</h4>
						</div>
						<div class="modal-body">
							<p>If you continue, all "In The Shop" will be <strong>moved</strong> to "Archive"!</p>
						</div>
						<div class="modal-footer">
							<button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
							<asp:Button ID="btnArchive" OnClientClick="<% %>" class="btn btn-warning" runat="server" Text="Archive Week" CommandArgument='<%# Eval("Id") %>' OnCommand="Archive_Click" />
							
						</div>
					</div>
				</div>
			</div>
			<div id="alertWE" runat="server" class="alert alert-danger alert-dismissable">
				<button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
				<strong>Warning:</strong> Please enter a "weekending date".
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
								</div>
							</div>
						</div><!-- /.modal-body -->
					</div><!-- /.modal-content -->
				</div><!-- /.modal-dialog -->
			</div><!-- /.modal -->
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
										<div class="form-group">
											<label for="addonameid" class="col-sm-2 control-label">Name</label>
											<div class="col-sm-6">
												<asp:TextBox ID="addonameid"  runat="server" Text="" CssClass="form-control-modal" ></asp:TextBox>
											</div>
											<label for="addo_statusid" class="col-sm-1 control-label">Status</label>
											<div class="col-sm-3">
												<asp:DropDownList id="addo_statusid" runat="server" CssClass="form-control-modal" 
												SelectedValue='<%# Eval("status") %>' TabIndex='<%# TabIndex %>'>
													<asp:ListItem Value="Named"> Named </asp:ListItem>
													<asp:ListItem Value="Scheduled"> Scheduled </asp:ListItem>
													<asp:ListItem Value="In The Shop"> In The Shop </asp:ListItem>
												</asp:DropDownList>
											</div>
										</div>
										<div class="form-group">
											<label for="addo_serviceid" class="col-sm-2 control-label">Service</label>
											<div class="col-sm-6">
												<asp:TextBox ID="addo_serviceid" runat="server" Text="" CssClass="form-control-modal" ></asp:TextBox>
											</div>
											<label class="col-sm-1 control-label" for="ddlReg">Area</label>
											<div class="col-sm-3">
												<asp:DropDownList id="ddlReg" name="ddlReg" runat="server" CssClass="form-control-modal"></asp:DropDownList>												
											</div>
										</div>
										<div class="form-group">
											<label for="apptid3" class="col-sm-2 control-label">Scheduled</label>
											<div class="col-sm-6">
												<asp:TextBox ID="apptid3" runat="server" Text="" CssClass="form-control-modal" ></asp:TextBox>
											</div>
											<script type="text/javascript">
												$(function () {
													$('#apptid3').datetimepicker();
												});
											</script>
											<label for="lineid3" class="col-sm-1 control-label">Line</label>
											<div class="col-sm-3">
												<asp:DropDownList id="lineid3" runat="server" CssClass="form-control-modal"
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
										<div class="form-group">
											<label for="regid2" class="col-sm-2 control-label">Terminal</label>
											<div class="col-sm-6">
												<asp:TextBox ID="regid2" runat="server" Text="" CssClass="form-control-modal" ></asp:TextBox>
											</div>
											<label for="fsmid" class="col-sm-1 control-label">FSM</label>
											<div class="col-sm-3">
												<asp:TextBox ID="fsmid" runat="server" Text="" CssClass="form-control-modal" ></asp:TextBox>
											</div>
										</div>		
										<div class="form-group">
											<label for="addophoneid" class="col-sm-2 control-label">Phone</label>
											<div class="col-sm-6">
												<asp:TextBox ID="addophoneid" runat="server" Text="" CssClass="form-control-modal" ></asp:TextBox>
											</div>
											<label for="addo_orgid" disabled="false" class="col-sm-1 control-label">Org</label>
											<div class="col-sm-3">
												<asp:DropDownList id="addo_orgid" runat="server" CssClass="form-control-modal" 
												SelectedValue='<%# Eval("org") %>' TabIndex='<%# TabIndex %>'>
													<asp:ListItem Value="Day"> Day </asp:ListItem>
													<asp:ListItem Value="Fdn"> Fdn </asp:ListItem>							
												</asp:DropDownList>	
											</div>
										</div>						
										<div class="form-group">
											<label for="email" class="col-sm-2 control-label">Email</label>
											<div class="col-sm-6">
												<asp:TextBox ID="email" runat="server" Text="" CssClass="form-control-modal" ></asp:TextBox>
											</div>
											<label class="col-sm-1 control-label" for="addo_rankid">Rank</label>
											<div class="col-sm-3">
												<select class="form-control-modal" name="addo_rankid" id="addo_rankid">
													<option value=""></option>
													<option value="a">A</option>
													<option value="b">B</option>
													<option value="c">C</option>
												</select>
											</div>											
											
										</div>						
										<div class="form-group">
											<label for="addo_noteid" class="col-sm-2 control-label">Note</label>
											<div class="col-sm-10">
												<asp:TextBox TextMode="MultiLine" Rows="2" ID="addo_noteid" runat="server" Text="" CssClass="form-control-modal" ></asp:TextBox>
											</div>
										</div>
										<div class="form-group">
											<div class="col-sm-5">
												<asp:TextBox ID="id2" type="hidden" runat="server" Text="" CssClass="form-control-modal" ></asp:TextBox>
											</div>
											<div class="col-sm-5">
												<asp:TextBox ID="addo_addoid" type="hidden" runat="server" Text="" CssClass="form-control-modal" ></asp:TextBox>
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
										<div class="form-group">
											<label for="lblnameid" class="col-sm-2 control-label">Name</label>
											<div class="col-sm-6">
												<asp:TextBox ID="lblnameid" runat="server" Text="" CssClass="form-control-modal" ></asp:TextBox>
											</div>
											<label for="statusid" class="col-sm-1 control-label">Status</label>
											<div class="col-sm-3">
												<asp:DropDownList id="statusid" runat="server" CssClass="form-control-modal" 
												SelectedValue='<%# Eval("status") %>' TabIndex='<%# TabIndex %>'>
													<asp:ListItem Value="Named"> Named </asp:ListItem>
													<asp:ListItem Value="Scheduled"> Scheduled </asp:ListItem>
													<asp:ListItem Value="In The Shop"> In The Shop </asp:ListItem>
												</asp:DropDownList>
											</div>
										</div>
										<div class="form-group">
											<label for="serviceid" class="col-sm-2 control-label">Service</label>
											<div class="col-sm-6">
												<asp:TextBox ID="serviceid" runat="server" Text="" CssClass="form-control-modal" ></asp:TextBox>
											</div>
											<label class="col-sm-1 control-label" for="ddlReg">Area</label>
											<div class="col-sm-3">
												<asp:DropDownList id="ddlReg3" name="ddlReg" runat="server" CssClass="form-control-modal"></asp:DropDownList>												
											</div>
										</div>
										<div class="form-group">
											<label for="edit_scheduled" class="col-sm-2 control-label">Scheduled</label>
											<div class="col-sm-6">
												<asp:TextBox ID="edit_scheduled" runat="server" Text="" CssClass="form-control-modal" ></asp:TextBox>
											</div>
											<script type="text/javascript">
												$(function () {
													$('#edit_scheduled').datetimepicker();
												});
											</script>								
											<label for="lineid" disabled="true" class="col-sm-1 control-label">Line</label>
											<div class="col-sm-3">
												<asp:DropDownList id="lineid" disabled="true" runat="server" CssClass="form-control-modal"
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
										<div class="form-group">
											<label for="regid" class="col-sm-2 control-label">Terminal</label>
											<div class="col-sm-6">
												<asp:TextBox ID="regid" runat="server" Text="" CssClass="form-control-modal" ></asp:TextBox>
											</div>
											<label for="bird_dogid" class="col-sm-1 control-label">FSM</label>
											<div class="col-sm-3">
												<asp:TextBox ID="bird_dogid" runat="server" Text="" CssClass="form-control-modal" ></asp:TextBox>
											</div>
										</div>						
										<div class="form-group">
											<label for="phoneid" class="col-sm-2 control-label">Phone</label>
											<div class="col-sm-6">
												<asp:TextBox ID="phoneid" runat="server" Text="" CssClass="form-control-modal" ></asp:TextBox>
											</div>
											<label for="orgid" class="col-sm-1 control-label">Org</label>
											<div class="col-sm-3">
												<asp:DropDownList id="orgid" runat="server" CssClass="form-control-modal" 
												SelectedValue='<%# Eval("org") %>' TabIndex='<%# TabIndex %>'>
													<asp:ListItem Value="Day"> Day </asp:ListItem>
													<asp:ListItem Value="Fdn"> Fdn </asp:ListItem>							
												</asp:DropDownList>	
											</div>
										</div>						
										<div class="form-group">
											<div margin-top:15px>
											</div>
											<label for="edit_email" class="col-sm-2 control-label">Email</label>
											<div class="col-sm-6">
												<asp:TextBox ID="edit_email" runat="server" Text="" CssClass="form-control-modal" ></asp:TextBox>
											</div>
											<label for="rankid" class="col-sm-1 control-label">Rank</label>
											<div class="col-sm-3">
												<asp:DropDownList id="rankid" runat="server" CssClass="form-control-modal"
												SelectedValue='<%# Eval("rank") %>' TabIndex='<%# TabIndex %>'>
													<asp:ListItem Value=""> </asp:ListItem>
													<asp:ListItem Value="a"> A </asp:ListItem>
													<asp:ListItem Value="b"> B </asp:ListItem>
													<asp:ListItem Value="c"> C </asp:ListItem>
												</asp:DropDownList>
											</div>
										</div>						
										<div class="form-group">
											<label for="notesid" class="col-sm-2 control-label">Note</label>
											<div class="col-sm-10">
												<asp:TextBox TextMode="MultiLine" Rows="3" ID="notesid" runat="server" Text="" CssClass="form-control-modal" ></asp:TextBox>
											</div>
										</div>
										<div class="form-group">
											<div class="col-sm-10">
												<asp:TextBox ID="id" type="hidden" runat="server" Text="" CssClass="form-control-modal" ></asp:TextBox>
											</div>
										</div>					
									</fieldset>
									<div class="modal-footer">
										<asp:Button ID="btnUpdate" OnClientClick="<% %>" class="btn btn-default" runat="server" Text="Save" CommandArgument='<%# Eval("Id") %>' OnCommand="btnUpdate_Click" />
									</div>							
								</div>
							</div>
						</div><!-- /.modal-body --> 
					</div><!-- /.modal-content -->
				</div><!-- /.modal-dialog -->
			</div><!-- /.modal -->
			<div id="addModal" class="modal fade" tabindex="-1" method="POST" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
				<script type="text/javascript">
					$(document).ready(function () {
						$('#addModal').on('hidden.bs.modal', function (e) {
							$('#nameid').val('');
							$('#rankid2').val('');
							$('#serviceid2').val('');
							$('#amountid2').val('');
							$('#ddlReg2').val('');
							$('#lineid2').val('');
							$('#apptid2').val('');
							$('#datefor').val('');
							$('#phoneid2').val('');
							$('#statusid2').val('');
							$('#bird_dogid2').val('');
							$('#notesid2').val('');
						});
					});
				</script>
				<div class="modal-dialog modal-lg" role="document">
					<div class="modal-content">
						<div class="modal-header">
							<button type="button" class="close" data-dismiss="modal">&times;</button>
							<h3 class="modal-title" id="addModalTitle">Add Record</h3>
						</div>
						<div class="modal-body">
							<div class="row">
								<div class="col-md-12">
									<fieldset>
										<div class="form-group" >
											<label class="col-sm-2 control-label" for="textinput">Name</label>
											<div class="col-sm-6">
												<asp:TextBox ID="nameid" name="nameid" runat="server" Text="" CssClass="form-control-modal" style="width=100%" ></asp:TextBox>
											</div>
											<label class="col-sm-1 control-label" for="textinput">Amount</label>
											<div class="col-sm-3">
												<asp:TextBox ID="amountid2" name="amountid2" runat="server" Text="" CssClass="form-control-modal" ></asp:TextBox>
											</div>
											
<!-- 										<label class="col-sm-1 control-label" for="statusid2">Status</label>
											<div class="col-sm-3">
												<select class="form-control-modal" name="statusid2" id="statusid2">
													<option value=""></option>
													<option value="Future Prospect"> Future Prospect </option>
													<option value="Now Prospect"> Now Prospect </option>
													<option value="Open Cycle"> Open Cycle </option>
													<option value="GI Confirmed"> GI Confirmed </option>
													<option value="GI Invoiced"> GI Invoiced </option>
												</select>
											</div>
 -->										</div>
										<div class="form-group">
											<label class="col-sm-2 control-label" for="textinput">Service</label>
											<div class="col-sm-6">
												<asp:TextBox ID="serviceid2" name="serviceid2" runat="server" Text="" CssClass="form-control-modal" ></asp:TextBox>
											</div>
											<label class="col-sm-1 control-label" for="textinput">Rank</label>
											<div class="col-sm-3">
												<select class="form-control-modal" name="rankid2" id="rankid2">
													<option value=""></option>
													<option value="a">A</option>
													<option value="b">B</option>
													<option value="c">C</option>
												</select>
											</div>											
										</div>
										<div class="form-group">
											<label class="col-sm-2 control-label" for="textinput">Main Reg</label>
											<div class="col-sm-6">
												<asp:DropDownList id="ddlReg2" name="ddlReg" runat="server" CssClass="form-control-modal"></asp:DropDownList>												
											</div>
											<label class="col-sm-1 control-label" for="textinput">Line</label>
											<div class="col-sm-3">
												<select class="form-control-modal" name= "lineid2" id="lineid2">
													<option value=""></option>
													<option Value="Resign"> Resign </option>
													<option Value="FSM"> FSM </option>
													<option Value="Prospecting"> Prospecting </option>
													<option Value="Arrival"> Arrival </option>
													<option Value="CF"> CF </option>
													<option Value="Other"> Other </option>			
												</select>
											</div>										
										</div>
									<!-- 	<div class="form-group">
											<label class="col-sm-2 control-label" for="apptid2" >Scheduled</label>
											<div class="col-sm-6">
												<asp:TextBox ID="apptid2" runat="server" Text="" CssClass="form-control-modal" ></asp:TextBox>
											</div>
											<script type="text/javascript">
												$(function () {
													$('#apptid2').datetimepicker();
												});
											</script>
											<label class="col-sm-1 control-label" for="textinput">For</label>
											<div class="col-sm-3">
												<select class="form-control-modal" name="datefor" id="datefor">
													<option value=""></option>
													<option value="Contact"> Contact </option>
													<option value="Reg Int"> Reg Int </option>
													<option value="Payment"> Payment </option>
													<option value="Service Start"> Service Start </option>
													<option value="Resign"> Resign </option>
												</select>
											</div>
										</div> -->
										<!-- <div class="form-group">
											<label class="col-sm-2 control-label" for="phoneid2" >Phone</label>
											<div class="col-sm-6">
												<asp:TextBox ID="phoneid2" name="phoneid2" runat="server" Text="" CssClass="form-control-modal" ></asp:TextBox>
											</div>
											
											<label for="blank1" class="col-sm-1 control-label"></label>						
											<div class="col-sm-3">
												<input type="text" id="blank1" disabled="true" class="form-control-modal">
											</div>
										</div> -->
										<div class="form-group">
											<label for="bird_dogid2" class="col-sm-2 control-label">Bird Dog</label>
											<div class="col-sm-6">
												<asp:TextBox ID="bird_dogid2" name="bird_dogid2" runat="server" Text="" CssClass="form-control-modal" ></asp:TextBox>
											</div>
											<label for="orgid2" class="col-sm-1 control-label"></label>						
											<div class="col-sm-3">
												<input type="text" id="orgid2" disabled="true" class="form-control-modal">
											</div>
										</div>						
										<div class="form-group">
										<label for="notesid2" class="col-sm-2 control-label">Note</label>
										<div class="col-sm-10">
											<textarea rows="3" class="form-control-modal" name="notesid2" id="notesid2"></textarea>
										</div>
									</div>
									</fieldset>
								<div class="modal-footer">
									<asp:Button ID="btnSubmitIt" OnClientClick="<% %>" class="btn btn-primary" runat="server" Text="Save" CommandArgument='<%# Eval("Id") %>' OnCommand="btnSubmit_Click" />
									<!-- <button type="submit" class="btn btn-primary" data-dismiss="modal" aria-hidden="true">Close</button>-->
								</div>
								</div>	
							</div>	
						</div>	<!-- /.modal-body -->
					</div><!-- /.modal-content -->
				</div><!-- /.modal-dialog -->
			</div><!-- /.modal -->
			<div id="delConfReg" class="modal fade">
				<script type="text/javascript">
					function ConfirmDeleteReg() {
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
									<fieldset>
										<p> Are you sure you want to Delete this Reg?</p>
										<asp:TextBox ID="RegistrarID" type="hidden" runat="server" Text="" ></asp:TextBox>
									</fieldset>
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
							<h3 class="modal-title" id="addModalTitle">Add Item</h3>
						</div>
						<div class="modal-body">
							<div class="row">
								<div class="col-md-12">
									<fieldset>
										<div class="form-group" >
											<label class="col-sm-1 control-label" for="desc1">Desc1</label>
											<div class="col-sm-3">
												<asp:TextBox ID="desc1" name="desc1" runat="server" Text="" CssClass="form-control-modal" style="width=100%" ></asp:TextBox>
											</div>
											<label class="col-sm-1 control-label" for="desc2">Desc2</label>
											<div class="col-sm-3">
												<asp:TextBox ID="desc2" name="desc2" runat="server" Text="" CssClass="form-control-modal" ></asp:TextBox>
											</div>
											<label class="col-sm-1 control-label" for="desc3">Desc3</label>
											<div class="col-sm-3">
												<asp:TextBox ID="desc3" name="desc3" runat="server" Text="" CssClass="form-control-modal" ></asp:TextBox>
											</div>
										</div>
									</fieldset>
								</div>
							</div>
								<div class="modal-footer">
									<asp:Button ID="btnSubmitReg" OnClientClick="<% %>" class="btn btn-primary" runat="server" Text="Save" CommandArgument='<%# Eval("Id") %>' OnCommand="btnAddReg_Click" />
									<!-- <button type="submit" class="btn btn-primary" data-dismiss="modal" aria-hidden="true">Close</button>-->
								</div>
						</div>	
					</div><!-- /.modal-content -->
				</div><!-- /.modal-dialog -->
			</div><!-- /.modal -->			

		</form>
	</body>
</br>
JSR v.1.0 2018 
<!-- <p><a href="/dev/index.aspx" target="_blank">Beta Version</a></p> -->
</html>