# Day 5 -- Code Walkthrough

This document explains the three walkthrough topics required for the Day
5 submission:

1.  Employee class
2.  LINQ query: `GetEmployeesByDepartment()`
3.  Async operation: `GetAllEmployeesAsync()`

------------------------------------------------------------------------

## 1. Employee Class Walkthrough

**File:** `EmployeeDepartmentManagement.Models/Employee.cs`

### Purpose

The `Employee` class is the domain model representing an employee. It
stores employee information and provides behaviour for updating salary
and displaying employee details.

### Properties and encapsulation

``` csharp
public int Id { get; private set; }
public string Name { get; private set; }
public decimal Salary { get; private set; }
public Department Department { get; private set; }
```

-   `Id` stores the employee identifier.
-   `Name` stores the employee name.
-   `Salary` stores the employee salary.
-   `Department` identifies the employee's department.
-   `private set` allows other classes to read these properties but
    prevents them from assigning values directly.

This is **encapsulation**: the class controls how its internal state is
modified.

### Constructor and validation

``` csharp
public Employee(int id, string name, decimal salary, Department department)
{
    if (id <= 0)
        throw new ArgumentException("Invalid employee ID.");

    if (string.IsNullOrWhiteSpace(name))
        throw new ArgumentException("Employee name is required.");

    Id = id;
    Name = name;
    Department = department;

    UpdateSalary(salary);
}
```

The constructor initializes a new employee. It validates that the ID is
positive and the name is not null, empty, or whitespace. It delegates
salary validation to `UpdateSalary()`.

Invalid input causes an `ArgumentException`, preventing an employee from
being initialized with an invalid ID, name, or salary.

### Salary update

``` csharp
public void UpdateSalary(decimal newSalary)
{
    if (newSalary <= 0)
        throw new ArgumentException("Salary must be greater than zero.");

    Salary = newSalary;
}
```

Salary changes go through this method. It validates the new salary
before changing the property, keeping the validation rule in one place.

### DisplayDetails and polymorphism

``` csharp
public virtual void DisplayDetails()
{
    Console.WriteLine($"Employee ID: {Id}");
    Console.WriteLine($"Name: {Name}");
    Console.WriteLine($"Department: {Department.Name}");
    Console.WriteLine($"Salary: {Salary}");
}
```

The `virtual` keyword allows a derived class to override the method. The
`Manager` class inherits from `Employee` and overrides
`DisplayDetails()` to include its team-size information. This
demonstrates **inheritance** and **polymorphism**.

### Mentor explanation

> The Employee class is the domain model for an employee. It contains
> employee properties and controls how important values such as salary
> are updated. The properties use private setters to support
> encapsulation. The constructor validates the employee ID and name, and
> it uses the salary-update method to validate the initial salary.
> DisplayDetails is virtual, allowing the Manager class to override it
> and display additional manager-specific information. This demonstrates
> encapsulation, inheritance, and polymorphism.

------------------------------------------------------------------------

## 2. LINQ Query Walkthrough

**File:**
`EmployeeDepartmentManagement.Models/Services/EmployeeQueries.cs`

### Method: GetEmployeesByDepartment()

``` csharp
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
```

### Purpose

This method returns employees who belong to the requested department.
For example, searching for `"IT"` or `"it"` matches employees whose
department is named `"IT"`.

### Explanation

**`IEnumerable<Employee>`**

The input accepts a sequence of employees, such as a list or array. The
return type is also `IEnumerable<Employee>` because the method returns a
filtered sequence.

**Null validation**

``` csharp
ArgumentNullException.ThrowIfNull(employees);
```

This checks that the employee collection is not null. If it is null, the
method throws `ArgumentNullException` rather than trying to query a
missing collection.

**Department-name validation**

``` csharp
if (string.IsNullOrWhiteSpace(departmentName))
    throw new ArgumentException("Department name is required.");
```

An empty or whitespace-only department name is not a valid search value,
so the method throws `ArgumentException`.

**LINQ `Where()`**

``` csharp
employees.Where(e => ...)
```

`Where()` filters a sequence. It evaluates the condition for each
employee and produces a sequence containing only matching employees.

**Lambda expression**

``` csharp
e => e.Department.Name.Equals(...)
```

`e` represents the current employee being examined. The expression
compares that employee's department name with the requested department
name.

**Case-insensitive comparison**

``` csharp
StringComparison.OrdinalIgnoreCase
```

This comparison ignores uppercase/lowercase differences. Therefore,
`"IT"`, `"it"`, and `"It"` match.

### Deferred execution and ToList()

`Where()` uses deferred execution: the filtering is performed when the
returned sequence is enumerated.

``` csharp
var result = EmployeeQueries.GetEmployeesByDepartment(employees, "IT");
var employeeList = result.ToList();
```

Calling `ToList()` enumerates the query and materializes the matching
employees into a list.

### Complexity

For `n` employees:

-   **Time:** O(n), because the query may inspect every employee.
-   **Space:** The `Where()` iterator itself uses constant iterator
    state. Materializing the results with `ToList()` requires space
    proportional to the number of matching employees.

### Regression-test connection

The regression test verifies that searching with a different letter case
still returns the expected employees. Searching for `"it"` should match
employees whose department is named `"IT"`.

The defect was demonstrated by temporarily changing `OrdinalIgnoreCase`
to `Ordinal`. The regression test failed. Restoring `OrdinalIgnoreCase`
corrected the defect, and the same test passed.

