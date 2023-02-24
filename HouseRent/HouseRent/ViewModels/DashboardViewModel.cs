using HouseRent.Models;

namespace HouseRent.ViewModels
{
    public class DashboardViewModel
    {
        public List<Order> Orders { get; set; }
        public toDoList ToDoList { get; set; }
        public List<toDoList> ToDoLists { get; set; }
        public List<AdminMessage> AdminMessages { get; set; }
    }
}
