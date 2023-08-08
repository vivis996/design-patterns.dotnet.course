namespace designPatterns.Factory;

public class Foo
{
    private Foo()
    {
        // 
    }

    private async Task<Foo> InitAsync()
    {
        await Task.Delay(1000);
        return this;
    }

    public static Task<Foo> CreateAsync()
    {
        var result = new Foo();
        return result.InitAsync();
    }
}

public class AsyncFactoryMethod : IRun
{
    public async void Run()
    {
        Foo x = await Foo.CreateAsync();
    }
}

