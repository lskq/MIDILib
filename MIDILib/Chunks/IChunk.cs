namespace MIDILib.Chunks;

public interface IChunk
{
    public byte[] Bytes { get; }

    public string Type => System.Text.Encoding.ASCII.GetString(Bytes[0..4]);
    public int Length => BitConverter.ToInt32(Enumerable.Reverse(Bytes[4..8]).ToArray());
}
