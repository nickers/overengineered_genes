using System.Runtime.InteropServices.ComTypes;

namespace ConsoleApp1;

public interface IChromosome;
public interface IComponent;

public class Genome : List<IChromosome>;

public interface IBlob {}

public interface IBlobBuilder<in T> where T: IComponent
{
    bool Apply(Genome g, T c);
}

public abstract class BlobDirector<TB, TG>
    where TB : IBlob
    where TG: Genome
{
    public virtual IBlobBuilder<BlobForThisSim>[] Systems { get; }


    public virtual BlobForThisSim GetBlob(GenomeForThisSimulation g)
    {
        var blob = new BlobForThisSim()
        {
            Genome = g,
        };
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