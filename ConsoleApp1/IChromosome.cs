namespace ConsoleApp1;

public class SpeedChromosome : IChromosome
{
    public enum SpeedClass
    {
        Slow,
        Medium,
        Fast,
    };

    public SpeedClass Speed { get; set; } = SpeedClass.Medium;
}

public class MaxAgeChromosome : IChromosome
{
}


public class SpeedBlobSetter<T> : IBlobBuilder<T> where T: IMaxSpeed
{
    public bool Apply(Genome g, T c)
    {
        var x = g.OfType<SpeedChromosome>().Single();
        c.MaxSpeed = x.Speed switch
        {
            SpeedChromosome.SpeedClass.Slow => 1,
            SpeedChromosome.SpeedClass.Medium => 5,
            SpeedChromosome.SpeedClass.Fast => 15,
            _ => throw new ArgumentOutOfRangeException()
        };
        return true;
    }
}

public class AgeBlobSetter<T> : IBlobBuilder<T> where T : IMaxAge
{
    public bool Apply(Genome g, T c)
    {
        if (!g.OfType<MaxAgeChromosome>().Any())
        {
            throw new ApplicationException("Must have at least one MaxAge chromosome");
        }

        c.MaxAge = Random.Shared.Next(100);
        return true;
    }
}



public interface IMaxSpeed : IComponent
{
    int MaxSpeed { get; set; }
}

public interface IMaxAge : IComponent
{
    int MaxAge { get; set; }
}

