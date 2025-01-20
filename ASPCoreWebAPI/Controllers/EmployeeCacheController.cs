using ASPCoreWebAPI.Cache;
using ASPCoreWebAPI.Data;
using ASPCoreWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASPCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeCacheController : ControllerBase
    {
        private readonly IcacheService _cacheService;
        private readonly MyDbContext _context;

        public EmployeeCacheController(IcacheService cacheService, 
            MyDbContext context)
        {
            _cacheService = cacheService;
            _context = context;
        }

        [HttpGet("Employees")]
        public IEnumerable<Employee> Get()
        {
            var cachedData = _cacheService.GetData<IEnumerable<Employee>>("Employees");
            if (cachedData != null)
            {
                return cachedData;
            }
            var expirationTime = DateTime.Now.AddMinutes(50);
            cachedData = _context.Employees.ToList();
            _cacheService.SetData<IEnumerable<Employee>>("Employees", cachedData, expirationTime);
            return cachedData;
        }

        [HttpGet("Employee/{id}")]
        public Employee Get(int id)
        {
            var cachedData = _cacheService.GetData<IEnumerable<Employee>>("Employees");
            Employee employee = new Employee();
            if (cachedData != null)
            {
                employee = cachedData.FirstOrDefault(x => x.Id == id);
                return employee;
            }
            employee = _context.Employees.FirstOrDefault(x => x.Id == id);
            return employee;
        }

        [HttpPut("Update")]
        public void Update(Employee employee)
        {
            _context.Employees.Update(employee);
            _context.SaveChanges();
            _cacheService.RemoveData("Employees");
        }

        [HttpDelete("Delete")]
        public void Delete(int id)
        {
            var employee = _context.Employees.FirstOrDefault(x => x.Id == id);
            _context.Employees.Remove(employee);
            _context.SaveChanges();
            _cacheService.RemoveData("Employees");
        }
    }
}
