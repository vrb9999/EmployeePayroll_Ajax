using EmployeePayroll_Ajax.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePayroll_Ajax.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeePayrollDBContext employeePayrollDBContext;
        public EmployeeController (EmployeePayrollDBContext employeePayrollDBContext)
        {
            this.employeePayrollDBContext = employeePayrollDBContext;
        }

        public async Task<IActionResult> Index()
        {
            return View(await employeePayrollDBContext.Employee.ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> AddEmployee(int Id=0)
        {
            if(Id == 0)
            {
                return View(new EmployeeModel());
            }
            else
            {
                var emp = await employeePayrollDBContext.Employee.FindAsync(Id);
                if(emp == null)
                {
                    return NotFound();
                }
                return View(emp);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEmployee(int Id, [Bind("Emp_Id,Name,Gender,Department,Notes")] EmployeeModel emps)
        {
            if(ModelState.IsValid)
            {
                //Insert
                if (Id == 0)
                {
                    employeePayrollDBContext.Add(emps);
                    await employeePayrollDBContext.SaveChangesAsync();
                }                  

                
                // Update
                else
                {
                    try
                    {
                        employeePayrollDBContext.Update(emps);
                        await employeePayrollDBContext.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!EmployeeModelExists(emps.Emp_Id))
                        { 
                            return NotFound(); 
                        }
                        else
                        { 
                            throw; 
                        }
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", employeePayrollDBContext.Employee.ToList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddEmployee", emps) }); 
        }
        [HttpGet]
        public async Task<IActionResult> DeleteEmployee(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var emp = await employeePayrollDBContext.Employee.FirstOrDefaultAsync(x => x.Emp_Id == id);
            if(emp == null)
            {
                return NotFound();
            }
            return View(emp);
        }

        [HttpPost, ActionName("DeleteEmployee")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var emp = await employeePayrollDBContext.Employee.FindAsync(id);
            employeePayrollDBContext.Employee.Remove(emp);
            await employeePayrollDBContext.SaveChangesAsync();
            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", employeePayrollDBContext.Employee.ToList())});        
        }

        private bool EmployeeModelExists(int Id)
        {
            return employeePayrollDBContext.Employee.Any(x => x.Emp_Id == Id);
        }
    }
}
