using Amazon.DynamoDBv2.DataModel;
using StudentServiceDDB.Dtos;
using StudentServiceDDB.Entity;

namespace StudentServiceDDB;

public class StudentServiceDDB : IStudentServiceDDB
{
    private readonly IDynamoDBContext _context;

    public StudentServiceDDB(IDynamoDBContext context)
    {
        _context=context;
    }

    public async Task AddStudent(Student student)
    {
        await _context.SaveAsync(student);
    }

    public Task DeleteStudentDto(int studentId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<StudentDto>> GetAllStudents()
    {
        throw new NotImplementedException();
    }

    public Task<StudentDto> GetStudent(int Id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateStudentDto(StudentDto StudentDto)
    {
        throw new NotImplementedException();
    }
}