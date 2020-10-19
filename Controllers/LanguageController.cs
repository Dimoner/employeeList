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
  public class LanguageController : BaseController
  {
    public LanguageController(ApplicationContext context) : base(context) { }

    [Route("/languages")]
    public IActionResult Languages()
    {
      IEnumerable<CodeLanguage> codeLanguages = db.CodeLanguages.Include(lang => lang.EmployeeLanguages);

      return View(codeLanguages);
    }

    [Route("/languages/create")]
    [HttpPost]
    public async Task<IActionResult> CreateLanguage(ParamNewLanguage newLanguage)
    {
      if (ModelState.IsValid)
      {
        CodeLanguage lang = new CodeLanguage { Language = newLanguage.Language };

        db.CodeLanguages.Add(lang);

        await db.SaveChangesAsync();
      }

      return LocalRedirect("/languages");
    }

    [Route("/languages/edit")]
    [HttpPost]
    public async Task<IActionResult> EditLanguage(ParamEditLanguage editLanguage)
    {
      if (ModelState.IsValid)
      {
        CodeLanguage codeLanguage = db.CodeLanguages.FirstOrDefault(lang => lang.Id == editLanguage.Id);

        codeLanguage.Language = editLanguage.Language;

        await db.SaveChangesAsync();
      }

      return LocalRedirect("/languages");
    }

    [Route("/languages/delete/{id}")]
    [HttpDelete]
    public async Task<IActionResult> EditLanguage(int? id)
    {
      Console.WriteLine(id);

      if (id != null)
      {
        CodeLanguage codeLanguage = await db.CodeLanguages
        .Include(lang => lang.EmployeeLanguages)
        .FirstOrDefaultAsync(lang => lang.Id == id);

        if (codeLanguage.EmployeeLanguages.Count() == 0)
        {
          db.CodeLanguages.Remove(codeLanguage);
          db.SaveChanges();
          return Ok(Json("Язык успешно удален"));
        }
        return BadRequest(Json("Сначала удалите носителей языка!!!"));
      }

      return BadRequest(Json("Ошибка в работе метода"));
    }
  }
}