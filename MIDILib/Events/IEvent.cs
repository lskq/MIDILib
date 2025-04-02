namespace MIDILib.Events;

public interface IEvent
{
    public byte[] Bytes { get; }

    public int DeltaTime { get; }
    public int Type { get; }
    public int Length { get; }

    public (int deltaTime, int type, int length) ParseBytes(byte[] bytes);
}
