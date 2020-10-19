using System.Collections.Generic;
using TestDimaBack.Models;

namespace TestDimaBack.ViewModels
{
  public class ViewEmployees
  {
    public IEnumerable<Employee> Employees { get; set; }

    public IEnumerable<CodeLanguage> CodeLanguages { get; set; }

    public IEnumerable<Department> Departments { get; set; }

    public int DepartmentSort { get; set; }

    public int CodeLanguageSort { get; set; }
  }
}