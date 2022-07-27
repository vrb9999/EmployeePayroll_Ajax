using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePayroll_Ajax.Models
{
    public class EmployeePayrollDBContext : DbContext
    {
        public EmployeePayrollDBContext(DbContextOptions<EmployeePayrollDBContext> options) : base(options)
        { }

        public DbSet<EmployeeModel> Employee { get; set; }
    }
}
