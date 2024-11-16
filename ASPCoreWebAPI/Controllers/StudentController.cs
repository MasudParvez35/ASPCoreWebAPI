using ASPCoreWebAPI.Data;
using ASPCoreWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPCoreWebAPI.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly MyDbContext _context;

    public StudentController(MyDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Student>>> GetStudents()
    {
        var students = await _context.Students.ToListAsync();
        
        return Ok(students);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Student>> GetStudentById(int id)
    {
        //var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);
        //return student;
        var student = await _context.Students.FindAsync(id);
        if (student == null)
            return NotFound();
        return student;
    }

    [HttpPost]
    public async Task<ActionResult<Student>> CreateStudent(Student student)
    {
        await _context.Students.AddAsync(student);
        await _context.SaveChangesAsync();

        return Ok(student);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Student>> UpdateStudent(int id, Student student)
    {
        if (id != student.Id)
            return BadRequest();
        _context.Update(student);
        await _context.SaveChangesAsync();
        return Ok(student);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Student>> DeleteStudent(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null)
            return NotFound();
        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
        
        return Ok();
    }
}
