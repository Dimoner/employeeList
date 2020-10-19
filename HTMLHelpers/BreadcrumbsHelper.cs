using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TestDimaBack.HTMLHelpers
{
  public static class BreadcrumbsHelper
  {
    public static HtmlString CreateBreadCrumbs(this IHtmlHelper html, ActiveBreadcrumbs state)
    {
      string result = "<ul class=\"container_breadcrumbs\">";

      result += $"<a href=\"/\" class=\"container_breadcrumbs_text {AddActive(state, ActiveBreadcrumbs.Employee)}\">Сотрудники</a>";
      result += $"<a href=\"/departments\" class=\"container_breadcrumbs_text {AddActive(state, ActiveBreadcrumbs.Department)}\">Отделы</a>";
      result += $"<a href=\"/languages\" class=\"container_breadcrumbs_text {AddActive(state, ActiveBreadcrumbs.Languages)}\">Языки</a>";

      result += "</ul>";

      return new HtmlString(result);
    }

    public enum ActiveBreadcrumbs {
      Employee,
      Department,
      Languages
    }

    private static string AddActive(ActiveBreadcrumbs state, ActiveBreadcrumbs currentState)
    {
      if(state == currentState)
      {
        return "active";
      }

      return "";
    }
  }
}