using TestDimaBack.Models;
using System.Linq;
using System.Collections.Generic;

namespace TestDimaBack
{
  public static class SampleData
  {
    public static void Initialize(ApplicationContext context)
    {
      if (!context.Employees.Any() && !context.Departments.Any() && !context.CodeLanguages.Any())
      {
        Department d1 = new Department { DepartmentName = "Front", Floor = 1 };

        Department d2 = new Department { DepartmentName = "Back", Floor = 2 };

        Department d3 = new Department { DepartmentName = "Test", Floor = 3 };

        context.Departments.AddRange(new List<Department> { d1, d2, d3 });
        context.SaveChanges();

        CodeLanguage php = new CodeLanguage { Language = "PHP" };
        CodeLanguage cs = new CodeLanguage { Language = "CS" };
        CodeLanguage ts = new CodeLanguage { Language = "TS" };

        context.CodeLanguages.AddRange(new List<CodeLanguage> { php, cs, ts });
        context.SaveChanges();

        Employee n1 = new Employee { FirstName = "Dima", SecondName = "Egorov", Department = d1, Age = 21 };
        Employee n2 = new Employee { FirstName = "Vova", SecondName = "Vova", Department = d1, Age = 45 };
        Employee n3 = new Employee { FirstName = "Sasha", SecondName = "Sasha", Department = d2, Age = 21 };
        Employee n4 = new Employee { FirstName = "Dasha", SecondName = "Dasha", Department = d3, Age = 32 };

        context.Employees.AddRange(new List<Employee> { n1, n2, n3, n4 });
        context.SaveChanges();

        n1.EmployeeLanguages.Add(new EmployeeLanguage { CodeLanguageId = ts.Id, EmployeeId = n1.Id });
        n1.EmployeeLanguages.Add(new EmployeeLanguage { CodeLanguageId = cs.Id, EmployeeId = n1.Id });
        n2.EmployeeLanguages.Add(new EmployeeLanguage { CodeLanguageId = ts.Id, EmployeeId = n2.Id });
        n3.EmployeeLanguages.Add(new EmployeeLanguage { CodeLanguageId = cs.Id, EmployeeId = n3.Id });
        n4.EmployeeLanguages.Add(new EmployeeLanguage { CodeLanguageId = php.Id, EmployeeId = n4.Id });
        context.SaveChanges();
      }
    }
  }
}