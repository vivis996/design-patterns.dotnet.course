using System.Diagnostics;

namespace designPatterns.Solid;

public class SingleResponsibilityPrinciple
{
    public void Run()
    {
        var j = new Journal();
        j.AddEntry("I cried today");
        j.AddEntry("I ate a bug");

        Console.WriteLine(j);
        var p = new Persistance();
        var desktopFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        var fileName = Path.Combine(desktopFolder, "journal.txt");
        p.SaveToFile(j, fileName, true);
        Process.Start(fileName);
    }
}

// just stores a couple of journal entries and ways of
// working with them
public class Journal
{
    private readonly List<string> entries = new List<string>();

    private static int Count = 0;

    public int AddEntry(string text)
    {
        entries.Add($"{++Count}: {text}");
        return Count;
    }

    public void RemoveEntry(int index)
    {
        entries.RemoveAt(index);
    }

    public override string ToString()
    {
        return string.Join(Environment.NewLine, entries);
    }

    // breaks single responsibility principle
    public void Save(string fileName)
    {
        File.WriteAllText(fileName, ToString());
    }

    public static Journal Load(string fileName)
    {
        throw new NotImplementedException();
    }

    public void Load(Uri uri)
    {

    }
}

// handles the responsibility of persisting objects
public class Persistance
{
    public void SaveToFile(Journal j, string fileName, bool overwrite = false)
    {
        if (overwrite || !File.Exists(fileName))
            File.WriteAllText(fileName, j.ToString());
    }
}