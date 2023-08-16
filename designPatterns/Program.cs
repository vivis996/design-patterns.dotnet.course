namespace designPatterns;

public class Program
{
    public static void Main()
    {
        IRun p = new Bridge.Bridge();
        p.Run();
    }
}