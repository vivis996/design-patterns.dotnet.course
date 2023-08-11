using MoreLinq;

namespace designPatterns.Singleton;

public interface IDatabase
{
	int GetPopulation(string name);
}

public class SingletonDatabase : IDatabase
{
    private Dictionary<string, int> capitals;
    private static int instanceCount;
    public static int Count => instanceCount;

    private static Lazy<SingletonDatabase> instance = new(() => new SingletonDatabase());

    public static SingletonDatabase Instance => instance.Value;

    private SingletonDatabase()
    {
        instanceCount++;
        Console.WriteLine("Initializing database");
        var path = Path.Combine(new FileInfo(typeof(IDatabase).Assembly.Location).DirectoryName, "Singleton/capitals.txt");
        capitals = File.ReadLines(path)
                       .Batch(2)
                       .ToDictionary(list => list.ElementAt(0).Trim(),
                                     list => int.Parse(list.ElementAt(1).Trim()));
    }

    public int GetPopulation(string name)
    {
        return capitals[name];
    }
}

public class SingletonRecordFinder
{
    public int GetTotalPopulation(IEnumerable<string> names)
    {
        int result = 0;
        foreach (var name in names)
        {
            result += SingletonDatabase.Instance.GetPopulation(name);
        }
        return result;
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
