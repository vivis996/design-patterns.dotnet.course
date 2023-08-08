namespace designPatterns.Builder.FacetedBuilder;

public class Person
{
    // address
    public string StreetAddress, PostCode, City;

    // employment
    public string CompanyName, Position;
    public int AnnualIncome;

    public override string? ToString()
    {
        return $"{nameof(this.StreetAddress)}: {this.StreetAddress}, " +
            $"{nameof(this.PostCode)}: {this.PostCode}, " +
            $"{nameof(this.City)}: {this.City}, " +
            $"{nameof(this.CompanyName)}: {this.CompanyName}," +
            $"{nameof(this.Position)}: {this.Position}, " +
            $"{nameof(this.AnnualIncome)}: {this.AnnualIncome}";
    }
}

public class PersonBuilder // facade
{
    // reference!
    protected Person person = new();

    public PersonJobBuilder works => new PersonJobBuilder(person);
    public PersonAddressBuilder lives => new PersonAddressBuilder(person);

    public static implicit operator Person(PersonBuilder pb)
    {
        return pb.person;
    }
}

public class PersonAddressBuilder : PersonBuilder
{
    // might not work with a value type!
    public PersonAddressBuilder(Person person)
    {
        this.person = person;
    }

    public PersonAddressBuilder At(string streetAddress)
    {
        this.person.StreetAddress = streetAddress;
        return this;
    }

    public PersonAddressBuilder WithPostcode(string postCode)
    {
        this.person.PostCode = postCode;
        return this;
    }

    public PersonAddressBuilder In(string city)
    {
        this.person.City = city;
        return this;
    }
}

public class PersonJobBuilder : PersonBuilder
{
    public PersonJobBuilder(Person person)
    {
        this.person = person;
    }

    public PersonJobBuilder At(string companyName)
    {
        this.person.CompanyName = companyName;
        return this;
    }

    public PersonJobBuilder AsA(string position)
    {
        this.person.Position = position;
        return this;
    }

    public PersonBuilder Earning(int amount)
    {
        this.person.AnnualIncome = amount;
        return this;
    }
}

public class FacetedBuilder : IRun
{
    public void Run()
    {
        var pb = new PersonBuilder();
        Person person = pb
            .lives.At("123 new york stret")
                .In("New York City")
                .WithPostcode("10002")
            .works.At("fabrikam")
                .AsA("Engineer")
                .Earning(123000);

        Console.WriteLine(person);
    }
}
