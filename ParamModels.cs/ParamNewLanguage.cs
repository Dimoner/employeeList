using System.ComponentModel.DataAnnotations;

namespace TestDimaBack.ParamModels
{
  public class ParamNewLanguage
  {
    [Required]
    public string Language { get; set; }
  }
}