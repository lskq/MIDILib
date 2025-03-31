using System.Text;

namespace MIDILib.Events;

public class MIDISysexEvent : IMIDIEvent
{
    public int DeltaTime { get; }
    public int Length { get; }
    public byte[] Bytes { get; }

    public string BytesASCII => new ASCIIEncoding().GetString(Bytes, 0, Bytes.Length);

    public MIDISysexEvent(int deltaTime, int length, byte[] bytes)
    {
        DeltaTime = deltaTime;
        Length = length;
        Bytes = bytes;
    }
}
