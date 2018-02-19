using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace IRO.Task.NoteBase.PL
{
    static class CommandLineParser
    {
        static public string[] Parse(string lineToParse, int maxArguments)
        {
            if (String.IsNullOrWhiteSpace(lineToParse))
                return new string[1];

            Regex regex = new Regex("(\"[^\"]+\"|[^\\s\"]+)");
            MatchCollection args = regex.Matches(lineToParse);

            var arguments = new string[maxArguments];

            for (int i = 0; i < maxArguments && i < args.Count; i++)
                arguments[i] = args[i].Value.Replace("\"", String.Empty);
            return arguments;

        }
    }
}
