using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetEmployeeByIdAsync(int Id);
        Task<Employee> GetEmployeeByNameAsync(Employee employee);
        Task<IReadOnlyList<Employee>> GetEmployeesAsync();
        Task<Employee> CreateEmployee(Employee employee);
        bool DeleteEmployee(int Id);
    }
}
