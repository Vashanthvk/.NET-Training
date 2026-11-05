
using System;
using System.Collections.Generic;

class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Department { get; set; }
    public decimal Salary { get; set; }
}

class Program
{
    static List<Employee> employees = new List<Employee>();

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
            Console.WriteLine("5. Exit");

            Console.Write("Enter choice: ");

            string? input = Console.ReadLine();

            if (!int.TryParse(input, out int choice))
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
                    isRunning = false;
                    Console.WriteLine("Application closed.");
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }

    static void AddEmployee()
    {
        Console.Write("Enter ID: ");

        if (!int.TryParse(Console.ReadLine(), out int id) || id <= 0)
        {
            Console.WriteLine("Invalid employee ID.");
            return;
        }

        if (employees.Exists(e => e.Id == id))
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
            || salary < 0)
        {
            Console.WriteLine("Invalid salary.");
            return;
        }

        Employee employee = new Employee
        {
            Id = id,
            Name = name,
            Department = department,
            Salary = salary
        };

        employees.Add(employee);

        Console.WriteLine("Employee added successfully.");
    }

    static void ViewEmployees()
    {
        if (employees.Count == 0)
        {
            Console.WriteLine("No employees found.");
            return;
        }

        foreach (Employee employee in employees)
        {
            Console.WriteLine(
                $"{employee.Id} | {employee.Name} | " +
                $"{employee.Department} | {employee.Salary}");
        }
    }

    static void SearchEmployee()
    {
        Console.Write("Enter Employee ID: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        Employee? employee = employees.Find(e => e.Id == id);

        if (employee == null)
        {
            Console.WriteLine("Employee not found.");
            return;
        }

        Console.WriteLine($"Name: {employee.Name}");
        Console.WriteLine($"Department: {employee.Department}");
        Console.WriteLine($"Salary: {employee.Salary}");
    }

    static void CalculateSalary()
    {
        Console.Write("Enter Employee ID: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        Employee? employee = employees.Find(e => e.Id == id);

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
}
