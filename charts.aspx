<!DOCTYPE html>
<%@ Page Language="C#" Trace="False" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="charts.aspx.cs" Inherits="_Default" Debug="true" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"  
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
	
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
	<meta charset="utf-8">
	<meta name="viewport" content="width=device-width, initial-scale=1">
	<meta http-equiv="x-ua-compatible" content="ie=edge">

	<title>Testing Calendar</title>

		<link rel="stylesheet" href="css/fullcalendar.min.css">
		<link rel="stylesheet" href="css/fullcalendar.print.css" media="print">
		<link rel="stylesheet" href="css/font-awesome.min.css">
		<link rel="stylesheet" href="css/adminlte.css">
		<link rel="stylesheet" href="css/jquery-ui.css">
		
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
        <a href="index.aspx" class="nav-link">Home</a>
      </li>
      <!-- <li class="nav-item d-none d-sm-inline-block"> -->
        <!-- <a href="#" class="nav-link">Contact</a> -->
      <!-- </li> -->
    </ul>

	
    <!-- Right navbar links -->
	<ul class="navbar-nav ml-auto">
	  <li class="nav-item">
		<a runat="server" id="pr" class="nav-icon fa fa-circle text-info" onserverclick="clickTest" title="Test">
		</a>							
	  </li>	  
	</ul>

	
  </nav>
  <!-- /.navbar -->

  <!-- Main Sidebar Container -->
  <aside class="main-sidebar sidebar-light-primary elevation-4">
    <!-- Brand Logo -->
    <a href="index.aspx" class="brand-link">
      <img src="img/scn_sm.png" alt="Scn Logo" class="brand-image img-circle elevation-3"
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

			  <li class="nav-item" >
				<a href="index.aspx" class="nav-link" >
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
					<a href="pages/invoiced.aspx" class="nav-link">
						<i class="fa fa-money nav-icon text-info"></i>
						<p>GI Invoiced</p>
					</a>
				</li>
				<li class="nav-item">
					<a href="pages/confirmed.aspx" class="nav-link">
						<i class="fa fa-money nav-icon nav-icon text-secondary"></i>
						<p>GI Confirmed</p>
					</a>
				</li>
				<li class="nav-item">
					<a href="pages/opencycle.aspx" class="nav-link">
						<i class="fa fa-user-circle nav-icon text-info"></i>
						<p>Open Cycle</p>
					</a>
				</li>			
				<li class="nav-item">
					<a href="pages/now_prospects.aspx" class="nav-link">
						<i class="fa fa-user-circle nav-icon text-info"></i>
						<p>Now Prospects</p>
					</a>
				</li>			
				<li class="nav-item">
					<a href="pages/future_prospects.aspx" class="nav-link">
						<i class="fa fa-user-circle nav-icon text-info"></i>
						<p>Future Prospects</p>
					</a>
				</li>			
				</ul>
			  </li>
				<li class="nav-item">
					<a href="pages/archive.aspx" class="nav-link">
						<i class="fa fa-id-card nav-icon text-secondary"></i>
						<p>Archive</p>
					</a>
				</li>	  
				<li class="nav-item">
					<a href="reports.aspx" class="nav-link">
						<i class="fa fa-pie-chart nav-icon text-info"></i>
						<p>Charts</p>
					</a>
				</li>	  

			  <li class="nav-header">PROSPECTING:</li>
			  
			  <li class="nav-item" >
				<a href="pages/pastInvoices.aspx" class="nav-link" >
				  <i class="nav-icon fa fa-money text-info"></i>
				  <p>
					Past Invoices 
					<span class="right badge badge-info">new</span>
				  </p>
				</a>
			  </li>
			  <li class="nav-item" >
				<a href="pages/fppp.aspx" class="nav-link" >
				  <i class="nav-icon fa fa-user-circle-o text-info"></i>
				  <p>
					Fully Partially Paid
					<span class="right badge badge-info">new</span>
				  </p>
				</a>
			  </li>
			  <li class="nav-header">COMING SOON...</li>
			  <li class="nav-item">
				<a href="../bis.aspx" class="nav-link">
				  <i class="fa fa-address-card-o nav-icon"></i>
				  <p>BIS Tracking System</p>
				</a>
			  </li>
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
    <!-- Sidebar -->
  </aside>
  
  <!-- Content Wrapper. Contains page content -->
  <div class="content-wrapper">
    <!-- Content Header (Page header) -->
    <div class="content-header">
      <div class="container-fluid">
        <div class="row mb-2">
          <div class="col-sm-6">
            <h1 class="m-0 text-dark">Testing Calendar</h1>
          </div><!-- /.col -->
          <div class="col-sm-6">
            <ol class="breadcrumb float-sm-right">
              <li class="breadcrumb-item"><a href="./index.aspx">Home</a></li>
              <li class="breadcrumb-item active">Reports</li>
            </ol>
          </div><!-- /.col -->
        </div><!-- /.row -->
      </div><!-- /.container-fluid -->
    </div>
    <!-- /.content-header -->
	
	<form id="form1" runat="server">
	<asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
    <!-- Main content -->
    <section class="content">
      <div class="container-fluid">
		<div class="row" >
			<!-- SEARCH WEEKEND FEATURE -->				
			<div class="col-12">
				<div class="card">		
					<div class="card-body">
						<div class="row">
						  <div class="col-md-3">
							<div class="card">
							  <div class="card-header">
								<h4 class="card-title">Draggable Events</h4>
							  </div>
							  <div class="card-body">
								<!-- the events -->
								<div id="external-events">
								  <div class="external-event bg-success">Lunch</div>
								  <div class="external-event bg-warning">Go home</div>
								  <div class="external-event bg-info">Do homework</div>
								  <div class="external-event bg-primary">Work on UI design</div>
								  <div class="external-event bg-danger">Sleep tight</div>
								  <div class="checkbox">
									<label for="drop-remove">
									  <input type="checkbox" id="drop-remove">
									  remove after drop
									</label>
								  </div>
								</div>
							  </div>
							  <!-- /.card-body -->
							</div>
							<!-- /. box -->
							<div class="card">
							  <div class="card-header">
								<h3 class="card-title">Create Event</h3>
							  </div>
							  <div class="card-body">
								<div class="btn-group" style="width: 100%; margin-bottom: 10px;">
								  <!--<button type="button" id="color-chooser-btn" class="btn btn-info btn-block dropdown-toggle" data-toggle="dropdown">Color <span class="caret"></span></button>-->
								  <ul class="fc-color-picker" id="color-chooser">
									<li><a class="text-primary" href="#"><i class="fa fa-square"></i></a></li>
									<li><a class="text-warning" href="#"><i class="fa fa-square"></i></a></li>
									<li><a class="text-success" href="#"><i class="fa fa-square"></i></a></li>
									<li><a class="text-danger" href="#"><i class="fa fa-square"></i></a></li>
									<li><a class="text-muted" href="#"><i class="fa fa-square"></i></a></li>
								  </ul>
								</div>
								<!-- /btn-group -->
								<div class="input-group">
								  <input id="new-event" type="text" class="form-control" placeholder="Event Title">

								  <div class="input-group-append">
									<button id="add-new-event" type="button" class="btn btn-primary btn-flat">Add</button>
								  </div>
								  <!-- /btn-group -->
								</div>
								<!-- /input-group -->
							  </div>
							</div>
						  </div>						
						  <div class="col-md-6">
							<div class="card card-primary">
							  <div class="card-body p-0">
								<!-- THE CALENDAR -->
								<div id="calendar"></div>
							  </div>
							  <!-- /.card-body -->
							</div>
							<!-- /. box -->
						  </div>
						</div>
					</div>
				</div>
			</div>
		</div>			

		<div class="row" >
			<!-- SEARCH WEEKEND FEATURE -->				
			<div class="col-12">
				<div class="card">		
					<div class="card-body">
						<div class="row">
							<div class="col-12">	
								<asp:Label runat="server" id="ErrorText" Text="" />									
							</div>				
						</div>
					</div>
				</div>
			</div>
		</div>			


            <!-- /.card -->
            
	</div>		  		  
        <!-- /.row -->
    </section>
    <!-- /.content -->	
	
  <!-- Control Sidebar -->
  <aside class="control-sidebar control-sidebar-dark">
    <!-- Control sidebar content goes here -->
  </aside>
  <!-- /.control-sidebar -->

