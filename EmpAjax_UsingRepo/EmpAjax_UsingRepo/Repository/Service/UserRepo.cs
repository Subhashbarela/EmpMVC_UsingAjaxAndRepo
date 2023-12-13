using EmpAjax_UsingRepo.DataContext;
using EmpAjax_UsingRepo.Models.Account;
using EmpAjax_UsingRepo.Models.ViewModel;
using EmpAjax_UsingRepo.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EmpAjax_UsingRepo.Repository.Service
{
    public class UserRepo : IUserRepo
    {
        private readonly ApplicationContext _context;

        public UserRepo(ApplicationContext context)
        {
            _context=context;
        }
        public bool SignUpUser(SignUpUserViewModel model)
        {
            bool status = false;
            if (model != null)
            {
                var user = new User()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Mobile = model.Mobile,
                    Password = model.Password,
                    IsActive = model.IsActive,
                };
                _context.Users.Add(user);
                _context.SaveChanges();
                status = true;
            }
            else
            {
                status = false;
            }
            return status;

        }
        public async Task<User> UserLogin(LoginSignUpViewModel login)

        {
            if (login != null)
            {
                var data =await _context.Users.Where(e => e.UserName == login.UserName).SingleOrDefaultAsync();

                return data;
            }

            return null; // or throw an exception, depending on your error-handling strategy
        }

    }
}
