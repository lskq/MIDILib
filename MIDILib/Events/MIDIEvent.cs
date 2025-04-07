using System;

namespace MIDILib.Events;

public class MIDIEvent : IEvent
{
    public int DeltaTime { get; }
    public int StatusByte { get; }

    public byte[] DataBytes { get; }

    public MIDIEvent(byte[] bytes)
    {
        (DeltaTime, StatusByte, DataBytes) = ParseBytes(bytes);
    }

    public (int deltaTime, int statusByte, byte[] dataBytes) ParseBytes(byte[] bytes)
    {
        int deltaTime = MIDIMath.NextVlqToInt(bytes, out int index);
        int statusByte = bytes[index] >= 0x80 ? bytes[index] : -1;

        byte[] dataBytes = statusByte == -1 ? bytes[index..] : bytes[(index + 1)..];

        return (deltaTime, statusByte, dataBytes);
    }
}
