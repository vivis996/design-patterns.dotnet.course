﻿namespace designPatterns;

public class Program
{
    public static void Main()
    {
        IRun p = new Adapter.GenericAdapter();
        p.Run();
    }
}