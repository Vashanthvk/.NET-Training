
using System;
using System.Collections.Generic;
using EmployeeDepartmentManagement.Models;
using EmployeeDepartmentManagement.Models.Interfaces;
using EmployeeDepartmentManagement.Models.Services;

class Program
{
    static IEmployeeService employeeService = new EmployeeService();

    static void Main()
    {
        bool isRunning = true;

        while (isRunning)
        {
            Console.WriteLine("\n===== Employee Management =====");
            Console.WriteLine("1. Add Employee");
            Console.WriteLine("2. View Employees");
            Console.WriteLine("3. Search Employee");
            Console.WriteLine("4. Calculate Salary");
            Console.WriteLine("5. Update Employee Salary");
            Console.WriteLine("6. Exit");

            Console.Write("Enter choice: ");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Enter a valid number.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    AddEmployee();
                    break;

                case 2:
                    ViewEmployees();
                    break;

                case 3:
                    SearchEmployee();
                    break;

                case 4:
                    CalculateSalary();
                    break;

                case 5:
                    UpdateEmployeeSalary();
                    break;

                case 6:
                    isRunning = false;
                    Console.WriteLine("Application closed.");
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }

    // Add Employee
    static void AddEmployee()
    {
        Console.Write("Enter ID: ");

        if (!int.TryParse(Console.ReadLine(), out int id) || id <= 0)
        {
            Console.WriteLine("Invalid employee ID.");
            return;
        }

        if (employeeService.GetEmployeeById(id) != null)
        {
            Console.WriteLine("Employee ID already exists.");
            return;
        }

        Console.Write("Enter Name: ");
        string name = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Name is required.");
            return;
        }

        Console.Write("Enter Department: ");
        string department = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(department))
        {
            Console.WriteLine("Department is required.");
            return;
        }

        Console.Write("Enter Salary: ");

        if (!decimal.TryParse(Console.ReadLine(), out decimal salary)
            || salary <= 0)
        {
            Console.WriteLine("Salary must be greater than zero.");
            return;
        }

        try
        {
            Department employeeDepartment = new Department(1, department);

            Employee employee = new Employee(
                id,
                name,
                salary,
                employeeDepartment
            );

            employeeService.AddEmployee(employee);

            Console.WriteLine("Employee added successfully.");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    // View Employees
    static void ViewEmployees()
    {
        List<Employee> employees = employeeService.GetAllEmployees();

        if (employees.Count == 0)
        {
            Console.WriteLine("No employees found.");
            return;
        }

        Console.WriteLine("\n===== EMPLOYEE DETAILS =====");

        foreach (Employee employee in employees)
        {
            employee.DisplayDetails();
            Console.WriteLine("----------------------------");
        }
    }

    // Search Employee
    static void SearchEmployee()
    {
        Console.Write("Enter Employee ID: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        Employee? employee = employeeService.GetEmployeeById(id);

        if (employee == null)
        {
            Console.WriteLine("Employee not found.");
            return;
        }

        employee.DisplayDetails();
    }

    // Calculate Salary
    static void CalculateSalary()
    {
        Console.Write("Enter Employee ID: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        Employee? employee = employeeService.GetEmployeeById(id);

        if (employee == null)
        {
            Console.WriteLine("Employee not found.");
            return;
        }

        decimal bonus = employee.Salary * 0.10m;
        decimal totalSalary = employee.Salary + bonus;

        Console.WriteLine($"Basic Salary: {employee.Salary}");
        Console.WriteLine($"Bonus: {bonus}");
        Console.WriteLine($"Total Salary: {totalSalary}");
    }

    // Update Employee Salary
    static void UpdateEmployeeSalary()
    {
        Console.Write("Enter Employee ID: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid Employee ID.");
            return;
        }

        Console.Write("Enter New Salary: ");

        if (!decimal.TryParse(Console.ReadLine(), out decimal salary)
            || salary <= 0)
        {
            Console.WriteLine("Salary must be greater than zero.");
            return;
        }

        bool updated = employeeService.UpdateEmployeeSalary(id, salary);

        if (updated)
        {
            Console.WriteLine("Salary updated successfully.");
        }
        else
        {
            Console.WriteLine("Employee not found.");
        }
    }
}
