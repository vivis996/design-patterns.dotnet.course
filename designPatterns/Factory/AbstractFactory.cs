namespace designPatterns.Factory;

public interface IHotDrink
{
    void Consume();
}

internal class Tea : IHotDrink
{
    public void Consume()
    {
        Console.WriteLine("This tea is nice but I'd prefer it with milk.");
    }
}

internal class Coffee : IHotDrink
{
    public void Consume()
    {
        Console.WriteLine("This coffee is sensational!");
    }
}

public interface IHotDrinkFactory
{
    IHotDrink Prepare(int amount);
}

internal class TeaFactory : IHotDrinkFactory
{
    public IHotDrink Prepare(int amount)
    {
        Console.WriteLine($"Put in a tea bag, boil water, pour {amount} ml, add lemon, enjoy");
        return new Tea();
    }
}

internal class CoffeeFactory : IHotDrinkFactory
{
    public IHotDrink Prepare(int amount)
    {
        Console.WriteLine($"Grind some beans, boil water, pour {amount} ml, add cream and sugar, enjoy");
        return new Coffee();
    }
}

public class HotDrinkMachine
{
    //public enum AvailableDrink
    //{
    //    Coffee,
    //    Tea,
    //}

    //private Dictionary<AvailableDrink, IHotDrinkFactory> factories = new();

    //public HotDrinkMachine()
    //{
    //    foreach (AvailableDrink drink in Enum.GetValues(typeof(AvailableDrink)))
    //    {
    //        IHotDrinkFactory factory = (IHotDrinkFactory)Activator
    //                                        .CreateInstance(Type
    //                                                        .GetType("designPatterns.Factory." +
    //                                                                 Enum.GetName(typeof(AvailableDrink), drink) +
    //                                                                 "Factory"));
    //        factories.Add(drink, factory);
    //    }
    //}

    //public IHotDrink MakeDrink(AvailableDrink drink, int amount)
    //{
    //    return factories[drink].Prepare(amount);
    //}
    private List<Tuple<string, IHotDrinkFactory>> factories = new();

    public HotDrinkMachine()
    {
        foreach (var t in typeof(HotDrinkMachine).Assembly.GetTypes())
        {
            if (typeof(IHotDrinkFactory).IsAssignableFrom(t) && !t.IsInterface)
            {
                factories.Add(Tuple.Create(t.Name.Replace("Factory", string.Empty), Activator.CreateInstance(t) as IHotDrinkFactory));
            }
        }
    }

    public IHotDrink MakeDrink()
    {
        Console.WriteLine("Available drinks:");
        for (int i = 0; i < factories.Count; i++)
        {
            var tuple = factories[i];
            Console.WriteLine($"{i}: {tuple.Item1}");
        }
        while (true)
        {
            Console.WriteLine("Select an available drink.");
            string s;
            if ((s = Console.ReadLine()) != null &&
                int.TryParse(s, out int i) &&
                i >= 0 &&
                i < factories.Count)
            {
                Console.WriteLine("Specify amount:");
                if ((s = Console.ReadLine()) != null &&
                    int.TryParse(s, out int amount) &&
                    amount > 0)
                {
                    return factories[i].Item2.Prepare(amount);
                }
            }
            Console.WriteLine($"Incorrect input, try again!{Environment.NewLine}");
        }
    }
}

public class AbstractFactory : IRun
{
    public void Run()
    {
        var machine = new HotDrinkMachine();
        var drink = machine.MakeDrink();
        //var drink = machine.MakeDrink(HotDrinkMachine.AvailableDrink.Tea, 100);
        drink.Consume();
    }
}
