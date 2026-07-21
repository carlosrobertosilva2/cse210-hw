using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Inheritance Learning Activity ===\n");

        // Test the base Assignment class
        Console.WriteLine("1. Testing Base Assignment Class:");
        Assignment simpleAssignment = new Assignment("Samuel Bennett", "Multiplication");
        Console.WriteLine(simpleAssignment.GetSummary());
        Console.WriteLine();

        // Test the MathAssignment class
        Console.WriteLine("2. Testing MathAssignment Class:");
        MathAssignment mathAssignment = new MathAssignment(
            "Roberto Rodriguez", 
            "Fractions", 
            "7.3", 
            "8-19"
        );
        Console.WriteLine(mathAssignment.GetSummary());
        Console.WriteLine(mathAssignment.GetHomeworkList());
        Console.WriteLine();

        // Test the WritingAssignment class
        Console.WriteLine("3. Testing WritingAssignment Class:");
        WritingAssignment writingAssignment = new WritingAssignment(
            "Mary Waters", 
            "European History", 
            "The Causes of World War II"
        );
        Console.WriteLine(writingAssignment.GetSummary());
        Console.WriteLine(writingAssignment.GetWritingInformation());
        Console.WriteLine();

        // Additional demonstration - showing inheritance in action
        Console.WriteLine("4. Demonstrating Inheritance Benefits:");
        Console.WriteLine("The MathAssignment and WritingAssignment classes both");
        Console.WriteLine("inherited the GetSummary() method from Assignment.");
        Console.WriteLine("They also each added their own specialized methods.");
        Console.WriteLine();
        
        Console.WriteLine("5. Demonstrating Substitution Principle:");
        Console.WriteLine("A MathAssignment object can be used wherever an");
        Console.WriteLine("Assignment object is expected (Liskov Substitution).");
        
        // This shows substitution - storing a derived class in a base class variable
        Assignment assignmentAsBase = new MathAssignment("John Doe", "Algebra", "5.2", "1-10");
        Console.WriteLine($"Using MathAssignment as Assignment: {assignmentAsBase.GetSummary()}");
        
        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}
