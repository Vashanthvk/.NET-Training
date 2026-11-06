
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeDepartmentManagement.Models;
using EmployeeDepartmentManagement.Models.Services;
using Xunit;

namespace EmployeeDepartmentManagement.Tests;

public class EmployeeServiceTests
{
    [Fact]
    public async Task GetAllEmployeesAsync_ReturnsEmployees()
    {
        // Arrange
        var service = new EmployeeService();
        var department = new Department(1, "IT");

        service.AddEmployee(
            new Employee(101, "Arun", 50000, department));

        // Act
        var result = await service.GetAllEmployeesAsync();

        // Assert
        Assert.Single(result);
        Assert.Equal("Arun", result[0].Name);
    }

    [Fact]
    public async Task GetAllEmployeesAsync_WhenCancelled_ThrowsOperationCanceledException()
    {
        // Arrange
        var service = new EmployeeService();

        using var cancellationTokenSource = new CancellationTokenSource();
        cancellationTokenSource.Cancel();

        // Act & Assert
        await Assert.ThrowsAnyAsync<OperationCanceledException>(
            () => service.GetAllEmployeesAsync(
                cancellationTokenSource.Token));
    }
}
