namespace TestDimaBack.Models
{
  public class EmployeeLanguage
  {
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }

    public int CodeLanguageId { get; set; }
    public CodeLanguage CodeLanguage { get; set; }
  }
}