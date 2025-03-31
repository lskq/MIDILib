namespace MIDILib.Chunks;

public class MIDIAlien(string type, int length, byte[] content) : IMIDIChunk
{
    public string Type { get; } = type;
    public int Length { get; } = length;
    public byte[] Content { get; } = content;
}