using System.ComponentModel.DataAnnotations;

namespace TestDimaBack.ParamModels
{
  public class ParamNewDepartment
  {
    [Required]
    public string DepartmentName { get; set; }
    [Required]
    public int Floor { get; set; }
  }
}