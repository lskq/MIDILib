using System;

namespace MIDILib.Events;

public class MIDIChannelMessage : IMIDIEvent
{
    public int DeltaTime { get; }
    public int Length { get; }
}
