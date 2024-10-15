using System.Reflection.Metadata;

public interface IChromosome { }

public class Genome : List<IChromosome>
{
}

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
public class MaxAgeChromosome: IChromosome {}

public interface IBlobBuilder
{
    bool Apply(Genome g, IComponent c);
}


public class SpeedBlobSetter: IBlobBuilder {
    public bool Apply(Genome g, IComponent c)
    {
        var x = g.OfType<SpeedChromosome>().Single();
        ((c as IMaxSpeed)!).MaxSpeed = x.Speed switch
        {
            SpeedChromosome.SpeedClass.Slow => 1,
            SpeedChromosome.SpeedClass.Medium => 5,
            SpeedChromosome.SpeedClass.Fast => 15,
            _ => throw new ArgumentOutOfRangeException()
        };
        return true;
    }
}
public class AgeBlobSetter: IBlobBuilder
{
    public bool Apply(Genome g, IComponent c)
    {
        if (!g.OfType<MaxAgeChromosome>().Any())
        {
            throw new ApplicationException("Must have at least one MaxAge chromosome");
        }

        ((c as IMaxAge)!).MaxAge = Random.Shared.Next(100);
        return true;
    }
}

public class GenomeForThisSimulation : Genome
{
    public GenomeForThisSimulation()
    {
        Add(new SpeedChromosome());
        Add(new MaxAgeChromosome());
    }
}

public class BlobBuilder
{
    private static readonly IBlobBuilder[] Systems =
    [
        new SpeedBlobSetter(),
        new AgeBlobSetter(),
    ];

    public static Blob NewBlob(GenomeForThisSimulation g)
    {
        var blob = new Blob();
        foreach (var s in Systems)
        {
            if (!s.Apply(g, blob))
            {
                throw new ApplicationException($"Can't create blob. Failed: {s.GetType().Name}");
            }
        }
        return blob;
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

public class Blob: IMaxAge, IMaxSpeed
{
    public int MaxAge { get; set; }
    public int MaxSpeed { get; set; }

    public override string ToString() => $"Blob(Speed: {MaxSpeed}, MaxAge: {MaxAge})";
}

public interface IComponent { }