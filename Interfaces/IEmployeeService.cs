
using System.Collections.Generic;

namespace EmployeeDepartmentManagement.Models.Interfaces;

public interface IEmployeeService
{
    void AddEmployee(Employee employee);

    List<Employee> GetAllEmployees();

    Employee? GetEmployeeById(int id);

    bool UpdateEmployeeSalary(int id, decimal salary);
}
