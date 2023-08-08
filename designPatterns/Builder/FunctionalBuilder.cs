namespace designPatterns.Builder.FunctionalBuilder;

public class Person
{
    public string Name, Position;
}

public abstract class FuncitonalBuilder<TSubject, TSelf>
                where TSelf : FuncitonalBuilder<TSubject, TSelf>
                where TSubject : new()
{
    private readonly List<Func<Person, Person>> actions
                        = new();

    public TSelf Do(Action<Person> action) => AddAction(action);

    private TSelf AddAction(Action<Person> action)
    {
        actions.Add(p =>
        {
            action(p);
            return p;
        });

        return (TSelf)this;
    }
    public Person Build() => actions.Aggregate(new Person(), (p, f) => f(p));
}

public sealed class PersonBuilder : FuncitonalBuilder<Person, PersonBuilder>
{
    public PersonBuilder Called(string name) => Do(p => p.Name = name);
}

//public sealed class PersonBuilder
//{
//    private readonly List<Func<Person, Person>> actions
//                        = new List<Func<Person, Person>>();

//    public PersonBuilder Called(string name) => Do(p => p.Name = name);

//    public PersonBuilder Do(Action<Person> action) => AddAction(action);

//    private PersonBuilder AddAction(Action<Person> action)
//    {
//        actions.Add(p => {
//            action(p);
//            return p;
//        });

//        return this;
//    }

//    public Person Build() => actions.Aggregate(new Person(), (p, f) => f(p));
//}

public static class PersonBuilderExtensions
{
    public static PersonBuilder WorksAs(this PersonBuilder builder, string position) => builder.Do(p => p.Position = position);
}

public class FunctionalBuilder : IRun
{
    public void Run()
    {
        var person = new PersonBuilder()
                .Called("Daniel")
                .WorksAs("Developer")
                .Build();
    }
}

