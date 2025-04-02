using System;

namespace MIDILib.Events;

public class MIDIEvent : IEvent
{
    public byte[] Bytes { get; }

    public int DeltaTime { get; }
    public int Type { get; }
    public int Length { get; }

    public MIDIEvent(byte[] bytes)
    {
        Bytes = bytes;

        (DeltaTime, Type, Length) = ParseBytes(Bytes);
    }

    public (int deltaTime, int type, int length) ParseBytes(byte[] bytes)
    {
        int deltaTime = MIDIMath.NextVlqToInt(bytes, out int i);
        int type = bytes[i] > 127 ? bytes[i] : 0;

        int length = bytes[i..].Length;
        if (type != 0) length -= 1;

        return (deltaTime, type, length);
    }
}
