namespace MIDILib.tests;

public class MIDILibFacts
{
    public class MIDIParserFacts
    {
        public static IEnumerable<object[]> TestMIDIs()
        {
            string filepath = Directory.GetCurrentDirectory() + "\\midi";

            DirectoryInfo di = new(filepath);

            IEnumerable<object[]> testMIDIs = [];
            foreach (var file in di.GetFiles("*.mid"))
            {
                testMIDIs = [.. testMIDIs, [file.FullName]];
            }

            return testMIDIs;
        }

        public class Parse : MIDIParserFacts
        {
            [Theory]
            [MemberData(nameof(TestMIDIs))]
            public void ReturnsMIDIFile(string filepath)
            {
                Assert.IsType<MIDIFile>(MIDIParser.Parse(filepath));
            }
        }
    }
}