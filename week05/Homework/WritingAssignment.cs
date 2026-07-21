public class WritingAssignment : Assignment
{
    private string _title;

    public WritingAssignment(string studentName, string topic, string title) 
        : base(studentName, topic)
    {
        _title = title;
    }

    public string GetWritingInformation()
    {
        // Using the GetStudentName() method from the base class
        // since _studentName is private
        return $"{_title} by {GetStudentName()}";
    }
}
