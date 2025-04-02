namespace MIDILib.Chunks;

public class AlienChunk(string type, int length, byte[] content) : IChunk
{
    public string Type { get; } = type;
    public int Length { get; } = length;
    public byte[] Content { get; } = content;
}