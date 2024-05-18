using Amazon.DynamoDBv2.DataModel;

namespace AWS_DynamoDb.Entity;

[DynamoDBTable("Students")]
public class Student
{
    [DynamoDBHashKey]
    public int Id { get; set; }

    [DynamoDBProperty]
    public bool IsActive { get; set; }

    [DynamoDBProperty]
    public int RollNumber { get; set; }

    [DynamoDBProperty]
    public string? StudentClass { get; set; }

    [DynamoDBProperty]
    public required string StudentName { get; set; }
}
