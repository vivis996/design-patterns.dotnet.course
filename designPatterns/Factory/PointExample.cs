namespace designPatterns.Factory;

public enum CoordinateSystem
{
    Cartesian,
    Polar,
}

public class Point
{
    private double x, y;

    /// <summary>
    /// Initializes a point from EITHER cartesian or polar
    /// </summary>
    /// <param name="a">if cartesian, rho if polar</param>
    /// <param name="b"></param>
    /// <param name="system"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public Point(double a, double b,
                 CoordinateSystem system = CoordinateSystem.Cartesian)
    {
        switch (system)
        {
            case CoordinateSystem.Cartesian:
                this.x = a;
                this.y = b;
                break;
            case CoordinateSystem.Polar:
                this.x = a * Math.Cos(b);
                this.y = a * Math.Sin(b);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(system), system, null);
        }
    }
}

public class PointExample : IRun
{
    public void Run()
    {

    }
}
