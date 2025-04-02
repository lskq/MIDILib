using System.Text;

namespace MIDILib.Events;

public class SysexEvent : IEvent
{
    public byte[] Bytes { get; }

    public int DeltaTime { get; }
    public int Type { get; }
    public int Length { get; }

    public SysexEvent(byte[] bytes)
    {
        Bytes = bytes;

        (DeltaTime, Type, Length) = ParseBytes(Bytes);
    }

    public (int deltaTime, int type, int length) ParseBytes(byte[] bytes)
    {
        int deltaTime = MIDIMath.NextVlqToInt(bytes[1..], out int index);
        int type = bytes[index];
        int length = MIDIMath.NextVlqToInt(bytes[(index + 1)..], out index);

        return (deltaTime, type, length);
    }
}