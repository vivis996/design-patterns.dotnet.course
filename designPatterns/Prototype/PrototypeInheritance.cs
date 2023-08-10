namespace designPatterns.Prototype.PrototypeInheritance;

public interface IDeepCopyable<T> where T : new()
{
    void CopyTo(T target);
    public T DeepCopy()
    {
        T t = new T();
        CopyTo(t);
        return t;
    }
}

public class Employee : Person, IDeepCopyable<Employee>
{
    public int Salary;

    public Employee() : base()
    {
    }

    public Employee(string[] names, Address address, int salary)
        : base(names, address)
    {
        Salary = salary;
    }

    public void CopyTo(Employee target)
    {
        base.CopyTo(target);
        target.Salary = this.Salary;
    }

    public override string ToString()
    {
        return $"{base.ToString()}, {nameof(Salary)}: {Salary}";
    }
}

public class Person : IDeepCopyable<Person>
{
    public string[] Names;
    public Address Address;

    public Person()
    {
    }

    public Person(string[] names, Address address)
    {
        this.Names = names;
        this.Address = address;
    }

    public void CopyTo(Person target)
    {
        target.Names = (string[])this.Names.Clone();
        target.Address = this.Address.DeepCopy();
    }

    public override string ToString()
    {
        return $"{nameof(Names)}: {string.Join(" ", Names)}, " +
            $"{nameof(Address)}: {Address}";
    }
}

public class Address : IDeepCopyable<Address>
{
    public string StreetName;
    public int HouseNumber;

    public Address()
    {
    }

    public Address(string streetName, int houseNumber)
    {
        this.StreetName = streetName;
        this.HouseNumber = houseNumber;
    }

    public void CopyTo(Address target)
    {
        target.StreetName = this.StreetName;
        target.HouseNumber = this.HouseNumber;
    }

    public override string ToString()
    {
        return $"{nameof(StreetName)}: {StreetName}, " +
            $"{nameof(HouseNumber)}: {HouseNumber}";
    }
}

public static class ExtensionMethods
{
    public static T DeepCopy<T>(this IDeepCopyable<T> item) where T : new()
    {
        return item.DeepCopy();
    }

    public static T DeepCopy<T>(this T person) where T : Person, new()
    {
        return ((IDeepCopyable<T>)person).DeepCopy();
    }
}

public class PrototypeInheritance : IRun
{
    public void Run()
    {
        var john = new Employee();
        john.Names = new[] { "John", "Doe" };
        john.Address = new Address
        {
            HouseNumber = 123,
            StreetName = "New York Av",
        };
        john.Salary = 321000;

        var copy = john.DeepCopy();
        var e = john.DeepCopy<Employee>();
        var p = john.DeepCopy<Person>();

        copy.Names[1] = "Smith";
        copy.Salary = 123000;
        copy.Address.HouseNumber++;

        Console.WriteLine(john);
        Console.WriteLine(copy);
    }
}
