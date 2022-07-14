using System.Text;

namespace Task1
{
    internal static class FileWorker
    {
        public static List<string> ReadTextFromFile(string filePath)
        {
            List<string> text = new();
            using (StreamReader reader = new(filePath))
            {
                while(!reader.EndOfStream)
                    text.Add(reader.ReadLine());
            }
            return text;
        }

        public static Dictionary<string, string> ReadDictionaryFromFile(string filePath)
        {
            Dictionary<string, string> dict = new();
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Dictionary file not found");
            }
            else
            {
                using (StreamReader reader = new(filePath))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        var str = line.Split('-', StringSplitOptions.RemoveEmptyEntries);
                        if (str.Length != 2)
                        {
                            throw new ArgumentException("Incorrect pair of key - value");
                        }
                        dict.Add(str[0], str[1]);
                    }
                }
            }
            return dict;
        }
        public static void WriteDictionaryToFile(Dictionary<string, string> dict, string filePath)
        {
            using(StreamWriter writer = new(filePath))
            {
                foreach (KeyValuePair<string, string> record in dict)
                {
                    writer.WriteLine($"{record.Key}-{record.Value}");
                }
            }
        }
        public static void WriteTextToFile(StringBuilder text, string filePath)
        {
            using (StreamWriter writer = new(filePath))
            {
                writer.WriteLine(text);
            }
        }
    }
}
