using MIDILib.Events;

namespace MIDILib.Chunks;

public class TrackChunk : IChunk
{
    public string Type { get; } = "MTrk";
    public int Length { get; }

    public IEvent[] Events { get; }
    public TrackChunk(int length, byte[] content)
    {
        Length = length;
        Events = ParseBytes(content);
    }

    private IEvent[] ParseBytes(byte[] bytes)
    {
        IEvent[] events = [];

        byte status = 0;

        int len = bytes.Length;
        for (int i = 0; i < len;)
        {
            int deltaTime = MIDIMath.NextVlqToInt(bytes[i..], out int index);

            IEvent ev = null;

            status = bytes[index] > 127 ? bytes[index] : status;

            int increment = 0;

            if (status == 0xF0 || status == 0xF7)
            {
                int length = MIDIMath.NextVlqToInt(bytes[(index + 1)..], out index);
                byte[] byteOutput = bytes[index..(index + length)];

                ev = new SysexEvent(deltaTime, length, byteOutput);

                increment = index + length;
            }
            else if (status == 0xFF)
            {
                int type = bytes[index + 1];
                int length = MIDIMath.NextVlqToInt(bytes[(index + 2)..], out index);
                byte[] byteOutput = bytes[index..(index + length)];

                ev = new MetaEvent(deltaTime, type, length, byteOutput);

                increment = index + 1 + length;
            }
            else
            {
                // Channel Message
            }

            events = [.. events, ev];

            i += increment;
        }

        return events;
    }
}
