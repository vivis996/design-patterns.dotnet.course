﻿using Autofac;

namespace designPatterns.Bridge;

public interface IRenderer
{
    void RenderCircle(float radius);
}

public class VectorRenderer : IRenderer
{
    public void RenderCircle(float radius)
    {
        Console.WriteLine($"Drawing a circle of radius {radius}");
    }
}

public class RasterRenderer : IRenderer
{
    public void RenderCircle(float radius)
    {
        Console.WriteLine($"Drawing pixels for circle with radius {radius}");
    }
}

public abstract class Shape
{
    protected IRenderer renderer;

    protected Shape(IRenderer renderer)
    {
        this.renderer = renderer;
    }

    public abstract void Draw();
    public abstract void Resize(float factor);
}

public class Circle : Shape
{
    private float radius;

    public Circle(IRenderer renderer, float radius) : base(renderer)
    {
        this.radius = radius;
    }

    public override void Draw()
    {
        this.renderer.RenderCircle(this.radius);
    }

    public override void Resize(float factor)
    {
        this.radius *= factor;
    }
}

public class Bridge : IRun
{
    public void Run()
    {
        var cb = new ContainerBuilder();
        cb.RegisterType<VectorRenderer>().As<IRenderer>()
            .SingleInstance();
        cb.Register((c, p) =>
                new Circle(c.Resolve<IRenderer>(), p.Positional<float>(0)));

        using (var c = cb.Build())
        {
            var circle = c.Resolve<Circle>(new PositionalParameter(0, 5f));
            circle.Draw();
            circle.Resize(2);
            circle.Draw();
        }
    }
}
