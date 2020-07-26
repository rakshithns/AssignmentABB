using AssignmentABB.Controllers;
using AssignmentABB.Dtos;
using AssignmentABB.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentABB.Test
{
    [TestClass]
    public class EmployeeControllerTest
    {
        private Mock<IEmployeeRepository> _repository;
        private IMapper _mapper;

        public EmployeeControllerTest()
        {
            _repository = new Mock<IEmployeeRepository>();
            _mapper = new MapperConfiguration(c => c.AddProfile<MappingProfiles>()).CreateMapper();
        }
        [TestMethod]
        public async Task TestGetEmployeesNotFound()
        {
            _repository.Setup(repo => repo.GetEmployeesAsync()).ReturnsAsync(new List<Employee>());
            var employeeController = new EmployeesController(_repository.Object, _mapper);

            var result = await employeeController.GetEmployees();

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task TestGetEmployees()
        {
            _repository.Setup(repo => repo.GetEmployeesAsync()).ReturnsAsync(GetTestEmployees());
            var employeeController = new EmployeesController(_repository.Object, _mapper);

            var result = await employeeController.GetEmployees();

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            Assert.AreEqual("Rakshi N S", ((result.Result as OkObjectResult).Value as List<EmployeeToReturn>).First().EmployeeName);
        }

        [TestMethod]
        public async Task TestGetEmployeeByIdNotFound()
        {
            Employee emp=null;
            _repository.Setup(repo => repo.GetEmployeeByIdAsync(It.IsAny<int>())).ReturnsAsync(emp);
            var employeeController = new EmployeesController(_repository.Object, _mapper);

            var result = await employeeController.GetEmployee(3);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task TestGetEmployeeByIdFound()
        {
            _repository.Setup(repo => repo.GetEmployeeByIdAsync(It.IsAny<int>())).ReturnsAsync(GetTestEmployees().First());
            var employeeController = new EmployeesController(_repository.Object, _mapper);

            var result = await employeeController.GetEmployee(2);

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            Assert.AreEqual("Rakshi N S", ((result.Result as OkObjectResult).Value as EmployeeToReturn).EmployeeName);
        }

        [TestMethod]
        public async Task TestEmployeeCreateAlreadyExists()
        {
            var employeeToCreate = new EmployeeToCreate { FirstName = "Rakshi", MiddleName = "N", LastName = "S", DateOfBirth = DateTime.Now, City = "Bangalore", State = "Karnataka", Country = "India", ZipCode = "560103" };
            _repository.Setup(repo => repo.GetEmployeeByNameAsync(It.IsAny<Employee>())).ReturnsAsync(GetTestEmployees().First());
            var employeeController = new EmployeesController(_repository.Object, _mapper);

            var result = await employeeController.CreateEmployee(employeeToCreate);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task TestEmployeeCreate()
        {
            var employeeToCreate = new EmployeeToCreate { FirstName = "Sakshi", MiddleName = "N", LastName = "S", DateOfBirth = DateTime.Now, City = "Bangalore", State = "Karnataka", Country = "India", ZipCode = "560103" };
            var CreatedEmployee = new Employee { Id = 3,FirstName = "Sakshi", MiddleName = "N", LastName = "S", DateOfBirth = DateTime.Now, City = "Bangalore", State = "Karnataka", Country = "India", ZipCode = "560103" };
            Employee emp = null;
            _repository.Setup(repo => repo.GetEmployeeByNameAsync(It.IsAny<Employee>())).ReturnsAsync(emp);
            _repository.Setup(repo => repo.CreateEmployee(It.IsAny<Employee>())).ReturnsAsync(CreatedEmployee);
            var employeeController = new EmployeesController(_repository.Object, _mapper);

            var result = await employeeController.CreateEmployee(employeeToCreate);

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            Assert.AreEqual("3", ((result.Result as OkObjectResult).Value as Employee).Id.ToString());
        }

        [TestMethod]
        public void TestDeleteNotFound()
        {
            _repository.Setup(repo => repo.DeleteEmployee(It.IsAny<int>())).Returns(false);
            var employeeController = new EmployeesController(_repository.Object, _mapper);

            var result = employeeController.Delete(3);

            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public void TestDeleteFound()
        {
            _repository.Setup(repo => repo.DeleteEmployee(It.IsAny<int>())).Returns(true);
            var employeeController = new EmployeesController(_repository.Object, _mapper);

            var result = employeeController.Delete(3);

            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        public List<Employee> GetTestEmployees()
        {
            return new List<Employee>()
            {
                new Employee{ Id = 1, FirstName="Rakshi", MiddleName="N", LastName="S", DateOfBirth=DateTime.Now, City="Bangalore", State="Karnataka", Country="India", ZipCode="560103" },
                new Employee{ Id = 2, FirstName="Rakshith", MiddleName="N", LastName="S", DateOfBirth=DateTime.Now, City="Bangalore", State="Karnataka", Country="India", ZipCode="560103" }
            };
        }
    }
}
