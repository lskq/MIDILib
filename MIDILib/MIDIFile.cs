using MIDILib.Chunks;

namespace MIDILib;

public class MIDIFile(byte[] bytes)
{
    public IChunk[] Chunks { get; set; } = ParseBytes(bytes);

    public static IChunk[] ParseBytes(byte[] bytes)
    {
        IChunk[] chunks = [];

        for (int i = 0; i < bytes.Length;)
        {
            string type = System.Text.Encoding.ASCII.GetString(bytes[i..(i + 4)]);
            int length = BitConverter.ToInt32(Enumerable.Reverse(bytes[(i + 4)..(i + 8)]).ToArray());

            byte[] chunkBytes = bytes[i..(i + 8 + length)];

            IChunk chunk = type switch
            {
                "MThd" => new HeaderChunk(chunkBytes),
                "MTrk" => new TrackChunk(chunkBytes),
                _ => new AlienChunk(chunkBytes),
            };

            chunks = [.. chunks, chunk];

            i += 8 + length;
        }

        return chunks;
    }
}
