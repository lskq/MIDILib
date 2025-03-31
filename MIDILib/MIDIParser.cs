using System.Text;

namespace MIDILib;

public static class MIDIParser
{
    public static MIDIFile Parse(string filepath)
    {
        using FileStream fs = File.OpenRead(filepath);

        byte[] buffer = new byte[fs.Length];
        var ascii = new ASCIIEncoding();
        fs.Read(buffer, 0, buffer.Length);

        for (int i = 0; i < buffer.Length;)
        {
            string type = ascii.GetString(buffer[i..(i + 4)]);

            byte[] lengthBytes = buffer[(i + 4)..(i + 8)];
            if (BitConverter.IsLittleEndian)
                Array.Reverse(lengthBytes);
            int length = BitConverter.ToInt32(lengthBytes);

            byte[] data = buffer[(i + 8)..(i + 8 + length)];

            if (type == "MThd")
            {
                // Header chunk
            }
            else if (type == "MTrk")
            {
                // Track chunk
            }
            else
            {
                // Alien chunk
            }

            i += 8 + length;
        }

        return new MIDIFile(buffer);
    }
}