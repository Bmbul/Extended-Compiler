using Common;
using LexicalAnalyser.IO;

namespace LexicalAnalyser.Analyser
{
    public abstract class LexicalAnalyserBase : ILexicalAnalyser
    {
        protected readonly string _filePath;
        protected readonly LexicalAnalyserResult _result = new LexicalAnalyserResult();
        protected readonly IFileReader _fileReader = new FileReader();

        protected LexicalAnalyserBase(string filePath)
        {
            _filePath = filePath;
        }


        public abstract LexicalAnalyserResult Tokenize();

        protected string GetFileContent()
        {
            return _fileReader.ReadFile(_filePath);
        }
    }
}
