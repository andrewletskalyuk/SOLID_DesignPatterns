using AWS_DynamoDb.Entity;
using AWS_DynamoDb.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AWS_DynamoDb.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly IServiceStudent _serviceStudent;

    public StudentController(IServiceStudent serviceStudent)
    {
        _serviceStudent = serviceStudent;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Student>>> GetAllStudents()
    {
        return Ok(await _serviceStudent.GetAllStudents());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Student>> GetStudent(int id)
    {
        var student = await _serviceStudent.GetStudent(id);
        if (student == null)
            return NotFound();

        return student;
    }

    [HttpPost]
    public async Task<IActionResult> AddStudent([FromBody] Student student)
    {
        await _serviceStudent.AddStudent(student);
        return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStudent(int id, [FromBody] Student student)
    {
        if (id != student.Id)
            return BadRequest("ID mismatch");

        var existingStudent = await _serviceStudent.GetStudent(id);
        if (existingStudent == null)
            return NotFound();

        await _serviceStudent.UpdateStudentDto(student);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        var existingStudent = await _serviceStudent.GetStudent(id);
        if (existingStudent == null)
            return NotFound();

        await _serviceStudent.DeleteStudentDto(id);
        return NoContent();
    }
}
