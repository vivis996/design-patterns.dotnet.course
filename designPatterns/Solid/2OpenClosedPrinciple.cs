namespace designPatterns.Solid;

public class OpenClosedPrinciple : IPrinciple
{
    public void Run()
    {
        var apple = new Product("Apple", Color.Red, Size.Small);
        var tree = new Product("Tree", Color.Green, Size.Large);
        var house = new Product("House", Color.Blue, Size.Large);

        var products = new[] { apple, tree, house };

        var pf = new ProductFilter();
        Console.WriteLine("Green products (old):" );
        foreach (var p in pf.FilterByColor(products, Color.Green))
        {
            Console.WriteLine($" - {p.Name} is green");
        }

        var bf = new BetterFilter();
        Console.WriteLine("Green products: (new):");
        foreach (var p in bf.Filter(products, new ColorSpecification(Color.Green)))
        {
            Console.WriteLine($" - {p.Name} is green");
        }

        Console.WriteLine("Large blue items");
        foreach (var p in bf.Filter(products, new AndSpecification<Product>(new ColorSpecification(Color.Blue), new SizeSpecification(Size.Large))))
        {
            Console.WriteLine($" - {p.Name} is large and blue");
        }
    }
}

public enum Color
{
    Red,
    Green,
    Blue,
}

public enum Size
{
    Small,
    Medium,
    Large,
    Yuge,
}

public class Product
{
    public string Name;
    public Color Color;
    public Size Size;

    public Product(string name, Color color, Size size)
    {
        this.Name = name;
        this.Color = color;
        this.Size = size;
    }
}

public class ProductFilter
{
    public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
    {
        foreach (var p in products)
        {
            if (p.Size == size)
                yield return p;
        }
    }

    public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
    {
        foreach (var p in products)
        {
            if (p.Color == color)
                yield return p;
        }
    }

    public IEnumerable<Product> FilterBySizeAndColor(IEnumerable<Product> products, Size size, Color color)
    {
        foreach (var p in products)
        {
            if (p.Size == size && p.Color == color)
                yield return p;
        }
    }
}

public class ColorSpecification : ISpecefication<Product>
{
    private Color color;

    public ColorSpecification(Color color)
    {
        this.color = color;
    }

    public bool IsSatisfied(Product t)
    {
        return t.Color == color;
    }
}

public class SizeSpecification : ISpecefication<Product>
{
    private Size size;

    public SizeSpecification(Size size)
    {
        this.size = size;
    }

    public bool IsSatisfied(Product t)
    {
        return t.Size == size;
    }
}

public class AndSpecification<T> : ISpecefication<T>
{
    private ISpecefication<T> first, second;

    public AndSpecification(ISpecefication<T> first, ISpecefication<T> second)
    {
        this.first = first;
        this.second = second;
    }

    public bool IsSatisfied(T t)
    {
        return first.IsSatisfied(t) && second.IsSatisfied(t);
    }
}

public class BetterFilter : IFilter<Product>
{
    public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecefication<Product> spec)
    {
        foreach (var i in items)
        {
            if (spec.IsSatisfied(i))
                yield return i;
        }
    }
}

public interface ISpecefication<T>
{
    bool IsSatisfied(T t);
}

public interface IFilter<T>
{
    IEnumerable<T> Filter(IEnumerable<T> items, ISpecefication<T> spec);
}