using System.ComponentModel.DataAnnotations;

namespace TestDimaBack.ParamModels
{
  public class ParamEditEmployee : ParamNewEmployee
  {
    [Required]
    public int Id { get; set; }
  }
}