This demonstrates the Red-Green regression workflow:

1.  Reproduce the defect.
2.  Run the regression test and observe failure.
3.  Correct the implementation.
4.  Run the same test again and verify it passes.

### Mentor explanation

> The GetEmployeesByDepartment method uses LINQ to filter an employee
> collection by department. It accepts IEnumerable`<Employee>`{=html},
> validates the collection and department name, and uses Where() with a
> lambda expression to select matching employees.
> StringComparison.OrdinalIgnoreCase makes the comparison
> case-insensitive. The query uses deferred execution, so filtering
> takes place when the result is enumerated. Calling ToList()
> materializes the matching employees into a list.

------------------------------------------------------------------------

## 3. Async Operation Walkthrough

**File:**
`EmployeeDepartmentManagement.Models/Services/EmployeeService.cs`

### Method: GetAllEmployeesAsync()

``` csharp
public async Task<List<Employee>> GetAllEmployeesAsync(
    CancellationToken cancellationToken = default)
{
    // Simulate asynchronous work
    await Task.Delay(100, cancellationToken);

    cancellationToken.ThrowIfCancellationRequested();

    return _employees.ToList();
}
```

### Purpose

This method asynchronously retrieves all employees from the service's
in-memory collection. It returns `Task<List<Employee>>`, which
represents an asynchronous operation that eventually produces a list of
employees.

### Explanation

**`Task<List<Employee>>`**

`Task<T>` represents an asynchronous operation that eventually produces
a value of type `T`. Here, `T` is `List<Employee>`.

**`async`**

The `async` keyword allows the method to use `await` and return an
asynchronous task.

**`await`**

``` csharp
await Task.Delay(100, cancellationToken);
```

`Task.Delay()` creates an asynchronous delay. The method awaits the
delay before continuing. The 100-millisecond delay simulates
asynchronous work; it is not a database query.

**`CancellationToken`**

``` csharp
CancellationToken cancellationToken = default
```

A cancellation token communicates a cancellation request to an
operation. The caller can provide a token if it needs the ability to
cancel the operation. The default value means the caller is not required
to provide one.

**Cancellation check**

``` csharp
cancellationToken.ThrowIfCancellationRequested();
```

This checks whether cancellation has been requested. If the token has
been cancelled, the method throws an `OperationCanceledException` or a
derived cancellation exception. The token is also passed to
`Task.Delay()`, allowing the delay to observe cancellation.

**Returning the result**

``` csharp
return _employees.ToList();
```

If the operation completes without cancellation, it returns a new list
containing the employees. `ToList()` avoids exposing the service's
internal list directly.

### CancellationTokenSource and CancellationToken

-   `CancellationTokenSource` is used by the caller to request
    cancellation.
-   `CancellationToken` is passed to the operation so it can observe the
    request.

Example:

``` csharp
using var cancellationTokenSource = new CancellationTokenSource();

cancellationTokenSource.Cancel();

var employees = await service.GetAllEmployeesAsync(
    cancellationTokenSource.Token);
```

In this example, cancellation is requested before the service method is
called. The delay observes the cancelled token and the operation ends
with a cancellation exception.

### Unit test for cancellation

``` csharp
[Fact]
public async Task GetAllEmployeesAsync_WhenCancelled_ThrowsOperationCanceledException()
{
    var service = new EmployeeService();

    using var cancellationTokenSource = new CancellationTokenSource();
    cancellationTokenSource.Cancel();

    await Assert.ThrowsAnyAsync<OperationCanceledException>(
        () => service.GetAllEmployeesAsync(cancellationTokenSource.Token));
}
```

The test creates the service and a cancellation-token source, cancels
it, passes the cancelled token to the method, and asserts that an
`OperationCanceledException` is thrown.

A separate success test verifies that the method returns employees when
cancellation has not been requested.

### Production distinction

The current implementation uses `Task.Delay(100)` to simulate
asynchronous work because the service stores employees in an in-memory
collection. In a production application, asynchronous work would
normally involve a genuinely asynchronous operation, such as a database
query using an API like `ToListAsync(cancellationToken)`. Adding `async`
and `await` around an in-memory list operation alone does not make the
data access genuinely asynchronous.

### Mentor explanation

> I implemented GetAllEmployeesAsync in EmployeeService. It returns
> Task\<List`<Employee>`{=html}\>, representing an asynchronous
> operation that eventually returns the employee list. The async and
> await keywords allow the method to await an operation without blocking
> the calling thread while it waits. I included a CancellationToken so
> the caller can request cancellation. Task.Delay is currently used to
> simulate asynchronous work because the application uses an in-memory
> collection. I wrote unit tests for both successful execution and
> cancellation, verifying that a cancelled operation throws
> OperationCanceledException.

------------------------------------------------------------------------

## Walkthrough Summary

  -----------------------------------------------------------------------
  Area                                Concepts demonstrated
  ----------------------------------- -----------------------------------
  Employee class                      Encapsulation, constructor
                                      validation, inheritance,
                                      polymorphism

  LINQ query                          IEnumerable, Where, lambda
                                      expressions, case-insensitive
                                      comparison, deferred execution

  Async operation                     Task`<T>`{=html}, async, await,
                                      CancellationToken, cancellation
                                      testing
  -----------------------------------------------------------------------

These three walkthroughs cover the class, LINQ query, and async
operation explanations required for the Day 5 submission.
