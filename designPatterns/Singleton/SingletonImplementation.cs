using MoreLinq;

namespace designPatterns.Singleton;

public interface IDatabase
{
	int GetPopulation(string name);
}

public class SingletonDatabase : IDatabase
{
    private Dictionary<string, int> capitals;
    private static Lazy<SingletonDatabase> instance => new(() => new SingletonDatabase());

    public static SingletonDatabase Instance => instance.Value;

    private SingletonDatabase()
    {
        Console.WriteLine("Initializing database");

        capitals = File.ReadLines("Singleton/capitals.txt")
                       .Batch(2)
                       .ToDictionary(list => list.ElementAt(0).Trim(),
                                     list => int.Parse(list.ElementAt(1).Trim()));
    }

    public int GetPopulation(string name)
    {
        return capitals[name];
    }
}

public class SingletonImplementation : IRun
{
    public void Run()
    {
        var db = SingletonDatabase.Instance;
        const string city = "Tokyo";
        Console.WriteLine($"{city} has population {db.GetPopulation(city)}");
    }
}
