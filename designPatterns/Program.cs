namespace designPatterns;

public class Program
{
    public static void Main()
    {
        IRun p = new Prototype.PrototypeInheritance.PrototypeInheritance();
        p.Run();
    }
}