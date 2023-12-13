using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace EmpAjax_UsingRepo.Models.ViewModel
{
    public class SignUpUserViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Please enter username")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please enter email")]
        [RegularExpression("[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}",ErrorMessage ="Please enter valid Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter mobile number")]
        [Display(Name ="Mobile Number")]
        public long? Mobile { get; set; }
        [Required(ErrorMessage = "Please enter password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please enter confirm password")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Confirm password can't matched!")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        [Display(Name = "Active")]
        public bool IsActive { get; set; }

       
    }
}
