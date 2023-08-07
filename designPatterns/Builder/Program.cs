using System.Text;

namespace designPatterns.Builder;

public class HtmlElement
{
    public string Name, Text;
    public List<HtmlElement> Elements = new List<HtmlElement>();
    private const int indentSize = 2;

    public HtmlElement()
    {

    }

    public HtmlElement(string name, string text)
    {
        this.Name = name;
        this.Text = text;
    }

    private string ToStringImpl(int indent)
    {
        var sb = new StringBuilder();
        var i = new string(' ', indentSize * indent);
        sb.AppendLine($"{i}<{Name}>");

        if (!string.IsNullOrWhiteSpace(Text))
        {
            sb.Append(new string(' ', indentSize * (indent + 1)));
            sb.AppendLine(Text);
        }
        foreach (var e in Elements)
        {
            sb.Append(e.ToStringImpl(indent + 1));
        }
        sb.AppendLine($"{i}</{Name}>");
        return sb.ToString();
    }

    public override string ToString()
    {
        return this.ToStringImpl(0);
    }
}

public class HtmlBuilder
{
    private readonly string rootName;
    HtmlElement root = new HtmlElement();

    public HtmlBuilder(string rootName)
    {
        this.rootName = rootName;
        this.root.Name = this.rootName;
    }

    public void AddChild(string childName, string childText)
    {
        var e = new HtmlElement(childName, childText);
        this.root.Elements.Add(e);
    }

    public override string ToString()
    {
        return root.ToString();
    }

    public void Clear()
    {
        root = new HtmlElement { Name = rootName };
    }
}

public class Program
{
    public void Run()
    {
        var hello = "hello";
        var sb = new StringBuilder();
        sb.Append("<p>");
        sb.Append(hello);
        sb.Append("</p>");
        Console.WriteLine(sb);

        sb.Clear();
        var words = new[] { "hello", "world" };
        sb.Append("<ul>");
        foreach (var word in words)
        {
            sb.AppendFormat("<li>{0}</li>", word);
        }
        sb.Append("</ul>");
        Console.WriteLine(sb);

        var builder = new HtmlBuilder("ul");
        builder.AddChild("li", "hello");
        builder.AddChild("li", "world");
        Console.WriteLine(builder.ToString());
    }
}
