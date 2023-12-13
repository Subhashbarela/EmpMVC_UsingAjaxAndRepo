using EmpAjax_UsingRepo.Models.Account;
using EmpAjax_UsingRepo.Models.ViewModel;
using System.Threading.Tasks;

namespace EmpAjax_UsingRepo.Repository.Interface
{
    public interface IUserRepo
    {
        bool SignUpUser(SignUpUserViewModel model);

        Task<User> UserLogin(LoginSignUpViewModel login);
    }
}
