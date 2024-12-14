using Common;

namespace LexicalAnalyser.Analyser
{
    public interface ILexicalAnalyser
    {
        LexicalAnalyserResult Tokenize();
    }
}