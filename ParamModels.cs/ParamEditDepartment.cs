using System.ComponentModel.DataAnnotations;

namespace TestDimaBack.ParamModels
{
  public class ParamEditDepartment : ParamNewDepartment
  {
    [Required]
    public int Id { get; set; }
  }
}