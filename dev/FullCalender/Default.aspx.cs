using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Collections;
namespace WebApplication1
{
    public partial class _Default : System.Web.UI.Page
    {
        [WebMethod]
        public static IList GetEvents()
        {
            IList events = new List<Event>();
            for (int i = 0; i < DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month); i++)
            {
                events.Add(new Event
                {
                    EventName = "My Event " + i.ToString(),
                    StartDate = DateTime.Now.AddDays(i).ToString("MM-dd-yyyy"),
                    EndDate = DateTime.Now.AddDays(1).ToString("MM-dd-yyyy"),
                    ImageType = i % 2,
                    Url = @"http://www.google.com"
                });
            }
            return events;
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
    public class Event
    {
        public Guid EventID { get { return new Guid(); } }
        public string EventName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int ImageType { get; set; }
        public string Url { get; set; }
    }
}
