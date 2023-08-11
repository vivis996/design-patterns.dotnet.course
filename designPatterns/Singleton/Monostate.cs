namespace designPatterns.Singleton;

public class CEO
{
    private static string name;
    private static int age;

    public string Name
    {
        get => name;
        set => name = value;
    }

    public int Age
    {
        get => age;
        set => age = value;
    }

    public override string ToString()
    {
        return $"{nameof(Name)}: {Name}, {nameof(Age)}: {Age}";
    }
}

public class Monostate : IRun
{
    public void Run()
    {
        var ceo = new CEO();
        ceo.Name = "Adam Smith";
        ceo.Age = 55;

        var ceo2 = new CEO();
        ceo.Name = "John Doe";
        ceo.Age = 45;

        Console.WriteLine(ceo);
        Console.WriteLine(ceo2);
    }
}

