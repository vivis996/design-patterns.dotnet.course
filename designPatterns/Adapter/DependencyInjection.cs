﻿using Autofac;
using Autofac.Features.Metadata;

namespace designPatterns.Adapter;

public interface ICommand
{
    void Execute();
}

public class SaveCommand : ICommand
{
    public void Execute()
    {
        Console.WriteLine("Saving a file");
    }
}

public class OpenCommand : ICommand
{
    public void Execute()
    {
        Console.WriteLine("Opening a file");
    }
}

public class Button
{
    private ICommand command;
    private string name;

    public Button(ICommand command, string name)
    {
        this.command = command;
        this.name = name;
    }

    public void Click()
    {
        this.command.Execute();
    }

    public void PrintMe()
    {
        Console.WriteLine($"I am a button called {name}");
    }
}

public class Editor
{
    private readonly IEnumerable<Button> buttons;
    public IEnumerable<Button> Buttons => buttons;

    public Editor(IEnumerable<Button> buttons)
    {
        this.buttons = buttons;
    }

    public void ClickAll()
    {
        foreach (var button in this.buttons)
        {
            button.Click();
        }
    }
}

public class DependencyInjection : IRun
{
    public void Run()
    {
        var b = new ContainerBuilder();
        b.RegisterType<SaveCommand>().As<ICommand>()
            .WithMetadata("Name", "Save");
        b.RegisterType<OpenCommand>().As<ICommand>()
            .WithMetadata("Name", "Open");
        //b.RegisterType<Button>();
        //b.RegisterAdapter<ICommand, Button>(cmd => new Button(cmd));
        b.RegisterAdapter<Meta<ICommand>, Button>(cmd =>
            new Button(cmd.Value, cmd.Metadata["Name"] as string)
        );
        b.RegisterType<Editor>();

        using (var c = b.Build())
        {
            var editor = c.Resolve<Editor>();
            //editor.ClickAll();
            foreach (var btn in editor.Buttons)
            {
                btn.PrintMe();
            }
        }
    }
}
