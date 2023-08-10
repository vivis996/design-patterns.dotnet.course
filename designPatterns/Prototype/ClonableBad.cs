namespace designPatterns.Prototype;

public class Person : ICloneable
{
    public string[] Names;
    public Address Address;

    public Person(string[] names, Address address)
    {
        this.Names = names;
        this.Address = address;
    }

    public override string ToString()
    {
        return $"{nameof(Names)}: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
    }

    public object Clone()
    {
        return new Person(Names, Address.Clone() as Address);
    }
}

public class Address : ICloneable
{
    public string StreetName;
    public int HouseNumber;

    public Address(string streetName, int houseNumber)
    {
        this.StreetName = streetName;
        this.HouseNumber = houseNumber;
    }

    public override string ToString()
    {
        return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
    }

    public object Clone()
    {
        return new Address(StreetName, HouseNumber);
    }
}

public class ClonableBad : IRun
{
    public void Run()
    {
        var john = new Person(new[] { "John", "Smith", },
            new Address("123 New York Av", 123));

        Person jane = john.Clone() as Person;
        jane.Names = new[] { "Jane" };
        jane.Address.HouseNumber = 321;

        Console.WriteLine(john);
        Console.WriteLine(jane);
    }
}
