namespace MIDILib.Events;

public class MIDISysexEvent : IMIDIEvent
{
    public int DeltaTime { get; }
    public int Length { get; }
    public string Message;

    /* public MIDISysexEvent(byte[] byteMessage)
    {
        (int deltaTime, int length, string message) = ParseBytes(byteMessage);

        DeltaTime = deltaTime;
        Length = length;
        Message = message;
    }

    public static (int, int, string) ParseBytes(byte[] bytes)
    {
        
    } */
}
