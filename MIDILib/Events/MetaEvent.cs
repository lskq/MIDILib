namespace MIDILib.Events;

public class MetaEvent : IEvent
{
    public int DeltaTime { get; }
    public int StatusByte { get; }

    public int TypeByte { get; }
    public int Length { get; }

    public byte[] DataBytes { get; }

    public MetaEvent(byte[] bytes)
    {
        (DeltaTime, StatusByte, TypeByte, Length, DataBytes) = ParseBytes(bytes);
    }

    public (int, int, int, int, byte[]) ParseBytes(byte[] bytes)
    {
        int deltaTime = MIDIMath.NextVlqToInt(bytes, out int index);
        int statusByte = bytes[index];
        int typeByte = bytes[index + 1];
        int length = MIDIMath.NextVlqToInt(bytes, out index, index + 2);
        byte[] dataBytes = bytes[index..];

        return (deltaTime, statusByte, typeByte, length, dataBytes);
    }
}
