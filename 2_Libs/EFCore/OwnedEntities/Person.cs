public record Location(string Country, string City);

public record Address(string? LineOne = default, string? LineTwo = default, Location? Location = default);

public record Person(string FirstName, string LastName, Address? PrivateAddress = default, Address? CompanyAddress = default);

