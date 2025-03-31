namespace MIDILib;

public static class MIDIParser
{
    public static MIDIFile ParseFile(string filepath)
    {
        using FileStream fs = File.OpenRead(filepath);

        byte[] bytes = new byte[fs.Length];
        fs.Read(bytes, 0, bytes.Length);

        return new MIDIFile(bytes);
    }

    public static MIDIFile ParseBytes(byte[] bytes)
    {
        return new MIDIFile(bytes);
    }
}