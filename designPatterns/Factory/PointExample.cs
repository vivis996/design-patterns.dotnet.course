namespace designPatterns.Factory;

public enum CoordinateSystem
{
    Cartesian,
    Polar,
}

public class Point
{
    private double x, y;
    private CoordinateSystem system;

    private Point(double x, double y, CoordinateSystem system)
    {
        this.x = x;
        this.y = y;
        this.system = system;
    }

    // factory method
    public static Point NewCartesianPoint(double x, double y)
    {
        return new Point(x, y, CoordinateSystem.Cartesian);
    }

    public static Point NewPolarPoint(double rho, double theta)
    {
        return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta), CoordinateSystem.Polar);
    }

    public override string ToString()
    {
        return $"{nameof(system)}: {system}, {nameof(x)}: {x}, {nameof(y)}: {y}";
    }
}

public class PointExample : IRun
{
    public void Run()
    {
        var point = Point.NewPolarPoint(1.0, Math.PI / 2);
        Console.WriteLine(point);
    }
}
