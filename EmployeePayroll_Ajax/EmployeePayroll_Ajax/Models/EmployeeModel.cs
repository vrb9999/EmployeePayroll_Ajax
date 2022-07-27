using System.ComponentModel.DataAnnotations;

namespace EmployeePayroll_Ajax.Models
{
    public class EmployeeModel
    {
        [Key]
        public int Emp_Id { get; set; }
        [RegularExpression("[A-Z]{1}[a-z]{3,}", ErrorMessage = "Please Enter for Employee Name Atleast 3 character with first letter capital")]
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Department { get; set; }
        public string Notes { get; set; }
    }
}
