namespace designPatterns;

public class Program
{
    public static void Main()
    {
        IRun p = new Builder.FunctionalBuilder.FunctionalBuilder();
        p.Run();
    }
}