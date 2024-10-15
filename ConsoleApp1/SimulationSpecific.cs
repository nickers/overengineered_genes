namespace ConsoleApp1;

public class GenomeForThisSimulation : Genome
{
    public GenomeForThisSimulation()
    {
        Add(new SpeedChromosome());
        Add(new MaxAgeChromosome());
    }
}

public class DirectoryForThisSim: BlobDirector<BlobForThisSim, GenomeForThisSimulation> {
    public override IBlobBuilder<BlobForThisSim>[] Systems => [
        new SpeedBlobSetter<BlobForThisSim>(),
        new AgeBlobSetter<BlobForThisSim>(),
    ];
}

public class BlobForThisSim: IBlob, IMaxAge, IMaxSpeed
{
    public GenomeForThisSimulation Genome { get; init; }
    public int MaxAge { get; set; }
    public int MaxSpeed { get; set; }

    public override string ToString() => $"Blob(Speed: {MaxSpeed}, MaxAge: {MaxAge})";
}