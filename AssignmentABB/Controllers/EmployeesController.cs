using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AssignmentABB.Dtos;
using AssignmentABB.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentABB.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeesController(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<EmployeeToReturn>>> GetEmployees()
        {
            var employees = await _employeeRepository.GetEmployeesAsync();
            if(!employees.Any())
            {
                return NotFound(new ApiResponse((int)HttpStatusCode.NotFound, "There are no employees"));
            }
            return Ok(_mapper.Map<IReadOnlyList<Employee>, IReadOnlyList<EmployeeToReturn>>(employees));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<EmployeeToReturn>> GetEmployee(int id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            if(employee==null)
            {
                return NotFound(new ApiResponse((int)HttpStatusCode.NotFound));
            }
            return Ok(_mapper.Map<Employee, EmployeeToReturn>(employee));
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(EmployeeToCreate employee)
        {
            var employeeToBeCreated = _mapper.Map<EmployeeToCreate, Employee>(employee);
            var existingEmployee = await _employeeRepository.GetEmployeeByNameAsync(employeeToBeCreated);
            if(existingEmployee!=null)
            {
                return BadRequest(new ApiResponse((int)HttpStatusCode.BadRequest, "Employee already exists"));
            }
            var createdEmployee = await _employeeRepository.CreateEmployee(employeeToBeCreated);
            return Ok(createdEmployee);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var ckeckEmployeeDeleted = _employeeRepository.DeleteEmployee(id);
            if(!ckeckEmployeeDeleted)
            {
                return NotFound(new ApiResponse((int)HttpStatusCode.NotFound));
            }
            return Ok();
        }
    }
}