using MIDILib.Events;

namespace MIDILib.Chunks;

public class TrackChunk(byte[] bytes) : IChunk
{
    public byte[] Bytes { get; } = bytes;
    public IEvent[] Events => null;//ParseBytes(Bytes);

    private IEvent[] ParseBytes(byte[] bytes)
    {
        IEvent[] events = [];

        int status = 0;
        bool running = false;

        int len = bytes.Length;
        for (int i = 8; i < len;)
        {
            int deltaTime = MIDIMath.NextVlqToInt(bytes[i..], out int index);

            IEvent ev = null;

            status = bytes[index] > 127 ? bytes[index] : status;
            running = bytes[index] <= 127;

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

                increment = index + length;
            }
            else
            {
                byte[] byteOutput;

                if (running)
                    byteOutput = bytes[index..(index + 2)];
                else
                    byteOutput = bytes[(index + 1)..(index + 3)];

                ev = new MIDIEvent(deltaTime, status, 2, byteOutput);
            }

            events = [.. events, ev];

            i += increment;
        }

        return events;
    }
}
