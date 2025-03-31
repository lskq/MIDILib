namespace MIDILib;

public static class MIDIMath
{
    public static int NextVlqToInt(byte[] bytes, out int index)
    {
        index = 0;

        int total = 0;
        int len = bytes.Length;

        for (int i = 0; i < len; i++)
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
                index = i + 1;
                break;
            }
        }

        return total;
    }
}
