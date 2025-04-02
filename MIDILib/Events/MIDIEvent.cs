using System;

namespace MIDILib.Events;

public class MIDIEvent : IEvent
{
    public int DeltaTime { get; }
    public int Type { get; }
    public int Length { get; }
    public byte[] Bytes { get; }

    public MIDIEvent(int deltaTime, int type, int length, byte[] bytes)
    {
        DeltaTime = deltaTime;
        Type = type;
        Length = length;
        Bytes = bytes;
    }
}
