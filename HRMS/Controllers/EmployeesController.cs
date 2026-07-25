using HRMS.DbContexts;
using HRMS.Dtos.Employees;
using HRMS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;
namespace HRMS.Controllers
{
    // Data Annotations : Extra Informations
    [Route("api/[controller]")] // api/Employees
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        // CRUD Operations
        // C : Create
        // R : Read
        // U : Update
        // D : Delete


        //HRMSContext _dbContext = new HRMSContext();

        // Dependency Injuction 
        private readonly HRMSContext _dbContext;
        public EmployeesController(HRMSContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet] 
        public IActionResult GetByCriteria([FromQuery] SearchEmployeeDto searchEmployeeDto) // Endpoint
        {
            try
            {
                // Query Syntax DEVELOPER
                var data = from emp in _dbContext.Employees
                           from dep in _dbContext.Departments.Where(x => x.Id == emp.DepartmentId).DefaultIfEmpty() // join / inner join - left join (DefaultIfEmpty)
                           from manager in _dbContext.Employees.Where(x => x.Id == emp.ManagerId).DefaultIfEmpty()
                           from position in _dbContext.Lookups.Where(x => x.Id == emp.PositionId).DefaultIfEmpty()

                           where (searchEmployeeDto.PositionId == null || emp.PositionId == searchEmployeeDto.PositionId) &&
                           (searchEmployeeDto.Name == null || emp.FirstName.ToUpper().Contains(searchEmployeeDto.Name.ToUpper()))

                           orderby emp.Id descending
                           select new EmployeeDto
                           {
                               Id = emp.Id,
                               Name = emp.FirstName + " " + emp.LastName,
                               PositionId = emp.PositionId,
                               PositionName = position.Name,
                               BirthDate = emp.BirthDate,
                               StartDate = emp.StartDate,
                               EndDate = emp.EndDate,
                               PhoneNumber = emp.PhoneNumber,
                               Email = emp.Email,
                               IsActive = emp.IsActive,
                               Salary = emp.Salary,
                               DepartmentId = emp.DepartmentId,
                               DepartmentName = dep.Name,
                               ManagerId = emp.ManagerId,
                               ManagerName = manager.FirstName + " " + manager.LastName
                           };

                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Exception(ex.Message));
            }



        }

        [HttpGet("{id:long}")] // Route Parameter
        public IActionResult GetById(long id)
        {
            try
            {
                //  var data = _dbContext.Employees.Join(
                //    _dbContext.Departments,
                //    employee => employee.DepartmentId,
                //    department => department.Id,
                //    (employee, department) => new EmployeeDto
                //    {
                //        Id = employee.Id,
                //        Name = employee.FirstName + " " + employee.LastName,
                //        Position = employee.Position,
                //        BirthDate = employee.BirthDate,
                //        StartDate = employee.StartDate,
                //        EndDate = employee.EndDate,
                //        DepartmentId = employee.DepartmentId,
                //        DepartmentName = department.Name,
                //    }
                //).FirstOrDefault(x => x.Id == id);

                //var data = employees.SingleOrDefault(x => x.Id == id);
                var data = _dbContext.Employees.Select(x => new EmployeeDto
                {
                    Id = x.Id,
                    Name = x.FirstName + " " + x.LastName,
                    PositionId = x.PositionId,
                    PositionName = x.Lookup.Name,
                    BirthDate = x.BirthDate,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    PhoneNumber = x.PhoneNumber,
                    Email = x.Email,
                    IsActive = x.IsActive,
                    Salary = x.Salary,
                    DepartmentId = x.DepartmentId,
                    DepartmentName = x.Department.Name,
                    ManagerId = x.ManagerId,
                    ManagerName = x.Manager.FirstName + " " + x.Manager.LastName,
                }).FirstOrDefault(x => x.Id == id);

                //var data = _dbContext.Employees.Include(x => x.Department).Include(x => x.Manager)
                //    .FirstOrDefault(x => x.Id == id);


                if (data == null)
                {
                    return NotFound("Employee Not Found");
                }


                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Exception(ex.Message));
            }



        }

        // Eager Loading : Include
        // Lazy Loading
        // Projection : Select => Navigation Property


        // Request => Body, Query Parameters
        [HttpPost]
        public IActionResult Add(SaveEmployeeDto employeeDto)
        {
            try
            {
                var employee = new Employee()
                {
                    Id = 0,//(employees.LastOrDefault()?.Id ?? 0) + 1,
                    FirstName = employeeDto.FirstName,
                    LastName = employeeDto.LastName,
                    PositionId = employeeDto.PositionId,
                    BirthDate = employeeDto.BirthDate,
                    StartDate = employeeDto.StartDate,
                    EndDate = employeeDto.EndDate,
                    Email = employeeDto.Email,
                    IsActive = employeeDto.IsActive,
                    PhoneNumber = employeeDto.PhoneNumber,
                    Salary = employeeDto.Salary,
                    DepartmentId = employeeDto.DepartmentId,
                    ManagerId = employeeDto.ManagerId,
                };

                _dbContext.Employees.Add(employee);

                _dbContext.SaveChanges();

                return Ok(employee.Id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Exception(ex.Message));
            }

        }

        // Request => Body, Query Parameters
        [HttpPut("{id:long}")] // Resource Update
        //[HttpPatch] // Resource Update
        public IActionResult Update(long id, [FromBody] SaveEmployeeDto employeeDto)
        {
            try
            {
                if (id != employeeDto.Id)
                {
                    return BadRequest("Id Mismatch");//400
                }

                var employee = _dbContext.Employees.FirstOrDefault(x => x.Id == employeeDto.Id);
                if (employee == null)
                {
                    return NotFound("Employee Does Not Exist");
                }

                employee.FirstName = employeeDto.FirstName;
                employee.LastName = employeeDto.LastName;
                employee.PositionId = employeeDto.PositionId;
                employee.BirthDate = employeeDto.BirthDate;
                employee.StartDate = employeeDto.StartDate;
                employee.EndDate = employeeDto.EndDate;
                employee.Email = employeeDto.Email;
                employee.IsActive = employeeDto.IsActive;
                employee.Salary = employeeDto.Salary;
                employee.PhoneNumber = employeeDto.PhoneNumber;
                employee.DepartmentId = employeeDto.DepartmentId;
                employee.ManagerId = employeeDto.ManagerId;

                _dbContext.SaveChanges();

                return Ok();

            }

            catch (Exception ex)
            {
                return StatusCode(500, new Exception(ex.Message));
            }


        }

        [HttpDelete("{id:long}")]
        public IActionResult Delete(long id)
        {
            try
            {
                var employee = _dbContext.Employees.FirstOrDefault(x => x.Id == id);
                if (employee == null)
                {
                    return NotFound("Employee Does Not Exist");
                }

                _dbContext.Employees.Remove(employee);
                _dbContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Exception(ex.Message));
            }

        }


    }
}

// Query Parameter => [FromQuery]
// Request Body => [FromBody]

// Simple Data type => string, int, long... --> (By Default) Query Parameters
// Complix Data type => Model, Dto, Object.. --> (By Default) Request Body

// Method Can Use Multiple Parameters Of Type [fromQuery]
// Method Can Not Use Multiple Parameters Of Type [FromBody]

// HttpGet => FromQuery