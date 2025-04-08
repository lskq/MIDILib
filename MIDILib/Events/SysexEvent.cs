using System.Text;

namespace MIDILib.Events;

public class SysexEvent : IEvent
{
    public int DeltaTime { get; }
    public int StatusByte { get; }

    public int Length { get; }

    public byte[] DataBytes { get; }

    public SysexEvent(byte[] bytes)
    {
        (DeltaTime, StatusByte, Length, DataBytes) = ParseBytes(bytes);
    }

    public (int deltaTime, int type, int length, byte[] dataBytes) ParseBytes(byte[] bytes)
    {
        int deltaTime = MIDIMath.NextVlqToInt(bytes, out int index);
        int statusByte = bytes[index];
        int length = MIDIMath.NextVlqToInt(bytes, out index, index + 1);

        byte[] dataBytes = bytes[index..];

        return (deltaTime, statusByte, length, dataBytes);
    }
}