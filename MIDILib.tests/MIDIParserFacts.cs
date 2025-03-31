namespace MIDILib.tests;

public class MIDIParserFacts
{
    public static IEnumerable<object[]> TestMIDIs()
    {
        string filepath = Directory.GetCurrentDirectory() + "\\midi\\";

        DirectoryInfo di = new(filepath);

        IEnumerable<object[]> testMIDIs = [];
        foreach (var file in di.GetFiles("*.mid"))
        {
            testMIDIs = [.. testMIDIs, [file.Name, filepath]];
        }

        return testMIDIs;
    }

    public class Parse : MIDIParserFacts
    {
        [Theory]
        [MemberData(nameof(TestMIDIs))]
        public void ReturnsMIDIFile(string name, string path)
        {
            Assert.IsType<MIDIFile>(MIDIParser.Parse(path + name));
        }
    }
}