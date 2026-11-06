
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeDepartmentManagement.Models.Interfaces;

public interface IEmployeeService
{
    void AddEmployee(Employee employee);

    List<Employee> GetAllEmployees();

    Employee? GetEmployeeById(int id);

    bool UpdateEmployeeSalary(int id, decimal salary);

    Task<List<Employee>> GetAllEmployeesAsync(
    CancellationToken cancellationToken = default);
}
