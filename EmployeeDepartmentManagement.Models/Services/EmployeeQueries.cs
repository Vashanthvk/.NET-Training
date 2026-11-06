
using System;
using System.Collections.Generic;
using System.Linq;
using EmployeeDepartmentManagement.Models;

namespace EmployeeDepartmentManagement.Models.Services;

// Strongly typed result for department employee counts
public sealed record DepartmentEmployeeCount(
    string Department,
    int EmployeeCount
);

public static class EmployeeQueries
{
    // 1. Filter employees by department
    public static IEnumerable<Employee> GetEmployeesByDepartment(
        IEnumerable<Employee> employees,
        string departmentName)
    {
        ArgumentNullException.ThrowIfNull(employees);

        if (string.IsNullOrWhiteSpace(departmentName))
            throw new ArgumentException("Department name is required.");

        return employees.Where(e =>
            e.Department.Name.Equals(
                departmentName,
                StringComparison.OrdinalIgnoreCase));
    }

    // 2. Filter employees by minimum salary
    public static IEnumerable<Employee> GetEmployeesByMinimumSalary(
        IEnumerable<Employee> employees,
        decimal minimumSalary)
    {
        ArgumentNullException.ThrowIfNull(employees);

        return employees.Where(e => e.Salary >= minimumSalary);
    }

    // 3. Sort employees by salary (ascending)
    public static IEnumerable<Employee> SortEmployeesBySalary(
        IEnumerable<Employee> employees)
    {
        ArgumentNullException.ThrowIfNull(employees);

        return employees.OrderBy(e => e.Salary);
    }

    // 4. Sort employees by name
    public static IEnumerable<Employee> SortEmployeesByName(
        IEnumerable<Employee> employees)
    {
        ArgumentNullException.ThrowIfNull(employees);

        return employees.OrderBy(
            e => e.Name,
            StringComparer.OrdinalIgnoreCase);
    }

    // 5. Group employees by department
    public static IEnumerable<IGrouping<string, Employee>>
        GroupEmployeesByDepartment(IEnumerable<Employee> employees)
    {
        ArgumentNullException.ThrowIfNull(employees);

        return employees.GroupBy(
            e => e.Department.Name,
            StringComparer.OrdinalIgnoreCase);
    }

    // 6. Search employee by name
    public static Employee? SearchEmployeeByName(
        IEnumerable<Employee> employees,
        string name)
    {
        ArgumentNullException.ThrowIfNull(employees);

        if (string.IsNullOrWhiteSpace(name))
            return null;

        return employees.FirstOrDefault(e =>
            e.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    // 7. Get employee names only
    public static IEnumerable<string> GetEmployeeNames(
        IEnumerable<Employee> employees)
    {
        ArgumentNullException.ThrowIfNull(employees);

        return employees.Select(e => e.Name);
    }

    // 8. Get employee count by department
    public static IEnumerable<DepartmentEmployeeCount>
        GetEmployeeCountByDepartment(IEnumerable<Employee> employees)
    {
        ArgumentNullException.ThrowIfNull(employees);

        return employees
            .GroupBy(
                e => e.Department.Name,
                StringComparer.OrdinalIgnoreCase)
            .Select(group => new DepartmentEmployeeCount(
                group.Key,
                group.Count()
            ));
    }
}
