namespace MIDILib.Events;

public interface IEvent
{
    public int DeltaTime { get; }
    public int Length { get; }
}
