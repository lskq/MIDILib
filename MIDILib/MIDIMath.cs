namespace MIDILib;

public static class MIDIMath
{
    public static int NextVlqToInt(byte[] bytes, out int nextIndex, int startIndex = 0)
    {
        nextIndex = -1;

        int total = 0;
        int len = bytes.Length;

        for (int i = startIndex; i < len; i++)
        {
            byte b = bytes[i];
            if (b > 0x7F)
            {
                // Msb = 1, more octets incoming
                total += (b - 0x80) * (int)Math.Pow(2, (len - 1 - i) * 7);
            }
            else
            {
                // Msb = 0, final octet
                total += b;
                nextIndex = i + 1;
                break;
            }
        }

        if (nextIndex == -1)
            throw new ArgumentException("No terminating octet found.");

        return total;
    }
}
