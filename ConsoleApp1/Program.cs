using ConsoleApp1;

var g = new GenomeForThisSimulation();
var dir = new DirectoryForThisSim();
var b = dir.GetBlob(g);

Console.WriteLine(b);