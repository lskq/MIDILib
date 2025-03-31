namespace MIDILib;

public interface IMIDIChunk
{
    public string Type { get; }
    public int Length { get; }
}
