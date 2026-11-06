
using System;
using System.Collections.Generic;
using System.Linq;
using EmployeeDepartmentManagement.Models.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeDepartmentManagement.Models.Services;

public class EmployeeService : IEmployeeService
{
    private readonly List<Employee> _employees = new();

    public void AddEmployee(Employee employee)
    {
        if (_employees.Any(e => e.Id == employee.Id))
        {
            throw new InvalidOperationException(
                "Employee ID already exists.");
        }

        _employees.Add(employee);
    }

    public List<Employee> GetAllEmployees()
    {
        return _employees.ToList();
    }

    public Employee? GetEmployeeById(int id)
    {
        return _employees.FirstOrDefault(e => e.Id == id);
    }

    public bool UpdateEmployeeSalary(int id, decimal salary)
    {
        Employee? employee = GetEmployeeById(id);

        if (employee == null)
            return false;

        employee.UpdateSalary(salary);

        return true;
    }

    public async Task<List<Employee>> GetAllEmployeesAsync(
    CancellationToken cancellationToken = default)
    {
        // Simulate asynchronous work
        await Task.Delay(100, cancellationToken);

        cancellationToken.ThrowIfCancellationRequested();

        return _employees.ToList();
    }
}
