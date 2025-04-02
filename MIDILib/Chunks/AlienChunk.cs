namespace MIDILib.Chunks;

public class AlienChunk(byte[] bytes) : IChunk
{
    public byte[] Bytes { get; } = bytes;
}