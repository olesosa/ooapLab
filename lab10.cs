namespace lab10;

public abstract class BaseHouse
{
    public void Build()
    {
        BuildFirstFloors();
        BuildSecondFloors();
        BuildSomethingAdditional();
    }

    protected virtual void BuildFirstFloors()
    {
        Console.WriteLine("First floor build");
    }

    protected virtual void BuildSecondFloors()
    {
        Console.WriteLine("Second floor build");
    }

    protected abstract void BuildSomethingAdditional();
}

public class HouseWithBalcony : BaseHouse
{
    protected override void BuildSomethingAdditional()
    {
        Console.WriteLine("Balcony build");
    }
}

public class HouseWithSomethingElse : BaseHouse
{
    protected override void BuildSomethingAdditional()
    {
        Console.WriteLine("Something Else build");
    }
}

public class Program
{
    static void Main()
    {
        var house = new HouseWithBalcony();
        var house2 = new HouseWithSomethingElse();

        house.Build();
        house2.Build();
    }
}
