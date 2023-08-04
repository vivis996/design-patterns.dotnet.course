using designPatterns.Solid;

namespace designPatterns;

public class Program
{
    public static void Main()
    {
        IPrinciple p = new DependencyInversionPrinciple();
        p.Run();
    }
}