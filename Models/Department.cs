using System.Collections.Generic;

namespace TestDimaBack.Models
{
  public class Department
  {
    public int Id { get; set; }

    public string DepartmentName { get; set; }

    public int Floor { get; set; }

    public List<Employee> Employees { get; set; }
  }
}