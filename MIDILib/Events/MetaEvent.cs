namespace MIDILib.Events;

public class MetaEvent : IEvent
{
    public int DeltaTime { get; }
    public int Type { get; }
    public int Length { get; }
    public byte[] Bytes { get; }

    public MetaEvent(int deltaTime, int type, int length, byte[] bytes)
    {
        DeltaTime = deltaTime;
        Type = type;
        Length = length;
        Bytes = bytes;
    }
}
