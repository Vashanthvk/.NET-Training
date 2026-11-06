
using EmployeeDepartmentManagement.Models;
using EmployeeDepartmentManagement.Models.Services;
using Xunit;

namespace EmployeeDepartmentManagement.Tests;

public class EmployeeQueriesTests
{
    private static List<Employee> GetTestEmployees()
    {
        Department it = new Department(1, "IT");
        Department hr = new Department(2, "HR");

        return new List<Employee>
        {
            new Employee(101, "Arun", 25000, it),
            new Employee(102, "Kamala", 60000, hr),
            new Employee(103, "Vimal", 45000, it),
            new Employee(104, "Sri", 50000, hr)
        };
    }

    [Fact]
    public void GetEmployeesByDepartment_ReturnsMatchingEmployees()
    {
        // Arrange
        var employees = GetTestEmployees();

        // Act
        var result = EmployeeQueries
            .GetEmployeesByDepartment(employees, "IT")
            .ToList();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.All(result, employee =>
            Assert.Equal("IT", employee.Department.Name));
    }

    [Fact]
    public void GetEmployeesByMinimumSalary_ReturnsMatchingEmployees()
    {
        // Arrange
        var employees = GetTestEmployees();

        // Act
        var result = EmployeeQueries
            .GetEmployeesByMinimumSalary(employees, 50000)
            .ToList();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.All(result, employee =>
            Assert.True(employee.Salary >= 50000));
    }

    [Fact]
    public void SortEmployeesBySalary_ReturnsAscendingOrder()
    {
        // Arrange
        var employees = GetTestEmployees();

        // Act
        var result = EmployeeQueries
            .SortEmployeesBySalary(employees)
            .ToList();

        // Assert
        Assert.Equal(
            new decimal[] { 25000, 45000, 50000, 60000 },
            result.Select(e => e.Salary).ToArray());
    }

    [Fact]
    public void GroupEmployeesByDepartment_ReturnsCorrectGroups()
    {
        // Arrange
        var employees = GetTestEmployees();

        // Act
        var result = EmployeeQueries
            .GroupEmployeesByDepartment(employees)
            .ToList();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal(2, result.Single(g => g.Key == "IT").Count());
        Assert.Equal(2, result.Single(g => g.Key == "HR").Count());
    }

    [Fact]
    public void SearchEmployeeByName_WithExistingName_ReturnsEmployee()
    {
        // Arrange
        var employees = GetTestEmployees();

        // Act
        var result = EmployeeQueries
            .SearchEmployeeByName(employees, "Sri");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(104, result.Id);
        Assert.Equal("Sri", result.Name);
    }

    [Fact]
    public void SearchEmployeeByName_WithUnknownName_ReturnsNull()
    {
        // Arrange
        var employees = GetTestEmployees();

        // Act
        var result = EmployeeQueries
            .SearchEmployeeByName(employees, "Unknown");

        // Assert
        Assert.Null(result);
    }
}
