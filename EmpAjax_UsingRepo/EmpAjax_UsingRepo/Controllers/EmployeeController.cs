using EmpAjax_UsingRepo.Models;
using EmpAjax_UsingRepo.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EmpAjax_UsingRepo.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepo _empRepo;

        public EmployeeController(IEmployeeRepo empRepo)
        {
            _empRepo = empRepo;
        }
        /// <summary>
        /// Get Method
        /// </summary>
        /// <returns></returns>
        //public  async Task<ActionResult<IEnumerable<Employee>>> Index()
        //{
        //    var userId = HttpContext.Session.GetInt32("UserId");
        //    var userName = HttpContext.Session.GetString("UserName");

        //    if (userId.HasValue && !string.IsNullOrEmpty(userName))
        //    {
        //        var result = await _empRepo.GetEmployeeList();
        //        if (result == null)
        //        {
        //            return NotFound();
        //        }
        //        return View(result);
        //    }
        //    return RedirectToAction("Login", "Account");
        //}

        public async Task<ActionResult<IEnumerable<Employee>>> Index()
        {
            var result = await _empRepo.GetEmployeeList();
            if (result == null)
            {
                return NotFound("Employee data is not found");
            }
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _empRepo.GetEmplyeeById(id);
            if (result == null)
            {
                TempData["error"] = "Id is not found";
                return NotFound();
            }
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _empRepo.GetEmplyeeById(id);
            if (result == null)
            {
                TempData["error"] = "Id is not found";
                return NotFound();
            }
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _empRepo.GetEmplyeeById(id);
            if (result == null)
            {
                TempData["error"] = "Id is not found";
                return NotFound();
            }
            return View(result);
        }

        /// <summary>
        /// Post Method
        /// </summary>
        /// <returns></returns>

        public IActionResult Create(Employee employee)
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
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Employee employee)
        {
            if (ModelState.IsValid && employee == null)
            {
                bool updateStatus = await _empRepo.UpdateEmployee(employee);

                if (updateStatus)
                {
                    TempData["success"] = "Data updated successfully..";
                    return RedirectToAction("Index");
                }
                else
                {
                    return BadRequest("Failed to update employee");
                }
            }
            else
            {
                return View();
            }

        }
        [HttpPost]
        public async Task<IActionResult> Delete(Employee employee)
        {
            if (employee is null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            bool deleteStatus = await _empRepo.DeleteEmployee(employee);

            if (deleteStatus)
            {
                TempData["success"] = "Data Deleted Successfully..";
                return RedirectToAction("Index");
            }
            else
            {
                return BadRequest("Failed to delete employee");
            }
        }

    }
}
