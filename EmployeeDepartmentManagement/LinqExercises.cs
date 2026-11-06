
using System;
using System.Collections.Generic;
using System.Linq;
using EmployeeDepartmentManagement.Models;
using EmployeeDepartmentManagement.Models.Interfaces;
using EmployeeDepartmentManagement.Models.Services;

public static class LinqExercises
{
    public static void Run(IEmployeeService employeeService)
    {
        List<Employee> employees = employeeService.GetAllEmployees();

        if (employees.Count == 0)
        {
            Console.WriteLine("No employees available.");
            return;
        }

        Console.WriteLine("\n===== LINQ EXERCISES =====");

        // Query 1: Filter employees by IT department
        Console.WriteLine("\n1. Employees in IT:");

        var itEmployees = employees
            .Where(e => e.Department.Name == "IT");

        foreach (var employee in itEmployees)
            Console.WriteLine(employee.Name);

        // Query 2: Employees earning at least 50000
        Console.WriteLine("\n2. Employees earning >= 50000:");

        var highSalaryEmployees = employees
            .Where(e => e.Salary >= 50000);

        foreach (var employee in highSalaryEmployees)
            Console.WriteLine($"{employee.Name} - {employee.Salary}");

        // Query 3: Sort by salary ascending
        Console.WriteLine("\n3. Employees sorted by salary:");

        var salaryAscending = employees
            .OrderBy(e => e.Salary);

        foreach (var employee in salaryAscending)
            Console.WriteLine($"{employee.Name} - {employee.Salary}");

        // Query 4: Sort by salary descending
        Console.WriteLine("\n4. Highest salary first:");

        var salaryDescending = employees
            .OrderByDescending(e => e.Salary);

        foreach (var employee in salaryDescending)
            Console.WriteLine($"{employee.Name} - {employee.Salary}");

        // Query 5: Select employee names
        Console.WriteLine("\n5. Employee names:");

        var employeeNames = employees
            .Select(e => e.Name);

        foreach (var name in employeeNames)
            Console.WriteLine(name);

        // Query 6: Group employees by department
        Console.WriteLine("\n6. Employees grouped by department:");

        var departmentGroups = EmployeeQueries
            .GroupEmployeesByDepartment(employees);

        foreach (var group in departmentGroups)
        {
            Console.WriteLine(group.Key);

            foreach (var employee in group)
                Console.WriteLine($"  {employee.Name}");
        }

        // Query 7: Count employees by department
        Console.WriteLine("\n7. Department-wise employee count:");

        var departmentCounts = EmployeeQueries
            .GetEmployeeCountByDepartment(employees);

        foreach (var item in departmentCounts)
            Console.WriteLine($"{item.Department}: {item.EmployeeCount}");

        // Query 8: Search employee by name
        Console.WriteLine("\n8. Search employee by name:");

        Console.Write("Enter employee name: ");
        string nameToSearch = Console.ReadLine() ?? "";

        Employee? foundEmployee = EmployeeQueries
            .SearchEmployeeByName(employees, nameToSearch);

        if (foundEmployee != null)
            foundEmployee.DisplayDetails();
        else
            Console.WriteLine("Employee not found.");
    }
}
