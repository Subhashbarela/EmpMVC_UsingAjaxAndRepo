using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace EmpAjax_UsingRepo.Models.Account
{
    public class UserLogin
    {
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool IsRemember { get; set; }
    }
}
