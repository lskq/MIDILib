namespace MIDILib.Events;

public interface IMIDIEvent
{
    public int DeltaTime { get; }
    public int Length { get; }
}
