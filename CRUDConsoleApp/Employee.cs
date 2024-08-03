using System.ComponentModel.DataAnnotations;

namespace CRUDConsoleApp
{
    public class Employee
    {
        public string EmployeeID { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}

