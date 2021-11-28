EmployeeId idKyle = new("J18");
Employee kyle = new Employee(idKyle, "Kyle Bush", 138_000.00m );

EmployeeId idMartin = new("J19");
Employee martin = new(idMartin, "Martin Truex Jr", 73_000.00m);

EmployeeId idKevin = new("S4");
Employee kevin = new(idKevin, "Kevin Harvick", 116_000.00m);

EmployeeId idDenny = new EmployeeId("J11");
Employee denny = new Employee(idDenny, "Denny Hamlin", 127_000.00m);

EmployeeId idJoey = new("T22");
Employee joey = new(idJoey, "Joey Logano", 96_000.00m);

EmployeeId idKyleL = new ("C42");
Employee kyleL = new (idKyleL, "Kyle Larson", 80_000.00m);


Dictionary<EmployeeId, Employee> employees = new(31)
{
    [idKyle] = kyle,
    [idMartin] = martin,
    [idKevin] = kevin,
    [idDenny] = denny,
    [idJoey] = joey,
};

foreach (var employee in employees.Values)
{
    Console.WriteLine(employee);
}

while (true)
{
    Console.Write("Enter employee id (X to exit)> ");
    string? userInput = Console.ReadLine();
    userInput = userInput?.ToUpper();
    if (userInput == null || userInput == "X") break;

    try
    {
        EmployeeId id = new(userInput);

        if (!employees.TryGetValue(id, out Employee? employee))
        {
            Console.WriteLine($"Employee with id {id} does not exist");
        }
        else
        {
            Console.WriteLine(employee);
        }
    }
    catch (EmployeeIdException ex)
    {
        Console.WriteLine(ex.Message);
    }
}
