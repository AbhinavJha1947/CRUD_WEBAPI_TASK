using CRUD_WEBAPI_TASK.DAL;
using CRUD_WEBAPI_TASK.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_WEBAPI_TASK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeDAL _empDAL;

        public EmployeeController(EmployeeDAL empDAL)
        {
            _empDAL = empDAL;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetEmployees()
        {
            return Ok(_empDAL.GetAllEmployees());
        }

        [HttpGet("{id}")]
        public ActionResult<Employee> GetEmployee(int id)
        {
            var employee = _empDAL.GetEmployeeById(id);
            if (employee == null) return NotFound();
            return Ok(employee);
        }

        [HttpPost]
        public ActionResult<Employee> AddEmployee(Employee emp)
        {
            _empDAL.AddEmployee(emp);
            return CreatedAtAction(nameof(GetEmployee), new { id = emp.EmployeeID }, emp);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, Employee emp)
        {
            if (id != emp.EmployeeID) return BadRequest();
            _empDAL.UpdateEmployee(emp);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            _empDAL.DeleteEmployee(id);
            return NoContent();
        }
    }
}
