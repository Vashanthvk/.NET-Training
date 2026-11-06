
namespace EmployeeDepartmentManagement.Models;

public class Manager : Employee
{
    public int TeamSize { get; private set; }

    public Manager(
        int id,
        string name,
        decimal salary,
        Department department,
        int teamSize)
        : base(id, name, salary, department)
    {
        if (teamSize < 0)
            throw new ArgumentException(
                "Team size cannot be negative.");

        TeamSize = teamSize;
    }

    public override void DisplayDetails()
    {
        base.DisplayDetails();
        Console.WriteLine($"Team Size: {TeamSize}");
    }
}
