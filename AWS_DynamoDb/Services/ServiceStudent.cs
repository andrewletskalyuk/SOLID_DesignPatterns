using Amazon.DynamoDBv2.DataModel;
using AWS_DynamoDb.Entity;

namespace AWS_DynamoDb.Services;

public class ServiceStudent : IServiceStudent
{
    private readonly IDynamoDBContext _context;

    public ServiceStudent(IDynamoDBContext context)
    {
        _context=context;
    }

    public async Task AddStudent(Student student)
    {
        await _context.SaveAsync(student);
    }

    public async Task DeleteStudentDto(int studentId)
    {
        await _context.DeleteAsync<Student>(studentId);
    }

    public async Task<IEnumerable<Student>> GetAllStudents()
    {
        var conditions = new List<ScanCondition>(); // Use ScanConditions if necessary
        return await _context.ScanAsync<Student>(conditions).GetRemainingAsync();
    }

    public async Task<Student> GetStudent(int id)
    {
        return await _context.LoadAsync<Student>(id);
    }

    public async Task UpdateStudentDto(Student student)
    {
        await _context.SaveAsync(student); // DynamoDBContext handles updates if the item already exists
    }
}
