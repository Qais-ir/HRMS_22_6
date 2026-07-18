using HRMS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HRMS.Dtos.Employees;
using HRMS.DbContexts;
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


        public static List<Employee> employees = new List<Employee>()
        {
            new Employee(){ Id = 1, FirstName = "Ahmad", LastName="Nasser", Email = "Ahmad@123.com", Position="Developer", BirthDate = new DateTime(1995,1,25), PhoneNumber="+9625588625", IsActive = true, StartDate = new DateTime(), Salary = 1000},
            new Employee(){ Id = 2, FirstName = "Layla", LastName = "Kareem", Email = "Layla@123.com", Position = "HR", BirthDate = new DateTime(2000,1,25), PhoneNumber = "+9625588625", IsActive = true, StartDate = new DateTime(2026, 1, 1), Salary = 1000},
            new Employee(){ Id = 3, FirstName = "Yousef", LastName = "Faris", Email = "Yousef@123.com", Position = "Manager", BirthDate = new DateTime(1996,1,25), PhoneNumber = "+9625588625", IsActive = true, StartDate = new DateTime(2026, 1, 1), Salary = 1200},
            new Employee(){ Id = 4, FirstName = "Nadia", LastName = "Zaid", Email = "Nadia@123.com", Position = "Developer", BirthDate = new DateTime(1999,1,25), PhoneNumber = "+9625588625", IsActive = true, StartDate = new DateTime(2026, 1, 1), Salary = 800}

        };


        [HttpGet] 
        public IActionResult GetByCriteria([FromQuery] SearchEmployeeDto searchEmployeeDto) // Endpoint
        {
            // Query Syntax DEVELOPER
            var data = from emp in _dbContext.Employees
                       from dep in _dbContext.Departments.Where(x => x.Id == emp.DepartmentId).DefaultIfEmpty() // join / inner join - left join (DefaultIfEmpty)
                       from manager in _dbContext.Employees.Where(x => x.Id == emp.ManagerId).DefaultIfEmpty()
                       where (searchEmployeeDto.Position == null || emp.Position.ToUpper().Contains(searchEmployeeDto.Position.ToUpper())) &&
                       (searchEmployeeDto.Name == null || emp.FirstName.ToUpper().Contains(searchEmployeeDto.Name.ToUpper()))
                       
                       orderby emp.Id descending
                       select new EmployeeDto
                       {
                           Id = emp.Id,
                           Name = emp.FirstName + " " + emp.LastName,
                           Position = emp.Position,
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

        [HttpGet("{id:long}")] // Route Parameter
        public IActionResult GetById(long id)
        {
            //var data = employees.SingleOrDefault(x => x.Id == id);
            var data = employees.Select(x => new EmployeeDto
            {
                Id = x.Id,
                Name = x.FirstName + " " + x.LastName,
                Position = x.Position,
                BirthDate = x.BirthDate,
                StartDate = x.StartDate,
                EndDate = x.EndDate
            }).FirstOrDefault(x => x.Id == id);


            if (data == null)
            {
                return NotFound("Employee Not Found");
            }
            
            
            return Ok(data);
        }

        // Request => Body, Query Parameters
        [HttpPost]
        public IActionResult Add(SaveEmployeeDto employeeDto)
        {
            var employee = new Employee()
            {
                Id = (employees.LastOrDefault()?.Id ?? 0) + 1,
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Position= employeeDto.Position,
                BirthDate = employeeDto.BirthDate,
                StartDate = employeeDto.StartDate,
                EndDate= employeeDto.EndDate,
                Email = employeeDto.Email,
                IsActive = employeeDto.IsActive,
                PhoneNumber = employeeDto.PhoneNumber,
                Salary = employeeDto.Salary,
            };

            employees.Add(employee);
            return Ok(employee.Id);
        }

        // Request => Body, Query Parameters
        [HttpPut("{id:long}")] // Resource Update
        //[HttpPatch] // Resource Update
        public IActionResult Update(long id, [FromBody] SaveEmployeeDto employeeDto)
        {
            if(id != employeeDto.Id)
            {
                return BadRequest("Id Mismatch");//400
            }

            var employee = employees.FirstOrDefault(x => x.Id == employeeDto.Id);
            if(employee == null)
            {
                return NotFound("Employee Does Not Exist");
            }

            employee.FirstName = employeeDto.FirstName;
            employee.LastName = employeeDto.LastName;
            employee.Position = employeeDto.Position;
            employee.BirthDate = employeeDto.BirthDate;
            employee.StartDate = employeeDto.StartDate;
            employee.EndDate = employeeDto.EndDate;
            employee.Email = employeeDto.Email;
            employee.IsActive = employeeDto.IsActive;
            employee.Salary = employeeDto.Salary;
            employee.PhoneNumber = employeeDto.PhoneNumber;

            return Ok();


        }

        [HttpDelete("{id:long}")]
        public IActionResult Delete(long id)
        {
            var employee = employees.FirstOrDefault(x => x.Id == id);
            if(employee == null)
            {
                return NotFound("Employee Does Not Exist");
            }

            employees.Remove(employee);
            return Ok();

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