using MIDILib.Chunks;

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

        [Theory]
        [MemberData(nameof(TestMIDIs))]
        public void ReturnedMIDILengthEqualsByteArrayLengthPlusEight(string name, string path)
        {
            MIDIFile midi = MIDIParser.ParseFile(path + name);

            int expected = 0;
            int actual = 0;

            foreach (IChunk chunk in midi.Chunks)
            {
                expected += chunk.Bytes.Length;
                actual += 8 + chunk.Length;
            }
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