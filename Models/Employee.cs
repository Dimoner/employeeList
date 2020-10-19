using System.Collections.Generic;

namespace TestDimaBack.Models
{
  public class Employee
  {
    public int Id { get; set; }

    public int DepartmentId { get; set; }
    public Department Department { get; set; }

    public List<EmployeeLanguage> EmployeeLanguages { get; set; }

    public Employee()
    {
      EmployeeLanguages = new List<EmployeeLanguage>();
    }

    public string FirstName { get; set; }

    public string SecondName { get; set; }
    public int Age { get; set; }
  }
}