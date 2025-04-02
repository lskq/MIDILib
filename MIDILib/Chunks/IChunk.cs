namespace MIDILib.Chunks;

public interface IChunk
{
    public string Type { get; }
    public int Length { get; }
}
