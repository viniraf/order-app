namespace OrderApp.Endpoints.Employees;

public class EmployeeRequest
{
    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string EmployeeCode { get; set; } = string.Empty;
}