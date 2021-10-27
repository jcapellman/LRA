using System.ComponentModel.DataAnnotations;

namespace LRA.Models
{
    public class RegistrationModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string PreferredMethod { get; set; }

        public string CourseType { get; set; }
    }
}
