using System.Collections.Generic;

namespace TestDimaBack.Models
{
  public class CodeLanguage
  {
    public int Id { get; set; }
    public string Language { get; set; }
    public List<EmployeeLanguage> EmployeeLanguages { get; set; }

    public CodeLanguage()
    {
      EmployeeLanguages = new List<EmployeeLanguage>();
    }
  }
}