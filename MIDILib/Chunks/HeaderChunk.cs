namespace MIDILib.Chunks;

public class HeaderChunk(byte[] bytes) : IChunk
{
    public byte[] Bytes { get; } = bytes;

    public int Format => (Bytes[8] << 8) + Bytes[9];
    public int Ntrks => (Bytes[10] << 8) + Bytes[11];
    public int Division => (Bytes[12] << 8) + Bytes[13];
}
