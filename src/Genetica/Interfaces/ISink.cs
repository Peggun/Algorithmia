namespace Genetica
{
    public interface ISink
    {
        /// <summary>
        /// Write to the curent sink's desired output. 
        /// </summary>
        /// <param name="noiseData"></param>
        /// <param name="outputPath"></param>
        void Write(float[,] noiseData, string? outputPath = null);
    }
}
