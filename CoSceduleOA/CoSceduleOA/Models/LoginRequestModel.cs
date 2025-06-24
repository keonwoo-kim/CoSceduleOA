using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CoScheduleOA.Models
{
    public class LoginRequestModel
    {
        [Required] 
        public string UserId { get; set; } = null!;
        [Required] 
        public string Password { get; set; } = null!;
    }
}
