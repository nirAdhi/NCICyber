using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace EventManagement.Models
{
	public class AdmindModel
	{ 

	}
	public class DashboardModel
	{
		public int hackathonCount { get; set; }
		public int bugCount { get; set; }
		public int cyberCount { get; set; }
		public int userCount { get; set; }
	}
	public class EventRegisterDetails
	{
		public string event1 { get; set; }
        public string event2 { get; set; }
        public string event3 { get; set; }
        public DateTime date { get; set; }
		public double amount { get; set; }
    }
}