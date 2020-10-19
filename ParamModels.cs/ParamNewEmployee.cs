using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestDimaBack.ParamModels
{
  public class ParamNewEmployee
  {
    [Required]
    public string Name { get; set; }
    [Required]
    public string SecondName { get; set; }
    [Required]
    public int Age { get; set; }
    [Required]
    public List<int> LanguageIds { get; set; }
    [Required]
    public int DepartmentId { get; set; }
  }
}