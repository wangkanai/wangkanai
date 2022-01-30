using System.ComponentModel.DataAnnotations;

namespace Wangkanai.Validation.Models;

public class DigitModel : BaseModel<DigitModel>, IPasswordModel
{
    [RequireDigit]
    public string Password { get; set; }
}