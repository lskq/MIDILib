namespace MIDILib.Chunks;

public class HeaderChunk(byte[] bytes) : IChunk
{
    public byte[] Bytes { get; } = bytes;

    public int Format => Bytes[8] * 2 + Bytes[9];
    public int Ntrks => Bytes[10] * 2 + Bytes[11];
    public int Division => Bytes[12] * 2 + Bytes[13];

    public string FormatType => (Format == 0) ? "Single Track" :
                                (Format == 1) ? "Multiple Tracks" :
                                (Format == 2) ? "Multiple Patterns" : "Unknown";
    public string DivisionType => (Division > 0x7FFF) ? "SMPTE" : "Ticks per quarter-note";
}
