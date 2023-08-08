namespace designPatterns;

public class Program
{
    public static void Main()
    {
        IRun p = new Factory.AbstractFactory();
        p.Run();
    }
}