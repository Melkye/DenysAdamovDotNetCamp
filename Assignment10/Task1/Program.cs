
using System.Text;
using Task1;

Dictionary<string, string> dictionary;
List<string> text;
try
{
    text = FileWorker.ReadTextFromFile(@"../../../Data/Text.txt");
    dictionary = FileWorker.ReadDictionaryFromFile(@"../../../Data/Dictionary.txt");
    Translator translator = new Translator();
    translator.SetDictionary(dictionary);
    foreach (string i in text)
    {
        translator.AddToText(i);
    }

    StringBuilder changedText = translator.Translate();
    FileWorker.WriteTextToFile(changedText, @"../../../Data/TranslatedText.txt");
}
catch (FileNotFoundException)
{
    Console.WriteLine("FileNotFoundException");
}
catch (ArgumentException)
{
    Console.WriteLine("ArgumentException");
}