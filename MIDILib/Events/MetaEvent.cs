namespace MIDILib.Events;

public class MetaEvent : IEvent
{
    public byte[] Bytes { get; }

    public int DeltaTime { get; }
    public int Type { get; }
    public int Length { get; }

    public MetaEvent(byte[] bytes)
    {
        Bytes = bytes;

        (DeltaTime, Type, Length) = ParseBytes(Bytes);
    }

    public (int, int, int) ParseBytes(byte[] bytes)
    {
        int deltaTime = MIDIMath.NextVlqToInt(bytes, out int index);
        int type = bytes[index + 1];
        int length = MIDIMath.NextVlqToInt(bytes[(index + 2)..], out index);

        return (deltaTime, type, length);
    }
}
