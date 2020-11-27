using System;
using System.Collections.Generic;

EmployeeId idJimmie = new("C48");
Employee jimmie = new(idJimmie, "Jimmie Johnson", 150926.00m);

EmployeeId idJoey = new("F22");
Employee joey = new (idJoey, "Joey Logano", 45125.00m);

EmployeeId idKyle = new("T18");
Employee kyle = new (idKyle, "Kyle Bush", 78728.00m);

EmployeeId idCarl = new("T19");
Employee carl = new (idCarl, "Carl Edwards", 80473.00m);

EmployeeId idMatt = new("T20");
Employee matt = new (idMatt, "Matt Kenseth", 113970.00m);

Dictionary<EmployeeId, Employee> employees = new (31)
{
    [idJimmie] = jimmie,
    [idJoey] = joey,
    [idKyle] = kyle,
    [idCarl] = carl,
    [idMatt] = matt
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
