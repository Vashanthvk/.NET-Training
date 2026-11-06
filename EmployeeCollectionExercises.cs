
using System;
using System.Collections.Generic;
using EmployeeDepartmentManagement.Models;
using EmployeeDepartmentManagement.Models.Interfaces;

public class EmployeeCollectionExercises
{
    public static void Run(IEmployeeService employeeService)
    {
        // Retrieve existing employee records
        List<Employee> employees = employeeService.GetAllEmployees();

        if (employees.Count == 0)
        {
            Console.WriteLine("No employees available.");
            Console.WriteLine("Add employees before running collection exercises.");
            return;
        }

        // 1. LIST - Display employee records
        Console.WriteLine("\n===== LIST COLLECTION =====");

        foreach (Employee employee in employees)
        {
            Console.WriteLine(
                $"{employee.Id} - {employee.Name} - {employee.Department.Name}"
            );
        }

        // 2. ARRAY - Store employee IDs
        Console.WriteLine("\n===== ARRAY COLLECTION =====");

        int[] employeeIds = new int[employees.Count];

        for (int i = 0; i < employees.Count; i++)
        {
            employeeIds[i] = employees[i].Id;
        }

        Console.WriteLine("Employee IDs:");

        foreach (int id in employeeIds)
        {
            Console.WriteLine(id);
        }

        // 3. DICTIONARY - Count employees by department
        Console.WriteLine("\n===== DICTIONARY COLLECTION =====");

        Dictionary<string, int> departmentCounts = new Dictionary<string, int>();

        foreach (Employee employee in employees)
        {
            string departmentName = employee.Department.Name;

            if (departmentCounts.ContainsKey(departmentName))
            {
                departmentCounts[departmentName]++;
            }
            else
            {
                departmentCounts.Add(departmentName, 1);
            }
        }

        foreach (KeyValuePair<string, int> item in departmentCounts)
        {
            Console.WriteLine(
                $"{item.Key}: {item.Value} employees"
            );
        }
    }
}
