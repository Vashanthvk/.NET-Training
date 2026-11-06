
namespace EmployeeDepartmentManagement.Models;

public class Employee
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public decimal Salary { get; private set; }
    public Department Department { get; private set; }

    public Employee(
        int id,
        string name,
        decimal salary,
        Department department)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid employee ID.");

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Employee name is required.");

        Id = id;
        Name = name;
        Department = department;

        UpdateSalary(salary);
    }

    public void UpdateSalary(decimal newSalary)
    {
        if (newSalary <= 0)
            throw new ArgumentException(
                "Salary must be greater than zero.");

        Salary = newSalary;
    }

    public virtual void DisplayDetails()
    {
        Console.WriteLine($"Employee ID: {Id}");
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Department: {Department.Name}");
        Console.WriteLine($"Salary: {Salary}");
    }
}
