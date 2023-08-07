namespace designPatterns.Solid;

public class Document
{
}

public interface IMachine
{
    void Print(Document d);
    void Scan(Document d);
    void Fax(Document d);
}

public class MultiFunctionPrinter : IMachine
{
    public void Fax(Document d)
    {
        //
    }

    public void Print(Document d)
    {
        //
    }

    public void Scan(Document d)
    {
        //
    }
}

public class OldFashionedPrinter : IMachine
{
    public void Fax(Document d)
    {
        throw new NotImplementedException();
    }

    public void Print(Document d)
    {
        //
    }

    public void Scan(Document d)
    {
        throw new NotImplementedException();
    }
}

public interface IPrinter
{
    void Print(Document d);
}

public interface IScanner
{
    void Scan(Document d);
}

public class Photocopier : IPrinter, IScanner
{
    public void Print(Document d)
    {
        throw new NotImplementedException();
    }

    public void Scan(Document d)
    {
        throw new NotImplementedException();
    }
}

public interface IMultiFunctionDevice : IScanner, IPrinter // ...
{

}

public class MultiFunctionMachine : IMultiFunctionDevice
{
    private IPrinter printer;
    private IScanner scanner;

    public MultiFunctionMachine(IPrinter printer, IScanner scanner)
    {
        this.printer = printer;
        this.scanner = scanner;
    }

    public void Print(Document d)
    {
        printer.Print(d);
    }

    public void Scan(Document d)
    {
        scanner.Scan(d);
        // Decorator
    }
}

// Interface Segregation Principle
public class InterfaceSegregationPrinciple : IPrinciple
{
    public void Run()
    {

    }
}

