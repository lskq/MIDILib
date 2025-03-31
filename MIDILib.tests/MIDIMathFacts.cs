namespace MIDILib.tests;

public class MIDIMathFacts
{
    public static IEnumerable<object[]> WellformedVlqs =>
    [
        [new byte[]{0x0}, 0x0],
        [new byte[]{0x40}, 0x40],
        [new byte[]{0x7F}, 0x7F],
        [new byte[]{0x81, 0x00}, 0x80],
        [new byte[]{0xC0, 0x00}, 0x2000],
        [new byte[]{0xFF, 0x7F}, 0x3FFF],
        [new byte[]{0x81, 0x80, 0x00}, 0x4000],
        [new byte[]{0xc0, 0x80, 0x00}, 0x100000],
        [new byte[]{0xFF, 0xFF, 0x7F}, 0x1FFFFF],
        [new byte[]{0x81, 0x80, 0x80, 0x00}, 0x200000],
        [new byte[]{0xC0, 0x80, 0x80, 0x00}, 0x8000000],
        [new byte[]{0xFF, 0xFF, 0xFF, 0x7F}, 0xFFFFFFF]
    ];

    public class NextVlqToInt : MIDIMathFacts
    {
        [Theory]
        [MemberData(nameof(WellformedVlqs))]
        public void WellformedVlqConvertsCorrectly(byte[] vlq, int expected)
        {
            var actual = MIDIMath.NextVlqToInt(vlq, out int index);
            Assert.Equal(expected, actual);
        }
    }
}
