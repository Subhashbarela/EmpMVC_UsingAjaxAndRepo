using EmpAjax_UsingRepo.DataContext;
using EmpAjax_UsingRepo.Models;
using EmpAjax_UsingRepo.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpAjax_UsingRepo.Repository.Service
{
    public class EmployeeRepo:IEmployeeRepo
    {
        private readonly ApplicationContext _context;

        public EmployeeRepo(ApplicationContext context)
        {
            _context = context;
        }

        public void AddEmployee(Employee employee)
        {           
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Employee>> GetEmployeeList()
        {
            var data = await _context.Employees.ToListAsync();
            return data;
        }

        public async Task<Employee> GetEmplyeeById(int id)
        {
           var data=await _context.Employees.Where(x =>x.Id == id).FirstOrDefaultAsync();
            return data;
        }

        public async Task<bool> UpdateEmployee(Employee employee)
        {
            bool status = false;

            if (employee != null)
            {
                _context.Employees.Update(employee);
                await _context.SaveChangesAsync();
                status = true;
            }

            return status;
        }
        public async Task<bool> DeleteEmployee(Employee employee)
        {     

            // Check if the employee exists in the database
            var existingEmployee = await _context.Employees.FindAsync(employee.Id);

            if (existingEmployee != null)
            {
                // Perform the deletion
                _context.Employees.Remove(existingEmployee);
                await _context.SaveChangesAsync();

                return true;
            }

            // Employee not found
            return false;
        }

    }
}
