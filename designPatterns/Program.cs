namespace designPatterns;

public class Program
{
    public static void Main()
    {
        IRun p = new Singleton.PerThreadSingletonMain();
        p.Run();
    }
}