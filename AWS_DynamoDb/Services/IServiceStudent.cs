using AWS_DynamoDb.Entity;

namespace AWS_DynamoDb.Services;

public interface IServiceStudent
{
    Task<IEnumerable<Student>> GetAllStudents();
    Task<Student> GetStudent(int Id);
    Task AddStudent(Student student);
    Task UpdateStudentDto(Student student);
    Task DeleteStudentDto(int studentId);
}
