namespace Algorithmia.Interfaces
{
    public interface ISink
    {
        void Write(float[,] noiseData, string? outputPath = null);
    }
}
