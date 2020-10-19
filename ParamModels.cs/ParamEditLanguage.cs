using System.ComponentModel.DataAnnotations;

namespace TestDimaBack.ParamModels
{
  public class ParamEditLanguage : ParamNewLanguage
  {
    [Required]
    public int Id { get; set; }
  }
}