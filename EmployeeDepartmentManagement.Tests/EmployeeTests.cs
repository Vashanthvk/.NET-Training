
using EmployeeDepartmentManagement.Models;
using Xunit;

namespace EmployeeDepartmentManagement.Tests;

public class EmployeeTests
{
    [Fact]
    public void Constructor_WithValidData_CreatesEmployee()
    {
        // Arrange
        Department department = new Department(1, "IT");

        // Act
        Employee employee = new Employee(
            101, "Arun", 50000, department);

        // Assert
        Assert.Equal(101, employee.Id);
        Assert.Equal("Arun", employee.Name);
        Assert.Equal(50000, employee.Salary);
        Assert.Equal("IT", employee.Department.Name);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_WithInvalidId_ThrowsException(int id)
    {
        // Arrange
        Department department = new Department(1, "IT");

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            new Employee(id, "Arun", 50000, department));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_WithInvalidName_ThrowsException(string name)
    {
        // Arrange
        Department department = new Department(1, "IT");

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            new Employee(101, name, 50000, department));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-100)]
    public void Constructor_WithInvalidSalary_ThrowsException(decimal salary)
    {
        // Arrange
        Department department = new Department(1, "IT");

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            new Employee(101, "Arun", salary, department));
    }

    [Fact]
    public void UpdateSalary_WithValidSalary_UpdatesEmployeeSalary()
    {
        // Arrange
        Department department = new Department(1, "IT");
        Employee employee = new Employee(
            101, "Arun", 40000, department);

        // Act
        employee.UpdateSalary(60000);

        // Assert
        Assert.Equal(60000, employee.Salary);
    }

    [Fact]
    public void UpdateSalary_WithInvalidSalary_ThrowsException()
    {
        // Arrange
        Department department = new Department(1, "IT");
        Employee employee = new Employee(
            101, "Arun", 40000, department);

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            employee.UpdateSalary(0));
    }
}
