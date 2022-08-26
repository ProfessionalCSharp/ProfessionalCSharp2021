namespace DictionarySample;

public record Employee
{
    private readonly string _name;
    private readonly decimal _salary;
    private readonly EmployeeId _id;

    public Employee(EmployeeId id, string name, decimal salary)
    {
        _id = id;
        _name = name;
        _salary = salary;
    }

    public override string ToString() => $"{_id}: {_name,-20} {_salary,12:C}";
}
