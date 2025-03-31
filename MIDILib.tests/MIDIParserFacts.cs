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

    public class ParseFile : MIDIParserFacts
    {
        [Theory]
        [MemberData(nameof(TestMIDIs))]
        public void ReturnsMIDIFile(string name, string path)
        {
            Assert.IsType<MIDIFile>(MIDIParser.ParseFile(path + name));
        }
    }

    public class ParseBytes : MIDIParserFacts
    {
        [Theory]
        [MemberData(nameof(TestMIDIs))]
        public void ReturnsMIDIFile(string name, string path)
        {
            using FileStream fs = File.OpenRead(path + name);

            byte[] bytes = new byte[fs.Length];
            fs.Read(bytes, 0, bytes.Length);

            Assert.IsType<MIDIFile>(MIDIParser.ParseBytes(bytes));
        }
    }
}