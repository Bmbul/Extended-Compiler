using LexicalAnalyser.Analyser;
using System;
using System.IO;
using Common;
using Common.ProcessRunner;
using Compiler.Parser;

public static class Program
{
    public static void Main(string[] args)
    {
        try
        {
            if (args.Length != 1)
            {
                throw new ApplicationException("Wrong number of arguments.");
            }
            ILexicalAnalyser analyser = new BetterLexicalAnalyser(args[0]);
            var tokens = analyser.Tokenize();
            
            if (tokens.ContainsInvalid(out LexicalToken wrongToken))
            {
                throw new ApplicationException($"Bad token: {wrongToken.Value}");
            }
            
            IParserEngine parserEngine = new ParserEngine(tokens);
            var fileNameWithoutExtention = Path.GetFileNameWithoutExtension(args[0]);
            parserEngine.Parse(fileNameWithoutExtention);

            ICommandRunner commandRunner = new CommandRunner(
                "gcc", 
                $"-nostartfiles -no-pie -o {fileNameWithoutExtention} {fileNameWithoutExtention}.s -lc -Wl,-dynamic-linker,/lib64/ld-linux-x86-64.so.2"
            );
            commandRunner.RunCommand();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}

