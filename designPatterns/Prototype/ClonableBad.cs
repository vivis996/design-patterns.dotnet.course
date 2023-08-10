using System.Xml.Serialization;
using Newtonsoft.Json;

namespace designPatterns.Prototype;

public static class ExtensionMethods
{
    public static T DeepCopy<T>(this T self)
    {
        string jsonString = JsonConvert.SerializeObject(self);
        return JsonConvert.DeserializeObject<T>(jsonString);
    }

    public static T DeepCopyXml<T>(this T self)
    {
        using (var ms = new MemoryStream())
        {
            var s = new XmlSerializer(typeof(T));
            s.Serialize(ms, self);
            ms.Position = 0;
            return (T)s.Deserialize(ms);
        }
    }
}

public class Person
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

    public Person(Person other)
    {
        this.Names = other.Names;
        this.Address = new Address(other.Address);
    }

    public override string ToString()
    {
        return $"{nameof(Names)}: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
    }
}

public class Address
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

    public Address(Address otherAddress)
    {
        this.StreetName = otherAddress.StreetName;
        this.HouseNumber = otherAddress.HouseNumber;
    }

    public override string ToString()
    {
        return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
    }
}

public class ClonableBad : IRun
{
    public void Run()
    {
        var john = new Person(new[] { "John", "Smith", },
            new Address("123 New York Av", 123));

        var jane = john.DeepCopyXml();
        jane.Names = new[] { "Jane" };
        jane.Address.HouseNumber = 321;

        Console.WriteLine(john);
        Console.WriteLine(jane);
    }
}
