using System.ComponentModel.DataAnnotations;

namespace EmpAjax_UsingRepo.Models.ViewModel
{
    public class LoginSignUpViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }       
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public bool IsRemember { get; set; }
       
    }
}
