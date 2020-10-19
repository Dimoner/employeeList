using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TestDimaBack.HTMLHelpers
{
  public static class SelectHelper
  {
    public static HtmlString CreateList(this IHtmlHelper html, int id, string name, int checkId)
    {
      string selected = id == checkId ? "selected" : "";

      string result = $"<option value=\"{id}\" {selected}>{name}</option>";

      return new HtmlString(result);
    }
  }
}