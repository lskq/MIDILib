using System.Text;

namespace MIDILib.Events;

public class SysexEvent : IEvent
{
    public int DeltaTime { get; }
    public int Length { get; }
    public byte[] Bytes { get; }

    public string BytesASCII => new ASCIIEncoding().GetString(Bytes, 0, Bytes.Length);

    public SysexEvent(int deltaTime, int length, byte[] bytes)
    {
        DeltaTime = deltaTime;
        Length = length;
        Bytes = bytes;
    }
}
