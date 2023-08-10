namespace designPatterns.Prototype;

public interface IPrototype<T>
{
    T DeepCopy();
}

public class Person : IPrototype<Person>
{
    public string[] Names;
    public Address Address;

    public Person(string[] names, Address address)
    {
        this.Names = names;
        this.Address = address;
    }

    public Person(Person other)
    {
        this.Names = other.Names;
        this.Address = new Address(other.Address);
    }

    public override string ToString()
    {
        return $"{nameof(Names)}: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
    }

    public Person DeepCopy()
    {
        return new Person(this.Names, this.Address.DeepCopy());
    }
}

public class Address : IPrototype<Address>
{
    public string StreetName;
    public int HouseNumber;

    public Address(string streetName, int houseNumber)
    {
        this.StreetName = streetName;
        this.HouseNumber = houseNumber;
    }

    public Address(Address otherAddress)
    {
        this.StreetName = otherAddress.StreetName;
        this.HouseNumber = otherAddress.HouseNumber;
    }

    public override string ToString()
    {
        return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
    }

    public Address DeepCopy()
    {
        return new Address(this.StreetName, this.HouseNumber);
    }
}

public class ClonableBad : IRun
{
    public void Run()
    {
        var john = new Person(new[] { "John", "Smith", },
            new Address("123 New York Av", 123));

        var jane = john.DeepCopy();
        jane.Names = new[] { "Jane" };
        jane.Address.HouseNumber = 321;

        Console.WriteLine(john);
        Console.WriteLine(jane);
    }
}
