using System.Text;

namespace MIDILib;

public class MIDIFile
{
    public IMIDIChunk[] Chunks { get; set; } = [];

    public MIDIFile(byte[] bytes)
    {
        ParseBytes(bytes);
    }

    private void ParseBytes(byte[] bytes)
    {
        var ascii = new ASCIIEncoding();

        for (int i = 0; i < bytes.Length;)
        {
            string type = ascii.GetString(bytes[i..(i + 4)]);

            byte[] lengthBytes = bytes[(i + 4)..(i + 8)];
            if (BitConverter.IsLittleEndian)
                Array.Reverse(lengthBytes);
            int length = BitConverter.ToInt32(lengthBytes);

            byte[] data = bytes[(i + 8)..(i + 8 + length)];

            IMIDIChunk chunk;

            if (type == "MThd")
            {
                chunk = new MIDIHeader(length, data);
            }
            else if (type == "MTrk")
            {
                chunk = new MIDITrack(length, data);
            }
            else
            {
                chunk = new MIDIAlien(type, length, data);
            }

            Chunks = [.. Chunks, chunk];

            i += 8 + length;
        }
    }
}
