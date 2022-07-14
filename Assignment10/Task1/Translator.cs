using System.Text;

namespace Task1
{
    internal class Translator
    {
        private Dictionary<string, string> _dict;
        private StringBuilder _text;
        private readonly string _textFile;
        private readonly string _dictFile;
        private const int MAX_NUMBER_OF_TRIES = 3;

        public Translator() : this(@"../../../Data/Text.txt", @"../../../Data/Dictionary.txt")
        { }

        public Translator(string textFile, string dictFile)
        {
            _dict = new Dictionary<string, string>();
            _text = new();
            _textFile = textFile;
            _dictFile = dictFile;
        }

        public Translator(Dictionary<string, string> dict, string text, string pathToText, string pathToDictionary)
        {
            _dict = new(dict);
            _text = new();
            _textFile = pathToText;
            _dictFile = pathToDictionary;
        }
        public void AddToText(string addText)
        {
            _text.Append(addText);
        }
        public void AddToText(StringBuilder addText)
        {
            _text.Append(addText);
        }

        public void SetDictionary(Dictionary<string, string> dictionary)
        {
            _dict = new(dictionary);
        }
        public StringBuilder Translate()
        {
            StringBuilder translatedText = new();
            StringBuilder preWord = new();
            StringBuilder word = new();
            StringBuilder punctuation = new();
            for (int i = 0; i < _text.Length; i++)
            {
                if (word.Length == 0)
                {
                    if (char.IsLetter(_text[i]))
                    {
                        word.Append(_text[i]);
                    }
                    else
                    {
                        preWord.Append(_text[i]);
                    }
                }
                else
                {
                    if (!char.IsLetter(_text[i]) || i == _text.Length - 1)
                    {
                        if (i == _text.Length - 1) // adding last letter
                        {
                            word.Append(_text[i]);
                            punctuation.Clear();
                        }
                        else
                        {
                            punctuation.Append(_text[i]);
                        }
                        string wordLowercased = word.ToString().ToLower();
                        int numberOfUserInputTries = 0;
                        if (!_dict.ContainsKey(wordLowercased))
                        {
                            bool successfulInput = false;
                            while (!successfulInput && numberOfUserInputTries < MAX_NUMBER_OF_TRIES)
                            {
                                successfulInput = AddToDictionaryManually(wordLowercased);
                                numberOfUserInputTries++;
                            }
                        }
                        if (!_dict.ContainsKey(wordLowercased))
                        {
                            // bad practice
                            // willing to learn how to use events to handle situations
                            // when need to get user input and continue previous flow
                            Console.WriteLine("Bad try");
                            break;
                        }
                        else
                        {
                            string translatedWord = _dict[wordLowercased];
                            if (char.IsUpper(word[0]))
                            {
                                if (word.Length > 1 && char.IsUpper(word[^1]))
                                {
                                    translatedWord = translatedWord.ToUpper();
                                }
                                else
                                {
                                    translatedWord = char.ToUpper(translatedWord[0]) + translatedWord[1..];
                                }
                            }
                            translatedText.Append(preWord + translatedWord + punctuation);
                        }
                        preWord.Clear();
                        word.Clear();
                        punctuation.Clear();
                    }
                    else
                    {
                        word.Append(_text[i]);
                    }
                }
            }
            return translatedText;
        }

        // bad practice
        // willing to learn how to use events to handle situations
        // when need to get user input and continue previous flow
        private bool AddToDictionaryManually(string word)
        {
            Console.WriteLine($"Set translation for '{word}'");
            string value = Console.ReadLine();
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }
            else
            {
                _dict.Add(word, value);
                FileWorker.WriteDictionaryToFile(_dict, _dictFile);
                return true;
            }
        }
    }
}
