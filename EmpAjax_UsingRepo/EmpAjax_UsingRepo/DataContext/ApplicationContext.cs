using EmpAjax_UsingRepo.Models;
using EmpAjax_UsingRepo.Models.Account;
using Microsoft.EntityFrameworkCore;

namespace EmpAjax_UsingRepo.DataContext
{
    public class ApplicationContext:DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext>option):base(option) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
