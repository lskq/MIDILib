using MIDILib.Events;

namespace MIDILib.Chunks;

public class TrackChunk : IChunk
{
    public byte[] Bytes { get; }
    public IEvent[] Events { get; }

    public TrackChunk(byte[] bytes)
    {
        Bytes = bytes;
        Events = ParseBytes(Bytes);
    }

    public IEvent[] ParseBytes(byte[] bytes)
    {
        IEvent[] events = [];

        int status = 0;

        int len = bytes.Length;
        for (int i = 8; i < len;)
        {
            MIDIMath.NextVlqToInt(bytes, out int j, i);

            status = bytes[j] >= 0x80 ? bytes[j] : status;

            int increment;
            IEvent ev;

            if (status == 0xF0 || status == 0xF7)
            {
                int length = MIDIMath.NextVlqToInt(bytes, out int k, j + 1);

                ev = new SysexEvent(bytes[i..(k + length)]);

                increment = -i + k + length;
            }
            else if (status == 0xFF)
            {
                int length = MIDIMath.NextVlqToInt(bytes, out int k, j + 2);

                ev = new MetaEvent(bytes[i..(k + length)]);

                increment = -i + k + length;
            }
            else
            {
                int size = 3;

                if (0xDF >= status && status >= 0xC0) size -= 1;

                if (status != bytes[j])
                {
                    size -= 1;
                    ev = new MIDIEvent(bytes[i..(j + size)], status);

                }
                else
                {
                    ev = new MIDIEvent(bytes[i..(j + size)]);
                }

                increment = -i + j + size;
            }

            events = [.. events, ev];

            i += increment;
        }

        return events;
    }
}