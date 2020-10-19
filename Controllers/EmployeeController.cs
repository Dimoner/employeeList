using System.Collections.Generic;
using TestDimaBack.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using TestDimaBack.ParamModels;
using TestDimaBack.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace TestDimaBack.Controllers
{
  public class EmployeeController : BaseController
  {
    public EmployeeController(ApplicationContext context) : base(context) { }

    [Route("/")]
    [HttpGet]
    public IActionResult Index(ParamEmployees? Sort)
    {
      IQueryable<Employee> Employees = db.Employees
      .Include(dep => dep.Department)
      .Include(d => d.EmployeeLanguages)
      .ThenInclude(d => d.CodeLanguage);

      IEnumerable<CodeLanguage> CodeLanguages = db.CodeLanguages;

      IEnumerable<Department> Departments = db.Departments;

      if (Sort != null && Convert.ToInt32(Sort.DepartmentSort) > 0)
        Employees = Employees.Where(sortPar => sortPar.DepartmentId == Convert.ToInt32(Sort.DepartmentSort));

      if (Sort != null && Convert.ToInt32(Sort.CodeLanguageSort) > 0)
        Employees = Employees.Where(sortParam => sortParam.EmployeeLanguages.Where(lang => lang.CodeLanguageId == Convert.ToInt32(Sort.CodeLanguageSort)).Count() != 0);

      SortEmployee sortOrder;

      if (Sort == null || Sort == null && Sort.SortEmployee == null)
      {
        sortOrder = SortEmployee.FirstNameAsc;
      }
      else
      {
        sortOrder = Sort.SortEmployee;
      }

      Employees = sortOrder switch
      {
        SortEmployee.FirstNameDesc => Employees.OrderByDescending(s => s.FirstName),
        SortEmployee.SecondNameAsc => Employees.OrderBy(s => s.SecondName),
        SortEmployee.SecondNameDesc => Employees.OrderByDescending(s => s.SecondName),
        SortEmployee.AgeAsc => Employees.OrderBy(s => s.Age),
        SortEmployee.AgeDesc => Employees.OrderByDescending(s => s.Age),
        SortEmployee.DepartmentAsc => Employees.OrderBy(s => s.Department.DepartmentName),
        SortEmployee.DepartmentDesc => Employees.OrderByDescending(s => s.Department.DepartmentName),
        _ => Employees.OrderBy(s => s.FirstName),
      };

      ViewBag.CurrentSortName = sortOrder;

      ViewBag.FirstNameSort = sortOrder == SortEmployee.FirstNameAsc ? SortEmployee.FirstNameDesc : SortEmployee.FirstNameAsc;
      ViewBag.SecondNameSort = sortOrder == SortEmployee.SecondNameAsc ? SortEmployee.SecondNameDesc : SortEmployee.SecondNameAsc;
      ViewBag.AgeSort = sortOrder == SortEmployee.AgeAsc ? SortEmployee.AgeDesc : SortEmployee.AgeAsc;
      ViewBag.DepartmentSort = sortOrder == SortEmployee.DepartmentAsc ? SortEmployee.DepartmentDesc : SortEmployee.DepartmentAsc;

      ViewEmployees ViewEmployees = new ViewEmployees
      {
        Employees = Employees.AsNoTracking(),
        CodeLanguages = CodeLanguages,
        Departments = Departments,
        CodeLanguageSort = Sort != null ? Convert.ToInt32(Sort.CodeLanguageSort) : 0,
        DepartmentSort = Sort != null ? Convert.ToInt32(Sort.DepartmentSort) : 0
      };

      return View(ViewEmployees);
    }

    [Route("/create")]
    [HttpPost]
    public IActionResult CreateEmployee(ParamNewEmployee newEmployee)
    {
      if (ModelState.IsValid)
      {
        Department dep = db.Departments.FirstOrDefault(dep => dep.Id == newEmployee.DepartmentId);

        Employee employee = new Employee
        {
          FirstName = newEmployee.Name,
          SecondName = newEmployee.SecondName,
          Department = dep,
          Age = newEmployee.Age
        };

        db.Add(employee);
        db.SaveChanges();

        foreach (var langId in newEmployee.LanguageIds)
        {
          employee.EmployeeLanguages.Add(new EmployeeLanguage { CodeLanguageId = langId, EmployeeId = employee.Id });
        }

        db.SaveChanges();
      }

      return LocalRedirect("/");
    }

    [Route("/delete/{id}")]
    [HttpDelete]
    public async Task<IActionResult> DeleteEmployee(int? id)
    {
      if (id != null && id != 0)
      {
        Employee employee = await db.Employees.FirstOrDefaultAsync(employee => employee.Id == id);

        db.Employees.Remove(employee);

        db.SaveChanges();
        return Ok(Json("Сотрудник успешно удален"));
      }

      return BadRequest(Json("Ошибка в работе метода"));
    }

    [Route("/edit")]
    [HttpPost]
    public async Task<IActionResult> EditEmployee(ParamEditEmployee paramEditEmployee)
    {
      if (ModelState.IsValid)
      {
        Employee employee = db.Employees
        .Include(employee => employee.EmployeeLanguages)
        .FirstOrDefault(emp => emp.Id == paramEditEmployee.Id);

        Department department = db.Departments.FirstOrDefault(dep => dep.Id == paramEditEmployee.DepartmentId);

        employee.FirstName = paramEditEmployee.Name;
        employee.SecondName = paramEditEmployee.SecondName;
        employee.Age = paramEditEmployee.Age;
        employee.DepartmentId = paramEditEmployee.DepartmentId;
        employee.Department = department;
        await db.SaveChangesAsync();

        string oldIds = "";

        foreach (var langId in employee.EmployeeLanguages)
        {
          oldIds += $"{langId.CodeLanguageId},";
        }

        string[] oldList = oldIds.TrimEnd(',').Split(new char[] { ',' });

        foreach (var langId in paramEditEmployee.LanguageIds)
        {
          if (!string.IsNullOrEmpty(Convert.ToString(langId)))
          {
            if (Array.IndexOf<string>(oldList, Convert.ToString(langId)) < 0)
            {
              employee.EmployeeLanguages.Add(new EmployeeLanguage { CodeLanguageId = langId, EmployeeId = employee.Id });
            }
          }
        }

        await db.SaveChangesAsync();

        string resultIds = "";

        foreach (var langId in employee.EmployeeLanguages)
        {
          if (paramEditEmployee.LanguageIds.IndexOf(langId.CodeLanguageId) < 0)
          {
            resultIds += $"{langId.CodeLanguageId},";
          }
        }

        string[] deleteIds = resultIds.Split(new char[] { ',' });

        foreach (var langId in deleteIds)
        {
          if (!string.IsNullOrEmpty(langId))
          {
            employee.EmployeeLanguages.Remove(employee.EmployeeLanguages.FirstOrDefault(p => p.CodeLanguageId == Convert.ToInt32(langId)));
          }
        }

        await db.SaveChangesAsync();
      }

      return LocalRedirect("/");
    }

    [Route("/error")]
    public IActionResult Error()
    {
      ViewBag.ErrorCode = Request.Query["code"];

      return View();
    }
  }
}