<!-- REQUIRED SCRIPTS -->

	</form>
  </div>
<!-- ./wrapper -->
	</div>


	
<script src="js/jquery.min.js"></script>
<script src="js/bootstrap.bundle.min.js"></script>
<script src="js/jquery-ui.min.js" type="text/javascript">//</script>
<script src="js/jquery.slimscroll.min.js"></script>
<script src="js/fastclick.js"></script>
<script src="js/adminlte.js"></script>
<script src="js/demo.js"></script>
<script type="text/javascript" src="/js/moment.js"></script>
<script src="js/fullcalendar.min.js"></script>
<!-- Page specific script -->
<script>
var events=new Array();
var numberofevents = this.serviceVariableGetDates.getTotal();
    for (i=0;i<numberofevents;i++)
    {
    //alert("numbrr:" + i);
    var dates=this.serviceVariableGetDates.getItem(i);
    console.log(dates.getData());
    var start_date = dates.getValue("c0");
    var end_date = dates.getValue("c1");
    var event_name = dates.getValue("c2");
    //var EventEntry = [ 'title: '+ event_name, 'start: '+ start_date,'end: '+ end_date ];
    event = new Object();       
    event.title = event_name; // this should be string
    event.start = start_date; // this should be date object
    event.end = end_date; // this should be date object
    event.color = "blue";
    event.allDay = false;
    this.label1.setCaption(start_date);
    //EventArray.push(EventEntry);
    console.log(events['title']);
    events.push(event);
    }
