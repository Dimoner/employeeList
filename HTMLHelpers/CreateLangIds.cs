using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestDimaBack.Models;
using System.Collections.Generic;

namespace TestDimaBack.HTMLHelpers
{
  public static class CreateLangIds
  {
    public static HtmlString CreateLangIdList(this IHtmlHelper html,  List<EmployeeLanguage> list)
    {
      string result = "";

      foreach (EmployeeLanguage item in list)
      {
          result += $"{item.CodeLanguageId},";
      }

      return new HtmlString(result.TrimEnd(','));
    }
  }
}