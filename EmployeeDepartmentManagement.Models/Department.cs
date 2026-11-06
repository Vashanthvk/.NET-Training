
namespace EmployeeDepartmentManagement.Models;

public class Department
{
    public int Id { get; private set; }
    public string Name { get; private set; }

    public Department(int id, string name)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid department ID.");

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Department name is required.");

        Id = id;
        Name = name;
    }
}
