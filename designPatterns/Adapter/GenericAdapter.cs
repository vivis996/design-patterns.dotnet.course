namespace designPatterns.Adapter;

// Vector2f, Vector3i

public interface IIntenger
{
    int Value { get; }
}

public static class Dimensions
{
    public class Two : IIntenger
    {
        public int Value => 2;
    }

    public class Three : IIntenger
    {
        public int Value => 3;
    }
}

public class Vector<TSelf, T, D>
    where D : IIntenger, new()
    where TSelf : Vector<TSelf, T, D>, new()
{
    protected T[] data;

    public Vector()
    {
        this.data = new T[new D().Value];
    }

    public Vector(params T[] values)
    {
        var requiredSize = new D().Value;
        this.data = new T[requiredSize];

        var providedSize = values.Length;
        for (var i = 0; i < Math.Min(requiredSize, providedSize); i++)
        {
            data[i] = values[i];
        }
    }

    public static TSelf Create(params T[] values)
    {
        var result = new TSelf();
        var requiredSize = new D().Value;
        result.data = new T[requiredSize];

        var providedSize = values.Length;
        for (var i = 0; i < Math.Min(requiredSize, providedSize); i++)
        {
            result.data[i] = values[i];
        }

        return result;
    }

    public T this[int index]
    {
        get => data[index];
        set => data[index] = value;
    }

    public T X
    {
        get => data[0];
        set => data[0] = value;
    }
}

public class VectorOfInt<D> : Vector<VectorOfInt<D>, int, D> where D : IIntenger, new()
{
    public VectorOfInt()
    {
    }

    public VectorOfInt(params int[] values) : base(values)
    {
    }

    public static VectorOfInt<D> operator +(VectorOfInt<D> lhs, VectorOfInt<D> rhs)
    {
        var result = new VectorOfInt<D>();
        var dim = new D().Value;
        for (var i = 0; i < dim; i++)
        {
            result[i] = lhs[i] + rhs[i];
        }
        return result;
    }
}

public class VectorOfFloat<TSelf, D> : Vector<TSelf, float, D>
    where D : IIntenger, new()
    where TSelf : VectorOfFloat<TSelf, D>, new()
{
}

public class Vector2i : VectorOfInt<Dimensions.Two>
{
    public Vector2i()
    {
    }

    public Vector2i(params int[] values) : base(values)
    {
    }
}

public class Vector3f : VectorOfFloat<Vector3f, Dimensions.Three>
{
    public override string ToString()
    {
        return $"{string.Join(",", data)}";
    }
}

public class GenericAdapter : IRun
{
    public void Run()
    {
        var v = new Vector2i(1, 2);
        v[0] = 0;

        var vv = new Vector2i(3, 2);

        var result = v + vv;

        var u = Vector3f.Create(3.5f, 2.2f, 1);
    }
}
