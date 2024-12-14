using System;
using System.Diagnostics;

namespace Common.ProcessRunner
{
    public class CommandRunner : ICommandRunner
    {
        private readonly string _command;
        private readonly string _arguments;

        public CommandRunner(string command, string arguments)
        {
            _command = command;
            _arguments = arguments;
        }

        public void RunCommand()
        {
            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = _command,
                    Arguments = _arguments,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                }
            };
            process.Start();
            process.WaitForExit();

            Console.WriteLine(process.StandardOutput.ReadToEnd());
            Console.WriteLine(process.StandardError.ReadToEnd());
            Console.WriteLine($"Command |{_command} {_arguments} | exited with code: {process.ExitCode}");
        }
    }
}