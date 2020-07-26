using Core.Entities;
using Core.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly Repository _repository;

        public EmployeeRepository(Repository repository)
        {
            _repository = repository;
        }
        public async Task<Employee> CreateEmployee(Employee employee)
        {
            _repository.Employees.Add(employee);
            await _repository.SaveChangesAsync();
            return employee ;
        }

        public bool DeleteEmployee(int Id)
        {
            var employee = _repository.Employees.Where(x => x.Id == Id).FirstOrDefault();
            if(employee==null)
            {
                return false;
            }
            _repository.Employees.Remove(employee);
            _repository.SaveChanges();
            return true;
        }

        public async Task<Employee> GetEmployeeByIdAsync(int Id)
        {
            return await _repository.Employees.Where(x => x.Id == Id).FirstOrDefaultAsync();           
        }

        public async Task<Employee> GetEmployeeByNameAsync(Employee employee)
        {
            return await _repository.Employees
                    .Where(x => x.FirstName == employee.FirstName 
                            && x.MiddleName == employee.MiddleName 
                            && x.LastName == employee.LastName
                          ).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<Employee>> GetEmployeesAsync()
        {
            return await _repository.Employees.ToListAsync();
        }
    }
}
