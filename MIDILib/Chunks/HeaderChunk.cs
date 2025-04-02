namespace MIDILib.Chunks;

public class HeaderChunk : IChunk
{
    public string Type { get; } = "MThd";
    public int Length { get; }
    public int Format { get; }
    public int Ntrks { get; }
    public int Division { get; }

    public string FormatType => (Format == 0) ? "Single Track" :
                                (Format == 1) ? "Multiple Tracks" :
                                (Format == 2) ? "Multiple Patterns" : "Unknown";
    public string DivisionType => (Division > 0x7FFF) ? "SMPTE" : "Ticks per quarter-note";

    public HeaderChunk(int length, byte[] content)
    {
        Length = length;
        Format = content[0] * 2 + content[1];
        Ntrks = content[2] * 2 + content[3];
        Division = content[4] * 2 + content[5];
    }
}
