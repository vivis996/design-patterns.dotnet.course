namespace designPatterns.Factory;

public enum CoordinateSystem
{
    None,
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

    public override string ToString()
    {
        return $"{nameof(system)}: {system}, {nameof(x)}: {x}, {nameof(y)}: {y}";
    }

    public static Point Origin => new Point(0, 0, CoordinateSystem.None);

    public static Point Origin2 => new Point(0, 0, CoordinateSystem.None);

    public static class Factory
    {
        // factory method
        public static Point NewCartesianPoint(double x, double y)
        {
            return new Point(x, y, CoordinateSystem.Cartesian);
        }

        public static Point NewPolarPoint(double rho, double theta)
        {
            return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta), CoordinateSystem.Polar);
        }
    }
}

public class PointExample : IRun
{
    public void Run()
    {
        var point = Point.Factory.NewPolarPoint(1.0, Math.PI / 2);
        Console.WriteLine(point);
    }
}
