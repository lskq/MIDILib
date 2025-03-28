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

        return new MIDIFile(buffer);
    }
}
