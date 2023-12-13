using EmpAjax_UsingRepo.Models;
using EmpAjax_UsingRepo.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EmpAjax_UsingRepo.Controllers
{
 [Authorize]
    public class AjaxController : Controller
    {
        private readonly IEmployeeRepo _empRepo;

        public AjaxController(IEmployeeRepo empRepo)
        {
          _empRepo = empRepo;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> EmployeeList()
        {
            var data = await _empRepo.GetEmployeeList();

            if (data == null || !data.Any())
            {
                return new JsonResult("Data not found");
            }

            return new JsonResult(data);
        }
        [HttpPost]
        public JsonResult AddEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var emp = new Employee()
                {
                    Name = employee.Name,
                    City = employee.City,
                    State = employee.State,
                    Salary = employee.Salary,
                };
                _empRepo.AddEmployee(emp);
                TempData["success"] = "Data inserted successfully..";
                return Json(new { success = true, message = "Data inserted successfully." });

            }
            TempData["error"] = "Invalid data. Please check the form.";
            return Json(new { success = false, message = "Invalid data. Please check the form." });
        }
        [HttpGet]

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var data = await _empRepo.GetEmplyeeById(id);

                if (data != null)
                {
                    return Json(new { success = true, data = data });
                }
                else
                {
                    return NotFound(new { success = false, message = "Employee not found" });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                return StatusCode(500, new { success = false, message = "An error occurred while fetching data." });
            }
        }


        [HttpPost]
        public async Task<IActionResult> Update(Employee employee)
        {
            if (ModelState.IsValid && employee != null)
            {
                bool updateStatus = await _empRepo.UpdateEmployee(employee);

                if (updateStatus)
                {
                   // TempData["success"] = "Data Updated successfully..";
                    return Json(new { success = true, message = "Data Updated successfully." });
                }
                else
                {
                   // TempData["error"] = "Invalid data. Please check the form.";
                    return Json(new { success = false, message = "Invalid data. Please check the form." });
                }
            }
            else
            {
                return Json(new { success = false, message = "Invalid data. Please fill input field." });
            }

        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var employeeToDelete = await _empRepo.GetEmplyeeById(id);

                if (employeeToDelete != null)
                {
                   bool stutus= await _empRepo.DeleteEmployee(employeeToDelete);
                    if (stutus)
                    {
                        return Json(new { success = true, message = "Employee deleted successfully" });
                    }
                    else
                    {
                        return NotFound(new { success = false, message = "Employee record is not deleted" });
                    }
                }
                else
                {
                    return NotFound(new { success = false, message = "Employee not found" });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                return StatusCode(500, new { success = false, message = "An error occurred while deleting the employee." });
            }
        }

    }
}
