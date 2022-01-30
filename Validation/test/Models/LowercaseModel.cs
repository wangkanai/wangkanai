using System.ComponentModel.DataAnnotations;

namespace Wangkanai.Validation.Models;

public class LowercaseModel : BaseModel<LowercaseModel>, IPasswordModel
{
    [RequireLowercase]
    public string Password { get; set; }
}