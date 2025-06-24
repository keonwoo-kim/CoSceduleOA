using System.ComponentModel.DataAnnotations;

namespace CoScheduleOA.Models
{
    public class CreateAccountRequestModel
    {
        [Required]
        public required string UserId { get; set; }
        [Required]
        public required string UserName { get; set; }
        [Required]
        public required string Password { get; set; }
        [Required]
        public required string RePassword { get; set; }
    }
}
