namespace MIDILib.Events;

public interface IEvent
{
    public int DeltaTime { get; }
    public int StatusByte { get; }
    public byte[] DataBytes { get; }
}
