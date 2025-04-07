using System;

namespace MIDILib.Events;

public class MIDIEvent : IEvent
{
    public int DeltaTime { get; }
    public int StatusByte { get; }

    public bool RunningStatus { get; } = false;
    public byte[] DataBytes { get; }

    public MIDIEvent(byte[] bytes, int runningStatusByte = -1)
    {
        (DeltaTime, StatusByte, DataBytes) = ParseBytes(bytes);

        if (runningStatusByte != -1)
        {
            StatusByte = runningStatusByte;
            RunningStatus = true;
        }
    }

    public (int deltaTime, int statusByte, byte[] dataBytes) ParseBytes(byte[] bytes)
    {
        int deltaTime = MIDIMath.NextVlqToInt(bytes, out int index);
        int statusByte = bytes[index] >= 0x80 ? bytes[index] : -1;

        byte[] dataBytes = statusByte == -1 ? bytes[index..] : bytes[(index + 1)..];

        return (deltaTime, statusByte, dataBytes);
    }
}
