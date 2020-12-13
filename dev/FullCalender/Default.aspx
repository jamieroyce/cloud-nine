<%@ Page Title="Home Page" Language="C#" MasterPageFile="Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="WebApplication1._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <script type="text/javascript">
        $(document).ready(function () {
            $.ajax({
                type: "POST",
                contentType: "application/json",
                data: "{}",
                url: "Default.aspx/GetEvents",
                dataType: "json",
                success: function (data) {
                    $('div[id*=fullcal]').fullCalendar({

                       
                        header: {
                            left: 'prev,next today',
                            center: 'title',
                            right: 'month,agendaWeek,agendaDay'
                        },
                        editable: true,
                        events: $.map(data.d, function (item, i) {
                            var event = new Object();
                            event.id = item.EventID;
                            event.start = new Date(item.StartDate);
                            event.end = new Date(item.EndDate);
                            event.title = item.EventName;
                            event.url = item.Url;
                            event.ImageType = item.ImageType;
                            return event;
                        }), eventRender: function (event, eventElement) {
                            if (event.ImageType) {
                                if (eventElement.find('span.fc-event-time').length) {
                                    eventElement.find('span.fc-event-time').before($(GetImage(event.ImageType)));
                                } else {
                                    eventElement.find('span.fc-event-title').before($(GetImage(event.ImageType)));
                                }
                            }
                        },
                        loading: function (bool) {
                            if (bool) $('#loading').show();
                            else $('#loading').hide();
                        }
                    });
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    debugger;
                }
            });
            $('#loading').hide();
            $('div[id*=fullcal]').show();
        });
        function GetImage(type) {
            if (type == 0) {
                return "<br/><img src = 'Styles/Images/attendance.png' style='width:24px;height:24px'/><br/>"
            }
            else if (type == 1) {
                return "<br/><img src = 'Styles/Images/not_available.png' style='width:24px;height:24px'/><br/>"
            }
            else
                return "<br/><img src = 'Styles/Images/not_available.png' style='width:24px;height:24px'/><br/>"
        }

    </script>
    <div id="loading">
        <img src="./Styles/images/loading_wh.gif" />
    </div>
    <div id="fullcal">
    </div>
</asp:Content>
