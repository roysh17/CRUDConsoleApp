// See https://aka.ms/new-console-template for more information

// Console program untuk in-memory CRUD operation

using CRUDConsoleApp;
using System.Text.RegularExpressions;

List<Employee> employees = new List<Employee>();

// Nomor menu aplikasi
var keyEntered = "";

DisplayMenu();



// Menampilkan menu aplikasi
void DisplayMenu()
{

    Console.WriteLine("App Menu:");
    Console.WriteLine("-------------------------------------------------------------");
    Console.WriteLine("App Menu Number 1 to display employees data");
    Console.WriteLine("App Menu Number 2 to add employee data");
    Console.WriteLine("App Menu Number 3 to delete data");
    Console.WriteLine("App Menu Number 0 to close program");
    Console.WriteLine("-------------------------------------------------------------");

    Console.WriteLine();
    Console.Write("Enter App Menu Number: ");
    keyEntered = Console.ReadLine();


    if (keyEntered.Trim() == "1")
    {
        DisplayEmployees();
    }
    else if (keyEntered.Trim() == "2")
    {
        AddEmployee();
    }
    else if (keyEntered.Trim() == "3")
    {
        DeleteEmployee();
    }
    else if (keyEntered.Trim() == "0")
    {
        Environment.Exit(0);
    }
    else
    {
        Console.WriteLine("  Error: Invalid App Number!");
        Console.WriteLine();
        DisplayMenu();
    }
}


// Clear screen
void ClearScreen()
{
    Console.Clear();
    Console.WriteLine("\x1b[3J");
}


// Menampilkan list employee 
void DisplayEmployees()
{
    ClearScreen();

    Console.WriteLine("# App Menu Number 1 - Display Employees Data");
    Console.WriteLine("-------------------------------------------------------------");

    if (employees.Count > 0)
    {
        foreach (Employee emp in employees)
        {
            Console.WriteLine("* EmployeeID: " + emp.EmployeeID);
            Console.WriteLine("* FullName: " + emp.FullName);
            Console.WriteLine("* BirthDate: " + emp.BirthDate.ToString("dd-MMM-yy"));
            Console.WriteLine("-------------------------------------------------------------");
        }
    }
    else
    {
        Console.WriteLine("  No employee data found.");
    }

    Console.WriteLine();
    Console.WriteLine("-------------------------------------------------------------");

    DisplayMenu();
}

// Menambah data employee
void AddEmployee()
{
    ClearScreen();

    Console.WriteLine("# App Menu Number 2 - Add Employee Data");
    Console.WriteLine("# ctrl+c to close program");
    Console.WriteLine("-------------------------------------------------------------");

    try
    {
        var employeeID = "";
        var fullName = "";
        var birthDate = "";

        var employee = new Employee();

        employee = ValidateEmployeeID(employeeID, ref employee);

        employee = ValidateFullName(fullName, ref employee);

        employee = ValidateBirthDate(birthDate, ref employee);

        employees.Add(employee);

        Console.WriteLine();
        Console.WriteLine("  Data has been saved.");
    }
    catch (Exception ex)
    {
        Console.WriteLine("  Error: " + ex.Message.ToString());
    }
    Console.WriteLine();
    Console.WriteLine("-------------------------------------------------------------");

    DisplayMenu();
}

// validasi EmployeeID tidak boleh null atau empty
Employee ValidateEmployeeID(string employeeID, ref Employee employee)
{
    var result = "";

    Console.Write("* Enter EmployeeID: ");
    employeeID = Console.ReadLine();

    if (string.IsNullOrEmpty(employeeID))
    {
        Console.WriteLine("  Error: EmployeeID is required!");

        return ValidateEmployeeID(employeeID, ref employee);
    }

    // EmployeeID tidak boleh duplikat
    var employeeAlreadyExists = employees.FirstOrDefault(x => x.EmployeeID == employeeID) != null;
    if (employeeAlreadyExists)
    {
        Console.WriteLine("  Error: EmployeeID '" + employeeID + "' already used by other employee!");

        return ValidateEmployeeID(employeeID, ref employee);
    }

    employee.EmployeeID = employeeID;

    return employee;
}

// validasi FullName tidak boleh null atau empty
Employee ValidateFullName(string fullName, ref Employee employee)
{
    var result = "";

    Console.Write("* Enter FullName: ");
    fullName = Console.ReadLine();

    if (string.IsNullOrEmpty(fullName))
    {
        Console.WriteLine("  Error: FullName is required!");

        return ValidateFullName(fullName, ref employee);
    }

    // FullName harus huruf
    var isValidFullnameFormat = Regex.IsMatch(fullName, @"^[a-zA-Z\s]+$");
    if (!isValidFullnameFormat)
    {
        Console.WriteLine("  Error: FullName must be letters!");

        return ValidateFullName(fullName, ref employee);
    }

    employee.FullName = fullName;

    return employee;
}

// validasi BirthDate tidak boleh null atau empty dan formatnya harus benar
Employee ValidateBirthDate(string birthDate, ref Employee employee)
{
    string result = "";

    Console.Write("* Enter BirthDate [format:YYYY-MM-DD, eg: 2000-02-10]: ");
    birthDate = Console.ReadLine();

    if (string.IsNullOrEmpty(birthDate))
    {
        Console.WriteLine("  Error: BirthDate is required!");

        return ValidateBirthDate(birthDate, ref employee);
    }

    try
    {
        result = (Convert.ToDateTime(birthDate)).ToString("yyyy-MM-dd");
        employee.BirthDate = Convert.ToDateTime(birthDate);
    }
    catch (Exception ex)
    {
        Console.WriteLine("  Error: " + ex.Message.ToString() + "!");

        return ValidateBirthDate(birthDate, ref employee);
    }

    return employee;
}

void DeleteEmployee()
{
    Console.WriteLine("# App Menu Number 3 - Delete Employee Data");
    Console.WriteLine("# ctrl+c to close program");
    Console.WriteLine("-------------------------------------------------------------");
    Console.Write("* Enter EmployeeID to delete: ");
    var employeeID = Console.ReadLine();

    try
    {
        // cek apakah data sudah ada di list employee
        var employeeToDelete = employees.FirstOrDefault(x => x.EmployeeID.ToLower().Trim() == employeeID.ToLower().Trim());

        // data akan dihapus apabila data ditemukan
        if (employeeToDelete != null)
        {
            employees.Remove(employeeToDelete);

            Console.WriteLine("  Data has been deleted.");
        }
        else
        {
            Console.WriteLine("  Error: EmployeeID '" + employeeID + "' not found!");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("  Error: " + ex.Message.ToString());
    }

    Console.WriteLine();
    Console.WriteLine("-------------------------------------------------------------");

    DisplayMenu();
}

