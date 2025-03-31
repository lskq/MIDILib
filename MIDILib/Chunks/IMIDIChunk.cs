namespace MIDILib.Chunks;

public interface IMIDIChunk
{
    public string Type { get; }
    public int Length { get; }
}
