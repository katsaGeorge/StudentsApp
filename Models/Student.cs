namespace StudentsDbApp.Models
{

/// <summary>
/// POCO (Plain old CLR Object)
/// </summary>
    public class Student
    {
        public int Id { get; set; }
        public string? FirstName { get; set; } = null!;
        public string? LastName { get; set; } = null!;
    }
}
