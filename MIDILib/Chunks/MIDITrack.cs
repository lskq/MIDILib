using MIDILib.Events;

namespace MIDILib.Chunks;

public class MIDITrack : IMIDIChunk
{
    public string Type { get; } = "MTrk";
    public int Length { get; }

    public IMIDIEvent[] Events { get; }
    public MIDITrack(int length, byte[] content)
    {
        Length = length;
    }

    private IMIDIEvent[] ParseBytes(byte[] bytes)
    {
        IMIDIEvent[] events = [];

        int len = bytes.Length;
        for (int i = 0; i < len;)
        {
            int deltaTime = MIDIMath.NextVlqToInt(bytes[i..], out int index);

            IMIDIEvent ev;
            byte status = bytes[index];

            if (status == 0xF0 || status == 0xF7)
            {
                int length = MIDIMath.NextVlqToInt(bytes[(index + 1)..], out index);
                byte[] byteOutput = bytes[index..(index + length)];

                ev = new MIDISysexEvent(deltaTime, length, byteOutput);

                i += index + length;
            }
            else if (status == 0xFF)
            {
                // Meta Event
            }
            else
            {
                // Channel Message
            }

            events = [.. events, ev];
        }

        return events;
    }
}
