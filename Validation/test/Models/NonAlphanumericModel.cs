using System.ComponentModel.DataAnnotations;

namespace Wangkanai.Validation.Models;

public class NonAlphanumericModel : BaseModel<NonAlphanumericModel>, IPasswordModel
{
    [Required]
    [RequireNonAlphanumeric]
    public string Password { get; set; }
}