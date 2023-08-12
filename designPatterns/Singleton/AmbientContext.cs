using System.Text;

namespace designPatterns.Singleton;

public sealed class BuildingContext : IDisposable
{
    public int WallHeight;
    private static Stack<BuildingContext> stack = new();

    static BuildingContext()
    {
        stack.Push(new BuildingContext(0));
    }

    public BuildingContext(int wallHeight)
    {
        WallHeight = wallHeight;
        stack.Push(this);
    }

    public static BuildingContext Current => stack.Peek();

    public void Dispose()
    {
        if (stack.Count > 1)
            stack.Pop();
    }
}

public class Building
{
    public List<Wall> Walls = new();

    public override string ToString()
    {
        var sb = new StringBuilder();
        foreach (var wall in Walls)
        {
            sb.AppendLine(wall.ToString());
        }
        return sb.ToString();
    }
}

public class Point
{
    private int x, y;

    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public override string ToString()
    {
        return $"{nameof(this.x)}: {this.x}, {nameof(this.y)}: {this.y}";
    }
}

public class Wall
{
    public Point Start, End;
    public int Height;

    public Wall(Point start, Point end)
    {
        this.Start = start;
        this.End = end;
        this.Height = BuildingContext.Current.WallHeight;
    }

    public override string ToString()
    {
        return $"{nameof(this.Start)}: {this.Start}, " +
            $"{nameof(this.End)}: {this.End}, " +
            $"{nameof(this.Height)}: {this.Height}";
    }
}

public class AmbientContext : IRun
{
    public void Run()
    {
        var house = new Building();
        // gnd 3000
        using (new BuildingContext(3000))
        {
            house.Walls.Add(new Wall(new Point(0, 0), new Point(5000, 0)));
            house.Walls.Add(new Wall(new Point(0, 0), new Point(0, 4000)));

            // 1st 3500
            using (var bc = new BuildingContext(3500))
            {
                house.Walls.Add(new Wall(new Point(0, 0), new Point(6000, 0)));
                house.Walls.Add(new Wall(new Point(0, 0), new Point(0, 4000)));
            }

            //gnd 3000
            house.Walls.Add(new Wall(new Point(5000, 0), new Point(5000, 4000)));
        }
        Console.WriteLine(house);
    }
}