$('#calendar').fullCalendar('addEventSource',events);
</script>	

<script>
  $(function () {

    /* initialize the external events
     -----------------------------------------------------------------*/
    function ini_events(ele) {
      ele.each(function () {

        // create an Event Object (http://arshaw.com/fullcalendar/docs/event_data/Event_Object/)
        // it doesn't need to have a start or end
        var eventObject = {
          title: $.trim($(this).text()) // use the element's text as the event title
        }

        // store the Event Object in the DOM element so we can get to it later
        $(this).data('eventObject', eventObject)

        // make the event draggable using jQuery UI
        $(this).draggable({
          zIndex        : 1070,
          revert        : true, // will cause the event to go back to its
          revertDuration: 0    //  original position after the drag
        })

      })
    }

    ini_events($('#external-events div.external-event'))

    /* initialize the calendar
     -----------------------------------------------------------------*/
    //Date for the calendar events (dummy data)
    var date = new Date()
    var d    = date.getDate(),
        m    = date.getMonth(),
        y    = date.getFullYear()
    $('#calendar').fullCalendar({
      header    : {
        left  : 'prev,next today',
        center: 'title',
        right : 'month,agendaWeek,agendaDay'
      },
      buttonText: {
        today: 'today',
        month: 'month',
        week : 'week',
        day  : 'day'
      },
      //Random default events
      events    : [
        {
          title          : 'All Day Event',
          start          : new Date(y, m, 1),
          backgroundColor: '#f56954', //red
          borderColor    : '#f56954' //red
        },
        {
          title          : 'Long Event',
          start          : new Date(y, m, d - 5),
          end            : new Date(y, m, d - 2),
          backgroundColor: '#f39c12', //yellow
          borderColor    : '#f39c12' //yellow
        },
        {
          title          : 'Meeting',
          start          : new Date(y, m, d, 10, 30),
          allDay         : false,
          backgroundColor: '#0073b7', //Blue
          borderColor    : '#0073b7' //Blue
        },
        {
          title          : 'Reg Interview',
          start          : new Date(y, m, d, 12, 0),
          end            : new Date(y, m, d, 14, 0),
          allDay         : false,
          backgroundColor: '#00c0ef', //Info (aqua)
          borderColor    : '#00c0ef' //Info (aqua)
        },
        {
          title          : 'Birthday Party',
          start          : new Date(y, m, d + 1, 19, 0),
          end            : new Date(y, m, d + 1, 22, 30),
          allDay         : false,
          backgroundColor: '#00a65a', //Success (green)
          borderColor    : '#00a65a' //Success (green)
        },
        {
          title          : 'Click for Google',
          start          : new Date(y, m, 28),
          end            : new Date(y, m, 29),
          url            : 'http://google.com/',
          backgroundColor: '#3c8dbc', //Primary (light-blue)
          borderColor    : '#3c8dbc' //Primary (light-blue)
        }
      ],
      editable  : true,
      droppable : true, // this allows things to be dropped onto the calendar !!!
      drop      : function (date, allDay) { // this function is called when something is dropped

        // retrieve the dropped element's stored Event Object
        var originalEventObject = $(this).data('eventObject')

        // we need to copy it, so that multiple events don't have a reference to the same object
        var copiedEventObject = $.extend({}, originalEventObject)

        // assign it the date that was reported
        copiedEventObject.start           = date
        copiedEventObject.allDay          = allDay
        copiedEventObject.backgroundColor = $(this).css('background-color')
        copiedEventObject.borderColor     = $(this).css('border-color')

        // render the event on the calendar
        // the last `true` argument determines if the event "sticks" (http://arshaw.com/fullcalendar/docs/event_rendering/renderEvent/)
        $('#calendar').fullCalendar('renderEvent', copiedEventObject, true)

        // is the "remove after drop" checkbox checked?
        if ($('#drop-remove').is(':checked')) {
          // if so, remove the element from the "Draggable Events" list
          $(this).remove()
        }

      }
    })

    /* ADDING EVENTS */
    var currColor = '#3c8dbc' //Red by default
    //Color chooser button
    var colorChooser = $('#color-chooser-btn')
    $('#color-chooser > li > a').click(function (e) {
      e.preventDefault()
      //Save color
      currColor = $(this).css('color')
      //Add color effect to button
      $('#add-new-event').css({
        'background-color': currColor,
        'border-color'    : currColor
      })
    })
    $('#add-new-event').click(function (e) {
      e.preventDefault()
      //Get value and make sure it is not null
      var val = $('#new-event').val()
      if (val.length == 0) {
        return
      }

      //Create events
      var event = $('<div />')
      event.css({
        'background-color': currColor,
        'border-color'    : currColor,
        'color'           : '#fff'
      }).addClass('external-event')
      event.html(val)
      $('#external-events').prepend(event)

      //Add draggable funtionality
      ini_events(event)

      //Remove event from text input
      $('#new-event').val('')
    })
  })
</script>	
</body>
</html>