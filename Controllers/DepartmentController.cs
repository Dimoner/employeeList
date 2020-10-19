using System.Collections.Generic;
using TestDimaBack.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestDimaBack.ParamModels;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace TestDimaBack.Controllers
{
  [Route("departments")]
  public class DepartmentController : BaseController
  {
    public DepartmentController(ApplicationContext context) : base(context) { }

    [Route("")]
    public IActionResult Departments()
    {
      IEnumerable<Department> departments = db.Departments.Include(dep => dep.Employees);
      return View(departments);
    }

    [Route("create")]
    [HttpPost]
    public async Task<IActionResult> CreateDepartment(ParamNewDepartment newDepartment)
    {
      if (ModelState.IsValid)
      {
        db.Departments.Add(new Department { DepartmentName = newDepartment.DepartmentName, Floor = newDepartment.Floor });
        await db.SaveChangesAsync();
      }

      return LocalRedirect("/departments");
    }

    [Route("edit")]
    [HttpPost]
    public async Task<IActionResult> CreateDepartment(ParamEditDepartment paramEditDepartment)
    {
      if (ModelState.IsValid)
      {
        Department department = db.Departments.FirstOrDefault(dep => dep.Id == paramEditDepartment.Id);

        department.DepartmentName = paramEditDepartment.DepartmentName;
        department.Floor = paramEditDepartment.Floor;

        await db.SaveChangesAsync();
      }

      return LocalRedirect("/departments");
    }

    [Route("delete/{id}")]
    [HttpDelete]
    public async Task<IActionResult> DeleteDepartment(int? id)
    {
      if (id != null)
      {
        Department department = db.Departments.Include(dep => dep.Employees).FirstOrDefault(dep => dep.Id == id);

        foreach (Employee employee in department.Employees)
        {
            db.Employees.Remove(employee);
        }

        db.Departments.Remove(department);

        await db.SaveChangesAsync();

        return Ok(Json("Отдел успешно удален"));
      }

      return BadRequest(Json("Произошла ошибка"));
    }
  }
}