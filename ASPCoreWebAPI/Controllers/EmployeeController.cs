using ASPCoreWebAPI.Data;
using ASPCoreWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPCoreWebAPI.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly MyDbContext _context;

    public EmployeeController(MyDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Employee>>> GetEmployees()
    {
        var employees = await _context.Employees.ToListAsync();
        
        return Ok(employees);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Employee>> GetEmployeeById(int id)
    {
        var employee = await _context.Employees.FirstOrDefaultAsync(s => s.Id == id);
        if (employee == null)
            return NotFound();
        
        return employee;
    }

    [HttpPost]
    public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
    {
        await _context.Employees.AddAsync(employee);
        await _context.SaveChangesAsync();

        return Ok(employee);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Employee>> UpdateEmployee(int id, Employee employee)
    {
        if (id != employee.Id)
            return BadRequest();
        
        _context.Update(employee);
        await _context.SaveChangesAsync();
        
        return Ok(employee);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Employee>> DeleteEmployee(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null)
            return NotFound();
        
        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
        
        return Ok();
    }
}
