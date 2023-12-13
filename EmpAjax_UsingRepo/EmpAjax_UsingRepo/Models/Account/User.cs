using System.ComponentModel.DataAnnotations;

namespace EmpAjax_UsingRepo.Models.Account
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public long? Mobile { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsActive { get; set; }
    }
}
