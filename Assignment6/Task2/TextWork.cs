namespace Task2
{
    public static class TextWork
    {
        public static List<string> GetSentencesFromFile(string inputFilename)
        {
            List<string> sentences = new List<string>();
            StreamReader sr = new StreamReader(inputFilename);
            bool previousLastIsWhole = true;
            bool currentLastIsWhole = true;
            string previousLast = "";
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine().Trim();
                if (line[^1] != '.')
                {
                    currentLastIsWhole = false;
                }
                string[] sentencesParts = line.Split('.');

                if (!previousLastIsWhole)
                {
                    sentencesParts[0] = previousLast + " " + sentencesParts[0];
                }
                if (!currentLastIsWhole)
                {
                    previousLast = sentencesParts[^1];
                    sentencesParts = sentencesParts[..^1];
                }
                foreach (string sentencePart in sentencesParts)
                {
                    sentences.Add(sentencePart.Trim() + ".");
                }
                previousLastIsWhole = currentLastIsWhole;
            }
            sr.Close();
            return sentences;
        }
        public static void WriteSentencesToFile(List<string> sentences, string outputFilename)
        {
            StreamWriter sw = new StreamWriter(outputFilename);
            foreach (string sentence in sentences)
            {
                sw.WriteLine(sentence);
            }
            sw.Close();
        }
        public static void PrintShortestLongestWordsToConsole(List<string> sentences)
        {
            foreach (string sentence in sentences)
            {
                Console.WriteLine($"Shortest: {GetShortestWord(sentence),5} | Longest: {GetLongestWord(sentence),15}");
            }
        }
        // violates SRP but I guess it's memory-efficient compared to storing all sentences
        public static void WriteSentencesToFileAndSortestLongestWordsToConsole(string inputFilename, string outputFilename)
        {
            StreamReader sr = new StreamReader(inputFilename);
            StreamWriter sw = new StreamWriter(outputFilename);
            while (!sr.EndOfStream)
            {
                string sentence = "";
                char symbol;
                do
                {
                    symbol = (char)sr.Read();
                    if (symbol == ' ' || !Char.IsControl(symbol))
                    {
                        sentence += symbol;
                    }
                }
                while (symbol != '.');

                sw.WriteLine(sentence.Trim());
                Console.WriteLine($"Shortest: {GetShortestWord(sentence),5} | Longest: {GetLongestWord(sentence),15}");
            }
            sr.Close();
            sw.Close();
        }
        public static string GetShortestWord(string s)
        {
            string[] words = s.Trim().Split();
            string shortest = s;
            foreach (string word in words)
            {
                if (word.Length < shortest.Length)
                {
                    shortest = word;
                }
            }
            return shortest;
        }
        public static string GetLongestWord(string s)
        {
            string[] words = s.Split();
            string longest = "";
            foreach (string word in words)
            {
                if (word.Length > longest.Length)
                {
                    longest = word;
                }
            }
            return longest;
        }
    }
}
