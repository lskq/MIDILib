namespace MIDILib;

public class MIDITrack : IMIDIChunk
{
    public string Type { get; } = "MTrk";
    public int Length { get; }
    public MIDITrack(int length, byte[] content)
    {
        Length = length;
    }
}
