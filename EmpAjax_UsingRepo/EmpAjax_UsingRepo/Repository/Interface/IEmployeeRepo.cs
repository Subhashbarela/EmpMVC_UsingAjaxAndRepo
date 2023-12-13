using EmpAjax_UsingRepo.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmpAjax_UsingRepo.Repository.Interface
{
    public interface IEmployeeRepo
    {
        Task<IEnumerable<Employee>> GetEmployeeList();
        Task<Employee> GetEmplyeeById(int id);

        void AddEmployee(Employee employee);

        Task<bool> UpdateEmployee(Employee employee);
        Task<bool> DeleteEmployee(Employee employee);
    }
}
