using System.Collections;
using System.Collections.ObjectModel;
using MoreLinq;

namespace designPatterns.Adapter;

public class Point
{
    public int X { get; set; }
    public int Y { get; set; }

    public Point(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    protected bool Equals(Point other)
    {
        return this.X == other.X && this.Y == other.Y;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return this.Equals(obj as Point);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return (this.X * 397) ^ this.Y;
        }
    }
}

public class Line
{
    public Point Start, End;

    public Line(Point start, Point end)
    {
        this.Start = start;
        this.End = end;
    }

    protected bool Equals(Line other)
    {
        return Equals(this.Start, other.Start) && Equals(this.End, other.End);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return this.Equals(obj as Line);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Start, End);
    }


}

public class VectorObject : Collection<Line>
{

}

public class VectorRectangle : VectorObject
{
    public VectorRectangle(int x, int y, int width, int height)
    {
        Add(new Line(new Point(x, y), new Point(x + width, y)));
        Add(new Line(new Point(x + width, y), new Point(x + width, y + height)));
        Add(new Line(new Point(x, y), new Point(x, y + height)));
        Add(new Line(new Point(x, y + height), new Point(x + width, y + height)));
    }
}

public class LineToPointAdapter : IEnumerable<Point>
{
    private static int count;

    private static Dictionary<int, List<Point>> cache = new();

    public LineToPointAdapter(Line line)
    {
        var hash = line.GetHashCode();
        if (cache.ContainsKey(hash)) return;

        Console.WriteLine($"{++count}: Generating points for line [{line.Start.X},{line.Start.Y}]-[{line.End.X},{line.End.Y}]");

        var points = new List<Point>();

        int left = Math.Min(line.Start.X, line.End.X);
        int right = Math.Max(line.Start.X, line.End.X);
        int top = Math.Min(line.Start.Y, line.End.Y);
        int bottom = Math.Max(line.Start.Y, line.End.Y);
        int dx = right - left;
        int dy = line.End.Y - line.Start.Y;

        if (dx == 0)
        {
            for (var y = top; y <= bottom; y++)
            {
                points.Add(new Point(left, y));
            }
        }
        else
        {
            if (dy == 0)
            {
                for (var x = left; x <= right; x++)
                {
                    points.Add(new Point(x, top));
                }
            }
        }

        cache.Add(hash, points);
    }

    public IEnumerator<Point> GetEnumerator()
    {
        return cache.Values.SelectMany(x => x).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}

public class VectorRaster : IRun
{
    public static readonly List<VectorObject> vectorObjects = new()
    {
        new VectorRectangle(1, 1, 10, 10),
        new VectorRectangle(3, 3, 6, 6),
    };

    public static void DrawPoint(Point p)
    {
        Console.Write(".");
    }

    public void Run()
    {
        Draw();
        Draw();
    }

    private static void Draw()
    {
        foreach (var vo in vectorObjects)
        {
            foreach (var line in vo)
            {
                var adapter = new LineToPointAdapter(line);
                adapter.ForEach(DrawPoint);
            }
        }
    }
}
