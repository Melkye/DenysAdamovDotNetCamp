using Task2;

try
{
    TextWork.WriteSentencesToFileAndSortestLongestWordsToConsole("Text.txt", "ResultAllInPlace.txt");

    List<string> sentences = TextWork.GetSentencesFromFile("Text.txt");
    TextWork.PrintShortestLongestWordsToConsole(sentences);
    TextWork.WriteSentencesToFile(sentences, "Result.txt");
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}