using System;

namespace MIDILib.Events;

public class MIDIChannelMessage : IEvent
{
    public int DeltaTime { get; }
    public int Length { get; }
}
